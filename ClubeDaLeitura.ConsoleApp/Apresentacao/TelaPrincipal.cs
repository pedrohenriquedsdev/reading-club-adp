using ClubeDaLeitura.ConsoleApp.Apresentacao.Base;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Apresentacao;

public class TelaPrincipal
{
    private RepositorioCaixa repositorioCaixa;
    private RepositorioRevista repositorioRevista;
    private RepositorioAmigo repositorioAmigo;
    private RepositorioEmprestimo repositorioEmprestimo;
    private RepositorioReserva repositorioReserva;

    public TelaPrincipal
    (
        RepositorioCaixa repositorioCaixa,
        RepositorioRevista repositorioRevista,
        RepositorioAmigo repositorioAmigo,
        RepositorioEmprestimo repositorioEmprestimo,
        RepositorioReserva repositorioReserva
    )
    {
        this.repositorioCaixa = repositorioCaixa;
        this.repositorioRevista = repositorioRevista;
        this.repositorioAmigo = repositorioAmigo;
        this.repositorioEmprestimo = repositorioEmprestimo;
        this.repositorioReserva = repositorioReserva;

        // 2. Criação de dados teste
        Caixa caixa = new Caixa("Lançamentos", "Vermelho", -1);
        repositorioCaixa.Cadastrar(caixa);

        Caixa caixa2 = new Caixa("Mangás", "Azul", 7);
        repositorioCaixa.Cadastrar(caixa2);

        Revista revista = new Revista("Action Comics", 155, 1990, caixa);
        repositorioRevista.Cadastrar(revista);

        Revista revista2 = new Revista("Hajime no Ippo", 1220, 2000, caixa2);
        repositorioRevista.Cadastrar(revista2);

        Amigo amigo = new Amigo("Joãozinho", "Dona Cleide", "49 98222-4353");
        repositorioAmigo.Cadastrar(amigo);

        Emprestimo emprestimo = new Emprestimo(revista, amigo);
        emprestimo.Abrir();
        repositorioEmprestimo.Cadastrar(emprestimo);

        Multa multa = new Multa(emprestimo, DateTime.Now);
        amigo.RegistrarMulta(multa);
    }

    public ITela? ApresentarMenuOpcoesPrincipal()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Clube da Leitura");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Gerenciar caixas de revistas");
        Console.WriteLine("2 - Gerenciar revistas");
        Console.WriteLine("3 - Gerenciar amigos");
        Console.WriteLine("4 - Gerenciar empréstimos");
        Console.WriteLine("5 - Gerenciar reservas");
        Console.WriteLine("S - Sair");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenuPrincipal = Console.ReadLine()?.ToUpper();

        if (opcaoMenuPrincipal == "1")
            return new TelaCaixa(repositorioCaixa);

        if (opcaoMenuPrincipal == "2")
            return new TelaRevista(repositorioRevista, repositorioCaixa);

        if (opcaoMenuPrincipal == "3")
            return new TelaAmigo(repositorioAmigo);

        if (opcaoMenuPrincipal == "4")
            return new TelaEmprestimo(repositorioEmprestimo, repositorioRevista, repositorioAmigo);

        if (opcaoMenuPrincipal == "5")
            return new TelaReserva(repositorioReserva, repositorioRevista, repositorioAmigo);

        return null;
    }
}