using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTaskOmega.Application.MappingProfiles
{
    using AutoMapper;
    using TestTaskOmega.Domain;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BaseEntity, BaseEntityHistory>()
                .ForMember(dest => dest.EntityId, opt => opt.MapFrom(src => src.Id));

            CreateMap<BaseEntityHistory, BaseEntity>(); // Optional: Create reverse mapping if needed
        }
    }
}
