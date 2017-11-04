using Microsoft.Extensions.Configuration;
using RoadStoryTracking.WebApi.Business.BusinessModels.Exceptions;
using RoadStoryTracking.WebApi.Business.BusinessModels.Responses;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Business.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly string _emailSenderServiceAddress;
        private readonly string _emailSenderServiceName;
        private readonly string _sendGridAPIKey;

        public EmailService(IConfiguration configuration)
        {
            if (string.IsNullOrEmpty(configuration["SendGrid:EmailServiceAddress"])
                || string.IsNullOrEmpty(configuration["SendGrid:EmailServiceName"])
                || string.IsNullOrEmpty(configuration["SendGrid:SendGridAPIKey"]))
            {
                throw new ApplicationException("Email sender service has bad configuration. Missing configuration keys");
            }

            _emailSenderServiceAddress = configuration["SendGrid:EmailServiceAddress"];
            _emailSenderServiceName = configuration["SendGrid:EmailServiceName"];
            _sendGridAPIKey = configuration["SendGrid:SendGridAPIKey"];
        }

        public async Task<BaseResponse> SendEmail(string emailTo, string fullName, string subject, string messageText, string messageHtml)
        {
            var message = CreateEmail(emailTo, fullName, subject, messageText, messageHtml);
            var sendGridResponse = await SendEmailAsync(message);

            if (sendGridResponse.StatusCode == HttpStatusCode.OK)
            {
                return new SuccessResponse<bool>(true);
            }
            else
            {
                return new ErrorResponse(new CustomApplicationException("SendGrid service could not send an email!", sendGridResponse));
            }
        }

        protected async Task<Response> SendEmailAsync(SendGridMessage message)
        {
            var client = new SendGridClient(_sendGridAPIKey);
            return await client.SendEmailAsync(message);
        }

        private SendGridMessage CreateEmail(string emailTo, string fullName, string subject, string messageText, string messageHtml)
        {
            var message = new SendGridMessage();

            message.SetFrom(new EmailAddress(_emailSenderServiceAddress, _emailSenderServiceName));

            var recipients = new List<EmailAddress>
            {
                new EmailAddress(emailTo, fullName)
            };

            message.AddTos(recipients);
            message.SetSubject(subject);
            message.AddContent(MimeType.Text, messageText);
            message.AddContent(MimeType.Html, messageHtml);

            return message;
        }
    }
}