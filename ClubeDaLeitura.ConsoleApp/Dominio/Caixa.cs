namespace ClubeDaLeitura.ConsoleApp.Dominio;

/*
    ● Campos obrigatórios:
        ○ Etiqueta (texto único, máximo 50 caracteres)
        ○ Cor (seleção de paleta ou hexadecimal)
        ○ Dias de empréstimo (número, padrão 7)
    ● Não pode haver etiquetas duplicadas
    ● Não permitir excluir uma caixa caso tenha revistas vinculadas
    ● Cada caixa define o prazo máximo para empréstimo de suas revistas
*/
// Encapsulamento
public class Caixa : EntidadeBase
{
    public string Etiqueta { get; set; } = string.Empty;
    public string Cor { get; set; } = string.Empty;
    public int DiasDeEmprestimo { get; set; } = 7;

    // construtor de classe
    // toda instância que for criada PRECISA dessas informações
    public Caixa(string etiqueta, string cor, int diasDeEmprestimo)
    {
        Etiqueta = etiqueta;
        Cor = cor;
        DiasDeEmprestimo = diasDeEmprestimo;
    }

    public override string[] Validar()
    {
        string erros = string.Empty;

        if (string.IsNullOrWhiteSpace(Etiqueta))
        {
            erros += "O campo \"Etiqueta\" é obrigatório;";
        }

        else if (Etiqueta.Length > 50)
        {
            erros += "O campo \"Etiqueta\" deve conter no máximo 50 caracteres;";
        }

        if (DiasDeEmprestimo < 1)
        {
            erros += "O campo \"Dias de Empréstimo\" deve conter um valor maior que 0;";
        }

        return erros.Split(';', StringSplitOptions.RemoveEmptyEntries); // separar
    }

    // substituição = implementação do método abstração
    public override void AtualizarRegistro(EntidadeBase entidadeAtualizada)
    {
        // (Caixa) = confia
        Caixa caixaAtualizada = (Caixa)entidadeAtualizada; // cast / conversão de tipo

        Etiqueta = caixaAtualizada.Etiqueta;
        Cor = caixaAtualizada.Cor;
        DiasDeEmprestimo = caixaAtualizada.DiasDeEmprestimo;
    }
}