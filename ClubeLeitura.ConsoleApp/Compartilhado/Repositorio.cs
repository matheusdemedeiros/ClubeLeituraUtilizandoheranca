using System;

namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public class Repositorio
    {
        protected  EntidadeBase[] elementos;
        
        protected int numeroEntidade;

        public Repositorio(int qtdElementos)
        {
            elementos = new EntidadeBase[qtdElementos];
        }

        public virtual void Inserir(EntidadeBase entidade)
        {
            entidade.numero = ++numeroEntidade;

            int posicaoVazia = ObterPosicaoVazia();

            elementos[posicaoVazia] = entidade;
        }

        public virtual void Editar(int numeroSelecionado, EntidadeBase entidadeAhSerEditada)
        {
            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i].numero == numeroSelecionado)
                {
                    entidadeAhSerEditada.numero = numeroSelecionado;

                    elementos[i] = entidadeAhSerEditada;

                    break;
                }
            }
        }

        public virtual void Excluir(int numeroSelecionado)
        {
            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i].numero == numeroSelecionado)
                {
                    elementos[i] = null;
                    break;
                }
            }
        }

        public virtual EntidadeBase[] SelecionarTodos()
        {
            int quantidadeElementos = ObterQtdElementos();

            EntidadeBase[] elementosInseridos = new EntidadeBase[quantidadeElementos];

            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] != null)
                    elementosInseridos[i] = elementos[i];
            }

            return elementosInseridos;
        }

        public virtual EntidadeBase SelecionarEntidade(int numeroEntidade)
        {
            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] != null && numeroEntidade == elementos[i].numero)
                    return elementos[i];
            }

            return null;
        }

        public bool VerificarNumeroEntidadeExiste(int numeroEntidade)
        {
            bool numeroEntidadeEncontrado = false;

            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] != null && elementos[i].numero == numeroEntidade)
                {
                    numeroEntidadeEncontrado = true;
                    break;
                }
            }

            return numeroEntidadeEncontrado;
        }

        protected int ObterQtdElementos()
        {
            int qtdElementos = 0;

            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] != null)
                    qtdElementos++;
            }

            return qtdElementos;
        }

        protected int ObterPosicaoVazia()
        {
            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] == null)
                    return i;
            }

            return -1;
        }

    }
}




