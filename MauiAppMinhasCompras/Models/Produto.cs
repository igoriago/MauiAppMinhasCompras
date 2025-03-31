using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        private string _descricao;
        private double _quantidade;
        private double _preco;
        private string _categoria;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Descricao
        {
            get => _descricao;
            set => _descricao = string.IsNullOrWhiteSpace(value)
                ? throw new Exception("Por favor, preencha a descrição.")
                : value;
        }

        public double Quantidade
        {
            get => _quantidade;
            set => _quantidade = value > 0 ? value : 1; // Define 1 se o valor for inválido
        }

        public double Preco
        {
            get => _preco;
            set => _preco = value > 0 ? value : 1; // Define 1 se o valor for inválido
        }

        public string Categoria
        {
            get => _categoria;
            set => _categoria = string.IsNullOrWhiteSpace(value)
                ? throw new Exception("Por favor, selecione uma categoria.")
                : value;
        }

        public double Total => Quantidade * Preco;
    }
}
