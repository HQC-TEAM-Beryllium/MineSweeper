using System;
using System.Linq;
using Wintellect.PowerCollections;

namespace minesweeper
{

    /// <summary>
    /// Create ScoreBoard!
    /// </summary>
    public class ScoreBoard
    {
        private readonly OrderedMultiDictionary<int, string> scoreBoard;

        public ScoreBoard()
        {
            this.scoreBoard = new OrderedMultiDictionary<int, string>(true);
        }

        public void AddPlayer(string playerName, int playerScore)
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

        public void PrintScoreBoard()
        {
            int currentCounter = 1;

            Console.WriteLine();
            if (this.scoreBoard.Values.Count == 0)
            {
                Console.WriteLine("Scoreboard empty!");
            }
            else
            {
                Console.WriteLine("Scoreboard:");
                //tui e magiq!
                //kvo da se prai - pone bachka
                foreach (int key in this.scoreBoard.Keys.OrderByDescending(obj => obj))
                {
                    bool FirstFive = false;

                    foreach (string person in this.scoreBoard[key])
                    {
                        if (currentCounter < 6)
                        //nedei se zamislq za tui 6!
                        //vqrno e i nqma kak da stane inache
                        //moje da go priemesh kato vid kod
                        {
                            Console.WriteLine("{0}. {1} --> {2} cells", currentCounter, person, key);
                            currentCounter++;
                        }
                        else
                        {
                            FirstFive = true;
                            break;
                        }
                    }
                    if (FirstFive)
                    {
                        break;
                    }
                }


            }
            Console.WriteLine();
        }
    }

    public class Mines
    {
        private const int NUMBER_OF_MINES = 15;
        private const int MINES_FIELD_ROWS = 5;
        private const int MINES_FIELD_COLS = 10;

        private static void Display(string[,] matrixOfTheMines, bool boomed)
        {
            Console.WriteLine();
            Console.WriteLine("     0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");

            for (int i = 0; i < matrixOfTheMines.GetLength(0); i++)
            {
                Console.Write("{0} | ", i);

                for (int j = 0; j < matrixOfTheMines.GetLength(1); j++)
                {
                    if (!(boomed) && ((matrixOfTheMines[i, j] == "") || (matrixOfTheMines[i, j] == "*")))
                    {
                        Console.Write(" ?");
                    }

                    if (!(boomed) && (matrixOfTheMines[i, j] != "") && (matrixOfTheMines[i, j] != "*"))
                    {
                        Console.Write(" {0}", matrixOfTheMines[i, j]);
                    }

                    if ((boomed) && (matrixOfTheMines[i, j] == ""))
                    {
                        Console.Write(" -");
                    }

                    if ((boomed) && (matrixOfTheMines[i, j] != ""))
                    {
                        Console.Write(" {0}", matrixOfTheMines[i, j]);
                    }

                }
                Console.WriteLine("|");
            }
            Console.WriteLine("   ---------------------");
        }

        private static bool Boom(string[,] matrixOfTheMines, int minesRow, int minesCol)
        {
            bool isKilled = false;
            int[] dRow = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] dCol = { 1, 0, -1, -1, -1, 0, 1, 1 };
            int minesCounter = 0;

            if (matrixOfTheMines[minesRow, minesCol] == "*")
            {
                isKilled = true;
            }

            if ((matrixOfTheMines[minesRow, minesCol] != "") && (matrixOfTheMines[minesRow, minesCol] != "*"))
            {
                Console.WriteLine("Illegal Move!");
            }

            if (matrixOfTheMines[minesRow, minesCol] == "")
            {
                for (int direction = 0; direction < 8; direction++)
                {
                    int newRow = dRow[direction] + minesRow;
                    int newCol = dCol[direction] + minesCol;

                    if ((newRow >= 0) && (newRow < matrixOfTheMines.GetLength(0)) &&
                        (newCol >= 0) && (newCol < matrixOfTheMines.GetLength(1)) &&
                        (matrixOfTheMines[newRow, newCol] == "*"))
                    {
                        minesCounter++;
                    }
                }
                matrixOfTheMines[minesRow, minesCol] += Convert.ToString(minesCounter);
            }
            return isKilled;
        }

