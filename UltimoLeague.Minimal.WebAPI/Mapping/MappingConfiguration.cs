using Microsoft.AspNetCore.Routing.Constraints;
using MongoDB.Bson;
using UltimoLeague.Minimal.Contracts.Dtos;
using UltimoLeague.Minimal.DAL.Entities;

namespace UltimoLeague.Minimal.WebAPI.Mapping
{
    public class MappingConfiguration : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ArenaRequest, Arena>();

            config.NewConfig<League, LeagueMinimal>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.BaseId, src => src.Id);

            config.NewConfig<League, LeagueDto>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.Sport, src => src.Sport)
                .Map(dest => dest.Sport.Id, src => src.Sport.BaseId);

            config.NewConfig<(LeagueRequest, Sport), League>()
                .Map(dest => dest, src => src.Item1)
                .Map(dest => dest.Sport, src => src.Item2)
                .Map(dest => dest.Sport.BaseId, src => src.Item2.Id);

            config.NewConfig<PlayerUpdateRequest, Player>()
                .IgnoreNullValues(true);

            config.NewConfig<Player, PlayerDto>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.ActiveTeam, src => src.ActiveTeam)
                .Map(dest => dest.ActiveTeam.Id, src => src.ActiveTeam.BaseId);

            config.NewConfig<Player, PlayerMinimalDto>()
               .Map(dest => dest, src => src)
               .Map(dest => dest.PlayerName, src => $"{src.FirstName} {src.LastName}");

            config.NewConfig<Player, PlayerMinimal>()
              .Map(dest => dest.BaseId, src => src.Id)
              .Map(dest => dest.PlayerName, src => $"{src.FirstName} {src.LastName}");

            config.NewConfig<(string, Player, Team), Registration>()
               .Map(dest => dest.RegistrationNumber, src => src.Item1)
               .Map(dest => dest.Player, src => src.Item2.Adapt<PlayerMinimal>())
               .Map(dest => dest.Team, src => src.Item3.Adapt<TeamMinimal>())
               .Map(dest => dest.PreviousTeam, src => src.Item2.ActiveTeam.Adapt<TeamMinimal>());

            config.NewConfig<Registration, RegistrationDto>()
              .Map(dest => dest, src => src)
              .Map(dest => dest.Player, src => src.Player)
              .Map(dest => dest.Player.Id, src => src.Player.BaseId)
              .Map(dest => dest.Team, src => src.Team)
              .Map(dest => dest.Team.Id, src => src.Team.BaseId)
              .Map(dest => dest.PreviousTeam, src => src.PreviousTeam)
              .Map(dest => dest.PreviousTeam.Id, src => src.PreviousTeam.BaseId);

            config.NewConfig<(SeasonRequest, LeagueMinimal, ObjectId), Season>()
                .Map(dest => dest, src => src.Item1)
                .Map(dest => dest.League, src => src.Item2)
                .Map(dest => dest.Id, src => src.Item3);

            config.NewConfig<Season, SeasonBaseDto>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.League, src => src.League)
                .Map(dest => dest.League.Id, src => src.League.BaseId);

            config.NewConfig<SportRequest, Sport>();

            config.NewConfig<SportUpdateRequest, Sport>()
                .IgnoreNullValues(true);

            config.NewConfig<(Team, League), TeamDto>()
               .IgnoreNullValues(true)
               .Map(dest => dest, src => src.Item1)
               .Map(dest => dest.League, src => src.Item2);

            config.NewConfig<Team, TeamDto>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.League, src => src.League)
                .Map(dest => dest.League.Id, src => src.League.BaseId);

            config.NewConfig<Team, TeamMinimal>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.BaseId, src => src.Id);

            config.NewConfig<Team, TeamMinimal>()
                .Map(dest => dest, src => src)
                .Map(dest => dest.BaseId, src => src.Id);

            config.NewConfig<(TeamRequest, League), Team>()
                .Map(dest => dest, src => src.Item1)
                .Map(dest => dest.League, src => src.Item2)
                .Map(dest => dest.League.BaseId, src => src.Item2.Id)
                .Map(dest => dest.Sport, src => src.Item2.Sport)
                .Map(dest => dest.Sport.BaseId, src => src.Item2.Sport.BaseId);

            config.NewConfig<(TeamUpdateRequest, League), Team>()
                .IgnoreNullValues(true)
                .Map(dest => dest, src => src.Item1)
                .Map(dest => dest.League, src => src.Item2)
                .Map(dest => dest.League.BaseId, src => src.Item2.Id)
                .Map(dest => dest.Sport, src => src.Item2.Sport)
                .Map(dest => dest.Sport.BaseId, src => src.Item2.Sport.BaseId);
        }
    }
}
