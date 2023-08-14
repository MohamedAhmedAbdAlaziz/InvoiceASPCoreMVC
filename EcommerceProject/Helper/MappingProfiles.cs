using AutoMapper;
using Core.Models.Entities;
using EcommerceProject.Dtos;
using EcommerceProject.Dtos.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EcommerceProject.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Invoice, InvoiceDto>()
            .ForMember(d => d.Customer, o => o.MapFrom(s => s.Customer.Name));
            CreateMap<Item, ITemViewModel>().ReverseMap();
        }
    }
}
