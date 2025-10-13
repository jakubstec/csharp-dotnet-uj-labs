namespace labs01;

public class HeroDialogPart : IDialogPart
{
    public string Content { get; }
    public NpcDialogPart? NextPart { get; }

    public HeroDialogPart(string content, NpcDialogPart? nextPart = null)
    {
        Content = content;
        NextPart = nextPart;
    }
}
