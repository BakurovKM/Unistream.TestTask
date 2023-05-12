using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unistream.TestTask.Db.Domain;

public class Entity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Required]
    public DateTime OperationDate { get; set; }

    [Required]
    public decimal Amount { get; set; }
    
    public Entity(Guid id)
    {
        Id = id;
    }
}