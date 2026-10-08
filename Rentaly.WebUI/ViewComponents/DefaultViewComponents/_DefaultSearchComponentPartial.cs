using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using System.Threading.Tasks;

namespace Rentaly.WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultSearchComponentPartial : ViewComponent
    {
        private readonly IBranchService _branchService;
        private readonly ICategoryService _categoryService;

        public _DefaultSearchComponentPartial(IBranchService branchService, ICategoryService categoryService)
        {
            _branchService = branchService;
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var branchList = await _branchService.TGetListAsync();
            var categoryList = await _categoryService.TGetListAsync();

            ViewBag.Branches = branchList;
            ViewBag.Categories = categoryList;

            return View();
        }
    }
}