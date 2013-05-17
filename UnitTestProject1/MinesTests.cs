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
        public void HasMineInSideTest()
        {
            var minesGame = new Mines();
            string[,] minesMatrix = new string[,] { { "*", "*", "*" }, { "*", "", "*" }, { "*", "*", "*" } };

            Assert.IsFalse(minesGame.HasMine(minesMatrix, 1, 1));

        }

        [TestMethod]
        public void HasMineOnBoundryTest()
        {
            var minesGame = new Mines();
            string[,] minesMatrix = new string[,] { { "*", "*", "*" }, { "*", "", "*" }, { "*", "*", "*" } };

            Assert.IsTrue(minesGame.HasMine(minesMatrix, 0, 0));

        }

        [TestMethod]
        public void IsAlreadyOpenTestEmpty()
        {
            var minesGame = new Mines();
            string[,] minesMatrix = new string[,] { { "", "*", "" }, { "*", "", "" }, { "*", "*", "*" } };

            minesGame.IsAlreadyOpen(minesMatrix, 1, 1);
            Assert.IsFalse(minesGame.IsAlreadyOpen(minesMatrix, 1, 1));

        }

        [TestMethod]
        public void IsAlreadyOpenTestMines()
        {
            var minesGame = new Mines();
            string[,] minesMatrix = new string[,] { { "", "*", "" }, { "*", "*", "" }, { "*", "*", "*" } };

            minesGame.IsAlreadyOpen(minesMatrix, 1, 1);
            Assert.IsFalse(minesGame.IsAlreadyOpen(minesMatrix, 1, 1));

        }

        [TestMethod]
        public void IsAlreadyOpenTestOpen()
        {
            var minesGame = new Mines();
            string[,] minesMatrix = new string[,] { { "", "*", "" }, { "*", "4", "" }, { "*", "*", "*" } };

            minesGame.IsAlreadyOpen(minesMatrix, 1, 1);
            Assert.IsTrue(minesGame.IsAlreadyOpen(minesMatrix, 1, 1));

        }
    }
}
