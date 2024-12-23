using RPG.Models;
using RPG.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG.Services
{
    public class GameService
    {
        private readonly GameRepository _gameRepository;

        public GameService(GameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public Game StartGame(Hero hero)
        {
            var game = new Game
            {
                HeroId = hero.Id,
                MonstersKilled = 0
            };

            _gameRepository.SaveGame(game);
            return game;
        }

        public void UpdateMonstersKilled(Game game)
        {
            game.MonstersKilled++;
            _gameRepository.UpdateGame(game);
        }
    }
}
