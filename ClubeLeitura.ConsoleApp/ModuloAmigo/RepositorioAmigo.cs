using ClubeLeitura.ConsoleApp.Compartilhado;
using System;

namespace ClubeLeitura.ConsoleApp.ModuloAmigo
{
    public class RepositorioAmigo : Repositorio
    {
        public RepositorioAmigo(int qtdAmigos) : base(qtdAmigos)
        {
            elementos = new Amigo[qtdAmigos];
        }
        
        public Amigo[] SelecionarAmigosComMulta()
        {
            Amigo[] amigosComMulta = new Amigo[ObterQtdAmigosComMulta()];

            int j = 0;

            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] != null)
                {
                    Amigo amigo = (Amigo)elementos[i];

                    if (amigo.TemMultaEmAberto())
                    {
                        amigosComMulta[j] = amigo;

                        j++;
                    }
                }
            }

            return amigosComMulta;
        }

        #region Métodos override

        public override Amigo[] SelecionarTodos()
        {
            int quantidadeAmigos = ObterQtdElementos();

            Amigo[] amigosinseridos = new Amigo[quantidadeAmigos];

            int j = 0;

            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] != null)
                {
                    amigosinseridos[j] = (Amigo)elementos[i];

                    j++;
                }
            }

            return amigosinseridos;
        }

        public override Amigo SelecionarEntidade(int numeroEntidade)
        {
            for (int i = 0; i < elementos.Length; i++)
            {
                Amigo amigo = (Amigo)elementos[i];

                if (numeroEntidade == amigo.numero)
                    return (Amigo)elementos[i];
            }

            return null;
        }

        #endregion

        #region Métodos privados

        private int ObterQtdAmigosComMulta()
        {
            int numeroAmigos = 0;

            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] != null)
                {
                    Amigo amigo = (Amigo)elementos[i];

                    if (amigo.TemMultaEmAberto())
                        numeroAmigos++;
                }
            }

            return numeroAmigos;
        }

        #endregion
    }
}
