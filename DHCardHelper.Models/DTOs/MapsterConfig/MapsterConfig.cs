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
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Feature, src => src.Feature)
                .Map(dest => dest.DomainId, src => src.DomainId)
                .Map(dest => dest.TypeId, src => src.DomainCardTypeId)
                .Map(dest => dest.Level, src => src.Level)
                .Map(dest => dest.RecallCost, src => src.RecallCost)
                .Map(dest => dest.DomainCardType, src => src.DomainCardType)
                .Map(dest => dest.Domain, src => src.Domain);

            TypeAdapterConfig<DomainCardDto, DomainCard>.NewConfig()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Domain)
                .Ignore(dest => dest.DomainCardType)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Feature, src => src.Feature)
                .Map(dest => dest.DomainId, src => src.DomainId)
                .Map(dest => dest.DomainCardTypeId, src => src.TypeId)
                .Map(dest => dest.Level, src => src.Level)
                .Map(dest => dest.RecallCost, src => src.RecallCost);

            TypeAdapterConfig<BackgroundCardDto, BackgroundCard>.NewConfig()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.BackgroundType)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Feature, src => src.Feature)
                .Map(dest => dest.Desciption, src => src.Desciption)
                .Map(dest => dest.BackgroundTypeId, src => src.BackgroundTypeId);

            TypeAdapterConfig<BackgroundCard, BackgroundCardDto>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Feature, src => src.Feature)
                .Map(dest => dest.Desciption, src => src.Desciption)
                .Map(dest => dest.BackgroundTypeId, src => src.BackgroundTypeId)
                .Map(dest => dest.BackgroundType, src => src.BackgroundType);

            TypeAdapterConfig<SubclassCardDto, SubclassCard>.NewConfig()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.CharacterClass)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Feature, src => src.Feature)
                .Map(dest => dest.CharacterClassId, src => src.CharacterClassId)
                .Map(dest => dest.MasteryType, src => src.MasteryType);

            TypeAdapterConfig<SubclassCard, SubclassCardDto>.NewConfig()
                .Ignore(d => d.SubclassHeaderColor)
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Feature, src => src.Feature)
                .Map(dest => dest.CharacterClass, src => src.CharacterClass)
                .Map(dest => dest.CharacterClassId, src => src.CharacterClassId)
                .Map(dest => dest.MasteryType, src => src.MasteryType);

            TypeAdapterConfig<CharacterSheet, CharacterSheetDto>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Name, src => src.Name);

            TypeAdapterConfig<CardSheet, CardSheetDto>.NewConfig()
                .Ignore(cs => cs.CardId)
                .Ignore(cs => cs.CharacterSheetId)
                .Ignore(cs => cs.SubclassHeaderColor)
                .Map(dest => dest.InLoadout, src => src.InLoadout)
                .Map(dest => dest.InLimit, src => src.InLimit)
                .Map(dest => dest.CardDto, src => src.Card)
                .Map(dest => dest.CharacterSheetDto, src => src.CharacterSheet);

            TypeAdapterConfig<Card, CardDto>.NewConfig()
                .Include<SubclassCard, SubclassCardDto>()
                .Include<BackgroundCard, BackgroundCardDto>()
                .Include<DomainCard, DomainCardDto>();
        }
    }
}
