using AWHumanResources.Data.ViewModels;
using FluentValidation;

namespace AWHumanResources.Data.Validators
{
    /// <summary>
    /// Encapsulates validation for <see cref="EmpDeptUpdateRequest"/>
    /// </summary>
    /// <seealso cref="FluentValidation.AbstractValidator{EmpDeptUpdateRequest}" />
    public class EmpDeptUpdateRequestValidator : AbstractValidator<EmpDeptUpdateRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmpDeptUpdateRequestValidator"/> class.
        /// </summary>
        public EmpDeptUpdateRequestValidator()
        {
            RuleFor(vm => vm.DepartmentID)
                .NotNull().WithMessage("DepartmentID should not be null.")
                .GreaterThan(0).WithMessage("DepartmentID should be greater than 0.");

            RuleFor(vm => vm.CurrentDeptEndDate)
                .NotNull().WithMessage("CurrentDeptEndDate should not be null.");

            RuleFor(vm => vm.NewDeptStartDate)
                .NotNull().WithMessage("NewDeptStartDate should not be null.");
        }
    }
}
