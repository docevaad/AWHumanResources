using AWHumanResources.Data.ViewModels;
using FluentValidation;

namespace AWHumanResources.Data.Validators
{
    public class EmpPayUpdateRequestValidator : AbstractValidator<EmpPayUpdateRequest>
    {
        public EmpPayUpdateRequestValidator()
        {
            RuleFor(vm => vm.Rate)
                .NotNull().WithMessage("Rate should not be null.")
                .InclusiveBetween((decimal)6.50, (decimal)200.00).WithMessage("Rate must be between 6.5 and 200.0 inclusive.");

            RuleFor(vm => vm.PayFrequency)
                .NotNull().WithMessage("Pay frequency should not be null.")
                .InclusiveBetween((byte) 1,(byte) 3).WithMessage("Rate must be between 1 and 3 inclusive.");

            RuleFor(vm => vm.RateChangeDate)
                .NotNull().WithMessage("RateChangeDate should not be null.");
        }
    }
}
