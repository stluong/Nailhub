
using System.ComponentModel.DataAnnotations.Schema;

namespace Generic.Core.Repository
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState ObjectState { get; set; }
    }
}