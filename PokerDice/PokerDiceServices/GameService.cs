using System;
using System.Collections.Generic;
using System.Text;

namespace PokerDice.Services
{
    /// <summary>
    /// Game Service
    /// </summary>
    public class GameService
    {
        private readonly Settings settings;

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="GameService"/> class.
        /// </summary>
        public GameService()
        {
            settings = new Settings();
        }

        /// <summary>
        /// Gets the dice result for human player.
        /// </summary>
        /// <returns></returns>
        public string GetDiceResultForHumanPlayer()
        {
            var currentHand = string.Empty;
            var allHands = new StringBuilder();
            var numberOfTurns = 0;
            do
            {
                //Roll the dices for human player
                var roll = settings.RollDice();
                currentHand = settings.GetHandNameByRoll(roll);
                allHands.Append(currentHand).Append(" ");
                Console.WriteLine($"The current hand is: {currentHand}");
                numberOfTurns++;
            } while (numberOfTurns < 3 && settings.RollDiceAgain());
            //return the highest hand
            return settings.GetTheHighestHand(allHands.ToString().Split(' '));
        }

        /// <summary>
        /// Gets the winner of the game.
        /// </summary>
        /// <param name="humanPlayerResult">The human player result.</param>
        /// <param name="aiPlayersResult">The ai players result.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public string GetWinner(string humanPlayerResult, IEnumerable<string> aiPlayersResult)
        {
            var playersResult = new List<string> {humanPlayerResult};
            playersResult.AddRange(aiPlayersResult);
            var winner = settings.GetTheHighestHand(playersResult);
            return winner;
        }

        /// <summary>
        /// Gets the dice result for ai player.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<string> GetDiceResultForAIPlayer(int numberOfAIPlayers)
        {
            //null check
            //todo: add existing functionalities to roll dice and get results for given number of players
            return new List<string>();
        }
    }
}
