using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using appPerfinAPI.Data;
using appPerfinAPI.Models;
using appPerfinAPI.Dtos;
using AutoMapper;

namespace appPerfinAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public CategoriaController(IRepository repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.ObterCategorias();
            return Ok(_mapper.Map<IEnumerable<CategoriaDto>>(result));
        }

        [HttpGet("{id:int}")]
        public IActionResult GetByID(int id)
        {
            var categoria = _repo.ObterCategoriaPorID(id);
            if (categoria == null) 
                return BadRequest("Categoria não foi Encontrada!");
            var categoriaDto = _mapper.Map<CategoriaDto>(categoria);
            return Ok(categoriaDto);
        }

        [HttpGet("{sigla}")]
        public IActionResult GetBySigla(string sigla)
        {
            var categoria = _repo.ObterCategoriaPorSigla(sigla.ToUpper());
            if (categoria == null)
                return BadRequest("Categoria não foi Encontrada!");
            var categoriaDto = _mapper.Map<CategoriaDto>(categoria);
            return Ok(categoria);
        }

        [HttpPost]
        public IActionResult Post(CategoriaDto cDto)
        {
            var categoria = _mapper.Map<Categoria>(cDto);
            _repo.Add(categoria);
            if (_repo.SaveChanges())
                return Created($"/api/categoria/{cDto.Id}", _mapper.Map<CategoriaDto>(categoria)); 
                //return Ok($"Categoria: {categoriaDto.Descricao} Adicionada com Sucesso!");
            return BadRequest($"Falha ao tentar cadastrar a Categoria: {categoria.Descricao}");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, CategoriaDto cDto)
        {
            var categoria = this.TrataAtualizacaoForm(cDto, id);
            if (categoria == null)
                return BadRequest("Categoria não foi Encontrada!");

            _repo.Update(categoria);
            if (_repo.SaveChanges())
                return Ok($"Categoria: {categoria.Descricao} alterada com Sucesso!");
            return BadRequest($"Falha ao tentar atualizar a Categoria: {categoria.Descricao}");
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, CategoriaDto cDto)
        {
            var categoria = this.TrataAtualizacaoForm(cDto, id);
            if (categoria == null)
                return BadRequest("Categoria não foi Encontrada!");
            
            _repo.Update(categoria);
            if (_repo.SaveChanges())
                return Ok($"Categoria: {categoria.Descricao} alterada com Sucesso!");
            return BadRequest($"Falha ao tentar atualizar a Categoria: {categoria.Descricao}");
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

        public Categoria TrataAtualizacaoForm(CategoriaDto categForm, int id)
        {
            var categSelecionada = _repo.ObterCategoriaPorID(id);

            if (categSelecionada == null)
                return null;
            if (categForm.Id < 1)
                categForm.Id = id;
            if (categForm.Descricao != "" && categForm.Descricao != null)
                categSelecionada.Descricao = categSelecionada.Descricao;
            if (categForm.Sigla != "" && categForm.Sigla != null)
                categSelecionada.Sigla = categSelecionada.Sigla;

            var categoria = _mapper.Map(categForm, categSelecionada);                
            return categoria;
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