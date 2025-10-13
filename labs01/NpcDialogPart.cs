namespace labs01;

public class NpcDialogPart : IDialogPart
{
    public string Content { get; }
    public List<HeroDialogPart> HeroResponses { get; }

    public NpcDialogPart(string content)
    {
        Content = content;
        HeroResponses = new List<HeroDialogPart>();
    }
}