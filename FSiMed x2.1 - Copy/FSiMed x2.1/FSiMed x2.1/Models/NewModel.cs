using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using FSiMed_x2._1.Models;

namespace FSiMed_x2._1.Models
{
    public class NewModel
    {
        //[MyValidate(ErrorMessage = "Cannot enter name except naveen")]
       // [Required(ErrorMessage = "Age is Required")]
        //[MinLength(18)] // Our custom attribute
        [Required(ErrorMessage = "Name is required.")]
        //[StringLength(40, ErrorMessage = "Name cannot be longer than 40 characters.")]
        public string Name { get; set; }
    }
}