using RPG.Repositories;
using RPG;
using RPG.Enums;
using RPG.Screens;
using RPG.Services;
using RPG.Models;

class Program
{
    static void Main(string[] args)
    {
        using var context = new RPGContext();

        // Repos and Services
        var heroRepository = new HeroRepository(context);
        var gameRepository = new GameRepository(context);
        var monsterRepository = new MonsterRepository(context);

        var heroService = new HeroService(heroRepository);
        var gameService = new GameService(gameRepository);
        var monsterService = new MonsterService(monsterRepository);

        Screen currentScreen = Screen.MainMenu;
        Hero hero = null;

        while (currentScreen != Screen.Exit)
        {
            switch (currentScreen)
            {
                case Screen.MainMenu:
                    MainMenu.Show();
                    currentScreen = Screen.CharacterSelect;
                    break;

                case Screen.CharacterSelect:
                    hero = CharacterSelect.Show(heroService);
                    currentScreen = Screen.InGame;
                    break;

                case Screen.InGame:
                    var inGame = new InGame(heroService, monsterService, gameService);
                    inGame.Show(hero);
                    currentScreen = Screen.Exit;
                    break;

                case Screen.Exit:
                    Exit.Show();
                    break;

                default:
                    currentScreen = Screen.Exit;
                    break;
            }
        }
    }
}
