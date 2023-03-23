using EVN.Core.Exceptions;
using EVN.Core.Helpers;
using EVN.Core.Models;
using EVN.Core.Properties;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace EVN.Core.Attributes
{
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private string[] _extensions;
        public AllowedExtensionsAttribute()
        {
            var configuration = ConfigurationHelper.GetConfiguration();
            var settings = configuration.GetSection("Extensions").Get<AllowExtensions>();
            _extensions = settings.ImageExtension;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if(file == null)
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
