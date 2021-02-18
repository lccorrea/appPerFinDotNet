namespace appPerfinAPI.Models
{
    public class Categoria
    {
        public int Id { get; set; }   
        public string Descricao { get; set; }     
        public string Sigla { get; set; }
        public Categoria() { }
        public Categoria(int id, string descricao, string sigla) 
        {
            this.Id = id;
            this.Descricao = descricao;
            this.Sigla = sigla;
        }
    }
}