using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InvestimentoApp.Domain.Entities;

[Table("Cotacao")]
public class Cotacao
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime Data { get; set; }

    [Required]
    [StringLength(30)]
    public string Indexador { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "DECIMAL(10,2)")]
    public decimal Valor { get; set; }
}