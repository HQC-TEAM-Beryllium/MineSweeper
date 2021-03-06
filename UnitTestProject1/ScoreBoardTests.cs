﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeper;

namespace UnitTestProject1
{
    [TestClass]
    public class ScoreBoardTests
    {

        [TestMethod]
        public void AddPlayerScoreTest()
        {
            const string playerName = "Pesho";
            const int playerScore = 20;
            var scoreBoard = new ScoreBoard();
            scoreBoard.AddPlayer(playerName, playerScore);
            Assert.AreEqual(true, scoreBoard.scoreBoard.ContainsKey(playerScore));
        }

        [TestMethod]
        public void AddPlayerScoreAndPlayerNameTest()
        {
            const string playerName = "Pesho";
            const int playerScore = 20;

            var newScoreBoard = new ScoreBoard();
            newScoreBoard.AddPlayer(playerName, playerScore);

            Assert.AreEqual(true, newScoreBoard.scoreBoard.Contains(playerScore, playerName));
        }

        [TestMethod]
        public void AddEmptyPlayerNameTest()
        {
            var output = new StringBuilder();
            var textWriter = new StringWriter(output);
            Console.SetOut(textWriter);

            const string playerName = "";
            const int playerScore = 20;
            const string expected = "unknown";
            var newScoreBoard = new ScoreBoard();
            newScoreBoard.AddPlayer(playerName, playerScore);

            Assert.IsTrue(newScoreBoard.ScoreBoardd.Contains(20, "unknown"));
        }


        [TestMethod]
        [ExpectedException (typeof( ArgumentOutOfRangeException))]
        public void AddNegativePlayerScoreTest()
        {
            const string playerName = "Pesho";
            const int playerScore = -1;

            var newScoreBoard = new ScoreBoard();
            newScoreBoard.AddPlayer(playerName, playerScore);
        }

        [TestMethod]
        public void AddZeroPlayerScoreTest()
        {
            const string playerName = "Pesho";
            const int playerScore = 0;
            
            var newScoreBoard = new ScoreBoard();
            newScoreBoard.AddPlayer(playerName, playerScore);

            Assert.AreEqual(true, newScoreBoard.scoreBoard.ContainsKey(playerScore));
        }
        
        [TestMethod]
        public void PrintEmtyScoreBoardTest()
        {
            var output = new StringBuilder();
            var textWriter = new StringWriter(output);
            Console.SetOut(textWriter);

            var newScoreboard = new ScoreBoard();
            newScoreboard.PrintScoreBoard();

            string outputStr = output.ToString();

            Assert.AreEqual("\r\nScoreboard is empty!\r\n\r\n", outputStr);
        }

        [TestMethod]
        public void PrintOnePlayerInScoreBoardTest()
        {
            var output = new StringBuilder();
            var textWriter = new StringWriter(output);
            Console.SetOut(textWriter);

            var newScoreboard = new ScoreBoard();
            newScoreboard.AddPlayer("Pesho", 29);
            newScoreboard.PrintScoreBoard();

            string outputStr = output.ToString();

            Assert.AreEqual("Scoreboard:\r\n1. Pesho --> 29 cells\r\n\r\n", outputStr);
        }

        [TestMethod]
        public void PrintThreePlayersInScoreBoardTest()
        {
            var output = new StringBuilder();
            var textWriter = new StringWriter(output);
            Console.SetOut(textWriter);

            var newScoreboard = new ScoreBoard();
            newScoreboard.AddPlayer("Pesho", 29);
            newScoreboard.AddPlayer("Angel", 5);
            newScoreboard.AddPlayer("John", 35);
            newScoreboard.PrintScoreBoard();

            string outputStr = output.ToString();

            Assert.AreEqual("Scoreboard:\r\n1. John --> 35 cells\r\n2. Pesho --> 29 cells\r\n3. Angel --> 5 cells\r\n\r\n", outputStr);
        }

        [TestMethod]
        public void PrintFivePlayersInScoreBoardTest()
        {
            var output = new StringBuilder();
            var textWriter = new StringWriter(output);
            Console.SetOut(textWriter);

            var newScoreboard = new ScoreBoard();
            newScoreboard.AddPlayer("Pesho", 29);
            newScoreboard.AddPlayer("Angel", 5);
            newScoreboard.AddPlayer("John", 35);
            newScoreboard.AddPlayer("Mike", 4);
            newScoreboard.AddPlayer("Ben", 3);
            newScoreboard.PrintScoreBoard();

            string outputStr = output.ToString();

            Assert.AreEqual("Scoreboard:\r\n1. John --> 35 cells\r\n2. Pesho --> 29 cells\r\n3. Angel --> 5 cells\r\n4. Mike --> 4 cells\r\n5. Ben --> 3 cells\r\n\r\n", outputStr);
        }


        [TestMethod]
        public void PrintSixPlayersInScoreBoardTest()
        {
            var output = new StringBuilder();
            var textWriter = new StringWriter(output);
            Console.SetOut(textWriter);

            var newScoreboard = new ScoreBoard();
            newScoreboard.AddPlayer("Pesho", 29);
            newScoreboard.AddPlayer("Angel", 5);
            newScoreboard.AddPlayer("John", 35);
            newScoreboard.AddPlayer("Mike", 4);
            newScoreboard.AddPlayer("Ben", 3);
            newScoreboard.AddPlayer("Hidden", 1);
            newScoreboard.PrintScoreBoard();

            string outputStr = output.ToString();

            Assert.AreEqual("Scoreboard:\r\n1. John --> 35 cells\r\n2. Pesho --> 29 cells\r\n3. Angel --> 5 cells\r\n4. Mike --> 4 cells\r\n5. Ben --> 3 cells\r\n\r\n", outputStr);
        }
    }
}