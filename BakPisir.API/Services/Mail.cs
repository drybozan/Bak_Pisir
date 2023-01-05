using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;

namespace BakPisir.API.Services
{
    public class Mail
    {
        public void sendMail(string mailAdres, string baslik, string mesaj)
        {
            SmtpClient client = new SmtpClient();
            MailMessage msg = new MailMessage();
            msg.IsBodyHtml = true;
            msg.To.Add(mailAdres);
            msg.From = new MailAddress("bak.pisir@yandex.com");
            msg.Subject = baslik;
            msg.Body += mesaj;
            msg.CC.Add(mailAdres);
            NetworkCredential info = new NetworkCredential("bak.pisir@yandex.com", "bakpisir2022");
            client.Port = 587;
            client.Host = "smtp.yandex.com.tr";
            client.EnableSsl = true;
            client.Credentials = info;

            client.Send(msg);
        }
    }
}