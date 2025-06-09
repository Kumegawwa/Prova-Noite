using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Modelos
{
    public class Livro
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Título deve ter no mínimo 3 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required]
        [MinLength(3, ErrorMessage = "Autor deve ter no mínimo 3 caracteres.")]
        public string Autor { get; set; } = string.Empty;

        [ForeignKey("Categoria")]
        public int CategoriaId { get; set; }

        public Categoria? Categoria { get; set; }
    }
}
