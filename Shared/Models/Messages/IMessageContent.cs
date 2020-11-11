namespace ChatService.Shared.Models.Messages
{
    public interface IMessageContent
    {
        string ContentString { get; }
        MessageContentTyp ContentTyp { get; } 
    }
}
