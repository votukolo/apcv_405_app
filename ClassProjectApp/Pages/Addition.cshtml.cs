using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClassProjectApp.Services;

namespace ClassProjectApp.Pages
{
    public class AdditionModel : PageModel
    {
        private readonly AdditionService _additionService;

        public AdditionModel(AdditionService additionService)
        {
            _additionService = additionService;
        }

        [BindProperty]
        public int NumberA { get; set; }

        [BindProperty]
        public int NumberB { get; set; }

        public int? Result { get; set; }

        public void OnGet()
        {
        }

        public void OnPost()
        {
            Result = _additionService.Add(NumberA, NumberB);
        }
    }
}