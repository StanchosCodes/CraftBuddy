using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CraftBuddy.Data.Models
{
	public class UserEvent
	{
		[Required]
		[ForeignKey(nameof(Organiser))]
		public Guid OrganiserId { get; set; }

		[Required]
		public ApplicationUser Organiser { get; set; } = null!;

		[Required]
		[ForeignKey(nameof(Event))]
		public int EventId { get; set; }

		[Required]
        public Event Event { get; set; } = null!;
	}
}
