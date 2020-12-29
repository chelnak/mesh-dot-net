using MESH.Api.Client.Entities;
using System.Threading.Tasks;

namespace MESH.Api.Client
{
    public interface IMESHApiClient
    {
        //Task<SendMessageResponse> SendMessage(string mailboxId); // should also handle chunking?

        //Task<GetTrackingInfoResponse> GetTrackingInfo(string mailboxId, string localId);

        Task<GetMessagesResponse> GetMessages();

        Task<GetMessageCountResponse> GetMessageCount();

        Task<Message> DownloadMessage(string messageId); // should also handle chunking?

        Task<AknowledgeMessageResponse> AknowledgeMessage(string messageId);
    }
}
