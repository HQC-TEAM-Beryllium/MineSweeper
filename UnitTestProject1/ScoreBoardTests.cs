using System;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeper;

namespace Minesweeper
{
    [TestClass]
    public class ScoreBoardTests
    {

        [TestMethod]
        public void AddPlayerScoreTest()
        {
            string playerName = "Pesho";
            int playerScore = 20;
            var scoreBoard = new ScoreBoard();
            scoreBoard.AddPlayer(playerName, playerScore);
            Assert.AreEqual(true, scoreBoard.scoreBoard.ContainsKey(playerScore));
        }

        [TestMethod]
        public void AddPlayerScoreAndPlayerNameTest()
        {
            string playerName = "Pesho";
            int playerScore = 20;

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

            string playerName = "";
            int playerScore = 20;
            var newScoreBoard = new ScoreBoard();
            newScoreBoard.AddPlayer(playerName, playerScore);

            string outputStr = output.ToString();

            Assert.AreEqual("Enter your name, please\r\n", outputStr);
        }

        [TestMethod]
        [ExpectedException (typeof( ArgumentOutOfRangeException))]
        public void AddNegativePlayerScoreTest()
        {
            string playerName = "Pesho";
            int playerScore = -1;

            var newScoreBoard = new ScoreBoard();
            newScoreBoard.AddPlayer(playerName, playerScore);
        }

        [TestMethod]
        public void AddZeroPlayerScoreTest()
        {
            string playerName = "Pesho";
            int playerScore = 0;
            
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