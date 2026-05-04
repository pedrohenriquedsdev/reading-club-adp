using ClubeDaLeitura.ConsoleApp.Apresentacao;
using ClubeDaLeitura.ConsoleApp.Apresentacao.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

// 1. Instanciação de dependências
RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
RepositorioRevista repositorioRevista = new RepositorioRevista();
RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();
RepositorioReserva repositorioReserva = new RepositorioReserva();

TelaPrincipal telaPrincipal = new TelaPrincipal
(
    repositorioCaixa,
    repositorioRevista,
    repositorioAmigo,
    repositorioEmprestimo,
    repositorioReserva
);

// 2. Loop do menu principal
while (true)
{
    ITela? telaSelecionada = telaPrincipal.ApresentarMenuOpcoesPrincipal();

    if (telaSelecionada == null)
    {
        Console.Clear();
        break;
    }

    // 3. Loop do menu interno
    while (true)
    {
        string? opcaoMenuInterno = telaSelecionada.ObterOpcaoMenu();

        if (opcaoMenuInterno == "S")
        {
            Console.Clear();
            break;
        }

        if (telaSelecionada is TelaBase telaBase)
        {
            if (opcaoMenuInterno == "1")
                telaBase.Cadastrar();

            else if (opcaoMenuInterno == "2")
                telaBase.Editar();

            else if (opcaoMenuInterno == "3")
                telaBase.Excluir();

            else if (opcaoMenuInterno == "4")
                telaBase.VisualizarTodos(deveExibirCabecalho: true);

            if (opcaoMenuInterno == "5")
            {
                if (telaBase is TelaAmigo telaAmigo)
                    telaAmigo.VisualizarMultas();
            }
        }

        else if (telaSelecionada is TelaEmprestimo telaEmprestimo)
        {
            if (opcaoMenuInterno == "S")
            {
                Console.Clear();
                break;
            }

            if (opcaoMenuInterno == "1")
                telaEmprestimo.Abrir();

            else if (opcaoMenuInterno == "2")
                telaEmprestimo.Concluir();

            else if (opcaoMenuInterno == "3")
                telaEmprestimo.VisualizarTodos(deveExibirCabecalho: true);
        }

        else if (telaSelecionada is TelaReserva telaReserva)
        {
            if (opcaoMenuInterno == "S")
            {
                Console.Clear();
                break;
            }

            if (opcaoMenuInterno == "1")
                telaReserva.Iniciar();

            else if (opcaoMenuInterno == "2")
                telaReserva.Concluir();

            else if (opcaoMenuInterno == "3")
                telaReserva.VisualizarTodas(deveExibirCabecalho: true);
        }
    }
}