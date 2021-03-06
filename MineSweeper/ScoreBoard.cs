﻿using System;
using System.Linq;
using Wintellect.PowerCollections;

namespace MineSweeper
{


    public class ScoreBoard
    {
        internal readonly OrderedMultiDictionary<int, string> scoreBoard;

        internal OrderedMultiDictionary<int, string> ScoreBoardd
        {
            get
            {
                return this.scoreBoard;
            }
        }



        public ScoreBoard()
        {
            this.scoreBoard = new OrderedMultiDictionary<int, string>(true);
        }

        //Add player with results in OrderedMultiDictionary
        public void AddPlayer(string playerName, int playerScore)
        {
            if (playerScore < 0)
            {
                throw new ArgumentOutOfRangeException("Player Score can NOT be negative!");
            }
            if (string.IsNullOrEmpty(playerName))
            {
                playerName = "unknown";
            }

            if (!scoreBoard.ContainsKey(playerScore))
            {
                scoreBoard.Add(playerScore, playerName);
            }
            else
            {
                scoreBoard[playerScore].Add(playerName);
            }

        }

        public void PrintScoreBoard()
        {

            const int MaxTopScoredUsers = 5;
            const int EmptyScoreBoard = 0;



            if (this.scoreBoard.Values.Count == EmptyScoreBoard)
            {
                Console.WriteLine("\r\nScoreboard is empty!");
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
