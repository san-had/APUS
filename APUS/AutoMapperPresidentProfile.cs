namespace APUS
{
    using APUS.Utils;
    using AutoMapper;

    public class AutoMapperPresidentProfile : Profile
    {
        public AutoMapperPresidentProfile()
        {
            CreateMap<DataAccess.DbPresident, Models.President>()
                    .ForMember(
                                dest => dest.TookOffice,
                                opt => opt.MapFrom(src => src.TookOffice.ParseEnDateFormat()))
                    .ForMember(
                                dest => dest.LeftOffice,
                                opt => opt.MapFrom(src => src.LeftOffice.ParseEnDateFormat()));
        }
    }
}
