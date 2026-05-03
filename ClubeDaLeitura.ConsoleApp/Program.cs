using ClubeDaLeitura.ConsoleApp.Apresentacao;
using ClubeDaLeitura.ConsoleApp.Apresentacao.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

// 1. Instanciação de dependências
RepositorioCaixa repositorioCaixa = new RepositorioCaixa();
RepositorioRevista repositorioRevista = new RepositorioRevista();
RepositorioAmigo repositorioAmigo = new RepositorioAmigo();
RepositorioEmprestimo repositorioEmprestimo = new RepositorioEmprestimo();

TelaPrincipal telaPrincipal = new TelaPrincipal
(
    repositorioCaixa,
    repositorioRevista,
    repositorioAmigo,
    repositorioEmprestimo
);

<<<<<<< HEAD
// 2. Criação de dados teste

Caixa caixa = new Caixa("Lançamentos", "Vermelho", 3);
repositorioCaixa.Cadastrar(caixa);

Revista revista = new Revista("Action Comics", 155, 1990, caixa);
repositorioRevista.Cadastrar(revista);

//POLIMORFISMO
EntidadeBase entidade = caixa;

entidade.AtualizarRegistro(new Caixa("Teste", "Vermelho", 5));

Amigo amigo = new Amigo("Joãozinho", "Dona Cleide", "49 98222-4353");
repositorioAmigo.Cadastrar(amigo);

// 3. Loop principal
while (true)
{
    //Console.Clear();
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
=======
// 2. Loop do menu principal
while (true)
{
    ITela? telaSelecionada = telaPrincipal.ApresentarMenuOpcoesPrincipal();
>>>>>>> v6

    if (telaSelecionada == null)
    {
        //Console.Clear();
        break;
    }

    // 3. Loop do menu interno
    while (true)
    {
        string? opcaoMenuInterno = telaSelecionada.ObterOpcaoMenu();

        if (opcaoMenuInterno == "S")
        {
<<<<<<< HEAD
            opcaoMenuInterno = telaCaixa.ObterOpcaoMenu();

            if (opcaoMenuInterno == "S")
            {
                //Console.Clear();
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
=======
            Console.Clear();
            break;
>>>>>>> v6
        }

        if (telaSelecionada is TelaBase telaBase)
        {
<<<<<<< HEAD
            opcaoMenuInterno = telaRevista.ObterOpcaoMenu();

            if (opcaoMenuInterno == "S")
            {
                //Console.Clear();
                break;
            }

=======
>>>>>>> v6
            if (opcaoMenuInterno == "1")
                telaBase.Cadastrar();

            else if (opcaoMenuInterno == "2")
                telaBase.Editar();

            else if (opcaoMenuInterno == "3")
                telaBase.Excluir();

            else if (opcaoMenuInterno == "4")
                telaBase.VisualizarTodos(deveExibirCabecalho: true);
        }

        else if (telaSelecionada is TelaEmprestimo telaEmprestimo)
        {
            opcaoMenuInterno = telaEmprestimo.ObterOpcaoMenu();

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
    }
}