using CraftBuddy.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CraftBuddy.Data.Configurations
{
	public class ProductTypeEntityConfiguration : IEntityTypeConfiguration<ProductType>
	{
		public void Configure(EntityTypeBuilder<ProductType> builder)
		{
			builder.HasData(this.GenerateProductTypes());
		}

		private ProductType[] GenerateProductTypes()
		{
			ICollection<ProductType> productTypes = new HashSet<ProductType>();

			ProductType productType;

			productType = new ProductType()
			{
				Id = 1,
				Name = "Hat"
			};

			productTypes.Add(productType);

			productType = new ProductType()
			{
				Id = 2,
				Name = "Banner"
			};

			productTypes.Add(productType);

			productType = new ProductType()
			{
				Id = 3,
				Name = "Topper"
			};

			productTypes.Add(productType);

			productType = new ProductType()
			{
				Id = 4,
				Name = "Flag"
			};

			productTypes.Add(productType);

			return productTypes.ToArray();
		}
	}
}
