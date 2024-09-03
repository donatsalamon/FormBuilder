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
    public class SheetVM
    {
        public Answer[] AnswersList { get; set; }
        public List<QuestionSheet>? QuestionSheetList { get; set; }
        public string? User { get; set; }
        public string? sheetName { get;set; }

    }
}
