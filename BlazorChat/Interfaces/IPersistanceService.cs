using DataModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorChat.Interfaces
{
    public interface IPersistanceService
    {
        Task PersistMessage(UserMessage message);

        Task<IEnumerable<UserMessage>> GetAllMessagesByRoom(string hubName);
    }
}
