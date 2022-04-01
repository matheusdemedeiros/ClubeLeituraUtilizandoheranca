
using ClubeLeitura.ConsoleApp.Compartilhado;

namespace ClubeLeitura.ConsoleApp.ModuloCaixa
{
    public class RepositorioCaixa : Repositorio
    {
        public RepositorioCaixa(int qtdElementos) : base(qtdElementos)
        {
        }

        public override Caixa[] SelecionarTodos()
        {
            int quantidadeCaixas = ObterQtdElementos();

            Caixa[] caixasInseridas = new Caixa[quantidadeCaixas];

            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] != null)
                    caixasInseridas[i] = (Caixa)elementos[i];
            }

            return caixasInseridas;
        }
        
        public override Caixa SelecionarEntidade(int numeroEntidade)
        {
            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] != null)
                {
                    Caixa caixa = (Caixa)elementos[i];
                    
                    if (numeroEntidade == caixa.numero)
                        return (Caixa)elementos[i];
                }
            }

            return null;
        }

        public bool EtiquetaJaUtilizada(string etiquetaInformada)
        {
            bool etiquetaJaUtilizada = false;
            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] != null)
                {
                    Caixa caixa = (Caixa)elementos[i];

                    if (caixa.Etiqueta == etiquetaInformada)
                        etiquetaJaUtilizada = true;
                    break;
                }
            }

            return etiquetaJaUtilizada;
        }
       
    }
}
