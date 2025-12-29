using MediatR;

namespace Domain.Common
{
    /// <summary>
    /// Classe de base pour tous les événements de domaine.
    /// </summary>
    public record BaseEvent: INotification
    {
    }
}
