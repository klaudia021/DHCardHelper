using DHCardHelper.Models.Entities;

namespace DHCardHelper.Models.DTOs.Character
{
    public class CharacterClassDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Domain> Domains { get; set; }
    }
}
