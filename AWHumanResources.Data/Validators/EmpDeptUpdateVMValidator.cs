using AWHumanResources.Data.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWHumanResources.Data.Validators
{
    public class EmpDeptUpdateVMValidator : AbstractValidator<EmpDeptUpdateVM>
    {
        public EmpDeptUpdateVMValidator()
        {
            RuleFor(vm => vm.BusinessEntityID)
                .NotNull().WithMessage("BusinessEntityID should not be null.")
                .GreaterThan(0).WithMessage("BusinessEntityID should be greater than 0.");

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
