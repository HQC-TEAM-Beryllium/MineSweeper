using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeper;

namespace UnitTestProject1
{
    [TestClass]
    public class MinesTests
    {
        [TestMethod]
        public void DisplayTest()
        {
            #region ConsoleTEST
            //var output = new StringBuilder();
            //var textWriter = new StringWriter(output);
            //Console.SetOut(textWriter);



//            var input = @"Test123
//test1234";
//            var textReader = new StringReader(input);
//            Console.SetIn(textReader);

//            Mines.PrintInitialMessage();
            #endregion
            //string[,] matrixOfTheMines = new string[5, 10];
            //bool boomed = false;
            //matrixOfTheMines[0, 0] = "";
            //matrixOfTheMines[0, 2] = "*";

            //Assert.IsFalse(!(boomed) &&5 ((matrixOfTheMines[0, 2] == "")));
            //Assert.IsTrue((matrixOfTheMines[0, 0] == string.Empty) || (matrixOfTheMines[0, 0] == "*"));
        }

        //[TestMethod]
        //public void BoomTest()
        //{
        //    string[,] matrixOfTheMines = new string[5, 10];
        //    int minesRow = 1;
        //    int minesCol = 2;
        //    matrixOfTheMines[minesRow, minesCol] = "";
        //    bool isKilled = false;
        //    Assert.AreEqual(isKilled, matrixOfTheMines[minesRow, minesCol] == "*");

        //    minesRow = 0;
        //    minesCol = 0;
        //    matrixOfTheMines[minesRow, minesCol] = "error";
        //    bool checkIsTrue = ((matrixOfTheMines[minesRow, minesCol] != "") && (matrixOfTheMines[minesRow, minesCol] != "*"));
        //    string message = "Illegal Move!";
        //    Assert.IsTrue(checkIsTrue == true && message == "Illegal Move!");
        //}

        //[TestMethod]
        //public void WinnerTest()
        //{
        //    string[,] matrix = new string[5, 10];
        //    int cntMines = 0;
        //    int counter = 1;
        //    matrix[0, 0] = counter.ToString();
        //    int matrixSize = matrix.GetLength(0) * matrix.GetLength(1);
        //    bool isWinner = true;
        //    bool checkHasMines = ((matrix[0, 0] == "") && (matrix[0, 0] == "*"));
        //    Assert.IsTrue((checkHasMines == false) && (matrix[0, 0] == counter.ToString()));
        //    Assert.AreEqual(cntMines == 0, checkHasMines == false);
        //    Assert.IsTrue((isWinner == true) && (matrixSize == 50));
        //}

        //[TestMethod]
        //public void ProverkaTest()
        //{
        //    string[] line = { "top", "restart", "exit" };
        //    bool checkLines = (line[0].Equals("top") || line[0].Equals("restart") || line[0].Equals("exit"));
        //    Assert.AreEqual(checkLines, true);
        //    Assert.IsTrue(!checkLines == false);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(FormatException))]
        //public void IsMoveEnteredTest()
        //{
        //    string line = "1 two";
        //    string[] inputParams = line.Split();
        //    int row = int.Parse(inputParams[0]);
        //    int col = int.Parse(inputParams[1]);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(IndexOutOfRangeException))]
        //public void FillWithRandomMinesTest()
        //{
        //    string[,] mines = new string[5, 10];
        //    mines[10, 20] += "*";
        //}

