using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.BranchDtos;
using Rentaly.EntityLayer.Entities;

namespace Rentaly.WebUI.Controllers
{
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
        public IActionResult CreateBranch()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBranch(CreateBranchDto createBranchDto)
        {
            await _branchService.TInsertAsync(createBranchDto);
            return RedirectToAction("BranchList");
        }
    }
}
