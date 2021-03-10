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
        private readonly IRepository _repo;

        public CategoriaController(IRepository repository)
        {
            _repo = repository;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repo.ObterCategorias());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetByID(int id)
        {
            var categoria = _repo.ObterCategoriaPorID(id);
            if (categoria == null) 
                return BadRequest("Categoria não foi Encontrada!");                
            return Ok(categoria);
        }

        [HttpGet("{sigla}")]
        public IActionResult GetBySigla(string sigla)
        {
            var categoria = _repo.ObterCategoriaPorSigla(sigla.ToUpper());
            if (categoria == null)
                return BadRequest("Categoria não foi Encontrada!");
            return Ok(categoria);
        }

        [HttpPost]
        public IActionResult Post(Categoria categoria)
        {
            _repo.Add(categoria);
            if (_repo.SaveChanges())
                return Ok($"Categoria: {categoria.Descricao} Adicionada com Sucesso!");
            return BadRequest($"Falha ao tentar cadastrar a Categoria: {categoria.Descricao}");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Categoria categoria)
        {
            var novaCategoria = this.TrataAtualizacaoForm(categoria, id);
            if (novaCategoria == null)
                return BadRequest("Categoria não foi Encontrada!");

            _repo.Update(novaCategoria);
            if (_repo.SaveChanges())
                return Ok($"Categoria: {novaCategoria.Descricao} alterada com Sucesso!");
            return BadRequest($"Falha ao tentar atualizar a Categoria: {novaCategoria.Descricao}");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, Categoria categoria)
        {
            var novaCategoria = this.TrataAtualizacaoForm(categoria, id);
            if (novaCategoria == null)
                return BadRequest("Categoria não foi Encontrada!");
            
            _repo.Update(novaCategoria);
            if (_repo.SaveChanges())
                return Ok($"Categoria: {novaCategoria.Descricao} alterada com Sucesso!");
            return BadRequest($"Falha ao tentar atualizar a Categoria: {novaCategoria.Descricao}");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var categoriaSelecionada = _repo.ObterCategoriaPorID(id);
            if (categoriaSelecionada == null)
                return BadRequest("Categoria não foi Encontrada!");
            
            _repo.Delete(categoriaSelecionada);
            if (_repo.SaveChanges())
                return Ok($"Categoria: {categoriaSelecionada.Descricao} Excluida com Sucesso!");
            return BadRequest($"Falha ao tentar excluir a Categoria: {categoriaSelecionada.Descricao}");
        }

        public Categoria TrataAtualizacaoForm(Categoria categoriaForm, int id)
        {
            Categoria categoriaSelecionada = _repo.ObterCategoriaPorID(id);
            if (categoriaSelecionada == null)
                return null;
            if (categoriaForm.Descricao != "" && categoriaForm.Descricao != null)
                categoriaSelecionada.Descricao = categoriaForm.Descricao;
            if (categoriaForm.Sigla != "" && categoriaForm.Sigla != null)
                categoriaSelecionada.Sigla = categoriaForm.Sigla;
            return categoriaSelecionada;
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