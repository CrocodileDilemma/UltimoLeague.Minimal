using Mapster;
using UltimoLeague.Minimal.DAL.Entities;

namespace UltimoLeague.Minimal.WebAPI.Mapping
{
    public class MappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<PlayerUpdateRequest, Player>()
                .IgnoreNullValues(true);

            config.NewConfig<TeamUpdateRequest, Team>()
                .IgnoreNullValues(true);
        }
    }
}
