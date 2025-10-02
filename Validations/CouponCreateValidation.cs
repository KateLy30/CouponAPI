using FluentValidation;
using CouponAPI.Models.DTO;
namespace CouponAPI.Validations;
    public class CouponCreateValidation : AbstractValidator<CouponCreateDTO>
    {
        public CouponCreateValidation() 
        {
            RuleFor(model => model.Name).NotEmpty();
            RuleFor(model => model.Percent).InclusiveBetween(1, 100);
        }

    }
