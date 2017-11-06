using System;
using System.Collections.Generic;
using System.Linq;

namespace Challenge.Solved
{
	public class WordsStatistics : IWordsStatistics
	{
		protected readonly IDictionary<string, int> statistics 
			= new Dictionary<string, int>();

		public virtual void AddWord(string word)
		{
			if (word == null) throw new ArgumentNullException(nameof(word));
			if (string.IsNullOrWhiteSpace(word)) return;
			if (word.Length > 10)
				word = word.Substring(0, 10);
			int count;
			statistics[word.ToLower()] = 1 + (statistics.TryGetValue(word.ToLower(), out count) ? count : 0);
		}

		public virtual IEnumerable<Tuple<int, string>> GetStatistics()
		{
			return statistics.OrderByDescending(kv => kv.Value)
				.ThenBy(kv => kv.Key)
				.Select(kv => Tuple.Create(kv.Value, kv.Key));
		}
	}
}