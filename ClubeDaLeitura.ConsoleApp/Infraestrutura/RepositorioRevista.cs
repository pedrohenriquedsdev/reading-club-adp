using ClubeDaLeitura.ConsoleApp.Dominio;

namespace ClubeDaLeitura.ConsoleApp.Infraestrutura;

public class RepositorioRevista : RepositorioBase
{
    public bool CaixaTemRevistasVinculadas(string caixaId)
    {
        for (int i = 0; i < registros.Length; i++)
        {
            Revista? revista = (Revista?)registros[i];

            if (revista == null)
                continue;

            if (revista.Caixa.Id == caixaId)
                return true;
        }

        return false;
    }

    public Revista?[] SelecionarDisponíveis()
    {
        Revista?[] resultado = new Revista[100];
        int index = 0;

        for (int i = 0; i < registros.Length; i++)
        {
            Revista? revista = (Revista?)registros[i];

            if (revista == null)
                continue;

            if (revista.Status == "Disponível")
            {
                resultado[index] = revista;
                index++;
            }
        }

        return resultado;
    }
}