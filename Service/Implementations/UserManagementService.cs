using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Helper;
using Service.Interfaces;
using Service.Models;
using Service.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class UserManagementService : IUserManagementService
    {
        readonly IUserRepository userRepository;
        readonly IConfirmRepository confirmRepository;
        public UserManagementService(IUserRepository userRepository, IConfirmRepository confirmRepository)
        {
            this.userRepository = userRepository;
            this.confirmRepository = confirmRepository;
        }

        public async Task<bool> CreateAsync(User user)
        {
            if (await userRepository.GetAsync(u => u.Email == user.Email || u.Nickname == user.Nickname) != null) return false;
            if (user.BirthdayDate != null)
            {
                var age = DateTime.Now.Subtract(user.BirthdayDate.Value);
                user.Age = age.Days / 365;
            }


            Random rand = new Random();
            int random = rand.Next(10000000, 100000000);
            if (!Mail.SendConfirmation(user.Email, random)) return false;

            var confirm = new Confirmation();
            confirm.Email = user.Email;
            confirm.Rand = random;
            await confirmRepository.CreateAsync(confirm);
            await userRepository.CreateAsync(user);
            await userRepository.SaveAsync();

            return true;


        }

        public async Task<bool> SendConfirmation(string email)
        {

            Random rand = new Random();
            int random = rand.Next(10000000, 100000000);
            if (Mail.SendConfirmation(email, random))
            {
                var confirm = await confirmRepository.GetAsync(c => c.Email == email);
                confirmRepository.Delete(confirm);
                var conf = new Confirmation();
                confirm.Email = email;
                confirm.Rand = random;
                confirm.Created_at = DateTime.Now;
                await confirmRepository.CreateAsync(conf);
                await confirmRepository.SaveAsync();
            

                return true;
            }
            return false;
        }


        public  User GetUser(string email, string password)
        {
            var user =  userRepository.GetIncludingAll(u => u.RoleID == u.Role.ID && u.Email == email && Secure.Encryptpass(password) == u.Password);
            if (user == null || user.Deleted_at != null) return null;
            if (user.Blocked == true) if (user.BlockedUntil < DateTime.Now) user.Blocked = false;
            return user;

        }

        public async Task<bool> CofirmMail(Confirmation confirm)
        {
            var confirms = await confirmRepository.GetAsync(a => a.Rand == confirm.Rand && a.Email == confirm.Email);
            if (confirms == null)
                return false;

            var user = await userRepository.GetAsync(u => u.Email == confirm.Email);
            if (confirm.Created_at.AddHours(24) >= DateTime.Now)
            {
                user.Activated = true;
                confirmRepository.Delete(confirms);
                await confirmRepository.SaveAsync();
                await userRepository.SaveAsync();

                return true;
            }


            return false;
        }

        public async Task<bool> CofirmUser(string email)
        {
            Random rand = new Random();
            int random = rand.Next(10000000, 100000000);
            if (!Mail.SendConfirmation(email, random)) return false;
            var userconfirm = await confirmRepository.GetAsync(c => c.Email == email);
            if (userconfirm == null) return false;
            userconfirm.Rand = random;
            await confirmRepository.SaveAsync();
            return true;
        }

        public async Task<bool> SendConfirmationPassword(string email)
        {
            Random rand = new Random();
            int random = rand.Next(10000000, 100000000);
            if (!Mail.SendRecoverPassword(email, random)) return false;


            var conf = await confirmRepository.GetAsync(c => c.Email == email);
            if (conf != null)
                confirmRepository.Delete(conf);
            var confirm = new Confirmation();
            confirm.Email = email;
            confirm.Rand = random;
            confirm.Created_at = DateTime.Now;
            await confirmRepository.CreateAsync(confirm);
            await confirmRepository.SaveAsync();
            return true;

        }


        public async Task<bool> ForgotPassword(string email, int number)
        {
            var confirm = await confirmRepository.GetAsync(x => x.Email == email);
            if (confirm == null) return false;

            if (confirm.Rand == number)
            {
                if (confirm.Created_at.AddHours(24) >= DateTime.Now)
                {
                    string new_password = RandomPassword.Generate();
                    if (Mail.SendRandomPassword(email, new_password))
                    {
                        var user = await userRepository.GetAsync(u => u.Email == email);
                        if (user.Deleted_at != null) return false;

                        user.Password = Secure.Encryptpass(new_password);
                        confirmRepository.Delete(confirm);
                        await confirmRepository.SaveAsync();
                        return true;

                    }
                }
            }

            return false;

        }

        public async Task<bool> ChangePassword(int id, string old_password, string new_password)
        {
            var user = await userRepository.GetAsync(u => u.ID == id && u.Password == Secure.Encryptpass(old_password));
            if (user == null || user.Deleted_at != null) return false;

            user.Password = Secure.Encryptpass(new_password);

            await userRepository.SaveAsync();
            return true;
        }

        public async Task<bool> ResendConfirmPasswors(string email)
        {
            Random rand = new Random();
            int random = rand.Next(10000000, 100000000);
            if (!Mail.SendRecoverPassword(email, random)) return false;


            var conf = await confirmRepository.GetAsync(c => c.Email == email);
            if (conf != null)
                confirmRepository.Delete(conf);
            var confirm = new Confirmation();
            confirm.Email = email;
            confirm.Rand = random;
            confirm.Created_at = DateTime.Now;
            await confirmRepository.CreateAsync(confirm);
            await confirmRepository.SaveAsync();
            return true;


        }

        public async Task<bool> ChangeRole(int change_id, int new_role, int user_id)
        {
            var user = await userRepository.GetAsync(u => u.ID == change_id);
            if (user == null || user.Deleted_at != null) return false;
            if (new_role <= 0 || new_role > 4) return false;
            if (new_role == Roles.Admin)
            {
                var changer = await userRepository.GetAsync(u => u.ID == user_id);
                if (changer.RoleID == Roles.Moderator) return false;
                changer.RoleID = Roles.Moderator;
                user.RoleID = new_role;
            }
            if (new_role == Roles.Moderator)
            {
                var change_user = await userRepository.GetAsync(u => u.ID == change_id);
                if (change_user.RoleID == Roles.Admin) return false;
                user.RoleID = new_role;
            }
            else
            {
                user.RoleID = new_role;
            }


            await userRepository.SaveAsync();
            return true;
        }


        public async Task<bool> DeleteUser(int id)
        {
            var user = await userRepository.GetAsync(u => u.ID == id);
            if (user == null || user.Deleted_at != null) return false;

            user.Deleted_at = DateTime.Now;
            await userRepository.SaveAsync();

            return true;
        }

        public async Task<bool> PatchUser(int id, User patch_user)
        {
            var user = await userRepository.GetAsync(u => u.ID == id);
            if (user == null || user.Deleted_at != null) return false;

            if (patch_user.FirstName != null)
                user.FirstName = patch_user.FirstName;
            if (patch_user.LastName != null)
                user.LastName = patch_user.LastName;
            if (patch_user.Nickname != null && await userRepository.GetAsync(u => u.Nickname == patch_user.Nickname) == null)
                user.Nickname = patch_user.Nickname;
            if (patch_user.AboutUser != null)
                user.AboutUser = patch_user.AboutUser;
            if (patch_user.Age != null)
                user.Age = patch_user.Age;
            if (patch_user.BirthdayDate != null)
                user.BirthdayDate = patch_user.BirthdayDate;

            await userRepository.SaveAsync();
            return true;

        }

        public async Task<User> GetUserByIDAsync(int id)
        {
            var user = await userRepository.GetAsync(x => x.ID == id);
            if (user == null || user.Deleted_at != null) return null;
            return user;
        }

        public async Task<bool> BlockUserAsync(int id)
        {
            var user = await userRepository.GetAsync(x => x.ID == id);
            if (user == null || user.Deleted_at != null) return false;
            user.Blocked = true;
            user.BlockedUntil = DateTime.Now.AddDays(10);
            await userRepository.SaveAsync();
            return true;
        }

        public async Task<bool> UnBlockUser(int id)
        {
            var user = await userRepository.GetAsync(x => x.ID == id);
            if (user == null || user.Deleted_at != null) return false;
            user.Blocked = false;
            user.BlockedUntil = null;
            await userRepository.SaveAsync();
            return true;
        }

        public async Task<List<User>> UsersList(int limit, int page)
        {
            return await userRepository.GetListOfAll(limit, page);
        }

        public bool TopWriters()
        {
            return true;
        }
    }
}