        [TestMethod]
        public void IsInTheMatrixTest()
        {
            var minesGame = new Mines();
            //down right ouside check
            Assert.IsFalse(minesGame.IsInTheMatrix(Mines.MinesFieldRows,Mines.MinesFieldCols));
            Assert.IsFalse(minesGame.IsInTheMatrix(Mines.MinesFieldRows , Mines.MinesFieldCols-1));
            Assert.IsFalse(minesGame.IsInTheMatrix(Mines.MinesFieldRows-1, Mines.MinesFieldCols));
            //down right inside check
            Assert.IsTrue(minesGame.IsInTheMatrix(Mines.MinesFieldRows - 1, Mines.MinesFieldCols-1));


            //down left outside check
            Assert.IsFalse(minesGame.IsInTheMatrix(Mines.MinesFieldRows, -1));
            Assert.IsFalse(minesGame.IsInTheMatrix(Mines.MinesFieldRows, 0));
            Assert.IsFalse(minesGame.IsInTheMatrix(Mines.MinesFieldRows - 1, -1));
            //down left inside check
            Assert.IsTrue(minesGame.IsInTheMatrix(Mines.MinesFieldRows - 1, 0));

            //up left outside check
            Assert.IsFalse(minesGame.IsInTheMatrix(-1, -1));
            Assert.IsFalse(minesGame.IsInTheMatrix(0, -1));
            Assert.IsFalse(minesGame.IsInTheMatrix(-1, 0));
            //up left inside check
            Assert.IsTrue(minesGame.IsInTheMatrix(0, 0));

            //up right outside check
            Assert.IsFalse(minesGame.IsInTheMatrix(-1, Mines.MinesFieldCols));
            Assert.IsFalse(minesGame.IsInTheMatrix(-1, Mines.MinesFieldCols-1));
            Assert.IsFalse(minesGame.IsInTheMatrix(0, Mines.MinesFieldCols));
            //up right inside check
            Assert.IsTrue(minesGame.IsInTheMatrix(0, Mines.MinesFieldCols - 1));


            //inside in the middle check
            Assert.IsTrue(minesGame.IsInTheMatrix(Mines.MinesFieldRows/2, Mines.MinesFieldCols/2));
        }

        [TestMethod]
        public void HasMineTest()
        {
            var input = @"- - - * - * - - - -
- - * - - - - - * -
* - * - * * - - - -
- - - - - * - * - -
- * * - * * - - * -";
            
             
            string[] matrix = input.Split(new char[] {' '},StringSplitOptions.RemoveEmptyEntries);
            var textReader = new StringReader(input);
            Console.SetIn(textReader);

            var minesGame = new Mines();
        }

        [TestMethod]
        public void IsCommandTopValidTest()
        {
            var minesGame = new Mines();

            Assert.IsTrue(minesGame.CheckIfCommandIsValid("Top"));
            Assert.IsTrue(minesGame.CheckIfCommandIsValid("top"));
            Assert.IsTrue(minesGame.CheckIfCommandIsValid("TOP"));
            
            Assert.IsFalse(minesGame.CheckIfCommandIsValid("T0P"));
        }

        [TestMethod]
        public void IsCommandRestartValidTest()
        {
            var minesGame = new Mines();

            Assert.IsTrue(minesGame.CheckIfCommandIsValid("Restart"));
            Assert.IsTrue(minesGame.CheckIfCommandIsValid("restart"));
            Assert.IsTrue(minesGame.CheckIfCommandIsValid("rEsTaRt"));

            Assert.IsFalse(minesGame.CheckIfCommandIsValid("ress"));
        }

        [TestMethod]
        public void IsCommandExitValidTest()
        {
            var minesGame = new Mines();

            Assert.IsTrue(minesGame.CheckIfCommandIsValid("Exit"));
            Assert.IsTrue(minesGame.CheckIfCommandIsValid("exit"));
            Assert.IsTrue(minesGame.CheckIfCommandIsValid("eXiT"));

            Assert.IsFalse(minesGame.CheckIfCommandIsValid("exitt"));
        }

        [TestMethod]
        public void PrintInitialMessageTest()
        {
            var expected = @"Welcome to the game “Minesweeper”. Try to reveal all cells without mines. Use 'top' to view the scoreboard, 'restart' to start a new game and 'exit' to quit  the game.";

            var output = new StringBuilder();
            var textWriter = new StringWriter(output);
            Console.SetOut(textWriter);

            var minesGame = new Mines();
            minesGame.PrintInitialMessage();

            Assert.AreEqual(expected, output.ToString());
        }
    }
}
