using static CraftBuddy.Common.ImagePathConstants.ImagePath;

namespace CraftBuddy.Services.Data.Utilities
{
    public static class ServiceUtilities
    {
        public static string ChangeImagePath(int productTypeId)
        {
            string imagePath = string.Empty;

            switch (productTypeId)
            {
                case 1:
                    {
                        imagePath = HatImagePath;
                    }
                    break;
                case 2:
                    {
                        imagePath = BannerImagePath;
                    }
                    break;
                case 3:
                    {
                        imagePath = TopperImagePath;
                    }
                    break;
                case 4:
                    {
                        imagePath = FlagImagePath;
                    }
                    break;
            }

            return imagePath;
        }
    }
}
