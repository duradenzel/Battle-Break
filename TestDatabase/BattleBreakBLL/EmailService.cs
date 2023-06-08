using BattleBreakDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace BattleBreakBLL
{
    public class EmailService
    {
        public string server = "smtp.office365.com";
        public int port = 587;

        public string senderEmail = "battlebreakstudio@outlook.com";
        public string senderPassword = "AmineGPT";


        public async Task SendEmail(string recipient)
        {

            string subject = "You've been invited to join a game!";

            SmtpClient client = new SmtpClient(server, port);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential(senderEmail, senderPassword);

            string body = "<html><body>\r\n\t<div class=\"header\">\r\n\t\t<img src=\"https://example.com/images/logo.png\" alt=\"Game Lobby Logo\">\r\n\t</div>\r\n\t<div class=\"body\">\r\n\t\t<h1>Invitation to Game Lobby</h1>\r\n\t\t<p>You have been invited to join a game lobby. Click the button below to join the lobby:</p>\r\n\t\t<p><a href=\"https://localhost:7098/Main/Game?gekozenspel=Tafelvoetbal\" class=\"button\">Join Game Lobby</a></p>\r\n\t</div>\r\n</body>\r\n</html>";
            MailMessage message = new MailMessage(senderEmail, recipient, subject, body);
            message.IsBodyHtml = true;
            client.Send(message);

            return;


        }

    }
}
