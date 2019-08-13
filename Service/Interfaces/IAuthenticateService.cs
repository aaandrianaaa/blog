using System;
using System.Collections.Generic;
using System.Text;
using Service.Models;


namespace Service.Interfaces

{
    public interface IAuthenticateService
    {

        bool IsAuthenticated(TokenRequest request, out string token);
    }
}
