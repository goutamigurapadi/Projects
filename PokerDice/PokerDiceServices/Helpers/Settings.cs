using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using PokerDice.Services;
using PokerDice.Services.Enums;
using PokerDice.Services.Helpers;

namespace PokerDice
{
    /// <summary>
    /// Setup the game
    /// </summary>
    public class Settings
    {
        private readonly Extensions extensions;

        public Settings()
        {
            extensions = new Extensions();
        }

        public int NumberOfAIPlayers { get; set; }

        /// <summary>
        /// Gets the game details from user.
        /// User can add more options for game
        /// </summary>
        public void GetGameDetailsFromUser()
        {
            //get number of AI players
            GetNumberOfAIPlayers();
        }

        /// <summary>
        /// Gets the number of ai players.
        /// </summary>
        private void GetNumberOfAIPlayers()
        {
            //ai players count
            int aiPlayersCount;

            //ask user until he enters right number of players
            do
            {
                //ask user to enter number of players
                Console.WriteLine("Please enter number of players between 2 to 5:");

                //get the players
                if (int.TryParse(Console.ReadLine(), out aiPlayersCount))
                {
                    NumberOfAIPlayers = aiPlayersCount;
                }
            }
            while (aiPlayersCount < 2 || aiPlayersCount > 5);
        }


        public string GetTheHighestHand(IEnumerable<string> hands)
        {
            var sortedHands = new List<string>();
            if (hands.Any())
            {
                sortedHands = SortHands(hands).ToList();
            }
            return sortedHands.FirstOrDefault();
        }

        private IEnumerable<string> SortHands(IEnumerable<string> hands)
        {
            var handsList = new List<int>();
            foreach (var hand in hands)
            {
                var thisHand = hand.Split('-')[0];
                var handNumber = (int)(Enum.Parse(typeof(PokerHandEnum), thisHand));
                handsList.Add(handNumber);
            }
            var sortedHandsList = handsList.OrderBy(s => s);
            return sortedHandsList.Select(s => ((PokerHandEnum)s).ToString()).ToList();
        }

        /// <summary>
        /// Rolls the dice.
        /// </summary>
        /// <returns></returns>
        public string RollDice()
        {
            var diceResult = new StringBuilder();
            for (var i = 0; i < 5; i++)
            {
                var randomDieResult = extensions.GetEnumDescription(GenericRandomPicker.RandomEnumValue<PokerDiceEnum>());
                diceResult.Append(randomDieResult);
            }
            return diceResult.ToString();
        }

       
        /// <summary>
        /// Rolls the dice again.
        /// </summary>
        /// <returns></returns>
        public bool RollDiceAgain()
        {
            //ask user until give correct answer
            while (true)
            {
                Console.Write("If you don't like the current hand, you can roll the dice again. Do you want to roll the dice again (yes/no)?");
                string response = Console.ReadLine()?.ToLower();
                //if response is yes, return true
                //if response no return false;
                switch (response)
                {
                    case "yes":
                        return true;
                    case "no":
                        return false;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the hand name by roll.
        /// </summary>
        /// <param name="roll">The roll.</param>
        /// <returns></returns>
        public string GetHandNameByRoll(string roll)
        {
            //Five a kind
            if (Regex.Match(roll, @"(?:9)+|(?:A)+|(?:K)+|(?:Q)+|(?:J)+").Length == 5 || Regex.Match(roll, @"(?:10)+|").Length == 10)
            {
                return $"{PokerHandEnum.FiveOfAKind} - {roll}";
            }
            //Four of a kind
            else if (Regex.Match(roll, @"(.)\1+").Length == 4 || Regex.Match(roll, @"(10)\1+").Length == 8)
            {
                return $"{PokerHandEnum.FourOfAKind} - {roll}";
            }
            //todo: not done
            //Full House - three of a kind and a pair
            else if ((Regex.Match(roll, @"([a-zA-Z0-9])\1{2}").Length == 3 &&
                      Regex.Match(roll, @"([a-zA-Z0-9])\1").Length == 2) ||
                     (Regex.Match(roll, @"(10)\1").Length == 4 &&
                      Regex.Match(roll, @"([a-zA-Z0-9])\1{2}").Length == 3) ||
                     (Regex.Match(roll, @"(10)\1{2}").Length == 6 && Regex.Match(roll, @"([a-zA-Z0-9])\1").Length == 2))
            {
                return $"{PokerHandEnum.FullHouse} - {roll}";
            }
            //Straight - all five different faces in sequence
            else if (extensions.GetEnumDescriptionInSequence().Contains(roll))
            {
                return $"{PokerHandEnum.Straight} - {roll}";
            }
            //Three of a kind
            else if (Regex.Match(roll, @"([a-zA-Z0-9])\1{2}").Length == 3 || Regex.Match(roll, @"(10)\1{2}").Length == 3)
            {
                return $"{PokerHandEnum.ThreeOfAKind} - {roll}";
            }
            //todo:not done
            //Two pair -has two pairs
            else if (extensions.HasTwoPairs(roll))
            {
                return $"{PokerHandEnum.TwoPair} - {roll}";
            }
            //one pair - has one pair
            else if (Regex.IsMatch(roll, @"(.)\1") || Regex.IsMatch(roll, @"(10)\1"))
            {
                return $"{PokerHandEnum.OnePair} - {roll}";
            }
            //Burst - assuming it is default case
            else
            {
                return $"{PokerHandEnum.Burst} - {roll}";
            }
        }
    }
}
