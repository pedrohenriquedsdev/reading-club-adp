using ClubeDaLeitura.ConsoleApp.Dominio;

namespace ClubeDaLeitura.ConsoleApp.Infraestrutura;

public class RepositorioReserva
{
    private Reserva?[] reservas = new Reserva[100];

    public void Cadastrar(Reserva emprestimo)
    {
        for (int i = 0; i < reservas.Length; i++)
        {
            if (reservas[i] == null)
            {
                reservas[i] = emprestimo;
                break;
            }
        }
    }

    public Reserva?[] SelecionarTodos()
    {
        return reservas;
    }

    public Reserva? SelecionarPorId(string idSelecionado)
    {
        for (int i = 0; i < reservas.Length; i++)
        {
            Reserva? e = reservas[i];

            if (e == null)
                continue;

            if (e.Id == idSelecionado)
                return e;
        }

        return null;
    }
}