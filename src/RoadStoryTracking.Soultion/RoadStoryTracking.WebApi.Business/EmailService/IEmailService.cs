using RoadStoryTracking.WebApi.Business.BusinessModels.Responses;
using System.Threading.Tasks;

namespace RoadStoryTracking.WebApi.Business.EmailService
{
    public interface IEmailService
    {
        Task<BaseResponse> SendEmail(string emailTo, string fullName, string subject, string messageText, string messageHtml);
    }
}