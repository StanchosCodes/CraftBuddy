using static CraftBuddy.Common.ImagePathConstants.ImagePath;

namespace CraftBuddy.Services.Data.Utilities
{
	public static class ServiceUtilities
	{
		public static string ChangeImagePath(int productTypeId, bool isCustom)
		{
			string imagePath = string.Empty;

			switch (productTypeId)
			{
				case 1:
					{
						if (isCustom)
						{
							imagePath = CustomHatImagePath;
						}
						else
						{
							imagePath = HatImagePath;
						}
					}
					break;
				case 2:
					{
						if (isCustom)
						{
							imagePath = CustomBannerImagePath;
						}
						else
						{
							imagePath = BannerImagePath;
						}
					}
					break;
				case 3:
					{
						if (isCustom)
						{
							imagePath = CustomTopperImagePath;
						}
						else
						{
							imagePath = TopperImagePath;
						}
					}
					break;
				case 4:
					{
						if (isCustom)
						{
							imagePath = CustomFlagImagePath;
						}
						else
						{
							imagePath = FlagImagePath;
						}
					}
					break;
			}

			return imagePath;
		}
	}
}
