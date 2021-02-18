using System.Collections.Generic;
using System.Linq;
using appPerfinAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace appPerfinAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private List<Categoria> Categorias = new List<Categoria>()
        {
            new Categoria(1, "Alimentação", "ALI"),
            new Categoria(2, "Bar / Festas", "BAR"),
            new Categoria(3, "Refeição", "REF"),
            new Categoria(4, "Lazer / Esporte", "LAZ"),
        };
        public CategoriaController() { }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Categorias);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetByID(int id)
        {
            var categoria = Categorias.FirstOrDefault(categ => categ.Id == id);
            if (categoria == null) 
                return BadRequest("Categoria não foi Encontrada!");                
            return Ok(categoria);
        }

        [HttpGet("{sigla}")]
        public IActionResult GetBySigla(string sigla)
        {
            var categoria = Categorias.FirstOrDefault(categ => categ.Sigla.ToUpper().Contains(sigla.ToUpper()));
            if (categoria == null)
                return BadRequest("Categoria não foi Encontrada!");
            return Ok(categoria);
        }

        //[HttpGet("byId")]
        //CHAMADA api/categoria/byId?id=1
        //[HttpGet("byId/{id}")]
        //CHAMADA api/categoria/byId/1
        //[HttpGet("byName")]
        //public IActionResult GetByName(string nome, string sobrenome)
        //CHAMADA api/categoria/byName?nome=Marta&sobrenome=Kente

        [HttpPost]
        public IActionResult Post(Categoria categoria)
        {
            return Ok($"Categoria {categoria.Descricao} Adicionada com Sucesso!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Categoria categoria)
        {
            var categoriaSelecionada = Categorias.FirstOrDefault(categ => categ.Id == id);
            if (categoriaSelecionada == null)
                return BadRequest("Categoria não foi Encontrada!");
            return Ok($"Categoria {categoria.Descricao} alterada com Sucesso!");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Categoria categoria)
        {
            var categoriaSelecionada = Categorias.FirstOrDefault(categ => categ.Id == id);
            if (categoriaSelecionada == null)
                return BadRequest("Categoria não foi Encontrada!");
            return Ok($"Categoria {categoria.Descricao} alterada com Sucesso!");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var categoriaSelecionada = Categorias.FirstOrDefault(categ => categ.Id == id);
            if (categoriaSelecionada == null)
                return BadRequest("Categoria não foi Encontrada!");
            return Ok($"Categoria {categoriaSelecionada.Descricao} Excluida com Sucesso!");
        }
    }
}