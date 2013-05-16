using System;

namespace MineSweeper
{
    public class Mines
    {
        private static readonly ScoreBoard scoreBoard = new ScoreBoard();

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

        private static bool HasMine(string[,] matrixOfTheMines, int inputRow, int inputCol)
        {
            bool isKilled = matrixOfTheMines[inputRow, inputCol] == "*";

            return isKilled;
        }

        private static bool IsAlreadyOpen(string[,] matrixOfTheMines, int inputRow, int inputCol)
        {
            if ((matrixOfTheMines[inputRow, inputCol] != "") && (matrixOfTheMines[inputRow, inputCol] != "*"))
            {
                return true;
            }
            return false;
        }

        private static void PrintNumberOfSurroundingMines(string[,] matrixOfTheMines, int inputRow, int inputCol)
        {
            int[] dRow = { 1, 1, 1, 0, -1, -1, -1, 0 };
            int[] dCol = { 1, 0, -1, -1, -1, 0, 1, 1 };
            int minesCounter = 0;
            const int MaxSurroundingCells = 8;

            if (matrixOfTheMines[inputRow, inputCol] == "")
            {
                for (int currentPosition = 0; currentPosition < MaxSurroundingCells; currentPosition++)
                {
                    int newRow = dRow[currentPosition] + inputRow;
                    int newCol = dCol[currentPosition] + inputCol;

                    if ((newRow >= 0) && (newRow < matrixOfTheMines.GetLength(0)) &&
                        (newCol >= 0) && (newCol < matrixOfTheMines.GetLength(1)) &&
                        (matrixOfTheMines[newRow, newCol] == "*"))
                    {
                        minesCounter++;
                    }
                }
                matrixOfTheMines[inputRow, inputCol] += Convert.ToString(minesCounter);
            }
        }

        private static bool IsAllCellsOpened(string[,] matrix, int cntMines)
        {
            bool isWinner = false;
            int counter = 0;

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int col = 0; col < matrix.GetLength(1); col++)
                {
                    if ((matrix[row, col] != "") && (matrix[row, col] != "*"))
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
            out int col, out bool isBoomed, out int minesCounter, out Random randomMines,
            out int revealedCellsCounter)
        {
            randomMines = new Random();
            row = 0;
            col = 0;
            minesCounter = 0;
            revealedCellsCounter = 0;
            isBoomed = false;
            mines = new string[MinesFieldRows, MinesFieldCols];

            for (int rows = 0; rows < mines.GetLength(0); rows++)
            {
                for (int cols = 0; cols < mines.GetLength(1); cols++)
                {
                    mines[rows, cols] = "";
                }
            }
        }

        private static void PrintInitialMessage()
        {
            string startMessage =
                @"Welcome to the game “Minesweeper”. Try to reveal all cells without mines. Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit  the game.";
            Console.WriteLine(startMessage + "\n");
        }

        public void PlayMines()
        {
            Random randomMines;
            string[,] mines;
            int inputRow;
            int inputCol;
            int minesCounter;
            int revealedCellsCounter;
            bool isBoomed;
            bool isRestarted = false;

            StartTheGame(out mines, out inputRow, out inputCol,
                out isBoomed, out minesCounter, out randomMines, out revealedCellsCounter);

            FillWithRandomMines(mines, randomMines);

            PrintInitialMessage();

            while (true)
            {
                if (isRestarted)
                {
                    StartTheGame(out mines, out inputRow, out inputCol,
                                 out isBoomed, out minesCounter, out randomMines, out revealedCellsCounter);
                    FillWithRandomMines(mines, randomMines);
                    isRestarted = false;
                }
                DrawField(mines, isBoomed);
                Console.Write("Enter row and column: ");
                string line = Console.ReadLine();

                if (IsMoveEntered(line, ref inputRow, ref inputCol))
                {
                    if (IsAlreadyOpen(mines, inputRow, inputCol))
                    {
                        Console.WriteLine("The cell is already opened!");
                        continue;
                    }

                    if (IsInTheMatrix(inputRow,inputCol))
                    {
                        PrintNumberOfSurroundingMines(mines, inputRow, inputCol);

                        if (HasMine(mines, inputRow, inputCol))
                        {
                            DrawField(mines, true);
                            ExploadedMineAction(revealedCellsCounter);
                            isRestarted = true;
                            continue;
                        }

                        if (IsAllCellsOpened(mines, minesCounter))
                        {
                            WinningAllMinesOpened(revealedCellsCounter);
                            isRestarted = true;
                            continue;
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
                        case "top": { scoreBoard.PrintScoreBoard(); continue; }
                        case "exit": { Console.WriteLine("\nGood bye!\n"); Environment.Exit(0); return; }
                        case "restart": { Console.WriteLine(); isRestarted = true; continue; }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Command!");
                }
            }
        }

        private static void WinningAllMinesOpened(int revealedCellsCounter)
        {
            Console.WriteLine("Congratulations! You are the WINNER!\n");

            Console.Write("Please enter your name for the top scoreboard: ");
            string currentPlayerName = Console.ReadLine();
            scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);

            Console.WriteLine();
        }

        private static void ExploadedMineAction(int revealedCellsCounter)
        {
            Console.Write("\nBooom! You are killed by a mine! ");
            Console.WriteLine("You revealed {0} cells without mines.", revealedCellsCounter);

            Console.Write("Please enter your name for the top scoreboard: ");
            string currentPlayerName = Console.ReadLine();
            scoreBoard.AddPlayer(currentPlayerName, revealedCellsCounter);

            Console.WriteLine();
        }

        private static bool CheckIfCommandIsValid(string command)
        {
            return command.Equals("top") || command.Equals("restart") || command.Equals("exit");
        }

        private static bool IsMoveEntered(string line, ref int row, ref int col)
        {
            line = line.Trim();
            string[] inputParams = line.Split();

            if (inputParams.Length == 2)
                return int.TryParse(inputParams[0], out row) && int.TryParse(inputParams[1], out col);
            return false;
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

        internal bool IsInTheMatrix(int inputRow, int inputCol)
        {
            bool isInRowRange = (inputRow >= 0) && (inputRow < MinesFieldRows);
            bool isInColRange = (inputCol >= 0) && (inputCol < MinesFieldCols);

            bool isInTheMatrix = isInRowRange && isInColRange;
            
            return isInTheMatrix;
        }
    }
}
