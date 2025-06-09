using System.ComponentModel.DataAnnotations;

namespace API.Modelos
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Nome { get; set; } = string.Empty;

        public ICollection<Livro>? Livros { get; set; }
    }
}
