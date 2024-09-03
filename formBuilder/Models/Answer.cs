using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace formBuilder.Models
{
    public class Answer
    {
        public int Id{ get; set; }
        public string? textAnswer { get; set; }

        public string? UserTableId { get; set; }
        [ForeignKey("UserTableId")]
        [ValidateNever]
        public User User { get; set; }

        //this is the one what the users write during filling the sheet
        public string? UsersDescription { get; set; }
        //name of the questionsheet where the answer belongs to
        public string? nameOfTheQuestionSheet { get; set; }

        public int? QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        [ValidateNever]
        public QuestionSheet QuestionSheet { get; set; }
        public int? Score { get; set; }

    }
}
