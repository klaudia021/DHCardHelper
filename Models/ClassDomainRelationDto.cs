namespace DHCardHelper.Models
{
    public class ClassDomainRelationDto
    {
        public string Class { get; set; }
        public List<string> Domains { get; set; } = new List<string>();
    }
}
