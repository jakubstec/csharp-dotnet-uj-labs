namespace labs01;

public class DialogParser
{
    private readonly string heroName;

    public DialogParser(Hero hero)
    {
        heroName = hero.Name;
    }

    public string ParseDialog(IDialogPart dialogPart)
    {
        return dialogPart.Content.Replace("#HERONAME#", heroName);
    }
}