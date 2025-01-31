using System;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace EFoodCommerce.Utilidades
{
	public class EmailSender : IEmailSender
	{
		public EmailSender()
		{

		}

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            throw new NotImplementedException();
        }
    }
}

