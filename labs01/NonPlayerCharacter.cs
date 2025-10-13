namespace labs01;

public class NonPlayerCharacter
{
    public string Name { get; }
    public NpcDialogPart RootDialog { get; }
    
    public NonPlayerCharacter(string name, NpcDialogPart rootDialog)
    {
        Name = name;
        RootDialog = rootDialog;
    }

    public NpcDialogPart StartTalking() => RootDialog;

}