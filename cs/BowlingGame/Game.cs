using System;
using BowlingGame.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace BowlingGame
{
    public class Game
    {
        private int score;
        private int frameSum;

        private int frameCount;

        bool newFrame;

        bool spare;
        int strikeCount;
        bool doubleStrike;

        public void Roll(int pins)
        {
            if(frameCount == 10)
            {
                throw new NotSupportedException();
            }
            UpdateScore(pins);

            UpdateFrame();
        }

        private void UpdateScore(int pins)
        {
            CountSpareBonus(pins);
            CountStrikeeBonus(pins);
            frameSum += pins;
            score += pins;
        }

        private void CountSpareBonus(int pins)
        {
            if (spare)
            {
                score += pins;
                spare = false;
            }
        }

        private void CountStrikeeBonus(int pins)
        {
            if (strikeCount > 0)
            {
                if (doubleStrike)
                {
                    doubleStrike = false;
                    score += pins;
                }
                score += pins;
                strikeCount--;
            }
        }

        private  void UpdateFrame()
        {
            if (frameSum > 10)
            {
                throw new ArgumentException("too many");
            }

            CheckSpare();
            CheckStrike();

            newFrame = !newFrame;
            if (frameSum == 10)
            {
                newFrame = true;
            }
            if(newFrame)
            {
                frameCount++;
                frameSum = 0;
            }
        }

        private void CheckSpare()
        {
            if (!newFrame && frameSum == 10)
            {
                spare = true;
            }
        }

        private void CheckStrike()
        {
            if (newFrame && frameSum == 10)
            {
                if (strikeCount > 0)
                    doubleStrike = true;
                strikeCount = 2;
            }
        }

        public int GetScore()
        {
            return score;
            
        }
        public Game()
        {
            frameCount = 0;
            score = 0;
            frameSum = 0;
            newFrame = true;
            spare = false;
            strikeCount = 0;
            doubleStrike = false;
        }
    }

    [TestFixture]
    public class Game_should : ReportingTest<Game_should>
    {
        private Game game;
        [SetUp]
        public void OneTimeSetup()
        {
             game = new Game();
        }
        [Test]
        public void HaveZeroScore_BeforeAnyRolls()
        {
            game
                .GetScore()
                .Should().Be(0);
        }

        [Test]
        public void ScoreOfOneRoll()
        {

            game.Roll(2);
            game.GetScore().Should().Be(2);
        }
        
        [Test]
        public void CorrectScoreOfOneFrame()
        {
            game.Roll(2);
            Action a = () => game.Roll(9);
            a.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void ThreeRollsWithScoreBiggerThenTen_DontThrowException()
        {
            game.Roll(3);
            game.Roll(4);
            game.Roll(4);
            game.GetScore().Should().Be(11);
        }
        [Test]
        public void SpareAndOneRoll()
        {
            game.Roll(7);
            game.Roll(3);
            game.Roll(5);
            game.GetScore().Should().Be(20);
        }
        [Test]
        public void StrikeAndOneRoll()
        {
            game.Roll(10);
            game.Roll(3);
            game.Roll(5);
            game.GetScore().Should().Be(26);
        }

        [Test]
        public void TripleStrike()
        {
            game.Roll(10);
            game.Roll(10);
            game.Roll(10);
            game.GetScore().Should().Be(60);
        }
        [Test]
        public void RollAutoFrames()
        {
            for(int i = 0; i < 9; i++)
            {
                game.Roll(10);
            }
            game.Roll(0);
            game.Roll(0);
            Action a = () => game.Roll(1);
            a.ShouldThrow<NotSupportedException>();
        
        }
        /*[Test]
        public void LastFrameStrike()
        {

        }*/
    }
}
