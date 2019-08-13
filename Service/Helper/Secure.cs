using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Service.Helper
{
    public static class Secure
    {
        public static string Encryptpass(string password)
        {
            string msg = "";
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            msg = Convert.ToBase64String(encode);
            return msg;
        }
    }

}
