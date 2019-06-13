﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using TimeSheet.Helper.Interfaces;
using TimeSheet.Helper.Models;

namespace TimeSheet.Helper
{
    public class EmailService : IEmailService
    {

        #region [Fields]

        /// <summary>
        /// The config value in appsetting.json
        /// </summary>
        private readonly IConfiguration _config;

        #endregion

        #region [Constructors]

        /// <summary>
        /// Initializes a new instance of the <see cref="EmailService" /> class.
        /// </summary>
        /// <param name="config">The config value.</param>
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        #endregion

        #region [Methods]

        /// <summary>
        /// Send email without specified template.  
        /// </summary>
        /// <param name="email">Email detail.</param>
        public void SendEmail(EmailModel email)
        {
            //Call method to send the email.
            this.SendTheEmail(email);
        }
        /// <summary>
        /// Send email with specified template.
        /// </summary>
        /// <param name="email">Email detail.</param>
        public void SendEmailWithTemplate(EmailModel email)
        {
            //Call method to send the email.
            this.SendTheEmail(email);
        }
        /// <summary>
        /// Send the email
        /// </summary>
        /// <param name="email">Email detail</param>
        private void SendTheEmail(EmailModel email)
        {
            //Get email configuration
            var smtpHost = _config["SMTP:Host"];
            var smtpPort = _config["SMTP:Port"];
            var requireCredential = _config["SMTP:RequireCredential"];
            var enableSSL = _config["SMTP:EnableSSL"];
            var user = _config["SMTP:User"];
            var password = _config["SMTP:Password"];

            SmtpClient client = new SmtpClient(smtpHost, int.Parse(smtpPort))
            {
                EnableSsl = Convert.ToBoolean(enableSSL),
                UseDefaultCredentials = false
            };
            if (requireCredential == "true")
            {
                client.Credentials = new NetworkCredential(user, password);
            }
            //Create an email.
            MailMessage mailItem = new MailMessage
            {
                From = new MailAddress(email.Sender) // must use organization email
            };
            mailItem.To.Add(email.Receiver);
            mailItem.Subject = email.Subject;
            mailItem.IsBodyHtml = true;
            mailItem.Body = email.Body;
            try
            {
                //Send an email 
                client.Send(mailItem);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message + ", Send :" + email.Sender + "Receiver :" + email.Receiver);
            }
        }

        #endregion

    }
}
