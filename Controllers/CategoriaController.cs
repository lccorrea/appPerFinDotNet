using System.Collections.Generic;
//using System.Linq;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
using AutoMapper;
using appPerfinAPI.Data;
using appPerfinAPI.Models;
using appPerfinAPI.Dtos;

namespace appPerfinAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public CategoriaController(IRepository repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Método responsavel por retornar todas as Categorias
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            var result = _repo.ObterCategorias();
            return Ok(_mapper.Map<IEnumerable<CategoriaDto>>(result));
        }

        /// <summary>
        /// Método responsavel por retornar apenas a Categoria do ID requisitado
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public IActionResult GetByID(int id)
        {
            var categoria = _repo.ObterCategoriaPorID(id);
            if (categoria == null) 
                return BadRequest("Categoria não foi Encontrada!");
            var categoriaDto = _mapper.Map<CategoriaDto>(categoria);
            return Ok(categoriaDto);
        }

        /// <summary>
        /// Método responsavel por retornar apenas as Categorias da SIGLA requisitada
        /// </summary>
        /// <returns></returns>
        [HttpGet("{sigla}")]
        public IActionResult GetBySigla(string sigla)
        {
            var categoria = _repo.ObterCategoriaPorSigla(sigla.ToUpper());
            if (categoria == null)
                return BadRequest("Categoria não foi Encontrada!");
            var categoriaDto = _mapper.Map<CategoriaDto>(categoria);
            return Ok(categoria);
        }

        /// <summary>
        /// Método responsavel pela criação de uma nova Categoria
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsavel pela atualização de uma Categoria
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsavel pela atualização de uma Categoria
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Método responsavel pela exclusão de uma Categoria
        /// </summary>
        /// <returns></returns>

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

        private Categoria TrataAtualizacaoForm(CategoriaDto categForm, int id)
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