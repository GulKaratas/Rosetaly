using AutoMapper;
using Rentaly.DtoLayer.About;
using Rentaly.DtoLayer.BranchDtos;
using Rentaly.DtoLayer.CarDtos;
using Rentaly.DtoLayer.CategoryDtos;
using Rentaly.DtoLayer.CustomerDtos;
using Rentaly.DtoLayer.FAQDtos;
using Rentaly.DtoLayer.RentalDtos;
using Rentaly.DtoLayer.Testimonial;
using Rentaly.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rentaly.BusinessLayer.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Customer, CreateCustomerDto>().ReverseMap();
            CreateMap<Customer, ResultCustomerDto>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();
            CreateMap<Customer, GetCustomerByIdDto>().ReverseMap();

            CreateMap<Branch, ResultBranchDto>().ReverseMap();
            CreateMap<Branch, CreateBranchDto>().ReverseMap();
            CreateMap<Branch, UpdateBranchDto>().ReverseMap();
            CreateMap<Branch, GetBranchByIdDto>().ReverseMap();

            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, GetCategoryByIdDto>().ReverseMap();

            CreateMap<Car, ResultCarDto>()
           .ForMember(d => d.BrandName, o => o.MapFrom(s => s.CarModel.Brand.BrandName))
           .ForMember(d => d.ModelName, o => o.MapFrom(s => s.CarModel.ModelName))
           .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.CategoryName));
            CreateMap<Car, GetCarByIdDto>()
           .ForMember(d => d.BrandName, o => o.MapFrom(s => s.CarModel.Brand.BrandName))
           .ForMember(d => d.ModelName, o => o.MapFrom(s => s.CarModel.ModelName))
           .ForMember(d => d.CategoryName, o => o.MapFrom(s => s.Category.CategoryName))
           .ForMember(d => d.BranchName, o => o.MapFrom(s => s.Branch.BranchName));

            CreateMap<Car, CreateCarDto>().ReverseMap();
            CreateMap<Car, UpdateCarDto>().ReverseMap();

            CreateMap<About, ResultAboutDto>().ReverseMap();
            CreateMap<About, CreateAboutDto>().ReverseMap();
            CreateMap<About, UpdateAboutDto>().ReverseMap();
            CreateMap<About, GetAboutById>().ReverseMap();

            CreateMap<Testimonial, ResultTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, CreateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, UpdateTestimonialDto>().ReverseMap();
            CreateMap<Testimonial, GetTestimonialByIdDto>().ReverseMap();

            CreateMap<FAQ, ResultFAQDto>().ReverseMap();
            CreateMap<FAQ, CreateFAQDto>().ReverseMap();
            CreateMap<FAQ, UpdateFAQDto>().ReverseMap();
            CreateMap<FAQ, GetFAQByIdDto>().ReverseMap();

            CreateMap<Rental, ResultRentalDto>().ReverseMap();
            CreateMap<Rental, CreateRentalDto>().ReverseMap();
            CreateMap<Rental, UpdateRentalDto>().ReverseMap();
            CreateMap<Rental, GetRentalByIdDto>().ReverseMap();
        }
    }
}
