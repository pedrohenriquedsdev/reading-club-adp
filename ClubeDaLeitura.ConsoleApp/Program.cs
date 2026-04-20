using ClubeDaLeitura.ConsoleApp.Apresentacao;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

// Repositórios
RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
RepositorioRevista repositorioRevista = new RepositorioRevista();
RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();

// Telas
TelaCaixa telaCaixa = new TelaCaixa(repositorioCaixa, repositorioRevista);
TelaRevista telaRevista = new TelaRevista(repositorioRevista, repositorioCaixa);
TelaAmigo telaAmigo = new TelaAmigo(repositorioAmigo, repositorioEmprestimo);
TelaEmprestimo telaEmprestimo = new TelaEmprestimo(repositorioEmprestimo, repositorioAmigo, repositorioRevista);

// Dados de exemplo
Caixa caixa = new Caixa("Lançamentos", "Vermelho", 3);
repositorioCaixa.Cadastrar(caixa);

Revista revista = new Revista("Action Comics", 155, 1990, caixa);
repositorioRevista.Cadastrar(revista);

while (true)
{
    Console.Clear();
    Console.WriteLine("---------------------------------");
    Console.WriteLine("Clube da Leitura");
    Console.WriteLine("---------------------------------");
    Console.WriteLine("1 - Gerenciar caixas de revistas");
    Console.WriteLine("2 - Gerenciar revistas");
    Console.WriteLine("3 - Gerenciar amigos");
    Console.WriteLine("4 - Gerenciar empréstimos");
    Console.WriteLine("S - Sair");
    Console.WriteLine("---------------------------------");
    Console.Write("> ");
    string? opcaoMenuPrincipal = Console.ReadLine()?.ToUpper();

    if (opcaoMenuPrincipal == "S")
    {
        Console.Clear();
        break;
    }

    while (true)
    {
        string? opcaoMenuInterno = string.Empty;

        if (opcaoMenuPrincipal == "1") // Caixas
        {
            opcaoMenuInterno = telaCaixa.ObterOpcaoMenu();

            if (opcaoMenuInterno == "S")
            {
                Console.Clear();
                break;
            }

            if (opcaoMenuInterno == "1")
                telaCaixa.Cadastrar();

            else if (opcaoMenuInterno == "2")
                telaCaixa.Editar();

            else if (opcaoMenuInterno == "3")
                telaCaixa.Excluir();

            else if (opcaoMenuInterno == "4")
                telaCaixa.VisualizarTodos(deveExibirCabecalho: true);
        }

        else if (opcaoMenuPrincipal == "2") // Revistas
        {
            opcaoMenuInterno = telaRevista.ObterOpcaoMenu();

            if (opcaoMenuInterno == "S")
            {
                Console.Clear();
                break;
            }

            if (opcaoMenuInterno == "1")
                telaRevista.Cadastrar();

            else if (opcaoMenuInterno == "2")
                telaRevista.Editar();

            else if (opcaoMenuInterno == "3")
                telaRevista.Excluir();

            else if (opcaoMenuInterno == "4")
                telaRevista.VisualizarTodos(deveExibirCabecalho: true);
        }

        else if (opcaoMenuPrincipal == "3") // Amigos
        {
            opcaoMenuInterno = telaAmigo.ObterOpcaoMenuAmigo();

            if (opcaoMenuInterno == "S")
            {
                Console.Clear();
                break;
            }

            if (opcaoMenuInterno == "1")
                telaAmigo.Cadastrar();

            else if (opcaoMenuInterno == "2")
                telaAmigo.Editar();

            else if (opcaoMenuInterno == "3")
                telaAmigo.Excluir();

            else if (opcaoMenuInterno == "4")
                telaAmigo.VisualizarTodos(deveExibirCabecalho: true);

            else if (opcaoMenuInterno == "5")
                telaAmigo.VisualizarEmprestimosDoAmigo();
        }

        else if (opcaoMenuPrincipal == "4") // Empréstimos
        {
            opcaoMenuInterno = telaEmprestimo.ObterOpcaoMenuEmprestimo();

            if (opcaoMenuInterno == "S")
            {
                Console.Clear();
                break;
            }

            if (opcaoMenuInterno == "1")
                telaEmprestimo.RegistrarEmprestimo();

            else if (opcaoMenuInterno == "2")
                telaEmprestimo.RegistrarDevolucao();

            else if (opcaoMenuInterno == "3")
                telaEmprestimo.VisualizarAbertos();

            else if (opcaoMenuInterno == "4")
                telaEmprestimo.VisualizarTodos(deveExibirCabecalho: true);
        }
        else
        {
            break;
        }
    }
}