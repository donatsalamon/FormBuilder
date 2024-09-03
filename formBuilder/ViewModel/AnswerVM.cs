using formBuilder.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace formBuilder.ViewModel
{
    public class AnswerVM
    {
        public Answer[] AnswersList { get; set; }
        public List<string>? User { get; set; }
        public List<string>? Questions { get; set; }
    }
}
