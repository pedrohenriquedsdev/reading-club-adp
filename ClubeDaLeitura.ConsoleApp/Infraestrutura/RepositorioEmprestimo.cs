using ClubeDaLeitura.ConsoleApp.Dominio;

namespace ClubeDaLeitura.ConsoleApp.Infraestrutura;

public class RepositorioEmprestimo : RepositorioBase
{
    public bool AmigoTemEmprestimoAtivo(string amigoId)
    {
        for (int i = 0; i < registros.Length; i++)
        {
            Emprestimo? emprestimo = (Emprestimo?)registros[i];

            if (emprestimo == null)
                continue;

            if (emprestimo.Amigo.Id == amigoId &&
                (emprestimo.Status == "Aberto" || emprestimo.Status == "Atrasado"))
                return true;
        }

        return false;
    }

    public bool AmigoTemEmprestimoVinculado(string amigoId)
    {
        for (int i = 0; i < registros.Length; i++)
        {
            Emprestimo? emprestimo = (Emprestimo?)registros[i];

            if (emprestimo == null)
                continue;

            if (emprestimo.Amigo.Id == amigoId)
                return true;
        }

        return false;
    }

    public Emprestimo?[] SelecionarPorAmigo(string amigoId)
    {
        Emprestimo?[] resultado = new Emprestimo[100];
        int index = 0;

        for (int i = 0; i < registros.Length; i++)
        {
            Emprestimo? emprestimo = (Emprestimo?)registros[i];

            if (emprestimo == null)
                continue;

            if (emprestimo.Amigo.Id == amigoId)
            {
                resultado[index] = emprestimo;
                index++;
            }
        }

        return resultado;
    }

    public void AtualizarStatusTodos()
    {
        for (int i = 0; i < registros.Length; i++)
        {
            Emprestimo? emprestimo = (Emprestimo?)registros[i];

            if (emprestimo == null)
                continue;

            emprestimo.AtualizarStatus();
        }
    }
}