namespace mvc.Models;

public class FavoriteBooks
{
    public List<string> Library { get; set; }

    public FavoriteBooks()
    {
        // Create a Books instance to get all titles
        var allBooks = new Books();
        Library = new List<string>
            {
                allBooks.Titles[0]
            };
    }

    public void AddFavorite(string title)
    {
        if (!string.IsNullOrWhiteSpace(title) && !Library.Contains(title))
        {
            Library.Add(title);
        }
    }
}
