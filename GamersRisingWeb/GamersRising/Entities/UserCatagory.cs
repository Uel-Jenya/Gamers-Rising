using GamersRising.Data;

namespace GamersRising.Entities
{
    public class UserCatagory
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationDbContext ApplicationDbContext { get; set; }

        public int GameId { get; set; }
        public Game? Game { get; set; }
    }
}
