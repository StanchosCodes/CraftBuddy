namespace CraftBuddy.Common
{
	public static class EntityValidationConstants
	{
		public static class ApplicationUser
		{
			public const int EmailMaxLength = 60;
			public const int EmailMinLength = 20;

			public const int UserNameMaxLength = 20;
			public const int UserNameMinLength = 4;

			public const int PasswordMaxLength = 100;
			public const int PasswordMinLength = 6;
		}

		public static class Article
		{
			public const int TitleMaxLength = 40;
			public const int TitleMinLength = 10;

			public const int DescriptionMaxLength = 80;
			public const int DescriptionMinLength = 10;
		}

		public static class Order
		{
            public const string AmountMinValue = "0";
			public const string AmountMaxValue = "1000";

			public const int AddressMaxLength = 60;
			public const int AddressMinLength = 5;
		}

		public static class ProductType
		{
			public const int NameMaxLength = 30;
			public const int NameMinLength = 3;
		}

		public static class Product
		{
			public const int DescriptionMaxLength = 80;
			public const int DescriptionMinLength = 10;

			public const string PriceMinValue = "0";
			public const string PriceMaxValue = "500";

            public const int ImagePathMaxLength = 200;
        }

		public static class OrderStatus
		{
			public const int NameMaxLength = 10;
			public const int NameMinLength = 5;
		}

		public static class Workshop
		{
			public const int TitleMaxLength = 40;
			public const int TitleMinLength = 10;

			public const int DescriptionMaxLength = 80;
			public const int DescriptionMinLength = 10;
		}
	}
}