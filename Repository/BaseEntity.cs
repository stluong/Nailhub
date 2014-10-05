using System.ComponentModel.DataAnnotations.Schema;
using Infrastructure.MyState;

namespace Infrastructure.Repository
{
    public abstract class BaseEntity : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; } //TODO: Renamed since a possible coflict with State entity column
    }
}