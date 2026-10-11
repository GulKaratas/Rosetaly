using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace Rentaly.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultFAQComponentPartial : ViewComponent
    {
        private readonly IFAQService _faqService;

        public _DefaultFAQComponentPartial(IFAQService faqService)
        {
            _faqService = faqService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var faqs = await _faqService.TGetListAsync();
            return View(faqs);
        }
    }
}
