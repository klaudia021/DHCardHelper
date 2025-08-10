using DHCardHelper.Models.Entities.Cards;
using Mapster;

namespace DHCardHelper.Models.DTOs.MappingProfile
{
    public static class MapsterConfig
    {
        public static void Configure()
        {
            TypeAdapterConfig<DomainCard, DomainCardDto>.NewConfig()
                .Ignore(dest => dest.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Feature, src => src.Feature)
                .Map(dest => dest.DomainId, src => src.DomainId)
                .Map(dest => dest.TypeId, src => src.TypeId)
                .Map(dest => dest.Level, src => src.Level)
                .Map(dest => dest.RecallCost, src => src.RecallCost);

            TypeAdapterConfig<DomainCardDto, DomainCard>.NewConfig()
                .Ignore(dest => dest.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Feature, src => src.Feature)
                .Map(dest => dest.DomainId, src => src.DomainId)
                .Map(dest => dest.TypeId, src => src.TypeId)
                .Map(dest => dest.Level, src => src.Level)
                .Map(dest => dest.RecallCost, src => src.RecallCost);
        }
    }
}
