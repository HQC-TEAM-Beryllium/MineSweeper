using System;
using System.Linq;
using Wintellect.PowerCollections;

namespace MineSweeper
{
    
    public class ScoreBoard
    {
        internal readonly OrderedMultiDictionary<int, string> scoreBoard;

        public ScoreBoard()
        {
            this.scoreBoard = new OrderedMultiDictionary<int, string>(true);
        }

        //Add player with results in OrderedMultiDictionary
        public void AddPlayer(string playerName, int playerScore)
        {
            if (playerName == string.Empty)
            {
                Console.WriteLine("Enter your name, please");
            }
            else if(playerName == null)
            {
                throw new NullReferenceException("PlayName can NOT be null");
            }
            else
            {
                if (!scoreBoard.ContainsKey(playerScore))
                {
                    scoreBoard.Add(playerScore, playerName);
                }
                else
                {
                    scoreBoard[playerScore].Add(playerName);
                }
            }
        }

        public void Print()
        {
            
            const int MaxTopScoredUsers = 5;
            const int EmptyScoreBoard = 0;

            Console.WriteLine();
            
            if (this.scoreBoard.Values.Count == EmptyScoreBoard)
            {
                Console.WriteLine("Scoreboard empty!");
            }
            else
            {
                int currentPlayerCounter = 1;

                Console.WriteLine("Scoreboard:");

                foreach (int score in this.scoreBoard.Keys.OrderByDescending(obj => obj))
                {
                    bool isFirstFiveScoreIsDisplayed = false;
                    

                    foreach (string person in this.scoreBoard[score])
                    {
                        if (currentPlayerCounter <= MaxTopScoredUsers)
                        {
                            Console.WriteLine("{0}. {1} --> {2} cells", currentPlayerCounter, person, score);
                            currentPlayerCounter++;
                        }
                        else
                        {
                            isFirstFiveScoreIsDisplayed = true;
                            break;
                        }
                    }

                    if (isFirstFiveScoreIsDisplayed)
                    {
                        break;
                    }
                }
            }
            Console.WriteLine();
        }
    }
}
