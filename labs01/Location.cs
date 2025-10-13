namespace labs01;

public class Location
{
    public string Name { get;}
    public List<NonPlayerCharacter> Npcs { get; }

    public Location(string name, List<NonPlayerCharacter> npcs)
    {
        Name = name;
        Npcs = npcs;
    }
}