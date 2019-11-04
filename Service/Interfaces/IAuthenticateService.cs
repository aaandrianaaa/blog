using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Service.Models;


namespace Service.Interfaces

{
    public interface IAuthenticateService
    {

        Task<string> IsAuthenticated(TokenRequest request);
    }
}
