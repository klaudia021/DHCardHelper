using DHCardHelper.Models.DTOs.Character;
using DHCardHelper.Models.Entities.Cards;
using DHCardHelper.Models.Entities.Characters;
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
                .Map(dest => dest.TypeId, src => src.DomainCardTypeId)
                .Map(dest => dest.Level, src => src.Level)
                .Map(dest => dest.RecallCost, src => src.RecallCost);

            TypeAdapterConfig<DomainCardDto, DomainCard>.NewConfig()
                .Ignore(dest => dest.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Feature, src => src.Feature)
                .Map(dest => dest.DomainId, src => src.DomainId)
                .Map(dest => dest.DomainCardTypeId, src => src.TypeId)
                .Map(dest => dest.Level, src => src.Level)
                .Map(dest => dest.RecallCost, src => src.RecallCost);

            TypeAdapterConfig<BackgroundCardDto, BackgroundCard>.NewConfig()
                .Ignore(dest => dest.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Feature, src => src.Feature)
                .Map(dest => dest.Desciption, src => src.Desciption)
                .Map(dest => dest.BackgroundTypeId, src => src.BackgroundTypeId);

            TypeAdapterConfig<BackgroundCard, BackgroundCardDto>.NewConfig()
                .Ignore(dest => dest.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Feature, src => src.Feature)
                .Map(dest => dest.Desciption, src => src.Desciption)
                .Map(dest => dest.BackgroundTypeId, src => src.BackgroundTypeId);

            TypeAdapterConfig<SubclassCardDto, SubclassCard>.NewConfig()
                .Ignore(dest => dest.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Feature, src => src.Feature)
                .Map(dest => dest.CharacterClassId, src => src.CharacterClassId)
                .Map(dest => dest.MasteryType, src => src.MasteryType);

            TypeAdapterConfig<SubclassCard, SubclassCardDto>.NewConfig()
                .Ignore(dest => dest.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Feature, src => src.Feature)
                .Map(dest => dest.CharacterClassId, src => src.CharacterClassId)
                .Map(dest => dest.MasteryType, src => src.MasteryType);

            TypeAdapterConfig<CharacterSheet, CharacterSheetDto>.NewConfig()
                .Ignore(dest => dest.UserId)
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name);
        }
    }
}
