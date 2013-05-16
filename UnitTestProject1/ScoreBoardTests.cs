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
            Assert.AreEqual("Enter your name, please", newScoreBoard.scoreBoard.Contains(playerScore, playerName));
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
        //[TestMethod]
        //public void PrintScoreBoardTest()
        //{
        //    string playerName = "Dragan";
        //    int playerScore = 1;
        //    OrderedMultiDictionary<int, string> scoreBoard = new OrderedMultiDictionary<int, string>(true);
        //    scoreBoard.Add(playerScore, playerName);
        //    Assert.IsNotNull(scoreBoard.Values.Count);
        //    Assert.AreEqual(true, scoreBoard.ContainsKey(playerScore));
        //    Assert.AreEqual("{" + playerName + "}", scoreBoard.Values.ToString());
        //    Assert.IsTrue(scoreBoard.Values.Count == 1);
        //}
    }
}
