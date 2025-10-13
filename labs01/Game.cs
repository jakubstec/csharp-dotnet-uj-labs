namespace labs01;

public class Game
{
    private const string GameName = "goofy ahh game";
    
    public void GameLoop()
    {
        while (true)
        {
            Console.WriteLine(@$"Witaj w grze {GameName}");
            Console.WriteLine("[1] Zacznij nową grę");
            Console.WriteLine("[X] Zamknij program");
            char input = char.ToUpper(Console.ReadKey().KeyChar);
            Console.WriteLine();

            switch (input)
            {
                case '1':
                    StartNewGame();
                    break;
                case 'X':
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Zły wybór! Spróbuj ponownie.");
                    break;
            }

        }
    }

    private void StartNewGame()
    {
        Console.Clear();
        
        string heroName = ChooseHeroName();
        EHeroClass heroClass = ChooseHeroClass(heroName);
        
        Hero hero = new Hero(heroName, heroClass);
        
        Console.Clear();
        Console.WriteLine($"{hero.HeroClass} {hero.Name} wyrusza na przygodę!");
        
        // Console.WriteLine("Naciśnij dowolny klawisz, by zakończyć program...");
        // Console.ReadKey();
        //
        // Environment.Exit(0);
        
        Console.WriteLine("Naciśnij dowolny klawisz, by kontynuowac...");
        Console.ReadKey();
        PlayGame(hero);
    }


    private void PlayGame(Hero hero)
    {
        var dog = CreateDogNpc();
        var cat = CreateCatNpc();

        var location = new Location("Głupczyce", new List<NonPlayerCharacter> { dog, cat });

        var parser = new DialogParser(hero);

        ShowLocation(location, parser);

    }
    private string ChooseHeroName()
    {
        while (true)
        {
            Console.Write("Podaj nazwę swojego bohatera: ");
            string heroName = Console.ReadLine()?.Trim() ?? "";

            if (ValidateHeroName(heroName))
            {
                return heroName;
            }

            Console.WriteLine("Niepoprawna nazwa! Spróbuj ponownie. (Minimum 2 litery, tylko znaki alfabetu.)");
        }
    }
    private EHeroClass ChooseHeroClass(string heroName)
    {
        while (true)
        {
            Console.WriteLine($"Witaj {heroName}, wybierz klasę bohatera:");
            Console.WriteLine("[1] Barbarzyńca");
            Console.WriteLine("[2] Paladyn");
            Console.WriteLine("[3] Amazonka");
            Console.Write("Twój wybór: ");

            char input = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (input)
            {
                case '1': return EHeroClass.Barbarzynca;
                case '2': return EHeroClass.Paladyn;
                case '3': return EHeroClass.Amazonka;
                default:
                    Console.WriteLine("Niepoprawny wybór, spróbuj ponownie.");
                    break;
            }
        }   
    }

    private void TalkTo(NonPlayerCharacter npc, DialogParser parser)
    {
        Console.Clear();
        var dialog = npc.StartTalking();

        while (dialog != null)
        {
            Console.WriteLine($"{npc.Name}: {parser.ParseDialog(dialog)}");

            if (dialog.HeroResponses.Count == 0)
            {
                break;
            }

            for (int i = 0; i < dialog.HeroResponses.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {parser.ParseDialog(dialog.HeroResponses[i])}");
            }

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > dialog.HeroResponses.Count)
            {
                Console.WriteLine("Niepoprawny wybór, spróbuj ponownie.");
            }

            dialog = dialog.HeroResponses[choice - 1].NextPart;
            Console.Clear();
        }

        Console.WriteLine("Koniec rozmowy. Naciśnij dowolny klawisz, aby wrócić do lokacji...");
        Console.ReadKey();
    }

    private void ShowLocation(Location location, DialogParser parser)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Jesteś w lokacji: {location.Name}");
            for (int i = 0; i < location.Npcs.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {location.Npcs[i].Name}");
            }
            Console.WriteLine("[X] Wyjdź z lokacji");

            var key = char.ToUpper(Console.ReadKey(true).KeyChar);

            if (key == 'X')
            {
                break;
            }

            if (int.TryParse(key.ToString(), out int choice) && choice >= 1 && choice <= location.Npcs.Count)
            {
                TalkTo(location.Npcs[choice - 1], parser);
            }
            else
            {
                Console.WriteLine("Niepoprawny wybór, spróbuj ponownie...");
                Console.ReadKey();
            }
        }
    }

    private bool ValidateHeroName(string heroName) => 
        heroName.Length >= 2 && heroName.All(char.IsAsciiLetter);
    
    
    // poprosilem llm o przykladowe dialogi
    private NonPlayerCharacter CreateDogNpc()
    {
        var endDialog1 = new NpcDialogPart("Dziękuję za pomoc!");
        var endDialog2 = new NpcDialogPart("No trudno, może następnym razem.");

        var midDialog = new NpcDialogPart("Musisz uważać w lesie, są tam wilki.");
        midDialog.HeroResponses.Add(new HeroDialogPart("Rozumiem, będę ostrożny", endDialog1));

        var heroResponse1 = new HeroDialogPart("Tak, chętnie pomogę", midDialog);
        var heroResponse2 = new HeroDialogPart("Nie, żegnaj", endDialog2);
        var heroResponse3 = new HeroDialogPart("Nie mam czasu");

        var rootDialog = new NpcDialogPart("Hej #HERONAME#, możesz mi pomóc?");
        rootDialog.HeroResponses.Add(heroResponse1);
        rootDialog.HeroResponses.Add(heroResponse2);
        rootDialog.HeroResponses.Add(heroResponse3);

        return new NonPlayerCharacter("Pies", rootDialog);
    }

    private NonPlayerCharacter CreateCatNpc()
    {
        var endDialog1 = new NpcDialogPart("Mniam, dziękuję za mleko!");
        var endDialog2 = new NpcDialogPart("No to trudno, może innym razem.");

        var midDialog = new NpcDialogPart("Chcesz jeszcze trochę mleka lub ciasteczka?");
        midDialog.HeroResponses.Add(new HeroDialogPart("Tak, poproszę ciasteczko", endDialog1));

        var heroResponse1 = new HeroDialogPart("Tak, poproszę", midDialog);
        var heroResponse2 = new HeroDialogPart("Nie, dziękuję", endDialog2);

        var rootDialog = new NpcDialogPart("Miau #HERONAME#, chcesz trochę mleka?");
        rootDialog.HeroResponses.Add(heroResponse1);
        rootDialog.HeroResponses.Add(heroResponse2);

        return new NonPlayerCharacter("Kot", rootDialog);
    }
}