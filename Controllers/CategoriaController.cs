using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using appPerfinAPI.Data;
using appPerfinAPI.Models;

namespace appPerfinAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly DataContext _context;

        public CategoriaController(DataContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Categorias);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetByID(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(categ => categ.Id == id);
            if (categoria == null) 
                return BadRequest("Categoria não foi Encontrada!");                
            return Ok(categoria);
        }

        [HttpGet("{sigla}")]
        public IActionResult GetBySigla(string sigla)
        {
            var categoria = _context.Categorias.FirstOrDefault(categ => categ.Sigla.ToUpper().Contains(sigla.ToUpper()));
            if (categoria == null)
                return BadRequest("Categoria não foi Encontrada!");
            return Ok(categoria);
        }

        [HttpPost]
        public IActionResult Post(Categoria categoria)
        {
            _context.Add(categoria);
            _context.SaveChanges();
            return Ok($"Categoria: {categoria.Descricao} Adicionada com Sucesso!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Categoria categoria)
        {
            var categoriaSelecionada = _context.Categorias.AsNoTracking().FirstOrDefault(categ => categ.Id == id);
            if (categoriaSelecionada == null)
                return BadRequest("Categoria não foi Encontrada!");
            
            var novaCategoria = this.TrataFormulario(categoriaSelecionada, categoria);
            
            _context.Update(novaCategoria);
            _context.SaveChanges();
            return Ok($"Categoria: {novaCategoria.Descricao} alterada com Sucesso!");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Categoria categoria)
        {
            var categoriaSelecionada = _context.Categorias.AsNoTracking().FirstOrDefault(categ => categ.Id == id);
            if (categoriaSelecionada == null)
                return BadRequest("Categoria não foi Encontrada!");
            
            var novaCategoria = this.TrataFormulario(categoriaSelecionada, categoria);
            
            _context.Update(novaCategoria);
            _context.SaveChanges();
            return Ok($"Categoria: {novaCategoria.Descricao} alterada com Sucesso!");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var categoriaSelecionada = _context.Categorias.FirstOrDefault(categ => categ.Id == id);
            if (categoriaSelecionada == null)
                return BadRequest("Categoria não foi Encontrada!");
            _context.Remove(categoriaSelecionada);
            _context.SaveChanges();
            return Ok($"Categoria: {categoriaSelecionada.Descricao} Excluida com Sucesso!");
        }

        private Categoria TrataFormulario(Categoria categoriaBanco, Categoria categoriaForm)
        {
            if (categoriaForm.Descricao != "" && categoriaForm.Descricao != null)
                categoriaBanco.Descricao = categoriaForm.Descricao;
            if (categoriaForm.Sigla != "" && categoriaForm.Sigla != null)
                categoriaBanco.Sigla = categoriaForm.Sigla;
            return categoriaBanco;
        }
        
        //[HttpGet("byId")]
        //CHAMADA api/categoria/byId?id=1
        //[HttpGet("byId/{id}")]
        //CHAMADA api/categoria/byId/1
        //[HttpGet("byName")]
        //public IActionResult GetByName(string nome, string sobrenome)
        //CHAMADA api/categoria/byName?nome=Marta&sobrenome=Kente
    }
}