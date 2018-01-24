using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FSiMed_x2._1.Models
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited =true)]
    public sealed class MyValidateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string vRes = "NAVEEN";
            if (value.ToString() != vRes)
            {
                return new ValidationResult("asdadsdsdasdadasdsd");
            }

            return ValidationResult.Success;
        }
    }
}