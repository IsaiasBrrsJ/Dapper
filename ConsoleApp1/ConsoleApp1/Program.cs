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
            //Dapper(connectionString);
            //Insert(connectionString);
            Select(connectionString);

        }

        public static void Select(string connectionString)
        {
            using (var conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM dbo.Produtos";
                string queryWhere = @"SELECT * FROM dbo.Produtos where Id = 1002";
                //var linhas = conn.Query<Produtos>(query).ToList();
                //linhas.ForEach(Console.WriteLine);


                //var linha = conn.QueryFirst<Produtos>(query);
                //Console.WriteLine(linha);
                //var where = conn.Query<Produtos>(queryWhere).ToList();
                //where.ForEach(Console.WriteLine);

                string quer = @"
                    SELECT * FROM dbo.Produtos;
                    SELECT * FROM dbo.Produtos where Id = 1002;
                ";

                var mulit = conn.QueryMultiple(quer, commandType: System.Data.CommandType.Text);

                var resultado = mulit.Read<Produtos>().ToList();
                var resultado1 = mulit.Read<Produtos>().ToList();

                Console.ForegroundColor = ConsoleColor.Red;
                resultado.ForEach(Console.WriteLine);
                Console.ResetColor();
                Console.WriteLine("Resultado2");
                resultado1.ForEach(Console.WriteLine);



            }
        }
       
        public static void Insert(string connectionString)
        {
            using (var conn = new SqlConnection(connectionString)) {
                string query = @$"insert into Produtos (Nome, Descricao, Preco, DataFrabricacao, DataValidade) values (@Nome, @Descricao, @Preco, @DataFrabricacao, @DataValidade)";

                //DynamicParameters param = new DynamicParameters();
                //param.Add("Nome", "Inserindo por param dapper");
                //param.Add("Descricao", "Produtos naturais");
                //param.Add("Preco", 200.3m);
                //param.Add("DataFrabricacao", DateTime.Now);
                //param.Add("DataValidade", DateTime.Now);

                //int linhasAfetadas = conn.Execute(query, param, commandType: System.Data.CommandType.Text);


                //if(linhasAfetadas == 0)
                //{
                //    Console.WriteLine("Falhou");
                //}
                //else
                //{
                //    Console.WriteLine($"Quantidade de linhas afetadas: {linhasAfetadas}");
                //}

                int linhasAfetadas = conn.Execute(query,
                    new[]
                    {
                        new{Nome = "MMTeste", Descricao = "Tesste1", Preco= 55m, DataFrabricacao = DateTime.Now, DataValidade = DateTime.Now},
                        new{Nome = "MMTeste2", Descricao = "Tesste2", Preco= 56m, DataFrabricacao = DateTime.Now, DataValidade = DateTime.Now},
                        new{Nome = "MMTeste3", Descricao = "Tesste3", Preco= 57m, DataFrabricacao = DateTime.Now, DataValidade = DateTime.Now},
                        new{Nome = "MMTeste4", Descricao = "Tesste4", Preco= 58m, DataFrabricacao = DateTime.Now, DataValidade = DateTime.Now},
                    });

                if(linhasAfetadas == 0)
                {
                    Console.WriteLine("Falhou");
                }
                else
                {
                    Console.WriteLine($"Quantidade de linhas afetadas: {linhasAfetadas}");
                }

            }

        }
        public static void Dapper(string connectionString)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = @"SELECT * FROM dbo.Produtos";



                //Select
                var listaProd = conn.Query<Produtos>(query).ToList();

              
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

                //Insert
                //conn.Insert<Produtos>(new Produtos {Preco = 22.43m,Nome = "TesteeDapper", Descricao = "TestandoDapper", DataFrabricacao = DateTime.Now, DataValidade = DateTime.Now});


                //Update
                //conn.Update<Produtos>(new Produtos { Id = 1005, Nome = "Alterado pelo dapper", Descricao = "dapper teste", Preco = 33.3m, DataFrabricacao = DateTime.Now, DataValidade = DateTime.Now });

                //Delete
                //conn.Delete<Produtos>(new Produtos { Id = 1007});

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
