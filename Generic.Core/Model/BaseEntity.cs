using System.ComponentModel.DataAnnotations.Schema;
using Generic.Core.Repository;

namespace Generic.Core.Model
{
    public abstract class BaseEntity : IObjectState
    {
        [NotMapped]
        public ObjectState ObjectState { get; set; } //TODO: Renamed since a possible coflict with State entity column
    }
}