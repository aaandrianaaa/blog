using Microsoft.AspNetCore.Http;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserManagementService
    {
       Task<User> GetUserAsync(string username, string password);
        Task<bool> CreateAsync(User user);

        Task<bool> CofirmMail(Confirmation confirm);
        Task<bool> CofirmUser(string email);
        Task<bool> SendConfirmation(string email);
        Task<bool> SendConfirmationPassword(string email);
        Task<bool> ForgotPassword(string email, int number);
        Task<bool> ChangePassword(int id, string old_password, string new_password);
        Task<bool> ResendConfirmPasswors(string email);
        Task<bool> ChangeRole(int id, int new_role, int moderator_id);
        Task<bool> DeleteUser(int id);
        Task<bool> PatchUser(int id, User patch_user);
        Task<User> GetUserByIDAsync(int id);
        Task<bool> BlockUserAsync(int id);
        Task<bool> UnBlockUser(int id);
        Task<List<User>> UsersList(int limit, int page);
        Task<bool> PostPhotoAsync(IFormFile file, int? UserId);


    }
    

}
