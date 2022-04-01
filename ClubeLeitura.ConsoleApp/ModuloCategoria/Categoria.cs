using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.ModuloRevista;
namespace ClubeLeitura.ConsoleApp.ModuloCategoria
{
    public class Categoria : EntidadeBase
    {
        private readonly string nome;
        private readonly int diasEmprestimo;

        public Revista[] revistas;

        public string Nome => nome;

        public int DiasEmprestimo => diasEmprestimo;

        public Categoria(string nome, int diasEmprestimo)
        {
            this.nome = nome;
            this.diasEmprestimo = diasEmprestimo;
        }

        public override string ToString()
        {
            string retorno =
            "\nNúmero: " + numero +
            "\nTipo de Categoria: " + nome +
            "\nLimite de empréstimo: " + diasEmprestimo + " dias\n";
            return retorno;
        }


    }
}
