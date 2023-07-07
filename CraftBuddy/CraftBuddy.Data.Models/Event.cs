using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static CraftBuddy.Common.EntityValidationConstants.Event;

namespace CraftBuddy.Data.Models
{
	public class Event
	{
        public Event()
        {
			this.UserEvents = new HashSet<UserEvent>();
			this.CreatedOn = DateTime.UtcNow;
			this.IsDeleted = false;
		}

        [Key]
        public int Id { get; set; }

		[Required]
		[MaxLength(NameMaxLength)]
		public string Name { get; set; } = null!;

		[Required]
		[MaxLength(DescriptionMaxLength)]
		public string Description { get; set; } = null!;

		[Required]
		public DateTime StartDate { get; set; }

		[Required]
		public DateTime EndDate { get; set; }

        [Required]
		public int ParticipantsCount { get; set; }

        [Required]
		public DateTime CreatedOn { get; set; }

		[Required]
		public bool IsDeleted { get; set; }

        public virtual ICollection<UserEvent> UserEvents { get; set; }
    }
}
