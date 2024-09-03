using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace formBuilder.Models
{
    public class QuestionSheet
    {
        public int Id{ get; set; }
        public string? Question { get; set; }
        public string? questionType{ get; set; }
        //this description is the one when added when the question is created
        public string? AddedDescription { get; set; }
        public int? FormId { get; set; }
        [ForeignKey("FormId")]
        [ValidateNever]
        public Form Form { get; set; }
        public string? formName { get; set; }

        //future child-parent questions necessery properties
        public string? typeOfQuestion { get; set; }
        public int? parentQuestionId { get; set; }
        public int? childQuestionId { get; set; }
        //public int? StatementId { get; set; }
        //[ForeignKey("StatementId")]
        //[ValidateNever]
        //public Statement Statement { get; set; }
        //public string? nameOfTheStatement { get; set; }

    }
}
