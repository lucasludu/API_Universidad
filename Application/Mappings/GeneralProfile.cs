using AutoMapper;
using Domain.Entities;
using Application.DTOs._career.Request;
using Application.DTOs._career.Response;
using Application.DTOs._subject.Request;
using Application.DTOs._subject.Response;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Career, CareerInsertRequest>().ReverseMap();
            CreateMap<Career, CareerResponse>().ReverseMap();

            CreateMap<Subject, SubjectInsertRequest>().ReverseMap();
            CreateMap<Subject, SubjectResponse>().ReverseMap();
        }
    }
}
