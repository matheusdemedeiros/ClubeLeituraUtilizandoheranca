using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloRevista;
using System;

namespace ClubeLeitura.ConsoleApp.ModuloEmprestimo
{
    public class TelaCadastroEmprestimo : TelaCadastro
    {
        private readonly RepositorioRevista repositorioRevista;
        private readonly RepositorioAmigo repositorioAmigo;
        private readonly TelaCadastroRevista telaCadastroRevista;
        private readonly TelaCadastroAmigo telaCadastroAmigo;

        public TelaCadastroEmprestimo(
            Notificador notificador,
            RepositorioEmprestimo repositorioEmprestimo,
            RepositorioRevista repositorioRevista,
            RepositorioAmigo repositorioAmigo,
            TelaCadastroRevista telaCadastroRevista,
            TelaCadastroAmigo telaCadastroAmigo,
            string titulo) : base(repositorioEmprestimo, notificador, titulo)
        {
            this.repositorioRevista = repositorioRevista;
            this.repositorioAmigo = repositorioAmigo;
            this.telaCadastroRevista = telaCadastroRevista;
            this.telaCadastroAmigo = telaCadastroAmigo;
        }

        public override string MostrarOpcoes()
        {
            MostrarTitulo(titulo);

            Console.WriteLine("Digite 1 para Registrar Empréstimo");
            Console.WriteLine("Digite 2 para Editar Empréstimo");
            Console.WriteLine("Digite 3 para Excluir Empréstimo");
            Console.WriteLine("Digite 4 para Visualizar");
            Console.WriteLine("Digite 5 para Visualizar Empréstimos em Aberto");
            Console.WriteLine("Digite 6 para Devolver um empréstimo");
            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public override void InserirNovaEntidade(string titulo)
        {
            MostrarTitulo(titulo);

            // Validação do Amigo
            Amigo amigoSelecionado = ObtemAmigo();

            if (amigoSelecionado.TemMultaEmAberto())
            {
                notificador.ApresentarMensagem("Este amigo tem uma multa em aberto.", TipoMensagem.Erro);
                return;
            }

            if (amigoSelecionado.TemEmprestimoEmAberto())
            {
                notificador.ApresentarMensagem("Este amigo já tem um empréstimo em aberto.", TipoMensagem.Erro);
                return;
            }


            // Validação da Revista
            Revista revistaSelecionada = ObtemRevista();

            if (revistaSelecionada.EstaReservada())
            {
                notificador.ApresentarMensagem("A revista selecionada já está reservada!", TipoMensagem.Erro);
                return;
            }

            if (revistaSelecionada.EstaEmprestada())
            {
                notificador.ApresentarMensagem("A revista selecionada já foi emprestada.", TipoMensagem.Erro);
                return;
            }

            Emprestimo emprestimo = ObtemEmprestimo(amigoSelecionado, revistaSelecionada);

            repositorio.Inserir(emprestimo);

            notificador.ApresentarMensagem("Empréstimo inserido com sucesso", TipoMensagem.Sucesso);
        }

        public void RegistrarDevolucao()
        {
            MostrarTitulo("Devolvendo Empréstimo");

            bool temEmprestimos = VisualizarEmprestimosEmAberto("Pesquisando");

            if (!temEmprestimos)
            {
                notificador.ApresentarMensagem("Nenhum empréstimo disponível para devolução.", TipoMensagem.Atencao);
                return;
            }

            int numeroEmprestimo = ObterNumeroEntidade();

            Emprestimo emprestimoParaDevolver = (Emprestimo)repositorio.SelecionarEntidade(numeroEmprestimo);

            if (!emprestimoParaDevolver.estaAberto)
            {
                notificador.ApresentarMensagem("O empréstimo selecionado não está mais aberto.", TipoMensagem.Atencao);
                return;
            }

            RepositorioEmprestimo rep = (RepositorioEmprestimo)repositorio;

            rep.RegistrarDevolucao(emprestimoParaDevolver);

            if (emprestimoParaDevolver.amigo.TemMultaEmAberto())
            {
                decimal multa = emprestimoParaDevolver.amigo.multa.Valor;

                notificador.ApresentarMensagem($"A devolução está atrasada, uma multa de R${multa} foi incluída.", TipoMensagem.Atencao);
            }

            notificador.ApresentarMensagem("Devolução concluída com sucesso!", TipoMensagem.Sucesso);
        }

        public override void EditarEntidade(string titulo)
        {
            MostrarTitulo(titulo);

            bool temEmprestimosCadastrados = VisualizarEntidades("Pesquisando","");

            if (temEmprestimosCadastrados == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhuma empréstimo cadastrado para poder editar", TipoMensagem.Atencao);
                return;
            }
            int numeroEmprestimo = ObterNumeroEntidade();

            Amigo amigoSelecionado = ObtemAmigo();

            Revista revistaSelecionada = ObtemRevista();

            Emprestimo emprestimoAtualizado = ObtemEmprestimo(amigoSelecionado, revistaSelecionada);

            repositorio.Editar(numeroEmprestimo, emprestimoAtualizado);

            notificador.ApresentarMensagem("Empréstimo editado com sucesso", TipoMensagem.Sucesso);
        }

        public bool VisualizarEmprestimosEmAberto(string tipo)
        {
            if (tipo == "Tela")
                MostrarTitulo("Visualização de Empréstimos em Aberto");

            RepositorioEmprestimo rep = (RepositorioEmprestimo)repositorio;

            Emprestimo[] emprestimos = rep.SelecionarEmprestimosAbertos();

            if (emprestimos.Length == 0)
                return false;

            for (int i = 0; i < emprestimos.Length; i++)
            {
                Emprestimo emprestimo = emprestimos[i];

                Console.WriteLine(emprestimo.ToString());

                Console.WriteLine();
            }

            return true;
        }

        #region Métodos privados

        private Emprestimo ObtemEmprestimo(Amigo amigo, Revista revista)
        {
            Emprestimo novoEmprestimo = new Emprestimo();

            novoEmprestimo.amigo = amigo;
            
            novoEmprestimo.revista = revista;

            return novoEmprestimo;
        }

        private Amigo ObtemAmigo()
        {
            bool temAmigosDisponiveis = telaCadastroAmigo.VisualizarEntidades("Pesquisando","");

            if (!temAmigosDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhum amigo disponível para cadastrar empréstimos.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número do amigo que irá pegar o empréstimo: ");
            int numeroAmigoEmprestimo = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Amigo amigoSelecionado = repositorioAmigo.SelecionarEntidade(numeroAmigoEmprestimo);

            return amigoSelecionado;
        }

        private Revista ObtemRevista()
        {
            bool temRevistasDisponiveis = telaCadastroRevista.VisualizarEntidades("Pesquisando", "");

            if (!temRevistasDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhuma revista disponível para cadastrar empréstimos.", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número da revista que irá será emprestada: ");
            int numeroRevistaEmprestimo = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Revista revistaSelecionada = repositorioRevista.SelecionarEntidade(numeroRevistaEmprestimo);

            return revistaSelecionada;
        }

        #endregion

    }
}
