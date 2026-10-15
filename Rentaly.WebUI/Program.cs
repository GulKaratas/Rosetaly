using Rentaly.BusinessLayer.Abstract;
using Rentaly.BusinessLayer.Concrete;
using Rentaly.BusinessLayer.Mapping;
using Rentaly.DataAccessLayer.Abstract;
using Rentaly.DataAccessLayer.Concrete;
using Rentaly.DataAccessLayer.EntityFramework;
using Rentaly.BusinessLayer.ValidationRules;
using Rentaly.DtoLayer.CarDtos;
using FluentValidation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ICategoryDal,EfCategoryDal>();
builder.Services.AddScoped<ICategoryService,CategoryManager>();

builder.Services.AddScoped<ICarDal, EfCarDal>();
builder.Services.AddScoped<ICarService, CarManager>();
builder.Services.AddScoped<IValidator<CreateCarDto>, CreateCarDtoValidator>();
builder.Services.AddScoped<ICarModelDal, EfCarModelDal>();
builder.Services.AddScoped<ICarModelService, CarModelManager>();

builder.Services.AddScoped<IBranchDal, EfBranchDal>();
builder.Services.AddScoped<IBranchService, BranchManager>();
builder.Services.AddScoped<RentalyContext>();

builder.Services.AddScoped<ICustomerDal, EfCustomerDal>();
builder.Services.AddScoped<ICustomerService, CustomerManager>();

builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(GeneralMapping).Assembly));

builder.Services.AddScoped<IBrandDal, EfBrandDal>();
builder.Services.AddScoped<IBrandService, BrandManager>();
builder.Services.AddScoped<IRentalDal, EfRentalDal>();
builder.Services.AddScoped<IRentalService, RentalManager>();
builder.Services.AddScoped<IReviewDal, EfReviewDal>();
builder.Services.AddScoped<IReviewService, ReviewManager>();
builder.Services.AddScoped<IAboutDal, EfAboutDal>();
builder.Services.AddScoped<IAboutService, AboutManager>();

builder.Services.AddScoped<ITestimonialDal, EfTestimonialDal>();
builder.Services.AddScoped<ITestimonialService, TestimonialManager>();

builder.Services.AddScoped<IOurFeatureService, OurFeatureManager>();
builder.Services.AddScoped<IOurFeatureDal, EfOurFeatureDal>();

builder.Services.AddScoped<IFAQDal, EfFAQDal>();
builder.Services.AddScoped<IFAQService, FAQManager>();

builder.Services.AddScoped<IRentalDal, EfRentalDal>();
builder.Services.AddScoped<IRentalService, RentalManager>();

builder.Services.AddScoped<IEmailService, EmailManager>();
builder.Services.AddScoped<IAiOperationsService, AiOperationsManager>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// HTTP istek hattını yapılandır.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Varsayılan HSTS süresi 30 gündür. Üretim senaryolarında değiştirmek isteyebilirsiniz: https://aka.ms/aspnetcore-hsts
    app.UseHsts();
}

app.UseHttpsRedirection();
// Bulunamayan sayfa kodlarını (404 vb.) yakalamak için:
app.UseStatusCodePagesWithReExecute("/Error/NotFound", "?statusCode={0}");
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
