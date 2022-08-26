namespace vendas.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; } = 0;
        public string Nome { get; set; } = "";
        public string Marca { get; set; } = "";
        public decimal Valor { get; set; } = decimal.Zero;
        public string Categoria { get; set; } = "";
    }
}
