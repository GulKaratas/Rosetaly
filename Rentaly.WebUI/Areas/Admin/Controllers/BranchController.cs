using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.BranchDtos;

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

        [HttpGet]
        public async Task<IActionResult> UpdateBranch(int id)
        {
            var value = await _branchService.TGetByIdAsync(id);
            var model = new UpdateBranchDto
            {
                BranchId = value.BranchId,
                BranchName = value.BranchName,
                City = value.City,
                Address = value.Address
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBranch(UpdateBranchDto updateBranchDto)
        {
            await _branchService.TUpdateAsync(updateBranchDto);
            return RedirectToAction("BranchList");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBranch(int id)
        {
            await _branchService.TDeleteAsync(id);
            return RedirectToAction("BranchList");
        }
    }
}
