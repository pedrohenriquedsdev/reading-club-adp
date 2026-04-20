namespace ClubeDaLeitura.ConsoleApp.Dominio;

/*
    Regras de Negócio:
        ● Campos obrigatórios:
            ○ Nome (mínimo 3 caracteres, máximo 100)
            ○ Nome do responsável (mínimo 3 caracteres, máximo 100)
            ○ Telefone (formato validado: 10-11 dígitos)
        ● Não pode haver amigos com o mesmo nome e telefone
*/
public class Amigo : EntidadeBase
{
    public string Nome { get; set; } = string.Empty;
    public string NomeResponsavel { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;

    public Amigo(string nome, string nomeResponsavel, string telefone)
    {
        Nome = nome;
        NomeResponsavel = nomeResponsavel;
        Telefone = telefone;
    }

    public override string[] Validar()
    {
        string erros = string.Empty;

        if (string.IsNullOrWhiteSpace(Nome))
            erros += "O campo \"Nome\" é obrigatório;";
        else if (Nome.Length < 3 || Nome.Length > 100)
            erros += "O campo \"Nome\" deve conter entre 3 e 100 caracteres;";

        if (string.IsNullOrWhiteSpace(NomeResponsavel))
            erros += "O campo \"Nome do Responsável\" é obrigatório;";
        else if (NomeResponsavel.Length < 3 || NomeResponsavel.Length > 100)
            erros += "O campo \"Nome do Responsável\" deve conter entre 3 e 100 caracteres;";

        if (string.IsNullOrWhiteSpace(Telefone))
            erros += "O campo \"Telefone\" é obrigatório;";
        else
        {
            string telefoneSomenteDigitos = string.Empty;
            foreach (char c in Telefone)
                if (char.IsDigit(c))
                    telefoneSomenteDigitos += c;

            if (telefoneSomenteDigitos.Length < 10 || telefoneSomenteDigitos.Length > 11)
                erros += "O campo \"Telefone\" deve conter entre 10 e 11 dígitos;";
        }

        return erros.Split(';', StringSplitOptions.RemoveEmptyEntries);
    }

    public override void AtualizarRegistro(EntidadeBase entidadeAtualizada)
    {
        Amigo amigoAtualizado = (Amigo)entidadeAtualizada;

        Nome = amigoAtualizado.Nome;
        NomeResponsavel = amigoAtualizado.NomeResponsavel;
        Telefone = amigoAtualizado.Telefone;
    }
}