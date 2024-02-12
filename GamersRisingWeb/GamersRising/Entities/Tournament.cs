using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GamersRising.Entities
{
    public class Tournament
    {
        private DateTime _releaseDate = DateTime.MinValue;

        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Title { get; set; }

        public string Description { get; set; }


        public int GameId { get; set; }
        public Game? Game { get; set; }


        //[Required(ErrorMessage = "Please select a valid item from the '{0}' dropdown list")]
        [Display(Name = "Tournament Format")]

        public int TournamentFormatId { get; set; }

        public TournamentFormat? TournamentFormat { get; set; }
        [NotMapped]
        public virtual ICollection<SelectListItem>? TournamentFormats { get; set; }

        //[Required(ErrorMessage = "Please select a valid item from the '{0}' dropdown list")]
        [Display(Name = "Tournament Mode")]

        public int TournamentModeId { get; set; }

        public TournamentMode? TournamentMode { get; set; }

        [NotMapped]
        public virtual ICollection<SelectListItem>? TournamentModes { get; set; }

        //[Required(ErrorMessage = "Please select a valid item from the '{0}' dropdown list")]
        [Display(Name = "Tournament Type")]

        public int TournamentTypeId { get; set; }
        public TournamentType? TournamentType { get; set; }

        [NotMapped]
        public virtual ICollection<SelectListItem>? TournamentTypes { get; set; }

        [Display(Name = "Start Time")]
        public string StartTime { get; set; }


        [Display(Name = "End Time")]
        public string EndTime { get; set; }

        [NotMapped]
        public int ContentId { get; set; }

        //public Content Content { get; set; }

    }
}
