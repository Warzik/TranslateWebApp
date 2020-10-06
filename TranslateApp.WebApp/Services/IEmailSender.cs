using System;
using System.Collections.Generic;
using System.Text;

namespace TranslateApp.WebApp.Services
{
    public interface IEmailSender
    {
        void SendEmail(string email, string subject, string bodyMessage);
    }
}
