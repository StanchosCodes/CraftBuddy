namespace CraftBuddy.Web.ViewModels.Workshop
{
    public class WorkshopDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public string StartDate { get; set; } = null!;

        public string EndDate { get; set; } = null!;

        public string Organiser { get; set; } = null!;

        public int ParticipantsCount { get; set; }

        public string ImagePath { get; set; } = null!;

        public string CreatedOn { get; set; } = null!;
    }
}
