namespace mvc.Models
{
    public class Books
    {
        public string[] Titles { get; set; }

        public Books()
        {
            Titles = new[]
            {
                "Clean Code",
                "The Pragmatic Programmer",
                "Design Patterns: Elements of Reusable Object-Oriented Software",
                "Refactoring: Improving the Design of Existing Code",
                "Domain-Driven Design",
                "Head First Design Patterns",
                "Working Effectively with Legacy Code",
                "You Don’t Know JS",
                "CLR via C#",
                "Pro ASP.NET Core MVC"
            };
        }
    }
}
