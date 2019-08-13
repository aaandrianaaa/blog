using Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserManagementService
    {
        User GetUser(string username, string password);
        Task<bool> CreateAsync(User user);

        Task<bool> CofirmMail(Confirmation confirm);
        Task<bool> CofirmUser(string email);
        Task<bool> SendConfirmation(string email);
    
    }

}
