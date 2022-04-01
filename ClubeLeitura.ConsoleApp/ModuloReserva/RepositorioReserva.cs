using ClubeLeitura.ConsoleApp.Compartilhado;

namespace ClubeLeitura.ConsoleApp.ModuloReserva
{
    public class RepositorioReserva : Repositorio
    {
        public RepositorioReserva(int qtdReservas) : base(qtdReservas)
        {
        }

        public override void Inserir(EntidadeBase entidadeReserva)
        {
            Reserva reserva = (Reserva)entidadeReserva;

            reserva.numero = ++numeroEntidade;

            reserva.Abrir();
            
            reserva.revista.RegistrarReserva(reserva);
            
            reserva.amigo.RegistrarReserva(reserva);

            elementos[ObterPosicaoVazia()] = reserva;
        }

        public override Reserva[] SelecionarTodos()
        {
            Reserva[] reservasInseridas = new Reserva[ObterQtdElementos()];

            int j = 0;

            for (int i = 0; i < reservasInseridas.Length; i++)
            {
                if (elementos[i] != null)
                {
                    reservasInseridas[j] = (Reserva)elementos[i];
                    j++;
                }
            }

            return reservasInseridas;
        }

        public Reserva[] SelecionarReservasEmAberto()
        {
            Reserva[] reservasInseridas = new Reserva[ObterQtdReservasEmAberto()];

            int j = 0;

            for (int i = 0; i < reservasInseridas.Length; i++)
            {
                if (elementos[i] != null)
                {
                    Reserva reserva = (Reserva)elementos[i];

                    if (reserva.EstaAberta)
                    {
                        reservasInseridas[j] = reserva;

                        j++;
                    }
                }
            }

            return reservasInseridas;
        }

        public override Reserva SelecionarEntidade(int numeroReserva)
        {
            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] != null && numeroReserva == elementos[i].numero)
                    return (Reserva)elementos[i];
            }

            return null;
        }

        public int ObterQtdReservasEmAberto()
        {
            int numeroReservas = 0;

            for (int i = 0; i < elementos.Length; i++)
            {
                if (elementos[i] != null)
                {
                    Reserva reserva = (Reserva)elementos[i];
                    
                    if (reserva.EstaAberta)
                        numeroReservas++;
                }
            }

            return numeroReservas;
        }

    }
}
