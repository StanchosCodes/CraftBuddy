namespace CraftBuddy.Web.ViewModels.Workshop
{
    public class WorkshopViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string StartDate { get; set; } = null!;

        public string Organiser { get; set; } = null!;

        public int ParticipantsCount { get; set; }

        public string ImagePath { get; set; } = null!;
    }
}
