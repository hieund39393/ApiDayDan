using EVN.Core.Common;
using EVN.Core.Exceptions;
using EVN.Core.Helpers;
using EVN.Core.Properties;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace EVN.Core.Attributes
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute()
        {
            var configuration = ConfigurationHelper.GetConfiguration();
            _maxFileSize = Convert.ToInt32(configuration["ImageMaxSize"]);
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
                throw new EvnException(string.Format(EvnResources.MSG_VALIDATE_MAX_LENGTH_IMAGE, _maxFileSize));
            }

            return ValidationResult.Success;
        }
    }
}
