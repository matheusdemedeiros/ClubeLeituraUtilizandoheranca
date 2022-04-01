using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.ModuloCaixa;
using ClubeLeitura.ConsoleApp.ModuloCategoria;
using System;

namespace ClubeLeitura.ConsoleApp.ModuloRevista
{
    public class TelaCadastroRevista : TelaCadastro
    {
        private readonly TelaCadastroCategoria telaCadastroCategoria;
        private readonly RepositorioCategoria repositorioCategoria;
        private readonly TelaCadastroCaixa telaCadastroCaixa;
        private readonly RepositorioCaixa repositorioCaixa;

        public TelaCadastroRevista(
            TelaCadastroCategoria telaCadastroCategoria,
            RepositorioCategoria repositorioCategoria,
            TelaCadastroCaixa telaCadastroCaixa,
            RepositorioCaixa repositorioCaixa,
            RepositorioRevista repositorioRevista,
            Notificador notificador, string titulo) : base(repositorioRevista, notificador, titulo)
        {
            this.telaCadastroCategoria = telaCadastroCategoria;
            this.repositorioCategoria = repositorioCategoria;
            this.telaCadastroCaixa = telaCadastroCaixa;
            this.repositorioCaixa = repositorioCaixa;
        }


        public override void InserirNovaEntidade(string titulo)
        {
            MostrarTitulo(titulo);

            Caixa caixaSelecionada = ObtemCaixa();

            Categoria categoriaSelecionada = ObtemCategoria();

            if (caixaSelecionada == null || categoriaSelecionada == null)
            {
                notificador
                    .ApresentarMensagem("Cadastre uma caixa e uma categoria antes de cadastrar revistas!", TipoMensagem.Atencao);
                return;
            }

            Revista novaRevista = ObterEntidade();

            novaRevista.caixa = caixaSelecionada;

            novaRevista.categoria = categoriaSelecionada;

            RepositorioRevista rep = (RepositorioRevista)repositorio;
            
            string statusValidacao = rep.Inserir(novaRevista);

            if (statusValidacao != "REGISTRO_VALIDO")
                notificador.ApresentarMensagem(statusValidacao, TipoMensagem.Erro);
            else
                notificador.ApresentarMensagem("Revista inserida com sucesso", TipoMensagem.Sucesso);
        }

        public override void EditarEntidade(string titulo)
        {
            MostrarTitulo(titulo);

            bool temRevistasCadastradas = VisualizarEntidades("Pesquisando", "Editando revista");

            if (temRevistasCadastradas == false)
            {
                notificador.ApresentarMensagem("Nenhuma revista cadastrada para poder editar", TipoMensagem.Atencao);
                return;
            }

            int numeroRevista = ObterNumeroEntidade();

            Console.WriteLine();

            Caixa caixaSelecionada = ObtemCaixa();

            Revista revistaAtualizada = ObterEntidade();

            revistaAtualizada.caixa = caixaSelecionada;

            RepositorioRevista rep = (RepositorioRevista)repositorio;
            
            rep.Editar(numeroRevista, revistaAtualizada);

            notificador.ApresentarMensagem("Revista editada com sucesso", TipoMensagem.Sucesso);
        }

        public override Revista ObterEntidade()
        {
            Console.Write("Digite a coleção da revista: ");
            string colecao = Console.ReadLine();

            Console.Write("Digite a edição da revista: ");
            int edicao = Convert.ToInt32(Console.ReadLine());

            Console.Write("Digite o ano da revista: ");
            int ano = Convert.ToInt32(Console.ReadLine());

            Revista novaRevista = new Revista(colecao, edicao, ano);

            return novaRevista;
        }

        private Categoria ObtemCategoria()
        {
            bool temCategoriasDisponiveis = telaCadastroCategoria.VisualizarEntidades("","");

            if (!temCategoriasDisponiveis)
            {
                notificador.ApresentarMensagem("Você precisa cadastrar uma categoria antes de uma revista!", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número da categoria da revista: ");
            int numCategoriaSelecionada = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Categoria categoriaSelecionada = repositorioCategoria.SelecionarEntidade(numCategoriaSelecionada);

            return categoriaSelecionada;
        }

        private Caixa ObtemCaixa()
        {
            bool temCaixasDisponiveis = telaCadastroCaixa.VisualizarEntidades("","");

            if (!temCaixasDisponiveis)
            {
                notificador.ApresentarMensagem("Não há nenhuma caixa disponível para cadastrar revistas", TipoMensagem.Atencao);
                return null;
            }

            Console.Write("Digite o número da caixa que irá inserir: ");
            
            int numCaixaSelecionada = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine();

            Caixa caixaSelecionada = repositorioCaixa.SelecionarEntidade(numCaixaSelecionada);

            return caixaSelecionada;
        }

    }
}
