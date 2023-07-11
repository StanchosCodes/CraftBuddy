using CraftBuddy.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CraftBuddy.Common.EntityValidationConstants.Product;

namespace CraftBuddy.Web.ViewModels.Product
{
    public class AddEditProductViewModel
    {
        public AddEditProductViewModel()
        {
            this.Types = new HashSet<ProductTypeViewModel>();
        }

        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Required]
        [Range(typeof(decimal), PriceMinValue, PriceMaxValue)]
        public decimal Price { get; set; }

        public int TypeId { get; set; }

        public virtual IEnumerable<ProductTypeViewModel> Types { get; set; }
    }
}
