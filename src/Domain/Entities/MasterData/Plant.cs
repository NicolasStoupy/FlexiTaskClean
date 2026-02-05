using Domain.Common.Exceptions;
using Domain.Enums;

namespace Domain.Entities.MasterData
{
    public class Plant : BaseAuditableEntity
    {
        public int PlantID { get; private set; }
        public string Code { get; private set; } = null!;
        public string? CommonName { get; private set; }

        public bool Active { get; private set; } = true;
        public DateTimeOffset CreatedAt { get; private set; } //to do remove
        public PlantLanguage Language { get; private set; }

        private readonly List<WorkArea> _workAreas = new();
        public IReadOnlyCollection<WorkArea> WorkAreas => _workAreas.AsReadOnly();

        //private readonly List<PlantIdentity> _plantIdentities = new();
        //public IReadOnlyCollection<PlantIdentity> PlantIdentities => _plantIdentities.AsReadOnly();
        public Plant()
        { }

        public Plant(string code, PlantLanguage language, string? commonName)
        {
            Code = code;
            Language = language;
            CommonName = commonName;
        }

        public void Update(string code, string commonName, string language, bool active)
        {
            PlantLanguage plantLanguage;
            if (!Enum.TryParse(language, out plantLanguage))
                throw new DomainException("Language is unknow");
            CommonName = CommonName;
            if (code == null)
                throw new DomainException("code must be null");
            if (active != Active)
                active = Active;
            Code = code;

        }
    }
}