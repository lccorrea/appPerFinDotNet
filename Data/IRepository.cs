using appPerfinAPI.Models;

namespace appPerfinAPI.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        bool SaveChanges();

        Categoria[] ObterCategorias();
        Categoria ObterCategoriaPorID(int categoriaID);
        Categoria ObterCategoriaPorSigla(string categoriaSigla);
    }
}