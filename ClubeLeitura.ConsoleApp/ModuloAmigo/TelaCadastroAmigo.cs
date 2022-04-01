using ClubeLeitura.ConsoleApp.Compartilhado;
using System;

namespace ClubeLeitura.ConsoleApp.ModuloAmigo
{
    public class TelaCadastroAmigo : TelaCadastro
    {
        public TelaCadastroAmigo(Repositorio repositorio, Notificador notificador, string titulo) : base(repositorio, notificador, titulo)
        {
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("\nDigite 2 para Editar");
            Console.WriteLine("\nDigite 3 para Excluir");
            Console.WriteLine("\nDigite 4 para Visualizar");
            Console.WriteLine("\nDigite 5 para Visualizar Amigos com Multa");
            Console.WriteLine("\nDigite 6 para Pagar Multas");
            Console.WriteLine("znDigite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public void PagarMulta()
        {
            MostrarTitulo("Pagamento de Multas");

            bool temAmigosComMulta = VisualizarAmigosComMulta("Pesquisando");

            if (!temAmigosComMulta)
            {
                notificador.ApresentarMensagem("Não há nenhum amigo com multas em aberto", TipoMensagem.Atencao);
                return;
            }

            int numeroAmigoComMulta = ObterNumeroEntidade();

            RepositorioAmigo rep = (RepositorioAmigo)repositorio;

            Amigo amigoComMulta = rep.SelecionarEntidade(numeroAmigoComMulta);

            amigoComMulta.PagarMulta();
        }
                        
        public bool VisualizarAmigosComMulta(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Amigos com Multa");

            RepositorioAmigo rep = (RepositorioAmigo)repositorio;

            Amigo[] amigos = rep.SelecionarAmigosComMulta();

            if (amigos.Length == 0)
                return false;

            for (int i = 0; i < amigos.Length; i++)
            {
                Amigo a = amigos[i];

                Console.WriteLine(a);

                Console.WriteLine("Multa: R$" + a.multa.Valor);

                Console.WriteLine();
            }

            return true;
        }

        public override Amigo ObterEntidade()
        {
            Console.Write("Digite o nome do amigo: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o nome do responsável: ");
            string nomeResponsavel = Console.ReadLine();

            Console.Write("Digite o número do telefone: ");
            string telefone = Console.ReadLine();

            Console.Write("Digite onde o amigo mora: ");
            string endereco = Console.ReadLine();

            Amigo amigo = new Amigo(nome, nomeResponsavel, telefone, endereco);

            return amigo;
        }
      
    }
}
