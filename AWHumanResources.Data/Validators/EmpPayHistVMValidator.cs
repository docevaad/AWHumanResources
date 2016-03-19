using AWHumanResources.Data.ViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWHumanResources.Data.Validators
{
    public class EmpPayUpdateVMValidator : AbstractValidator<EmpPayUpdateVM>
    {
        public EmpPayUpdateVMValidator()
        {
            //RuleFor(vm => vm.Rate).InclusiveBetween(6.50, 200.0);
        }
    }
}
