using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CraftBuddy.Common.EntityValidationConstants.Workshop;

namespace CraftBuddy.Data.Models
{
    public class Workshop
	{
        public Workshop()
        {
			this.Participants = new HashSet<WorkshopParticipant>();
			this.CreatedOn = DateTime.UtcNow;
			this.IsDeleted = false;
		}

        [Key]
        public int Id { get; set; }

		[Required]
		[MaxLength(TitleMaxLength)]
		public string Title { get; set; } = null!;

		[Required]
		[MaxLength(DescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Required]
		public DateTime StartDate { get; set; }

		[Required]
		public DateTime EndDate { get; set; }

        [Required]
        [ForeignKey(nameof(Organiser))]
        public Guid OrganiserId { get; set; }

        [Required]
        public ApplicationUser Organiser { get; set; } = null!;

        [Required]
		public int ParticipantsCount { get; set; }

		[Required]
		public string ImagePath { get; set; } = null!;

        [Required]
		public DateTime CreatedOn { get; set; }

		[Required]
		public bool IsDeleted { get; set; }

        public virtual ICollection<WorkshopParticipant> Participants { get; set; }
    }
}
