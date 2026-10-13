using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BranchController : Controller
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        public async Task<IActionResult> BranchList()
        {
            var values = await _branchService.TGetListAsync();
            return View(values);

        }

        [HttpGet]
        public async Task<IActionResult> CreateBranch()
        {
            var values = await _branchService.TGetListAsync();
            return View();
        }


    }
}
