using System;

namespace DataModels
{
    public class UserMessage
    {
        public int Id { get; set; }
        public DateTimeOffset TimeOffset { get; set; }
        public string MessageText { get; set; }
        public string InitiatedBy { get; set; }
        public string HubName { get; set; }
    }
}
