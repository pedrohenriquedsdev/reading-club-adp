using System.Security.Cryptography;

namespace ClubeDaLeitura.ConsoleApp.Dominio
{
    public class Caixa
    {
        public string Id { get; set; } = string.Empty;
        public string Etiqueta { get; set; } = string.Empty; //propiedade
        public string Cor { get; set; } = string.Empty; //propiedade
        public int DiasDeEmprestimo { get; set; } = 7; //propiedade

        //construtor de classe 
        //toda instância que for criada precisa dessas informações 
        public Caixa(string etiqueta, string cor, int diasDeEmprestimo) 
        {
            Id = Convert
                .ToHexString(RandomNumberGenerator.GetBytes(20))
                .ToLower()
                .Substring(0, 7);

            Etiqueta = etiqueta;
            Cor = cor;
            DiasDeEmprestimo = diasDeEmprestimo;
        }
    }
}
