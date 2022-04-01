using ClubeLeitura.ConsoleApp.Compartilhado;

namespace ClubeLeitura.ConsoleApp.ModuloCategoria
{
    public class RepositorioCategoria : Repositorio
    { 
        public RepositorioCategoria(int qtdElementos): base(qtdElementos)
        {
        }

        public override Categoria[] SelecionarTodos()
        {
            int quantidadeCategorias = ObterQtdElementos();

            Categoria[] categoriasInseridas = new Categoria[quantidadeCategorias];

            int j = 0;

            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] != null)
                {
                    categoriasInseridas[j] = (Categoria)elementos[i];
                    
                    j++;
                }
            }

            return categoriasInseridas;
        }

        public override Categoria SelecionarEntidade(int numeroCategoria)
        {
            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] != null && numeroCategoria == elementos[i].numero)
                    return (Categoria)elementos[i];
            }

            return null;
        }
        
    }
}
