namespace labs01;

public class Hero
{
    public string Name { get;}
    public EHeroClass HeroClass { get;}

    public Hero(string name, EHeroClass heroClass)
    {
        Name = name;
        HeroClass = heroClass;
    }
    
}