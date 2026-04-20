using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Apresentacao;

public class TelaAmigo : TelaBase
{
    private RepositorioAmigo repositorioAmigo;
    private RepositorioEmprestimo repositorioEmprestimo;

    public TelaAmigo(RepositorioAmigo rA, RepositorioEmprestimo rE) : base("Amigo", rA)
    {
        repositorioAmigo = rA;
        repositorioEmprestimo = rE;
    }

    public override void VisualizarTodos(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Amigos");

        Console.WriteLine(
            "{0, -7} | {1, -30} | {2, -30} | {3, -15}",
            "Id", "Nome", "Responsável", "Telefone"
        );

        EntidadeBase?[] amigos = repositorioAmigo.SelecionarTodas();

        bool temRegistro = false;

        for (int i = 0; i < amigos.Length; i++)
        {
            Amigo? a = (Amigo?)amigos[i];

            if (a == null)
                continue;

            temRegistro = true;

            Console.WriteLine(
                "{0, -7} | {1, -30} | {2, -30} | {3, -15}",
                a.Id, a.Nome, a.NomeResponsavel, a.Telefone
            );
        }

        if (!temRegistro)
            Console.WriteLine("Nenhum amigo cadastrado.");

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    public void VisualizarEmprestimosDoAmigo()
    {
        ExibirCabecalho("Empréstimos por Amigo");

        VisualizarTodos(deveExibirCabecalho: false);

        Console.WriteLine("---------------------------------");

        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID do amigo para ver seus empréstimos: ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                break;
        } while (true);

        Amigo? amigo = (Amigo?)repositorioAmigo.SelecionarPorId(idSelecionado);

        if (amigo == null)
        {
            ExibirMensagem("Amigo não encontrado.");
            return;
        }

        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Empréstimos de: {amigo.Nome}");
        Console.WriteLine("---------------------------------");

        Emprestimo?[] emprestimos = repositorioEmprestimo.SelecionarPorAmigo(idSelecionado);

        Console.WriteLine(
            "{0, -7} | {1, -25} | {2, -12} | {3, -12} | {4, -10}",
            "Id", "Revista", "Empréstimo", "Devolução", "Status"
        );

        bool temEmprestimo = false;

        for (int i = 0; i < emprestimos.Length; i++)
        {
            Emprestimo? e = emprestimos[i];

            if (e == null)
                continue;

            temEmprestimo = true;

            if (e.Status == "Atrasado")
                Console.ForegroundColor = ConsoleColor.Red;
            else if (e.Status == "Concluído")
                Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(
                "{0, -7} | {1, -25} | {2, -12} | {3, -12} | {4, -10}",
                e.Id,
                e.Revista.Titulo,
                e.DataEmprestimo.ToString("dd/MM/yyyy"),
                e.DataDevolucaoPrevista.ToString("dd/MM/yyyy"),
                e.Status
            );

            Console.ResetColor();
        }

        if (!temEmprestimo)
            Console.WriteLine("Nenhum empréstimo encontrado para este amigo.");

        Console.WriteLine("---------------------------------");
        Console.Write("Digite ENTER para continuar...");
        Console.ReadLine();
    }

    public new void Excluir()
    {
        ExibirCabecalho("Exclusão de Amigo");

        VisualizarTodos(deveExibirCabecalho: false);

        Console.WriteLine("---------------------------------");

        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID do registro que deseja excluir: ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                break;
        } while (true);

        if (repositorioEmprestimo.AmigoTemEmprestimoVinculado(idSelecionado))
        {
            ExibirMensagem("Não é possível excluir este amigo pois ele possui empréstimos vinculados.");
            return;
        }

        bool conseguiuExcluir = repositorioAmigo.Excluir(idSelecionado);

        if (!conseguiuExcluir)
        {
            ExibirMensagem("Não foi possível encontrar o registro requisitado.");
            return;
        }

        ExibirMensagem($"O registro \"{idSelecionado}\" foi excluído com sucesso.");
    }

    public new void Cadastrar()
    {
        ExibirCabecalho("Cadastro de Amigo");

        EntidadeBase novaEntidade = ObterDadosCadastrais();

        string[] erros = novaEntidade.Validar();

        Amigo novoAmigo = (Amigo)novaEntidade;

        if (erros.Length == 0 && repositorioAmigo.ExisteDuplicado(novoAmigo.Nome, novoAmigo.Telefone))
        {
            erros = new[] { "Já existe um amigo cadastrado com o mesmo nome e telefone." };
        }

        if (erros.Length > 0)
        {
            Console.WriteLine("---------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < erros.Length; i++)
                Console.WriteLine(erros[i]);

            Console.ResetColor();
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();

            Cadastrar();
            return;
        }

        repositorioAmigo.Cadastrar(novoAmigo);

        ExibirMensagem($"O registro \"{novoAmigo.Id}\" foi cadastrado com sucesso!");
    }

    public new void Editar()
    {
        ExibirCabecalho("Edição de Amigo");

        VisualizarTodos(deveExibirCabecalho: false);

        Console.WriteLine("---------------------------------");

        string? idSelecionado;

        do
        {
            Console.Write("Digite o ID do registro que deseja editar: ");
            idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                break;
        } while (true);

        Console.WriteLine("---------------------------------");

        EntidadeBase novaEntidade = ObterDadosCadastrais();

        string[] erros = novaEntidade.Validar();

        Amigo amigoAtualizado = (Amigo)novaEntidade;

        if (erros.Length == 0 && repositorioAmigo.ExisteDuplicado(amigoAtualizado.Nome, amigoAtualizado.Telefone, idSelecionado))
        {
            erros = new[] { "Já existe um amigo cadastrado com o mesmo nome e telefone." };
        }

        if (erros.Length > 0)
        {
            Console.WriteLine("---------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < erros.Length; i++)
                Console.WriteLine(erros[i]);

            Console.ResetColor();
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();

            Editar();
            return;
        }

        bool conseguiuEditar = repositorioAmigo.Editar(idSelecionado, amigoAtualizado);

        if (!conseguiuEditar)
        {
            ExibirMensagem("Não foi possível encontrar o registro requisitado.");
            return;
        }

        ExibirMensagem($"O registro \"{idSelecionado}\" foi editado com sucesso.");
    }

    public string? ObterOpcaoMenuAmigo()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Gestão de Amigos");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Cadastrar amigo");
        Console.WriteLine("2 - Editar amigo");
        Console.WriteLine("3 - Excluir amigo");
        Console.WriteLine("4 - Visualizar amigos");
        Console.WriteLine("5 - Ver empréstimos de um amigo");
        Console.WriteLine("S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        return Console.ReadLine()?.ToUpper();
    }

    protected override EntidadeBase ObterDadosCadastrais()
    {
        Console.Write("Digite o nome do amigo: ");
        string? nome = Console.ReadLine();

        Console.Write("Digite o nome do responsável: ");
        string? nomeResponsavel = Console.ReadLine();

        Console.Write("Digite o telefone (com DDD, 10-11 dígitos): ");
        string? telefone = Console.ReadLine();

        return new Amigo(nome ?? string.Empty, nomeResponsavel ?? string.Empty, telefone ?? string.Empty);
    }
}