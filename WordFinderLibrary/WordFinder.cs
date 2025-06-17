using System.Text;

namespace WordFinderLibrary;

public class WordFinder
{
    private readonly IEnumerable<string> _matrix;

    public WordFinder(IEnumerable<string> matrix)
    {
        _matrix = matrix;
    }

    public IEnumerable<string> Find(IEnumerable<string> wordstream)
    {
        int wordLength = _matrix.First().Length;

        var uniqueWordstream = wordstream.Where(w => w.Length<= wordLength && !string.IsNullOrEmpty(w)).Distinct().ToArray().AsSpan();
        var wordsFound = new List<string>();

        // iterate through each character ( row -> , column [n] )
        for (var rowIndex = 0; rowIndex < wordLength; rowIndex++)
        {
            for (var wordIndex = 0; wordIndex < wordLength; wordIndex++)
            {
                wordsFound.AddRange(WordFound(rowIndex, wordIndex, wordLength, uniqueWordstream));
            }
        }

        if (wordsFound.Count == 0)
        {
            return Enumerable.Empty<string>();  
        }

        var wordFoundHistogram = new Dictionary<string, int>();
        foreach (var word in wordsFound)
        {
            if (wordFoundHistogram.ContainsKey(word))
            {
                wordFoundHistogram[word]++;
            }
            else
            {
                wordFoundHistogram.Add(word, 1);
            }
        }

        return wordFoundHistogram.OrderByDescending(x => x.Value).Take(10).Select(x => x.Key);
    }

    private IEnumerable<string> WordFound(int rowIndex, int wordIndex, int wordLength, ReadOnlySpan<string> targetWords)
    {
        var result = new List<string>();
        result.AddRange(SearchHorizontal(rowIndex, wordIndex, wordLength, targetWords));
        result.AddRange(SearchVertical(rowIndex, wordIndex, wordLength, targetWords));

        return result;
    }

    private List<string> SearchHorizontal(int rowIndex, int wordIndex, int wordLength, ReadOnlySpan<string> targetWords)
    {
        ReadOnlySpan<char> wordTemp = _matrix.ElementAt(rowIndex).AsSpan();

        var result = new List<string>();
        for (int i = wordIndex, j = 1; i < wordLength; i++, j++)
        {
            foreach (var target in targetWords)
            {
                if (wordTemp.Slice(wordIndex, j).Equals(target, StringComparison.Ordinal))
                {
                    result.Add(target);
                }
            }
        }

        return result;
    }

    private List<string> SearchVertical(int rowIndex, int wordIndex, int wordLength, ReadOnlySpan<string> targetWords)
    {
        var result = new List<string>();
        var verticalWords = GetVerticalWord(rowIndex, wordIndex, wordLength);
        foreach (var vw in verticalWords)
        {
            if (targetWords.Contains(vw))
            {
                result.Add(vw);
            }
        }

        return result;
    }

    private IEnumerable<string> GetVerticalWord(int rowIndex, int charIndex, int wordLength)
    {

        StringBuilder builder = new StringBuilder();

        var verticals = new List<string>();

        for (int i = rowIndex, j = 0; i < wordLength; i++, j++)
        {
            builder.Append(_matrix.ElementAt(i)[charIndex]);
            verticals.Add(builder.ToString());
        }

        return verticals;
    }
}
