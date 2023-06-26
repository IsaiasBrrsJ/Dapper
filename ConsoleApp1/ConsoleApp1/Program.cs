using Microsoft.Data.SqlClient;
using Dapper;
using Dapper.Contrib.Extensions;


namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Dapper microORM

            var connectionString = @"Server=localhost;Database=UdePratcApi;User Id=sa;Password=estudos@2023;TrustServerCertificate=True;language=portuguese";
            //DapperContrib(connectionString);
            Dapper(connectionString);

        }
       
        public static void Dapper(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM dbo.Produtos";

                var listaProd = conn.Query<Produtos>(query).ToList();

                var a = conn.Insert<Produtos>(new Produtos {Preco = 22.43m,Nome = "TesteeDapper", Descricao = "TestandoDapper", DataFrabricacao = DateTime.Now, DataValidade = DateTime.Now});

            }
        }

        public static void DapperContrib(string connectionString)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM dbo.Estoque";

                //Dapper Select * from ...;
                var getAll = conn.GetAll<Estoque>().ToList();

                //Dapper select * from... where ...
                var getId = conn.Get<Estoque>(1);

                //Console.WriteLine(getId);
                getAll.ForEach(Console.WriteLine);
                
            }
        }
    }

    [Table("Estoque")]
    class Estoque
    {
        public Estoque()
        {
            
        }
        public int Id { get; set; }

        public int Quantidade { get; set; }

        public int ProdutoId { get; set; }

        public override string ToString()
        {
            var linha = "----------------------------------------------";

            return $"Id: {Id}\nQuantidade: {Quantidade}\nProdutoId: {ProdutoId}\n{linha}";
        }
    }

    [Table("Produtos")]
    class Produtos
    {
        public Produtos() { }
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = String.Empty;

        public decimal Preco { get; set; }

        public DateTime DataFrabricacao { get; set; }

        public DateTime DataValidade { get; set; }


        public override string ToString()
        {
            var linha = "----------------------------------------------";
            return $"Id: {Id}\nDescrição: {Descricao}\nPreço: {Preco}\nData de Fabricação: {DataFrabricacao}\nData de Validade: {DataValidade}\n{linha}";
        }
    }
}
