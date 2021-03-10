using System.Linq;
using appPerfinAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace appPerfinAPI.Data
{
    public class Repository : IRepository
    {
        private readonly DataContext _repo;
        
        public Repository(DataContext context)
        {
            _repo = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _repo.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _repo.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _repo.Remove(entity);
        }

        public bool SaveChanges()
        {
            if (_repo.SaveChanges() > 0)
                return true;
            return false;
        }

        public Categoria[] ObterCategorias()
        {
            IQueryable<Categoria> query = _repo.Categorias;
            /*
                if (includeDisciplina)
                {
                    query = query.Include(a => a.AlunoDisciplinas)
                                .ThenInclude(d => d.Disciplina)
                                .ThenInclude(p => p.Professor);
                }
            */
            /*
                query = query.AsNoTracking()
                            .OrderBy(a => a.Id)
                            .Where(aluno => alunoDisciplinas.Any(ad => ad.DisciplinaID == disciplinaID));
            */
            query = query.AsNoTracking().OrderBy(c => c.Id);
            return query.ToArray();
        }

        public Categoria ObterCategoriaPorID(int categoriaID)
        {
            IQueryable<Categoria> query = _repo.Categorias;
            query = query.AsNoTracking().Where(c => c.Id == categoriaID);
            return query.FirstOrDefault();
        }

        public Categoria ObterCategoriaPorSigla(string categoriaSigla)
        {
            IQueryable<Categoria> query = _repo.Categorias;
            query = query.AsNoTracking().Where(c => c.Sigla == categoriaSigla);
            return query.FirstOrDefault();
        }
    }
}