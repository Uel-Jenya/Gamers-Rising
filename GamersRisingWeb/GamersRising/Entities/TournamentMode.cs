using GamersRising.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamersRising.Entities
{
    public class TournamentMode: IPrimaryProperties
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Thumbnail Image Path")]
        public string ThumbnailImagePath { get; set; }

        [ForeignKey("TournamentModeId")]
        public virtual ICollection<Tournament>? Tournaments { get; set; }

    }
}
