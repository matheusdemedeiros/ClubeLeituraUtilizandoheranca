using ClubeLeitura.ConsoleApp.Compartilhado;

namespace ClubeLeitura.ConsoleApp.ModuloCaixa
{
    public class Caixa : EntidadeBase
    {
        private readonly string cor;
        private readonly string etiqueta;

        public string Cor => cor;

        public string Etiqueta => etiqueta;

        public Caixa(string cor, string etiqueta)
        {
            this.cor = cor;
            this.etiqueta = etiqueta;
        }

        public override string ToString()
        {
            string retorno = "\nNúmero: " + numero + "\nCor: " + cor + "\nEtiqueta: " + etiqueta + "\n";

            return retorno;
        }

    }
}
