using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace formBuilder.Models
{
    public class Form
    {
        public int Id { get; set; }
        public string? nameOftheForm { get; set; }
    }
}
