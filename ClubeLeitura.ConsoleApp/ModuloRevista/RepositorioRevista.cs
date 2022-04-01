using ClubeLeitura.ConsoleApp.Compartilhado;

namespace ClubeLeitura.ConsoleApp.ModuloRevista
{
    public class RepositorioRevista : Repositorio
    {
        public RepositorioRevista(int qtdRevistas): base(qtdRevistas)
        {
        }
        
        public string Inserir(Revista revista)
        {
            string validacao = revista.Validar();

            if (validacao != "REGISTRO_VALIDO")
                return validacao;

            revista.numero = ++numeroEntidade;

            int posicaoVazia = ObterPosicaoVazia();

            elementos[posicaoVazia] = revista;

            return validacao;
        }

        public override Revista[] SelecionarTodos()
        {
            int quantidadeRevistas = ObterQtdElementos();

            Revista[] revistasInseridas = new Revista[quantidadeRevistas];

            int j = 0;

            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] != null)
                {
                    revistasInseridas[j] = (Revista)elementos[i];
                    
                    j++;
                }
            }

            return revistasInseridas;
        }

        public override Revista SelecionarEntidade(int numeroRevista)
        {
            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] != null && numeroRevista == elementos[i].numero)
                    return (Revista)elementos[i];
            }

            return null;
        }
    }
}