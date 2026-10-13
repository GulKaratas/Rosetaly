using Microsoft.AspNetCore.Mvc;
using Rentaly.BusinessLayer.Abstract;
using Rentaly.DtoLayer.Testimonial;

namespace Rentaly.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TestimonialController : Controller
    {
        private readonly ITestimonialService _testimonialService;

        public TestimonialController(ITestimonialService testimonialService)
        {
            _testimonialService = testimonialService;
        }

        public async Task<IActionResult> TestimonialList()
        {
            var values = await _testimonialService.TGetListAsync();
            return View(values);

        }

        [HttpGet]
        public IActionResult CreateTestimonial()
        {
            var values = _testimonialService.TGetListAsync();
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> CreateTestimonial(CreateTestimonialDto createTestimonialDto)
        {
             await _testimonialService.TInsertAsync(createTestimonialDto);
            return RedirectToAction("TestimonialList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteTestimonial(int id)
        {
            await _testimonialService.TDeleteAsync(id);
            return RedirectToAction(nameof(TestimonialList));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTestimonial(int id)
        {
            var value = await _testimonialService.TGetByIdAsync(id);
          

            var model = new UpdateTestimonialDto
            {
                TestimonialId = value.TestimonialId,
                ClientName = value.ClientName,
                Title = value.Title,
                Comment = value.Comment,
                ImageUrl = value.ImageUrl
            };
            return View(model);
        }

        [HttpPost]
        
        public async Task<IActionResult> UpdateTestimonial(UpdateTestimonialDto updateTestimonialDto)
        {
            await _testimonialService.TUpdateAsync(updateTestimonialDto);
            return RedirectToAction(nameof(TestimonialList));
        }

    }
}
