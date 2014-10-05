
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.MyState
{
    public interface IObjectState
    {
        [NotMapped]
        ObjectState ObjectState { get; set; }
    }
}