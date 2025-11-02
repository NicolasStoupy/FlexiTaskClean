using Domain.Common;
using Domain.Events;

namespace Domain.Entities
{
    public class Plant : BaseAuditableEntity
    {

        public string Code { get; set; }

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (value )
                {
                    AddDomainEvent(new PlantCompletedEvent(this));
                }

                _isActive = value;
            }
        }

    }
}
