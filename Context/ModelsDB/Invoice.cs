using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Invoice {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public bool ItsPaid {get; set;}
    public double Amounth {get; set;}
    public int IdClient { get; set; }
    [ForeignKey("IdClient")]
    public virtual Client? Client {get; set;}
}