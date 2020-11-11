namespace ChatService.Shared.Models.Messages
{
    /// <summary>
    /// Dient zum melder des MessageState
    /// </summary>
    public class MessageStateModification
    {
        public string MessageGuid { get; set; }

        public MessageState MessageState { get; set; }
    }
}
