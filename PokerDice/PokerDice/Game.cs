using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;
using PokerDice.Services;

namespace PokerDice
{
    /// <inheritdoc />
    /// <summary>
    /// Game class
    /// </summary>
    public class Game: IDisposable
    {
        //game service interface
        private readonly GameService _gameService;
        private readonly Settings settings;

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game()
        {
            _gameService = new GameService();
            settings = new Settings();
        }
        /// <summary>
        /// Starts the game.
        /// </summary>
        public async void StartGame()
        {
            //welcome message
            Console.WriteLine("Welcome to Poker Hand Game");

            // ask the user and get the number of AI players in this game 
            settings.GetGameDetailsFromUser();

            //todo: add method called start game and move below logic
            //get dice result for human player
            var humanPlayerResult = _gameService.GetDiceResultForHumanPlayer();
            var AIPlayersResult = _gameService.GetDiceResultForAIPlayer(settings.NumberOfAIPlayers);
            //todo:Display AI hand for all players
            //gives the player name
            var winner = _gameService.GetWinner(humanPlayerResult, AIPlayersResult);
            Console.WriteLine($"The winner is {winner}");
        }

        /// <summary>
        /// Plays again.
        /// </summary>
        /// <returns></returns>
        public bool StartAgain()
        {
            //ask user until give correct answer
            while (true)
            {
                Console.Write("Hope you enjoyed the game. Do you want to play again (yes/no)?");
                string response = Console.ReadLine()?.ToLower();
                //if response is yes, return true
                //if response no return false;
                switch (response)
                {
                    case "yes":
                        return true;
                    case "no":
                        return false;
                }
            }
        }

        #region Dispose

        // Flag: Has Dispose already been called?
        bool _disposed = false;
        // Instantiate a SafeHandle instance.
        readonly SafeHandle _handle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _handle.Dispose();
                // Free any other managed objects here.
                //
            }

            // Free any unmanaged objects here.
            //
            _disposed = true;
        }


        #endregion
    }
}
