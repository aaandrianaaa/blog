using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System;
using System.IO;
using RestSharp;
using RestSharp.Authenticators;

namespace Service.Helper
{
    class Mail
    {
        public static bool SendConfirmation(string email, int random)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator = new HttpBasicAuthenticator("api", "bc494e921ad0d8691434e646e9a78f71-73ae490d-d915fcb6");
            RestRequest request = new RestRequest();
            request.AddParameter("domain", "sandboxc139b4025265469595fc702dace58f8d.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Mailgun Sandbox <postmaster@sandboxc139b4025265469595fc702dace58f8d.mailgun.org>");
            request.AddParameter("to", email);
            request.AddParameter("subject", "Hello");
            request.AddParameter("text", random.ToString());
            request.Method = Method.POST;
            var res = client.Execute(request);

            return true;
        }
    }
}
