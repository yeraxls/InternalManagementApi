using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Client {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}
    public required string Name {get; set;}
    public required string Surname {get; set;}
    public required string Phone {get; set;}
    public required string Mail {get; set;}

    public virtual ICollection<Invoice>? Invoices {get; set;}
}