using DHCardHelper.Models.Entities.Cards;
using DHCardHelper.Models.Shared;

namespace DHCardHelper.Models.DTOs.Character
{
    public class CardSheetDto
    {
        public int CharacterSheetId { get; set; }
        public CharacterSheetDto CharacterSheetDto { get; set; }


        public int CardId { get; set; }
        public CardDto CardDto { get; set; }


        public bool InLoadout { get; set; }

        public bool InLimit { get; set; }

        public GradientColor SubclassHeaderColor { get; set; }
    }
}
