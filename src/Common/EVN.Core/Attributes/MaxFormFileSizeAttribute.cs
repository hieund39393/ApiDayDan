using EVN.Core.Common;
using EVN.Core.Exceptions;
using EVN.Core.Properties;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace EVN.Domain.Attributes
{
    public class MaxFormFileSizeAttribute : ValidationAttribute
	{
        private readonly int _maxFileSize = 300000;
        public MaxFormFileSizeAttribute()
        {
            var configuration = Helpers.GetConfiguration();
            _maxFileSize = Convert.ToInt32(configuration["FileMaxSize"]);
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file == null)
            {
                throw new EvnException(EvnResources.MSG_FILE_NOT_FOUND);
            }
            if (file.Length > _maxFileSize)
            {
                throw new EvnException(string.Format(EvnResources.MSG_VALIDATE_MAX_LENGTH_FORM_FILE, _maxFileSize));
            }

            return ValidationResult.Success;
        }
    }
}
