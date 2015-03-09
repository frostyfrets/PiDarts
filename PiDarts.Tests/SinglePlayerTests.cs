using System;
using PiDarts.Core;
using PiDarts.Test;
using Microsoft.Xna.Framework;
using PiDarts.Core.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PiDarts.Tests.DartboardSequences;

namespace PiDarts.Tests
{
    [TestClass]
    public class SinglePlayerTests
    {

        [TestMethod]
        public void TestGameEndOnScoreEqualsZero()
        {
            DartThrowSequence ds = new Perfect301Sequence();
            DartGameManager dgm = new DartGameManager(new GraphicsDeviceManager(new Game()),new DartThrowSequenceSimulator(ds));
            dgm.SetUpNewGame(GameSelection.Game301, 1);
            try
            {
                while (true)
                {
                    dgm.UpdateState();
                }
            }
            catch (DartEndOfSequenceException e) {
                Assert.AreEqual(DartGame.state,GameState.GAME_OVER);
            }
        }

        [TestMethod]
        public void TestBustOnScoreLessThanZero()
        {
            DartThrowSequence ds = new Bust301Sequence();
            DartGameManager dgm = new DartGameManager(new GraphicsDeviceManager(new Game()), new DartThrowSequenceSimulator(ds));
            dgm.SetUpNewGame(GameSelection.Game301, 1);
            try
            {
                while (true)
                {
                    dgm.UpdateState();
                }
            }
            catch (DartEndOfSequenceException e)
            {
                Assert.AreEqual(DartGame.state, GameState.BUST_TURN_END);
            }
        }

        [TestMethod]
        public void TestTurnEndAfterThreeThrows()
        {
            DartThrowSequence ds = new TurnEnd301Sequence();
            DartGameManager dgm = new DartGameManager(new GraphicsDeviceManager(new Game()), new DartThrowSequenceSimulator(ds));
            dgm.SetUpNewGame(GameSelection.Game301, 1);
            try
            {
                while (true)
                {
                    dgm.UpdateState();
                }
            }
            catch (DartEndOfSequenceException e)
            {
                Assert.AreEqual(DartGame.state, GameState.OK_TURN_END);
            }
        }

        [TestMethod]
        public void TestTurnOKStateAfter1Throw()
        {
            DartThrowSequence ds = new TurnOK1ThrowSequence();
            DartGameManager dgm = new DartGameManager(new GraphicsDeviceManager(new Game()), new DartThrowSequenceSimulator(ds));
            dgm.SetUpNewGame(GameSelection.Game301, 1);
            try
            {
                while (true)
                {
                    dgm.UpdateState();
                }
            }
            catch (DartEndOfSequenceException e)
            {
                Assert.AreEqual(DartGame.state, GameState.OK);
            }
        }

        [TestMethod]
        public void TestNoThrowsGameStart()
        {
            DartThrowSequence ds = new NoThrowDartSequence();
            DartGameManager dgm = new DartGameManager(new GraphicsDeviceManager(new Game()), new DartThrowSequenceSimulator(ds));
            dgm.SetUpNewGame(GameSelection.Game301, 1);
            Assert.AreEqual(DartGame.state, GameState.GAME_START);
            
        }
    
    }
}
