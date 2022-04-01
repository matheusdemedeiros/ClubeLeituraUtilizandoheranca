using System;

namespace ClubeLeitura.ConsoleApp.Compartilhado
{
    public class TelaCadastro
    {
        protected readonly Notificador notificador;
        protected readonly Repositorio repositorio;
        protected readonly string titulo;
        protected string tipoDeCadastro;

        public TelaCadastro(Repositorio repositorio, Notificador notificador, string titulo)
        {
            this.repositorio = repositorio;
            this.notificador = notificador;
            this.titulo = titulo;
        }

        public virtual string MostrarOpcoes()
        {
            MostrarTitulo(titulo);

            Console.WriteLine("Digite 1 para Inserir");
            Console.WriteLine("Digite 2 para Editar");
            Console.WriteLine("Digite 3 para Excluir");
            Console.WriteLine("Digite 4 para Visualizar todos");
            Console.WriteLine("Digite s para sair");

            string opcao = Console.ReadLine();

            return opcao;
        }

        public virtual void InserirNovaEntidade(string titulo)
        {
            MostrarTitulo(titulo);

            EntidadeBase novaEntidade = ObterEntidade();

            repositorio.Inserir(novaEntidade);

            notificador.ApresentarMensagem("Entidade inserida com sucesso!", TipoMensagem.Sucesso);
        }

        public virtual void EditarEntidade(string titulo)
        {
            MostrarTitulo(titulo);

            bool temElementosCadastrados = VisualizarEntidades("Pesquisando","");

            if (temElementosCadastrados == false)
            {
                notificador.ApresentarMensagem("Nenhuma entidade cadastrada para poder editar", TipoMensagem.Atencao);
                
                return;
            }

            int idElemento = ObterNumeroEntidade();

            EntidadeBase objetoAtualizado = ObterEntidade();

            repositorio.Editar(idElemento, objetoAtualizado);

            notificador.ApresentarMensagem("Entidade editada com sucesso", TipoMensagem.Sucesso);
        }

        public virtual int ObterNumeroEntidade()
        {
            int numeroEntidade;
            
            bool numeroEntidadeEncontrado;
            
            do
            {
                Console.Write("Digite o número da entidade que deseja editar: ");
                numeroEntidade = Convert.ToInt32(Console.ReadLine());

                numeroEntidadeEncontrado = repositorio.VerificarNumeroEntidadeExiste(numeroEntidade);

                if (numeroEntidadeEncontrado == false)
                    notificador.ApresentarMensagem("Número da entidade não encontrado, digite novamente", TipoMensagem.Atencao);

            } while (numeroEntidadeEncontrado == false);
            
            return numeroEntidade;
        }

        public virtual void ExcluirEntidade(string titulo)
        {
            MostrarTitulo(titulo);

            bool temEntidadesCadastradas = VisualizarEntidades("Pesquisando","");

            if (temEntidadesCadastradas == false)
            {
                notificador.ApresentarMensagem(
                    "Nenhuma entidade cadastrada para poder excluir", TipoMensagem.Atencao);
                return;
            }

            int numeroEntidade = ObterNumeroEntidade();

            repositorio.Excluir(numeroEntidade);

            notificador.ApresentarMensagem("Entidade excluída com sucesso", TipoMensagem.Sucesso);
        }

        public virtual bool VisualizarEntidades(string tipo, string titulo)
        {
            if (tipo == "Tela")
                MostrarTitulo(titulo);

            EntidadeBase[] entidades = repositorio.SelecionarTodos();

            if (entidades.Length == 0)
                return false;

            for (int i = 0; i < entidades.Length; i++)
            {
                EntidadeBase entidade = entidades[i];

                Console.WriteLine(entidade.ToString());
                
                Console.WriteLine();
            }

            return true;
        }

        public virtual EntidadeBase ObterEntidade()
        {
            EntidadeBase entidade = new EntidadeBase();

            return entidade;
        }

        protected void MostrarTitulo(string titulo)
        {
            Console.Clear();

            Console.WriteLine(titulo);

            Console.WriteLine();
        }

    }
}

