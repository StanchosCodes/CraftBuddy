using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CraftBuddy.Data.Models
{
	public class WorkshopParticipant
	{
		[Required]
		[ForeignKey(nameof(Participant))]
		public Guid ParticipantId { get; set; }

		[Required]
		public ApplicationUser Participant { get; set; } = null!;

		[Required]
		[ForeignKey(nameof(Workshop))]
		public int WorkshopId { get; set; }

		[Required]
        public Workshop Workshop { get; set; } = null!;
	}
}
