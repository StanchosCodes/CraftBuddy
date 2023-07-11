namespace CraftBuddy.Common
{
	public static class EntityValidationConstants
	{
		public static class ApplicationUser
		{
			public const int EmailMaxLength = 60;
			public const int UserNameMaxLength = 20;
		}

		public static class Set
		{
			public const int NameMaxLength = 30;
			public const int NameMinLength = 3;

			public const int DescriptionMaxLength = 80;
			public const int DescriptionMinLength = 10;

			public const string PriceMinValue = "1";
			public const string PriceMaxValue = "1000";

			public const int ImagePathMaxLength = 200;
		}

		public static class Purchase
		{
			public const string AmountMinValue = "1";
			public const string AmountMaxValue = "10000";

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

			public const string PriceMinValue = "1";
			public const string PriceMaxValue = "1000";

            public const int ImagePathMaxLength = 200;
        }

		public static class OrderStatus
		{
			public const int NameMaxLength = 10;
			public const int NameMinLength = 5;
		}

		public static class Event
		{
			public const int NameMaxLength = 40;
			public const int NameMinLength = 10;

			public const int DescriptionMaxLength = 80;
			public const int DescriptionMinLength = 10;
		}

		public static class CustomOrder
		{
			public const int DescriptionMaxLength = 80;
			public const int DescriptionMinLength = 10;
		}
	}
}