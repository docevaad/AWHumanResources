using AWHumanResources.Data.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AWHumanResources.Data.ViewModels
{
    public class EmpDeptUpdateRequest : IValidatableObject
    {
        public int DepartmentID { get; set; }
        public DateTime CurrentDeptEndDate { get; set; }
        public DateTime NewDeptStartDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new EmpDeptUpdateRequestValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}
