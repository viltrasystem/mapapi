namespace ViltrapportenApi.Profiles
{
    using AutoMapper;
    using ViltrapportenApi.Data.MapModels;
    using ViltrapportenApi.Modal;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LandDrawn, LandTeigDTO>()
                .ForMember(dest => dest.LandId, opt => opt.MapFrom(src => src.LandId))
                .ForMember(dest => dest.LandDrawnId, opt => opt.MapFrom(src => src.LandDrawnId))
                .ForMember(dest => dest.UuidLandDrawn, opt => opt.MapFrom(src => src.UuidLandDrawn))
                .ForMember(dest => dest.MunicipalityNo, opt => opt.MapFrom(src => src.MunicipalityNo))
                .ForMember(dest => dest.MunicipalityName, opt => opt.MapFrom(src => src.MunicipalityName))
                .ForMember(dest => dest.MainNo, opt => opt.MapFrom(src => src.MainNo))
                .ForMember(dest => dest.SubNo, opt => opt.MapFrom(src => src.SubNo))
                .ForMember(dest => dest.PlotNo, opt => opt.MapFrom(src => src.PlotNo))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.Properties, opt => opt.MapFrom(src => src.Properties))
                .ForMember(dest => dest.EditedBy, opt => opt.MapFrom(src => src.EditedBy))
                .ForMember(dest => dest.Geometry, opt => opt.MapFrom(src => src.Geometry))
                .ForMember(dest => dest.TeigId, opt => opt.MapFrom(src => src.TeigId));

            CreateMap<Teig, LandTeigDTO>()
                    .ForMember(dest => dest.TeigId, opt => opt.MapFrom(src => src.Teigid))
            .ForMember(dest => dest.Geometry, opt => opt.MapFrom(src => src.Omrade))
            .ForMember(dest => dest.MunicipalityNo, opt => opt.MapFrom(src => src.Kommunenummer))
            .ForMember(dest => dest.MunicipalityName, opt => opt.MapFrom(src => src.Kommunenavn))
            .ForMember(dest => dest.MainNo, opt => opt.MapFrom(src => src.Matrikkelnummertekst))
            .ForMember(dest => dest.SubNo, opt => opt.MapFrom(src => src.Matrikkelnummertekst))
            .ForMember(dest => dest.PlotNo, opt => opt.MapFrom(src => src.Matrikkelnummertekst))
            .ForMember(dest => dest.EditedBy, opt => opt.MapFrom(src => src.EditedBy));
        }
    }
}
