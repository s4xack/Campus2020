using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace Challenge
{
	[TestFixture]
	public class WordsStatistics_Tests
	{
		public virtual IWordsStatistics CreateStatistics()
		{
			// меняется на разные реализации при запуске exe
			return new WordsStatistics();
		}

		private IWordsStatistics wordsStatistics;

		[SetUp]
		public void SetUp()
		{
			wordsStatistics = CreateStatistics();
		}

		[Test]
		public void GetStatistics_IsEmpty_AfterCreation()
		{
			wordsStatistics.GetStatistics().Should().BeEmpty();
		}

		[Test]
		public void GetStatistics_ContainsItem_AfterAddition()
		{
			wordsStatistics.AddWord("abc");
			wordsStatistics.GetStatistics().Should().Equal(new WordCount("abc", 1));
		}

		[Test]
		public void GetStatistics_ContainsManyItems_AfterAdditionOfDifferentWords()
		{
			wordsStatistics.AddWord("abc");
			wordsStatistics.AddWord("def");
			wordsStatistics.GetStatistics().Should().HaveCount(2);
		}

		[Test]
		public void AddWord_GotNull_ThrowArgumentNullException()
		{
			Action a = () => wordsStatistics.AddWord(null);
			a.ShouldThrow<ArgumentNullException>();
		}

		[Test]
		public void AddWord_GotWhitespaces_CollectionShouldBeEmpty()
		{
			wordsStatistics.AddWord("   ");
			wordsStatistics.GetStatistics().Count().Should().Be(0);
		}

		[Test]
		public void AddWord_GotWordInUpperCase_ShouldContainWordInLowerCase()
		{
			wordsStatistics.AddWord("ABC");
			wordsStatistics.GetStatistics().All(w => w.Word == w.Word.ToLower()).Should().Be(true);
		}
		
		[Test]
		public void GetStatistics_OrderIsCorrect()
        { 
			wordsStatistics.AddWord("d");
			wordsStatistics.AddWord("b");
			wordsStatistics.AddWord("c");
			wordsStatistics.AddWord("c");
			wordsStatistics.AddWord("c");
			wordsStatistics.AddWord("b");
			wordsStatistics.AddWord("a");
			wordsStatistics.AddWord("a");
			wordsStatistics.AddWord("a");
            var r = wordsStatistics.GetStatistics();
			r.SequenceEqual(r.OrderByDescending(wordCount => wordCount.Count)
				.ThenBy(wordCount => wordCount.Word)).Should().BeTrue();
		}

		[Test]
		public void AddWord_SpacesBeginningWithDifSymbolsAtTheEnd_ShouldBeWhole()
		{
			wordsStatistics.AddWord("          a");
			wordsStatistics.AddWord("          b");
			wordsStatistics.GetStatistics().Should().Equal(new WordCount("          ", 2));
		}

		[Test]
		public void AddWord_SimilarBeginningBigAndTenSymbolsWord_ShouldBeWhole()
		{
			wordsStatistics.AddWord("         aa");
			wordsStatistics.AddWord("         a");
			wordsStatistics.GetStatistics().Should().Equal(new WordCount("         a", 2));
		}

		[Test, Timeout(250)]
		public void AddWord_TimeoutTest()
		{
			for (int i = 0; i < 500000; ++i)
			{
				wordsStatistics.AddWord("abcdef");
			}
		}




		
        [Test]
		public void AddWord_SimilarHashCodeWords_ShouldBeDifferent()
		{
			wordsStatistics.AddWord("xqzrbn");
			wordsStatistics.AddWord("krumld");
			wordsStatistics.GetStatistics().Count().Should().Be(2);
        }
		
		[Test]
		public void Test321()
		{
			wordsStatistics.AddWord("d");
			wordsStatistics.AddWord("b");
			wordsStatistics.AddWord("c");
			wordsStatistics.AddWord("c");
			wordsStatistics.AddWord("c");
			wordsStatistics.AddWord("b");
			wordsStatistics.AddWord("a");
			wordsStatistics.AddWord("a");
			wordsStatistics.AddWord("a");

			var first = wordsStatistics.GetStatistics().ToArray();
			var second = wordsStatistics.GetStatistics().ToArray();

			first.SequenceEqual(second).Should().BeTrue();
		}
		

        // Документация по FluentAssertions с примерами : https://github.com/fluentassertions/fluentassertions/wiki
    }
}