using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GamersRising.Entities
{
    public class Game
    {
        public int Id { get; set; }

        [ForeignKey("GameId")]
        public virtual ICollection<Tournament>? Tournaments { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }

        [Display(Name = "Active Tournaments")]
        public int? ActiveTourneys { get; set; }

        //[Required]
        //[Display(Name = "Thumbnail Image Path")]
        //public string ThumbnailImagePath { get; set; }

        

        [ForeignKey("GameId")]
        public virtual ICollection<UserCatagory>? UserCategory { get; set; }
    }
}
