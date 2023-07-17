using System.ComponentModel.DataAnnotations;
using static CraftBuddy.Common.EntityValidationConstants.Workshop;

namespace CraftBuddy.Web.ViewModels.Workshop
{
    public class AddEditWorkshopViewModel
    {
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
