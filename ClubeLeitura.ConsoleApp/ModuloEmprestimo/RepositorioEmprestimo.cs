using ClubeLeitura.ConsoleApp.Compartilhado;

namespace ClubeLeitura.ConsoleApp.ModuloEmprestimo
{
    public class RepositorioEmprestimo : Repositorio
    {

        public RepositorioEmprestimo(int qtdEmprestimos) : base(qtdEmprestimos)
        {
        }

        public override void Inserir(EntidadeBase entidadeEmprestimo)
        {
            Emprestimo emprestimo = (Emprestimo)entidadeEmprestimo;

            emprestimo.numero = ++numeroEntidade;

            emprestimo.Abrir();

            emprestimo.revista.RegistrarEmprestimo(emprestimo);

            emprestimo.amigo.RegistrarEmprestimo(emprestimo);

            elementos[ObterPosicaoVazia()] = emprestimo;
        }

        public bool RegistrarDevolucao(Emprestimo emprestimo)
        {
            emprestimo.Fechar();

            return true;
        }

        public override Emprestimo[] SelecionarTodos()
        {
            Emprestimo[] emprestimosInseridos = new Emprestimo[ObterQtdElementos()];

            int j = 0;

            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] != null)
                {
                    emprestimosInseridos[j] = (Emprestimo)elementos[i];

                    j++;
                }
            }

            return emprestimosInseridos;
        }

        public Emprestimo[] SelecionarEmprestimosAbertos()
        {
            Emprestimo[] emprestimosAbertos = new Emprestimo[ObterQtdEmprestimosAbertos()];

            int j = 0;

            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] != null)
                {
                    Emprestimo emprestimo = (Emprestimo)elementos[i];

                    if (emprestimo.estaAberto)
                    {
                        emprestimosAbertos[j] = emprestimo;

                        j++;
                    }
                }
            }

            return emprestimosAbertos;
        }

        public override Emprestimo SelecionarEntidade(int numeroEmprestimo)
        {
            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] != null && numeroEmprestimo == elementos[i].numero)
                    return (Emprestimo)elementos[i];
            }

            return null;
        }


        public int ObterQtdEmprestimosAbertos()
        {
            int numeroEmprestimos = 0;

            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] != null)
                {
                    Emprestimo emprestimo = (Emprestimo)elementos[i];

                    if (emprestimo.estaAberto)
                        numeroEmprestimos++;
                }
            }

            return numeroEmprestimos;
        }
        
    }
}
