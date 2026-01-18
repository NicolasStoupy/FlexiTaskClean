using Domain.Enums;

namespace Domain.Entities.MasterData
{
    public class Plant : BaseAuditableEntity<int>
    {
        public DateTimeOffset CreatedAt { get; set; }
        public string Code { get; set; } = null!;
        public string? CommonName { get; set; }
        public PlantLanguage Language { get; set; }
        public IList<WorkArea> WorkAreas { get; set; } = new List<WorkArea>();

        public Plant()
        { }

        public Plant(string code, PlantLanguage language, string? commonName)
        {
            Code = code;
            Language = language;
            CommonName = commonName;
        }
    }
}