﻿using RoadStoryTracking.WebApi.Business.Models.Responses;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Business.Logic.Services.EmailService
{
    public interface IEmailService
    {
        Task<BaseResponse> SendEmail(string emailTo, string fullName, string subject, string messageText, string messageHtml);
    }
}