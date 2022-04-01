using ClubeLeitura.ConsoleApp.Compartilhado;
using ClubeLeitura.ConsoleApp.ModuloAmigo;
using ClubeLeitura.ConsoleApp.ModuloRevista;
using System;

namespace ClubeLeitura.ConsoleApp.ModuloReserva
{
    public class Reserva : EntidadeBase
    {
        public Amigo amigo;
        public Revista revista;
        private DateTime dataInicialReserva;
        private bool estaAberta;

        public bool EstaAberta => estaAberta;

        public void Abrir()
        {
            if (!EstaAberta)
            {
                estaAberta = true;
                dataInicialReserva = DateTime.Today;
            }
        }

        public void Fechar()
        {
            if (EstaAberta)
                estaAberta = false;
        }

        public bool EstaExpirada()
        {
            bool ultrapassouDataReserva = dataInicialReserva.AddDays(2) > DateTime.Today;

            if (ultrapassouDataReserva)
                estaAberta = false;

            return ultrapassouDataReserva;
        }

        public override string ToString()
        {
            string statusReserva = EstaAberta ? "Aberta" : "Fechada";
            string retorno =
            "\nNúmero: " + numero +
            "\nRevista reservada: " + revista.Colecao +
            "\nNome do amigo: " + amigo.Nome +
            "\nData de expiração da Reserva: " + dataInicialReserva.AddDays(2).ToShortDateString() + 
            "\nStatus da reserva: " + statusReserva+ "\n";
            return retorno;
        }

    }
}
