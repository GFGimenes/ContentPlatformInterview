using FluentValidation;
using System.Text.Json.Serialization;

namespace ContentPlatformInterview.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ProductType { get; set; } = string.Empty;

        public float Price { get; set; }

        public int Quantity { get; set; }

        public float Size { get; set; }

        [JsonIgnore]
        public double FinalPrice { get; set; }
    }

    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(x => x.Id)
                .NotEqual(0)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.ProductType)
                .Must(x => x.Equals( "Food") || x.Equals("Drink") || x.Equals("Clothing"))
                .NotEmpty()
                .WithMessage("The ProductType field needs to be one of the types: Drink, Food or Clothing");

            RuleFor(x => x.Price)
                .NotEqual(0)
                .NotEmpty();

            RuleFor(x => x.Quantity)
                .NotEmpty();

            RuleFor(x => x.Size)
                .NotEmpty();
        }
    }
}
