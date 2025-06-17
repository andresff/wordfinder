namespace WordFinder.Test;

public class WordFinderTest
{
    private WordFinderLibrary.WordFinder _finder;

    public WordFinderTest() 
    {
        List<string> matrix = new();
        matrix.Add("chill");
        matrix.Add("acwdo");
        matrix.Add("chill");
        matrix.Add("alndd");
        matrix.Add("alddc");

        _finder = new WordFinderLibrary.WordFinder(matrix);        
    }

    [Fact]
    public void WithTwoWordsFound_ReturnTwoResult()
    {
        List<string> wordStream = new List<string>() {"chill", "cold", "word", "wind"};

        var result = _finder.Find(wordStream);

        Assert.Equal(2, result.Count());
        Assert.Equal("chill", result.ElementAt(0));
        Assert.Equal("wind", result.ElementAt(1));
    }

    [Fact]
    public void WithRepeatedWordStrem_ReturnTwoResult()
    {
        List<string> wordStream = new List<string>() { "chill", "test", "chill", "cold", "word", "wind" };

        var result = _finder.Find(wordStream);

        Assert.Equal(2, result.Count());
        Assert.Equal("chill", result.ElementAt(0));
        Assert.Equal("wind", result.ElementAt(1));
    }

    [Fact]
    public void WithThreeWordsFound_ReturnThreeResult()
    {
        List<string> wordStream = new List<string>() { "chill", "cold", "word", "win", "wind" };

        var result = _finder.Find(wordStream);

        Assert.Equal(3, result.Count());
        Assert.Equal("chill", result.ElementAt(0));
        Assert.Equal("win", result.ElementAt(1));
        Assert.Equal("wind", result.ElementAt(2));
    }

    [Fact]

    public void WithNoWordFound_ReturnEmptyResult()
    {
        List<string> wordStream = new List<string>() { "chilld", "coldd", "wordd", "windd" };
        
        var result = _finder.Find(wordStream);

        Assert.Empty(result);
    }
}

