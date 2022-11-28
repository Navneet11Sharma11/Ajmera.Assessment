using Ajmera.Assessment.DL.Entities;
using Ajmera.Assessment.Shared.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ajmera.Assessment.BL.AutoMapperProfiles
{
    public class BookMasterProfile : Profile
    {
        public BookMasterProfile()
        {
            CreateMap<BookMaster, BookMasterDto>().ReverseMap();

            CreateMap<BookMasterDto, BookMaster>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => "API"))
                .ReverseMap();
        }
    }
}
