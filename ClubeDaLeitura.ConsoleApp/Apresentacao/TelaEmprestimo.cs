using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Apresentacao;

public class TelaEmprestimo : TelaBase
{
    private RepositorioEmprestimo repositorioEmprestimo;
    private RepositorioAmigo repositorioAmigo;
    private RepositorioRevista repositorioRevista;

    public TelaEmprestimo(
        RepositorioEmprestimo rE,
        RepositorioAmigo rA,
        RepositorioRevista rR
    ) : base("Empréstimo", rE)
    {
        repositorioEmprestimo = rE;
        repositorioAmigo = rA;
        repositorioRevista = rR;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Empréstimos");

        repositorioEmprestimo.AtualizarStatusTodos();

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -20} | {3, -12} | {4, -12} | {5, -10}",
            "Id", "Amigo", "Revista", "Empréstimo", "Devolução", "Status"
        );

        EntidadeBase?[] emprestimos = repositorioEmprestimo.SelecionarTodas();

        bool temRegistro = false;

        for (int i = 0; i < emprestimos.Length; i++)
        {
            Emprestimo? e = (Emprestimo?)emprestimos[i];

            if (e == null)
                continue;

            temRegistro = true;

            if (e.Status == "Atrasado")
                Console.ForegroundColor = ConsoleColor.Red;
            else if (e.Status == "Concluído")
                Console.ForegroundColor = ConsoleColor.Green;
            else
                Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -20} | {3, -12} | {4, -12} | {5, -10}",
                e.Id,
                e.Amigo.Nome,
                e.Revista.Titulo,
                e.DataEmprestimo.ToString("dd/MM/yyyy"),
                e.DataDevolucaoPrevista.ToString("dd/MM/yyyy"),
                e.Status
            );

            Console.ResetColor();
        }

        if (!temRegistro)
            Console.WriteLine("Nenhum empréstimo cadastrado.");

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    public void VisualizarAbertos()
    {
        ExibirCabecalho("Empréstimos em Aberto");

        repositorioEmprestimo.AtualizarStatusTodos();

        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -20} | {3, -12} | {4, -12} | {5, -10}",
            "Id", "Amigo", "Revista", "Empréstimo", "Devolução", "Status"
        );

        EntidadeBase?[] emprestimos = repositorioEmprestimo.SelecionarTodas();
        bool temRegistro = false;

        for (int i = 0; i < emprestimos.Length; i++)
        {
            Emprestimo? e = (Emprestimo?)emprestimos[i];

            if (e == null || e.Status == "Concluído")
                continue;

            temRegistro = true;

            if (e.Status == "Atrasado")
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -20} | {3, -12} | {4, -12} | {5, -10}",
                e.Id,
                e.Amigo.Nome,
                e.Revista.Titulo,
                e.DataEmprestimo.ToString("dd/MM/yyyy"),
                e.DataDevolucaoPrevista.ToString("dd/MM/yyyy"),
                e.Status
            );

            Console.ResetColor();
        }

        if (!temRegistro)
            Console.WriteLine("Nenhum empréstimo em aberto.");

        Console.WriteLine("---------------------------------");
        Console.Write("Digite ENTER para continuar...");
        Console.ReadLine();
    }

    public void RegistrarEmprestimo()
    {
        ExibirCabecalho("Registrar Empréstimo");

        // Selecionar amigo
        Console.WriteLine("--- Amigos Disponíveis ---");
        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -15}",
            "Id", "Nome", "Telefone"
        );

        EntidadeBase?[] amigos = repositorioAmigo.SelecionarTodas();
        bool temAmigo = false;

        for (int i = 0; i < amigos.Length; i++)
        {
            Amigo? a = (Amigo?)amigos[i];

            if (a == null)
                continue;

            temAmigo = true;
            Console.WriteLine("{0, -7} | {1, -30} | {2, -15}", a.Id, a.Nome, a.Telefone);
        }

        if (!temAmigo)
        {
            ExibirMensagem("Nenhum amigo cadastrado. Cadastre um amigo antes de registrar empréstimos.");
            return;
        }

        Console.WriteLine("---------------------------------");

        string? idAmigo;

        do
        {
            Console.Write("Digite o ID do amigo: ");
            idAmigo = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idAmigo) && idAmigo.Length == 7)
                break;
        } while (true);

        Amigo? amigoSelecionado = (Amigo?)repositorioAmigo.SelecionarPorId(idAmigo);

        if (amigoSelecionado == null)
        {
            ExibirMensagem("Amigo não encontrado.");
            return;
        }

        if (repositorioEmprestimo.AmigoTemEmprestimoAtivo(idAmigo))
        {
            ExibirMensagem($"O amigo \"{amigoSelecionado.Nome}\" já possui um empréstimo ativo. Registre a devolução antes de fazer um novo empréstimo.");
            return;
        }

        // Selecionar revista disponível
        Console.WriteLine("---------------------------------");
        Console.WriteLine("--- Revistas Disponíveis ---");
        Console.WriteLine(
            "{0, -7} | {1, -25} | {2, -6} | {3, -4} | {4, -15} | {5, -5}",
            "Id", "Título", "Edição", "Ano", "Caixa", "Dias"
        );

        Revista?[] revistasDisponiveis = repositorioRevista.SelecionarDisponíveis();
        bool temRevista = false;

        for (int i = 0; i < revistasDisponiveis.Length; i++)
        {
            Revista? r = revistasDisponiveis[i];

            if (r == null)
                continue;

            temRevista = true;
            Console.WriteLine(
                "{0, -7} | {1, -25} | {2, -6} | {3, -4} | {4, -15} | {5, -5}",
                r.Id, r.Titulo, r.NumeroEdicao, r.AnoPublicacao, r.Caixa.Etiqueta, r.Caixa.DiasDeEmprestimo
            );
        }

        if (!temRevista)
        {
            ExibirMensagem("Nenhuma revista disponível no momento.");
            return;
        }

        Console.WriteLine("---------------------------------");

        string? idRevista;

        do
        {
            Console.Write("Digite o ID da revista: ");
            idRevista = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idRevista) && idRevista.Length == 7)
                break;
        } while (true);

        Revista? revistaSelecionada = (Revista?)repositorioRevista.SelecionarPorId(idRevista);

        if (revistaSelecionada == null || revistaSelecionada.Status != "Disponível")
        {
            ExibirMensagem("Revista não encontrada ou não disponível.");
            return;
        }

        Emprestimo novoEmprestimo = new Emprestimo(amigoSelecionado, revistaSelecionada);

        repositorioEmprestimo.Cadastrar(novoEmprestimo);
        revistaSelecionada.Status = "Emprestada";

        Console.WriteLine("---------------------------------");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Empréstimo registrado com sucesso!");
        Console.WriteLine($"Amigo: {amigoSelecionado.Nome}");
        Console.WriteLine($"Revista: {revistaSelecionada.Titulo}");
        Console.WriteLine($"Data Empréstimo: {novoEmprestimo.DataEmprestimo:dd/MM/yyyy}");
        Console.WriteLine($"Data Devolução Prevista: {novoEmprestimo.DataDevolucaoPrevista:dd/MM/yyyy}");
        Console.ResetColor();
        Console.WriteLine("---------------------------------");
        Console.Write("Digite ENTER para continuar...");
        Console.ReadLine();
    }

    public void RegistrarDevolucao()
    {
        ExibirCabecalho("Registrar Devolução");

        repositorioEmprestimo.AtualizarStatusTodos();

        Console.WriteLine("--- Empréstimos em Aberto ---");
        Console.WriteLine(
            "{0, -7} | {1, -20} | {2, -20} | {3, -12} | {4, -10}",
            "Id", "Amigo", "Revista", "Devolução Prev.", "Status"
        );

        EntidadeBase?[] emprestimos = repositorioEmprestimo.SelecionarTodas();
        bool temAberto = false;

        for (int i = 0; i < emprestimos.Length; i++)
        {
            Emprestimo? e = (Emprestimo?)emprestimos[i];

            if (e == null || e.Status == "Concluído")
                continue;

            temAberto = true;

            if (e.Status == "Atrasado")
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(
                "{0, -7} | {1, -20} | {2, -20} | {3, -12} | {4, -10}",
                e.Id,
                e.Amigo.Nome,
                e.Revista.Titulo,
                e.DataDevolucaoPrevista.ToString("dd/MM/yyyy"),
                e.Status
            );

            Console.ResetColor();
        }

        if (!temAberto)
        {
            ExibirMensagem("Nenhum empréstimo em aberto para devolução.");
            return;
        }

        Console.WriteLine("---------------------------------");

        string? idEmprestimo;

        do
        {
            Console.Write("Digite o ID do empréstimo para registrar devolução: ");
            idEmprestimo = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idEmprestimo) && idEmprestimo.Length == 7)
                break;
        } while (true);

        Emprestimo? emprestimoSelecionado = (Emprestimo?)repositorioEmprestimo.SelecionarPorId(idEmprestimo);

        if (emprestimoSelecionado == null)
        {
            ExibirMensagem("Empréstimo não encontrado.");
            return;
        }

        if (emprestimoSelecionado.Status == "Concluído")
        {
            ExibirMensagem("Este empréstimo já foi concluído.");
            return;
        }

        emprestimoSelecionado.RegistrarDevolucao();

        Console.WriteLine("---------------------------------");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Devolução registrada com sucesso!");
        Console.WriteLine($"Amigo: {emprestimoSelecionado.Amigo.Nome}");
        Console.WriteLine($"Revista: {emprestimoSelecionado.Revista.Titulo}");
        Console.WriteLine($"Data Devolução: {emprestimoSelecionado.DataDevolucaoEfetiva:dd/MM/yyyy}");
        Console.ResetColor();
        Console.WriteLine("---------------------------------");
        Console.Write("Digite ENTER para continuar...");
        Console.ReadLine();
    }

    public string? ObterOpcaoMenuEmprestimo()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Gestão de Empréstimos");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Registrar empréstimo");
        Console.WriteLine("2 - Registrar devolução");
        Console.WriteLine("3 - Visualizar empréstimos em aberto");
        Console.WriteLine("4 - Visualizar todos os empréstimos");
        Console.WriteLine("S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        return Console.ReadLine()?.ToUpper();
    }

    protected override EntidadeBase ObterDadosCadastrais()
    {
        // Não utilizado diretamente — lógica encapsulada em RegistrarEmprestimo()
        throw new NotImplementedException("Use o método RegistrarEmprestimo() para criar empréstimos.");
    }
}