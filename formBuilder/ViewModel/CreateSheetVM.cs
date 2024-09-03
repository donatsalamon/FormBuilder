using formBuilder.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace formBuilder.ViewModel
{
    public class CreateSheetVM
    {
        //public List<Form>? listOfForms { get; set; }
        [ValidateNever]
        public List<SelectListItem>? everythingExceptChild { get; set; }
        public List<SelectListItem>? everythingExceptParent { get; set; }
        public List<SelectListItem>? StatementList { get; set; }
        public QuestionSheet? question { get; set; }

    }
}
