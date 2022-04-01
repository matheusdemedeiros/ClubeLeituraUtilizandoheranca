﻿using ClubeLeitura.ConsoleApp.Compartilhado;
using System;

namespace ClubeLeitura.ConsoleApp.ModuloCategoria
{
    public class TelaCadastroCategoria : TelaCadastro
    {
        public TelaCadastroCategoria(Repositorio repositorio, Notificador notificador, string titulo) : base(repositorio, notificador, titulo)
        {
        }
        
        public override Categoria ObterEntidade()
        {
            Console.Write("Digite o nome da categoria: ");
            string nome = Console.ReadLine();

            Console.Write("Digite o limite de dias de empréstimo das revistas: ");
            int diasEmprestimo = Convert.ToInt32(Console.ReadLine());

            Categoria novaCategoria = new Categoria(nome, diasEmprestimo);

            return novaCategoria;
        }
    }
}