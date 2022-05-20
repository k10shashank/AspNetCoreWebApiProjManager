using AspNetCoreWebApiProjManager.Shared;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace AspNetCoreWebApiProjManager.Attributes
{
    public class NotNullCheckAttribute : ValidationAttribute
    {
        public NotNullCheckAttribute() { }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string errorMessage = $"{validationContext.DisplayName.GetFileColumnName()} is Required.";
            return value is null || string.IsNullOrWhiteSpace(value.ToString())
                ? throw new AppException(errorMessage, HttpStatusCode.BadRequest)
                : new ValidationResult(ErrorMessage);
        }
    }
}