        private static bool Winner(string[,] matrix, int cntMines)
        {
            bool isWinner = false;
            int counter = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if ((matrix[i, j] != "") && (matrix[i, j] != "*"))
                    {
                        counter++;
                    }
                }

            }

            if (counter == matrix.Length - cntMines)
            {
                isWinner = true;
            }
            return isWinner;
        }

        private static void StartTheGame(out string[,] mines, out int row,
            out int col, out bool isBoomed, out int minesCounter, out Random randomMines, out int revealedCellsCounter)
        {
            randomMines = new Random();
            row = 0;
            col = 0;
            minesCounter = 0;
            revealedCellsCounter = 0;
            isBoomed = false;
            mines = new string[MINES_FIELD_ROWS, MINES_FIELD_COLS];

            for (int i = 0; i < mines.GetLength(0); i++)
            {
                for (int j = 0; j < mines.GetLength(1); j++)
                {
                    mines[i, j] = "";
                }
            }
        }

        private static void PrintInitialMessage()
        {
            string startMessage = @"Welcome to the game “Minesweeper”. Try to reveal all cells without mines. Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit  the game.";
            Console.WriteLine(startMessage + "\n");
        }

        public void PlayMines()
        {
            ScoreBoard scoreBoard = new ScoreBoard();
            Random randomMines;
            string[,] mines;
            int row;
            int col;
            int minesCounter;
            int revealedCellsCounter;
            bool isBoomed;
        // oxo glei glei
        // i go to si imam :)
        //start the Game
        start:
            StartTheGame(out mines, out row, out col,
                out isBoomed, out minesCounter, out randomMines, out revealedCellsCounter);

            FillWithRandomMines(mines, randomMines);

            PrintInitialMessage();

            while (true)
            {
                Display(mines, isBoomed);
            enterRowCol:
                Console.Write("Enter row and column: ");
                string line = Console.ReadLine();
                line = line.Trim();

                if (IsMoveEntered(line))
                {
                    string[] inputParams = line.Split();
                    row = int.Parse(inputParams[0]);
                    col = int.Parse(inputParams[1]);

                    if ((row >= 0) && (row < mines.GetLength(0)) && (col >= 0) && (col < mines.GetLength(1)))
                    {
                        bool hasBoomedMine = Boom(mines, row, col);

                        if (hasBoomedMine)
                        {
                            isBoomed = true;
                            Display(mines, isBoomed);
                            Console.Write("\nBooom! You are killed by a mine! ");
                            Console.WriteLine("You revealed {0} cells without mines.", revealedCellsCounter);

                            Console.Write("Please enter your name for the top scoreboard: ");
                            string currentPlayerName = Console.ReadLine();
                            scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);

                            Console.WriteLine();
                            goto start;

                        }

                        bool winner = Winner(mines, minesCounter);

                        if (winner)
                        {
                            Console.WriteLine("Congratulations! You are the WINNER!\n");

                            Console.Write("Please enter your name for the top scoreboard: ");
                            string currentPlayerName = Console.ReadLine();
                            scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);

                            Console.WriteLine();
                            goto start;

                        }
                        revealedCellsCounter++;
                    }
                    else
                    {
                        Console.WriteLine("Enter valid Row/Col!\n");
                    }
                }

                else if (Proverka(line))
                {
                    switch (line)
                    {
                        case "top":
                            {
                                scoreBoard.PrintScoreBoard();
                                goto enterRowCol;
                            }
                        case "exit":
                            {
                                Console.WriteLine("\nGood bye!\n");
                                Environment.Exit(0);
                                break;
                            }
                        case "restart":
                            {
                                Console.WriteLine();
                                goto start;

                            }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Command!");
                }
            }
        }

        private static bool Proverka(string line)
        {
            if (line.Equals("top") || line.Equals("restart") || line.Equals("exit"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool IsMoveEntered(string line)
        {
            bool validMove = false;

            try
            {
                string[] inputParams = line.Split();
                int row = int.Parse(inputParams[0]);
                int col = int.Parse(inputParams[1]);
                validMove = true;
            }
            catch
            {
                validMove = false;
            }
            return validMove;
        }

        private static void FillWithRandomMines(string[,] mines, Random randomMines)
        {
            int minesCounter = 0;

            while (minesCounter < NUMBER_OF_MINES)
            {
                int randomRow = randomMines.Next(0, 5);
                int randomCol = randomMines.Next(0, 10);
                if (mines[randomRow, randomCol] == "")
                {
                    mines[randomRow, randomCol] += "*";
                    minesCounter++;
                }
            }
        }
    }
}
