using System;

namespace MineSweeper
{
    public class Mines
    {
        static ScoreBoard scoreBoard = new ScoreBoard();
        private const int NumberOfMines = 15;
        private const int MinesFieldRows = 5;
        private const int MinesFieldCols = 10;

        private static void DrawField(string[,] matrixOfTheMines, bool boomed)
        {
            Console.WriteLine();
            Console.WriteLine("     0 1 2 3 4 5 6 7 8 9");
            Console.WriteLine("   ---------------------");

            for (int row = 0; row < matrixOfTheMines.GetLength(0); row++)
            {
                Console.Write("{0} | ", row);

                for (int col = 0; col < matrixOfTheMines.GetLength(1); col++)
                {
                    if (!(boomed) && ((matrixOfTheMines[row, col] == "") || (matrixOfTheMines[row, col] == "*")))
                    {
                        Console.Write(" ?");
                    }

                    if (!(boomed) && (matrixOfTheMines[row, col] != "") && (matrixOfTheMines[row, col] != "*"))
                    {
                        Console.Write(" {0}", matrixOfTheMines[row, col]);
                    }

                    if ((boomed) && (matrixOfTheMines[row, col] == ""))
                    {
                        Console.Write(" -");
                    }

                    if ((boomed) && (matrixOfTheMines[row, col] != ""))
                    {
                        Console.Write(" {0}", matrixOfTheMines[row, col]);
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
            mines = new string[MinesFieldRows, MinesFieldCols];

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
            
            Random randomMines;
            string[,] mines;
            int row;
            int col;
            int minesCounter;
            int revealedCellsCounter;
            bool isBoomed;

            StartTheGame(out mines, out row, out col,
                out isBoomed, out minesCounter, out randomMines, out revealedCellsCounter);

            FillWithRandomMines(mines, randomMines);

            PrintInitialMessage();

            while (true)
            {
                DrawField(mines, isBoomed);
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
                            DrawField(mines, true);
                            Console.Write("\nBooom! You are killed by a mine! ");
                            Console.WriteLine("You revealed {0} cells without mines.", revealedCellsCounter);

                            Console.Write("Please enter your name for the top scoreboard: ");
                            string currentPlayerName = Console.ReadLine();
                            scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);

                            Console.WriteLine();
                            PlayMines();

                        }

                        bool winner = Winner(mines, minesCounter);

                        if (winner)
                        {
                            Console.WriteLine("Congratulations! You are the WINNER!\n");

                            Console.Write("Please enter your name for the top scoreboard: ");
                            string currentPlayerName = Console.ReadLine();
                            scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);

                            Console.WriteLine();
                            PlayMines();

                        }
                        revealedCellsCounter++;
                    }
                    else
                    {
                        Console.WriteLine("Enter valid Row/Col!\n");
                    }
                }

                else if (CheckIfCommandIsValid(line))
                {
                    switch (line)
                    {
                        case "top":
                            {
                                scoreBoard.PrintScoreBoard();
                                continue;
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
                                PlayMines();
                                break;
                            }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Command!");
                }
            }
        }

        private static bool CheckIfCommandIsValid(string line)
        {
            return line.Equals("top") || line.Equals("restart") || line.Equals("exit");
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

            while (minesCounter < NumberOfMines)
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
