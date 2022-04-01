using ClubeLeitura.ConsoleApp.Compartilhado;
using System;

namespace ClubeLeitura.ConsoleApp.ModuloCaixa
{
    public class TelaCadastroCaixa : TelaCadastro
    {
        public TelaCadastroCaixa(Repositorio repositorio, Notificador notificador, string titulo) : base(repositorio, notificador, titulo)
        {
        }
        
        public override Caixa ObterEntidade()
        {
            Console.Write("Digite a cor: ");
            string cor = Console.ReadLine();

            Console.Write("Digite a etiqueta: ");
            string etiqueta = Console.ReadLine();

            bool etiquetaJaUtilizada;

            do
            {
                RepositorioCaixa rep = (RepositorioCaixa)repositorio;

                etiquetaJaUtilizada = rep.EtiquetaJaUtilizada(etiqueta);

                if (etiquetaJaUtilizada)
                {
                    notificador.ApresentarMensagem("Etiqueta já utilizada, por gentileza informe outra", TipoMensagem.Erro);

                    Console.Write("Digite a etiqueta: ");
                    etiqueta = Console.ReadLine();
                }

            } while (etiquetaJaUtilizada);

            Caixa caixa = new Caixa(cor, etiqueta);

            return caixa;
        }

    }
}