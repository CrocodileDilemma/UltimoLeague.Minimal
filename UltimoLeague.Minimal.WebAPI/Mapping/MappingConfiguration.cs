using FluentResults;
using Mapster;
using MongoDB.Bson;
using System.Numerics;
using UltimoLeague.Minimal.Contracts.Requests;
using UltimoLeague.Minimal.DAL.Entities;
using UltimoLeague.Minimal.WebAPI.Utilities;

namespace UltimoLeague.Minimal.WebAPI.Mapping
{
    public class MappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ArenaRequest, Arena>();

            config.NewConfig<(LeagueRequest, Sport), League>()
                .Map(dest => dest, src => src.Item1)
                .Map(dest => dest.Sport, src => src.Item2);

            config.NewConfig<PlayerUpdateRequest, Player>()
                .IgnoreNullValues(true);

            config.NewConfig<(string, Player, Team), Registration>()
               .Map(dest => dest.RegistrationNumber, src => src.Item1)
               .Map(dest => dest.PlayerId, src => src.Item2.Id)
               .Map(dest => dest.TeamId, src => src.Item3.Id)
               .Map(dest => dest.PreviousTeamId, src => src.Item2.ActiveTeamId);

            config.NewConfig<(Registration, PlayerDto, Team), RegistrationDto>()
              .Map(dest => dest, src => src.Item1)
              .Map(dest => dest.Player, src => src.Item2)
              .Map(dest => dest.Team, src => src.Item3)
              .Map(dest => dest.PreviousTeam, src => src.Item2.ActiveTeam);

            config.NewConfig<(SeasonRequest, League), Season>()
            .Map(dest => dest, src => src.Item1)
            .Map(dest => dest.LeagueId, src => src.Item2.Id);

            config.NewConfig<SportRequest, Sport>();

            config.NewConfig<(Team, League), TeamDto>()
               .IgnoreNullValues(true)
               .Map(dest => dest, src => src.Item1)
               .Map(dest => dest.League, src => src.Item2);
               
            config.NewConfig<(TeamRequest, League), Team>()
                .Map(dest => dest, src => src.Item1)
                .Map(dest => dest.SportId, src => src.Item2.Sport.Id);

            config.NewConfig<(TeamUpdateRequest, League), Team>()
                .IgnoreNullValues(true)
                .Map(dest => dest, src => src.Item1)
                .Map(dest => dest.SportId, src => src.Item2.Sport.Id);
        }
    }
}
