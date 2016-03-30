using AWHumanResources.Data.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AWHumanResources.Data.ViewModels
{
    /// <summary>
    /// Request object to update an employee's pay.
    /// </summary>
    /// <seealso cref="IValidatableObject" />
    public class EmpPayUpdateRequest : IValidatableObject
    {
        public byte PayFrequency { get; set; }
        public decimal Rate { get; set; }
        public DateTime RateChangeDate { get; set; }

        /// <summary>
        /// Validates <see cref="EmpPayUpdateRequest"/>
        /// </summary>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>
        /// A collection that holds failed-validation information.
        /// </returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = new EmpPayUpdateRequestValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(item => new ValidationResult(item.ErrorMessage, new[] { item.PropertyName }));
        }
    }
}