using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Helper;
using Service.Interfaces;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementations
{
    public class UserManagementService : IUserManagementService
    {
        BlogContext db;
        public UserManagementService(BlogContext db)
        {
            this.db = db;
        }

        public async Task<bool> CreateAsync(User user)
        {
            if (db.Users.Where(u => u.Email == user.Email || u.Nickname == user.Nickname).FirstOrDefault() == null)
            {


                Random rand = new Random();
                int random = rand.Next(10000000, 100000000);
                if (Mail.SendConfirmation(user.Email, random))
                {
                    var confirm = new Confirmation();
                    confirm.Email = user.Email;
                    confirm.Rand = random;
                    db.Confirmations.Add(confirm);
                    db.Users.Add(user);
                    await db.SaveChangesAsync();

                    return true;
                }
            }

            return false;
        }

        public async Task<bool> SendConfirmation(string email)
        {

            Random rand = new Random();
            int random = rand.Next(10000000, 100000000);
            if (Mail.SendConfirmation(email, random))
            {
                var confirm = db.Confirmations.Where(c => c.Email == email).First();
                db.Confirmations.Remove(confirm);
                var conf = new Confirmation();
                confirm.Email = email;
                confirm.Rand = random;
                db.Confirmations.Add(conf);
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public User GetUser(string email, string password)
        {

            var user = db.Users.Include(u => u.Role).Where(u => u.RoleID == u.Role.ID).FirstOrDefault(u => u.Email == email && Secure.Encryptpass(password) == u.Password);
            if (user != null)
            {

                return user;
            }
            return null;

        }

        public async Task<bool> CofirmMail(Confirmation confirm)
        {
            var confirms = db.Confirmations.Where(a => a.Rand == confirm.Rand && a.Email == confirm.Email).FirstOrDefault();
            if (confirms == null)
                return false;
            var user = db.Users.Where(u => u.Email == confirm.Email).First();
            if (confirm.Created_at.AddHours(24) >= DateTime.Now)
            {
                user.Activated = true;
                db.Confirmations.Remove(confirms);
                await db.SaveChangesAsync();

                return true;
            }


            return false;
        }

        public async Task<bool> CofirmUser(string email)
        {
            Random rand = new Random();
            int random = rand.Next(10000000, 100000000);
            if (Mail.SendConfirmation(email, random))
            {
                var userconfirm = db.Confirmations.Where(c => c.Email == email).First();
                userconfirm.Rand = random;
                await db.SaveChangesAsync();
                return true;
            }
            return false;
        }






    }
}
