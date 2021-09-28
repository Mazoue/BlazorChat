using BlazorChat.Interfaces;
using DataModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BlazorChat.Services
{
    public class PersistanceService : IPersistanceService
    {
        private string _currentDirectory = Path.Combine(Directory.GetCurrentDirectory(), @"Messages\");

        public PersistanceService()
        {
        }

        public async Task<IEnumerable<UserMessage>> GetAllMessagesByRoom(string hubName)
        {
            var combinedPath = $"{Path.Combine(_currentDirectory, hubName)}.json";
            var currentChat = await File.ReadAllTextAsync(combinedPath);
            var messages = JsonConvert.DeserializeObject<List<UserMessage>>(currentChat);
            return messages;
        }

        public async Task PersistMessage(UserMessage message)
        {
            var combinedPath = $"{Path.Combine(_currentDirectory, message.HubName)}.json";

            if (await CheckIfFirstMessage(combinedPath))
            {
                await File.WriteAllTextAsync(combinedPath, $"[{JsonConvert.SerializeObject(message)}]");
            }
            else
            {
                var currentChat = await File.ReadAllTextAsync(combinedPath);
                var messages = JsonConvert.DeserializeObject<List<UserMessage>>(currentChat);
                messages.Add(message);
                await File.WriteAllTextAsync($"{combinedPath}", JsonConvert.SerializeObject(messages));
            }
        }

        private static async Task<bool> CheckIfFirstMessage(string filePath)
        {
            var isFirstMessage = false;
            if (!File.Exists(filePath))
            {
                isFirstMessage = true;
            }
            var currentChat = await File.ReadAllTextAsync(filePath);
            if (!currentChat.Contains('[') || !currentChat.Contains(']'))
            {
                isFirstMessage = true;
            }
            return isFirstMessage;

        }
    }
}
