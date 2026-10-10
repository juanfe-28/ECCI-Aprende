using System.ComponentModel.DataAnnotations;

namespace Plataforma_Aprendizaje.Models;

public class Categoria
{
    [Key]
    public int Id { get; set; }

[Required]
    [MaxLength(100)]
    public string Nombre { get; set; } = string.Empty;

    [MaxLength(300)]
    public string Descripcion { get; set; } = string.Empty;

    public List<Curso> Cursos { get; set; } = new();

}
