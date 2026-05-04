using ClubeDaLeitura.ConsoleApp.Apresentacao.Base;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Apresentacao;

public class TelaAmigo : TelaBase
{
    private RepositorioAmigo repositorioAmigo;

    public TelaAmigo(RepositorioAmigo repositorioAmigo) : base("Amigo", repositorioAmigo)
    {
        this.repositorioAmigo = repositorioAmigo;
    }

    public override string? ObterOpcaoMenu()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de Amigos");
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"1 - Cadastrar amigo");
        Console.WriteLine($"2 - Editar amigo");
        Console.WriteLine($"3 - Excluir amigo");
        Console.WriteLine($"4 - Visualizar amigos");
        Console.WriteLine($"5 - Visualizar multas de um amigo");
        Console.WriteLine("S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenu = Console.ReadLine()?.ToUpper();

        return opcaoMenu;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Amigos");

        Console.WriteLine(
            "{0, -7} | {1, -15} | {2, -15} | {3, -13}",
            "Id", "Nome", "Responsável", "Telefone"
        );

        EntidadeBase?[] amigos = repositorioAmigo.SelecionarTodos();

        for (int i = 0; i < amigos.Length; i++)
        {
            Amigo? a = (Amigo?)amigos[i];

            if (a == null)
                continue;

            Console.WriteLine(
                "{0, -7} | {1, -15} | {2, -15} | {3, -13}",
                a.Id, a.Nome, a.NomeResponsavel, a.Telefone
            );
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    protected override EntidadeBase ObterDadosCadastrais()
    {
        Console.Write("Digite o nome: ");
        string nome = Console.ReadLine() ?? string.Empty; // null coalescing operator

        Console.Write("Digite o nome do responsável: ");
        string nomeResponsavel = Console.ReadLine() ?? string.Empty;

        Console.Write("Digite o telefone: ");
        string telefone = Console.ReadLine() ?? string.Empty;

        return new Amigo(nome, nomeResponsavel, telefone);
    }

    public void VisualizarMultas()
    {
        Amigo? amigoSelecionado = null;

        do
        {
            ExibirCabecalho("Visualização de Multas de Amigo");

            VisualizarTodos(deveExibirCabecalho: false);

            Console.WriteLine("---------------------------------");

            string? idSelecionado;

            Console.Write("Digite o ID do registro que deseja visualizar (ou S para sair): ");
            idSelecionado = Console.ReadLine() ?? string.Empty;

            if (idSelecionado.ToUpper() == "S")
                return;

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                amigoSelecionado = (Amigo?)repositorioAmigo.SelecionarPorId(idSelecionado);

            if (amigoSelecionado != null)
                break;
        } while (true);

        Console.WriteLine("---------------------------------");

        Console.WriteLine(
            "{0, -7} | {1, -12} | {2, -7} | {3, -20} | {4, -10}",
            "Id", "Ocorrência", "Valor", "Revista", "Status"
        );

        Multa?[] multas = amigoSelecionado.Multas;

        for (int i = 0; i < multas.Length; i++)
        {
            Multa? m = multas[i];

            if (m == null)
                continue;

            Console.WriteLine(
                "{0, -7} | {1, -12} | {2, -7} | {3, -20} | {4, -10}",
                m.Id,
                m.DataOcorrencia.ToShortDateString(),
                m.Valor.ToString("C2"),
                m.Emprestimo.Revista.Titulo,
                m.Status
            );
        }

        Console.WriteLine("---------------------------------");
        Console.WriteLine("Digite ENTER para continuar...");
        Console.ReadLine();
    }
}