using System;
using Wintellect.PowerCollections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MineSweeper;

namespace Minesweeper
{
    [TestClass]
    public class ScoreBoardTests
    {
        [TestMethod]
        public void ScoreBoardTestConstructor()
        {
            ScoreBoard scoreBoard = new ScoreBoard();
            Assert.IsNotNull(scoreBoard);
        }

        [TestMethod]
        public void AddPlayerTest()
        {
            string playerName = "Dragan";
            int playerScore = 1;
            var scoreBoard = new ScoreBoard();
            scoreBoard.AddPlayer(playerName, playerScore);
            Assert.AreEqual(true, scoreBoard.scoreBoard.ContainsKey(playerScore));
        }

        [TestMethod]
        public void PrintScoreBoardTest()
        {
            string playerName = "Dragan";
            int playerScore = 1;
            OrderedMultiDictionary<int, string> scoreBoard = new OrderedMultiDictionary<int, string>(true);
            scoreBoard.Add(playerScore, playerName);
            Assert.IsNotNull(scoreBoard.Values.Count);
            Assert.AreEqual(true, scoreBoard.ContainsKey(playerScore));
            Assert.AreEqual("{" + playerName + "}", scoreBoard.Values.ToString());
            Assert.IsTrue(scoreBoard.Values.Count == 1);
        }
    }
}
