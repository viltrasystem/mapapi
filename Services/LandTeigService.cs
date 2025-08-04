    using AutoMapper;
using ViltrapportenApi.Data.MapModels;
using ViltrapportenApi.Modal;
namespace ViltrapportenApi.Services
{

    public class LandTeigService : ILandTeigService
    {
        private readonly IMapper _mapper;

        public LandTeigService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public LandTeigDTO GetLandTeigDataFromLandTable(LandDrawn land)
        {
            return _mapper.Map<LandTeigDTO>(land);
        }

        public LandTeigDTO GetLandTeigDataFromTeigTable(Teig teig)
        {
            return _mapper.Map<LandTeigDTO>(teig);
        }
    }

}
