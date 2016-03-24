using AWHumanResources.Data.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AWHumanResources.Data.ViewModels
{
    public class EmpPayUpdateVM : IValidatableObject
    {
        public int BusinessEntityID { get; set; }
        public byte PayFrequency { get; set; }
        public decimal Rate { get; set; }
        public DateTime RateChangeDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new EmpPayUpdateVMValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}