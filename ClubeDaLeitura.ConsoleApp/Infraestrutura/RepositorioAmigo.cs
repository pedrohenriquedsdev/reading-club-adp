using ClubeDaLeitura.ConsoleApp.Dominio;

namespace ClubeDaLeitura.ConsoleApp.Infraestrutura;

public class RepositorioAmigo : RepositorioBase
{
    public bool ExisteDuplicado(string nome, string telefone, string? idIgnorar = null)
    {
        for (int i = 0; i < registros.Length; i++)
        {
            Amigo? amigo = (Amigo?)registros[i];

            if (amigo == null)
                continue;

            if (idIgnorar != null && amigo.Id == idIgnorar)
                continue;

            string telefoneLimpo = LimparTelefone(telefone);
            string telefoneCadastrado = LimparTelefone(amigo.Telefone);

            if (amigo.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase) &&
                telefoneCadastrado == telefoneLimpo)
                return true;
        }

        return false;
    }

    private string LimparTelefone(string telefone)
    {
        string resultado = string.Empty;
        foreach (char c in telefone)
            if (char.IsDigit(c))
                resultado += c;
        return resultado;
    }
}