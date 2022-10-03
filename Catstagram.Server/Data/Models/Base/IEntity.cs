using System.ComponentModel.DataAnnotations;

namespace Catstagram.Server.Data.Models.Base
{
    public interface IEntity
    {
        DateTime CreatedOn { get; set; }
        DateTime? ModifiedOn { get; set; }
        string CreatedBy { get; set; }
        string ModifiedBy { get; set; }
    }
}
