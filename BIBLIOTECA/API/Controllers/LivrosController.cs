using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Modelos;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivrosController : ControllerBase
    {
        private readonly BibliotecaDbContext _context;

        public LivrosController(BibliotecaDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarLivro([FromBody] Livro livro)
        {
            if (string.IsNullOrWhiteSpace(livro.Titulo) || livro.Titulo.Length < 3)
                return BadRequest(new { mensagem = "Título deve ter no mínimo 3 caracteres." });

            if (string.IsNullOrWhiteSpace(livro.Autor) || livro.Autor.Length < 3)
                return BadRequest(new { mensagem = "Autor deve ter no mínimo 3 caracteres." });

            var categoria = await _context.Categorias.FindAsync(livro.CategoriaId);
            if (categoria == null)
                return NotFound(new { mensagem = "Categoria inválida. O ID da categoria fornecido não existe." });

            livro.Categoria = categoria;

            _context.Livros.Add(livro);
            await _context.SaveChangesAsync();

            var livroComCategoria = new
            {
                livro.Id,
                livro.Titulo,
                livro.Autor,
                livro.CategoriaId,
                Categoria = new { categoria.Id, categoria.Nome }
            };

            return CreatedAtAction(nameof(BuscarPorId), new { id = livro.Id }, livroComCategoria);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> BuscarPorId(int id)
        {
            var livro = await _context.Livros
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (livro == null)
                return NotFound(new { mensagem = $"Livro com ID {id} não encontrado." });

            var resultado = new
            {
                livro.Id,
                livro.Titulo,
                livro.Autor,
                livro.CategoriaId,
                Categoria = new { livro.Categoria!.Id, livro.Categoria.Nome }
            };

            return Ok(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ListarTodos()
        {
            var livros = await _context.Livros
                .Include(l => l.Categoria)
                .Select(l => new
                {
                    l.Id,
                    l.Titulo,
                    l.Autor,
                    l.CategoriaId,
                    Categoria = new { l.Categoria!.Id, l.Categoria.Nome }
                })
                .ToListAsync();

            return Ok(livros);
        }
    }
}
