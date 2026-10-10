using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Plataforma_Aprendizaje.Models;

public class Curso
{
    [Key]
    public int Id { get; set; }

[Required]
    [MaxLength(150)]
    public string Nombre { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string Descripcion { get; set; } = string.Empty;

    [Column(TypeName = "decimal(10,2)")]
    public decimal Precio { get; set; }

    public int CategoriaId { get; set; }

    public Categoria? Categoria { get; set; }

}
