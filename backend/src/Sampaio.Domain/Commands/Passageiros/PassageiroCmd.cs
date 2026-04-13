namespace Sampaio.Domain.Commands.Passageiros
{
    public class PassageiroCmd
    {
        public int Assento { get; set; }

        public string Nome { get; set; }

        public string Documento { get; set; }

        public string Telefone { get; set; }
        
        public bool Comprador { get; set; } = false;
    }
}