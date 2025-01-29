public class Message
{
    public List<string> To { get; }
    public string Subject { get; }
    public string Content { get; }
    public string? Attachment { get; }

    public Message(IEnumerable<string> to, string subject, string content, string? attachment = null)
    {
        To = new List<string>(to);
        Subject = subject;
        Content = content;
        Attachment = attachment;
    }
}
