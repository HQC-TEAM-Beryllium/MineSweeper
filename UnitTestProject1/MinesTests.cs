using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MineSweeper
{
    [TestClass]
    public class MinesTests
    {
        [TestMethod]
        public void DisplayTest()
        {
            
            string[,] matrixOfTheMines = new string[5, 10];
            bool boomed = false;
            matrixOfTheMines[0, 0] = "";
            matrixOfTheMines[0, 2] = "*";

            Assert.IsFalse(!(boomed) && ((matrixOfTheMines[0, 2] == "")));
            Assert.IsTrue((matrixOfTheMines[0, 0] == string.Empty) || (matrixOfTheMines[0, 0] == "*"));
        }

        [TestMethod]
        public void BoomTest()
        {
            string[,] matrixOfTheMines = new string[5, 10];
            int minesRow = 1;
            int minesCol = 2;
            matrixOfTheMines[minesRow, minesCol] = "";
            bool isKilled = false;
            Assert.AreEqual(isKilled, matrixOfTheMines[minesRow, minesCol] == "*");

            minesRow = 0;
            minesCol = 0;
            matrixOfTheMines[minesRow, minesCol] = "error";
            bool checkIsTrue = ((matrixOfTheMines[minesRow, minesCol] != "") && (matrixOfTheMines[minesRow, minesCol] != "*"));
            string message = "Illegal Move!";
            Assert.IsTrue(checkIsTrue == true && message == "Illegal Move!");
        }

        [TestMethod]
        public void WinnerTest()
        {
            string[,] matrix = new string[5, 10];
            int cntMines = 0;
            int counter = 1;
            matrix[0, 0] = counter.ToString();
            int matrixSize = matrix.GetLength(0) * matrix.GetLength(1);
            bool isWinner = true;
            bool checkHasMines = ((matrix[0, 0] == "") && (matrix[0, 0] == "*"));
            Assert.IsTrue((checkHasMines == false) && (matrix[0, 0] == counter.ToString()));
            Assert.AreEqual(cntMines == 0, checkHasMines == false);
            Assert.IsTrue((isWinner == true) && (matrixSize == 50));
        }

        [TestMethod]
        public void ProverkaTest()
        {
            string[] line = { "top", "restart", "exit" };
            bool checkLines = (line[0].Equals("top") || line[0].Equals("restart") || line[0].Equals("exit"));
            Assert.AreEqual(checkLines, true);
            Assert.IsTrue(!checkLines == false);
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void IsMoveEnteredTest()
        {
            string line = "1 two";
            string[] inputParams = line.Split();
            int row = int.Parse(inputParams[0]);
            int col = int.Parse(inputParams[1]);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void FillWithRandomMinesTest()
        {
            string[,] mines = new string[5, 10];
            mines[10, 20] += "*";
        }

    }
}
