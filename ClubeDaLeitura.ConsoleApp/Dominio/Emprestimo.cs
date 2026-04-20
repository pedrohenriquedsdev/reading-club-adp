namespace ClubeDaLeitura.ConsoleApp.Dominio;

/*
    Regras de Negócio:
        ● Campos obrigatórios:
            ○ Amigo
            ○ Revista (disponível no momento)
            ○ Data empréstimo (automática)
            ○ Data devolução (calculada conforme caixa)
        ● Status possíveis: Aberto / Concluído / Atrasado
        ● Cada amigo só pode ter um empréstimo ativo por vez
        ● A data de devolução é calculada automaticamente (data empréstimo + dias da caixa)
*/
public class Emprestimo : EntidadeBase
{
    public Amigo Amigo { get; set; }
    public Revista Revista { get; set; }
    public DateTime DataEmprestimo { get; set; }
    public DateTime DataDevolucaoPrevista { get; set; }
    public DateTime? DataDevolucaoEfetiva { get; set; }
    public string Status { get; private set; } = "Aberto";

    public Emprestimo(Amigo amigo, Revista revista)
    {
        Amigo = amigo;
        Revista = revista;
        DataEmprestimo = DateTime.Now;
        DataDevolucaoPrevista = DataEmprestimo.AddDays(revista.Caixa.DiasDeEmprestimo);
    }

    public void AtualizarStatus()
    {
        if (DataDevolucaoEfetiva.HasValue)
        {
            Status = "Concluído";
        }
        else if (DateTime.Now > DataDevolucaoPrevista)
        {
            Status = "Atrasado";
        }
        else
        {
            Status = "Aberto";
        }
    }

    public void RegistrarDevolucao()
    {
        DataDevolucaoEfetiva = DateTime.Now;
        Revista.Status = "Disponível";
        AtualizarStatus();
    }

    public override string[] Validar()
    {
        string erros = string.Empty;

        if (Amigo == null)
            erros += "O campo \"Amigo\" é obrigatório;";

        if (Revista == null)
            erros += "O campo \"Revista\" é obrigatório;";
        else if (Revista.Status != "Disponível")
            erros += "A revista selecionada não está disponível para empréstimo;";

        return erros.Split(';', StringSplitOptions.RemoveEmptyEntries);
    }

    public override void AtualizarRegistro(EntidadeBase entidadeAtualizada)
    {
        // Empréstimos não são editados diretamente - apenas devolução é registrada
    }
}