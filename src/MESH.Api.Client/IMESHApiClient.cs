using MESH.Api.Client.Entities;
using System.Threading.Tasks;

namespace MESH.Api.Client
{
    public interface IMESHApiClient
    {
        //Task<SendMessageResponse> SendMessage(string mailboxId); // should also handle chunking?

        Task<GetMessagesResponse> GetMessages();

        Task<GetMessageCountResponse> GetMessageCount();

        Task<DownloadMessageResponse> DownloadMessage(string messageId); // should also handle chunking?

        Task<AcknowledgeMessageResponse> AcknowledgeMessage(string messageId);
    }
}
