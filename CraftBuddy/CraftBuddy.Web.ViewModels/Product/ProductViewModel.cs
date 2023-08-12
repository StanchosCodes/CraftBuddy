using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static CraftBuddy.Common.EntityValidationConstants.Product;

namespace CraftBuddy.Web.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Type { get; set; } = null!;

        public string Crafter { get; set; } = null!;

        public decimal Price { get; set; }

        public string ImagePath { get; set; } = null!;
    }
}
