using AspNetCoreWebApiProjManager.Shared;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace AspNetCoreWebApiProjManager.Attributes
{
    public class ValidValueCheckAttribute : ValidationAttribute
    {
        private object[] ValidValues { get; set; }
        public ValidValueCheckAttribute(params object[] validValues)
        {
            ValidValues = validValues;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorMessage = $"Invalid {validationContext.DisplayName.GetFileColumnName()} Value - {value}";
            return !ValidValues.Contains(value)
                ? throw new AppException(errorMessage, HttpStatusCode.BadRequest)
                : new ValidationResult(ErrorMessage);
        }
    }
}
