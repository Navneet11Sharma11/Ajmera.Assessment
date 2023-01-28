using Ajmera.Assessment.DL.Models;
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
            CreateMap<BookMaster, BookMasterResponseDto>().ReverseMap();

            CreateMap<BookMasterResponseDto, BookMaster>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => "API"))
                .ForMember(dest => dest.BookMasterID, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<BookMasterRequestDto, BookMaster>()
                .ForMember(dest => dest.BookMasterID, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => "API"))
                .ReverseMap();

        }
    }
}
