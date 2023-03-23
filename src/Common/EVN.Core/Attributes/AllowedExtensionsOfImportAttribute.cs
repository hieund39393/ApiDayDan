using EVN.Core.Common;
using EVN.Core.Exceptions;
using EVN.Core.Models;
using EVN.Core.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace EVN.Domain.Attributes
{
    public class AllowedExtensionsOfImportAttribute : ValidationAttribute
    {
        private string[] _extensions;
        public AllowedExtensionsOfImportAttribute()
        {
            var configuration = Helpers.GetConfiguration();
            var settings = configuration.GetSection("FormImportExtensions").Get<FormImportExtensions>();
            _extensions = settings.FileExtension;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file == null)
            {
                throw new EvnException(EvnResources.MSG_VALIDATE_EXTENSION_IMAGE);
            }
            var extension = Path.GetExtension(file.FileName);
            if (file != null)
            {
                if (!_extensions.Contains(extension.ToLower()))
                {
                    throw new EvnException(EvnResources.MSG_VALIDATE_EXTENSION_IMAGE);
                }
            }

            return ValidationResult.Success;
        }
    }
}
