using System;
using System.IO;
using NUnit.Framework;

namespace Chess
{
    [TestFixture]
    public class ChessProblem_Test
    {
        private static ChessProblem chessProblem;

        [SetUp]
        public void SetUp()
        {
            chessProblem = new ChessProblem();
        }

        [Test]
        public void RepeatedMethodCallDoNotChangeBehaviour()
        {
            var boardLines = new[]
            {
                "        ",
                "        ",
                "        ",
                "   q    ",
                "    K   ",
                " Q      ",
                "        ",
                "        ",
            };
            chessProblem.LoadFrom(new BoardParser().ParseBoard(boardLines));
            chessProblem.CalculateChessStatus(PieceColor.White);
            Assert.AreEqual(ChessStatus.Check, chessProblem.ChessStatus);

            // Now check that internal board modifications during the first call do not change answer
            chessProblem.CalculateChessStatus(PieceColor.White);
            Assert.AreEqual(ChessStatus.Check, chessProblem.ChessStatus);
        }

        [Test]
        public void AllTests()
        {
            var dir = TestContext.CurrentContext.TestDirectory;
            var testsCount = 0;
            foreach (var filename in Directory.GetFiles(Path.Combine(dir, "ChessTests"), "*.in"))
            {
                TestOnFile(filename);
                testsCount++;
            }
            Console.WriteLine("Tests passed: " + testsCount);
        }

        private static void TestOnFile(string filename)
        {
            var boardLines = File.ReadAllLines(filename);
            chessProblem.LoadFrom(new BoardParser().ParseBoard(boardLines));
            var expectedAnswer = File.ReadAllText(Path.ChangeExtension(filename, ".ans")).Trim();
            chessProblem.CalculateChessStatus(PieceColor.White);
            Assert.AreEqual(expectedAnswer, chessProblem.ChessStatus.ToString().ToLower(), "Failed test " + filename);
        }
    }
}