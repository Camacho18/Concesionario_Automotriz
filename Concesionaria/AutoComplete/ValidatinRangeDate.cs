using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Concesionaria.AutoComplete
{
    public class ValidatinRangeDate : ValidationAttribute
    {
       
    
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
            // your validation logic

            return value!=null ? (DateTime)value >= Convert.ToDateTime("01/01/0001") && (DateTime)value <= DateTime.Now.Date ? ValidationResult.Success : new ValidationResult("La fecha debe ser igual o menor a la actual.") : ValidationResult.Success;
            }        
    }
}