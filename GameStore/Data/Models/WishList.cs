namespace GameStore.Models
{
    public class WishList
    {
        public int Id { get; set; }

        // Foreign key to User
        public Guid UserId { get; set; }
        public User? User { get; set; }

        // Foreign key to Game
        public int GameId { get; set; }
        public Game? Game { get; set; }

        public int WishCount { get; set; }
    }
}
