using ClubeDaLeitura.ConsoleApp.Dominio;

namespace ClubeDaLeitura.ConsoleApp.Infraestrutura;

public class RepositorioCaixa : RepositorioBase
{
    public bool EtiquetaJaExiste(string etiqueta, string? idIgnorar = null)
    {
        for (int i = 0; i < registros.Length; i++)
        {
            Caixa? caixa = (Caixa?)registros[i];

            if (caixa == null)
                continue;

            if (idIgnorar != null && caixa.Id == idIgnorar)
                continue;

            if (caixa.Etiqueta.Equals(etiqueta, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }
}