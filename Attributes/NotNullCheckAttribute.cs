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
            string errorMessage = $"{validationContext.DisplayName.GetDisplayName()} is Required.";
            if (value is null || string.IsNullOrWhiteSpace(value.ToString()))
                throw new AppException(errorMessage, HttpStatusCode.BadRequest);
            return new ValidationResult(ErrorMessage);
        }
    }
}
