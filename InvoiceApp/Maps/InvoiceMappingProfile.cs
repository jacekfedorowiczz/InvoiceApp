using AutoMapper;
using InvoiceApp.Entities;
using InvoiceApp.Models.Models;

namespace InvoiceApp.Maps
{
    public class InvoiceMappingProfile : Profile
    {
        public InvoiceMappingProfile()
        {
            CreateMap<Invoice, InvoiceDto>();
        }
    }
}
