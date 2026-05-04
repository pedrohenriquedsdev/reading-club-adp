using ClubeDaLeitura.ConsoleApp.Apresentacao.Base;
using ClubeDaLeitura.ConsoleApp.Dominio;
using ClubeDaLeitura.ConsoleApp.Dominio.Base;
using ClubeDaLeitura.ConsoleApp.Infraestrutura;

namespace ClubeDaLeitura.ConsoleApp.Apresentacao;

public class TelaReserva : ITela
{
    private RepositorioReserva repositorioReserva;
    private RepositorioRevista repositorioRevista;
    private RepositorioAmigo repositorioAmigo;

    public TelaReserva
    (
        RepositorioReserva repositorioReserva,
        RepositorioRevista repositorioRevista,
        RepositorioAmigo repositorioAmigo
    )
    {
        this.repositorioReserva = repositorioReserva;
        this.repositorioRevista = repositorioRevista;
        this.repositorioAmigo = repositorioAmigo;
    }

    public string? ObterOpcaoMenu()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de Reservas");
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"1 - Iniciar reserva");
        Console.WriteLine($"2 - Concluir reserva");
        Console.WriteLine($"3 - Visualizar reservas");
        Console.WriteLine("S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");
        string? opcaoMenu = Console.ReadLine()?.ToUpper();

        return opcaoMenu;
    }

    public void Iniciar()
    {
        ExibirCabecalho("Abertura de Reserva");

        Reserva reserva = ObterDadosCadastrais();

        string[] erros = reserva.Validar();

        if (erros.Length > 0)
        {
            Console.WriteLine("---------------------------------");

            Console.ForegroundColor = ConsoleColor.Red;

            for (int i = 0; i < erros.Length; i++)
            {
                string erro = erros[i];

                Console.WriteLine(erro);
            }

            Console.ResetColor();
            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();

            Iniciar();
            return;
        }

        Amigo amigoReserva = reserva.Amigo;

        if (amigoReserva.ContemEmprestimoAberto)
        {
            Console.WriteLine("---------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Este amigo contém um empréstimo em aberto...");
            Console.WriteLine("O empréstimo deve ser concluído antes de uma reserva ser iniciada.");
            Console.ResetColor();
            Console.WriteLine("---------------------------------");

            Console.WriteLine("---------------------------------");
            Console.Write("Digite ENTER para continuar...");
            Console.ReadLine();
            return;
        }

        if (amigoReserva.ContemMultaAtiva)
        {
            Multa multa = amigoReserva.ObterMultaAtiva()!;

            Console.WriteLine("---------------------------------");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Este amigo contém uma multa ativa...");
            Console.WriteLine("O pagamento precisa ser efetuado antes de prosseguir.");
            Console.ResetColor();
            Console.WriteLine("---------------------------------");

            Console.WriteLine(
                "{0, -7} | {1, -12} | {2, -7} | {3, -20} | {4, -10}",
                "Id", "Ocorrência", "Valor", "Revista", "Status"
            );

            Console.WriteLine(
                "{0, -7} | {1, -12} | {2, -7} | {3, -20} | {4, -10}",
                multa.Id,
                multa.DataOcorrencia.ToShortDateString(),
                multa.Valor.ToString("C2"),
                multa.Emprestimo.Revista.Titulo,
                multa.Status
            );

            Console.WriteLine("---------------------------------");
            Console.Write("Deseja efetuar o pagamento? (s/N): ");
            string opcaoPagamento = Console.ReadLine() ?? string.Empty;

            if (opcaoPagamento.ToUpper() == "S")
            {
                multa.Quitar();

                Emprestimo emprestimoMulta = multa.Emprestimo;
                emprestimoMulta.Concluir();

                Console.WriteLine("---------------------------------");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("A multa foi quitada com sucesso!");
                Console.ResetColor();
                Console.WriteLine("---------------------------------");
                Console.Write("Digite ENTER para continuar...");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("---------------------------------");
                Console.Write("Digite ENTER para continuar...");
                Console.ReadLine();
                return;
            }
        }

        reserva.Iniciar();

        repositorioReserva.Cadastrar(reserva);

        ExibirMensagem($"A reserva \"{reserva.Id}\" foi iniciada e cadastrada com sucesso.");
    }

    public void Concluir()
    {
        ExibirCabecalho("Conclusão de Reserva");

        VisualizarTodas(deveExibirCabecalho: false);

        Console.WriteLine("---------------------------------");

        Reserva? reservaSelecionada = null;

        do
        {
            Console.Write("Digite o id da reserva que deseja concluir (ou S para sair): ");
            string? idSelecionado = Console.ReadLine() ?? string.Empty;

            if (idSelecionado.ToUpper() == "S")
                return;

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                reservaSelecionada = repositorioReserva.SelecionarPorId(idSelecionado);

        } while (reservaSelecionada == null);

        Console.WriteLine("---------------------------------");

        Console.Write("Deseja realmente concluir a reserva selecionada? (s/N): ");
        string? opcaoContinuar = Console.ReadLine()?.ToUpper();

        if (opcaoContinuar != "S")
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.ReadLine();
            return;
        }

        reservaSelecionada.Concluir();

        ExibirMensagem($"A reserva \"{reservaSelecionada.Id}\" foi concluída com sucesso.");
    }

    public void VisualizarTodas(bool deveExibirCabecalho)
    {
        if (deveExibirCabecalho)
            ExibirCabecalho("Visualização de Reservas");

        Console.WriteLine(
            "{0, -7} | {1, -15} | {2, -10} | {3, -10} | {4, -15} | {5, -10}",
            "Id", "Revista", "Amigo", "Abertura", "Conclusão.", "Status"
        );

        Reserva?[] reservas = repositorioReserva.SelecionarTodos();

        for (int i = 0; i < reservas.Length; i++)
        {
            Reserva? r = reservas[i];

            if (r == null)
                continue;

            Console.Write("{0, -7} | ", r.Id);
            Console.Write("{0, -15} | ", r.Revista.Titulo);
            Console.Write("{0, -10} | ", r.Amigo.Nome);
            Console.Write("{0, -10} | ", r.DataInicio.ToShortDateString());
            Console.Write("{0, -15} | ", r.DataConclusao?.ToShortDateString());

            string status = r.Status.ToString();

            if (r.Status == StatusReserva.Indefinido)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
            else if (r.Status == StatusReserva.Ativa)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            else if (r.Status == StatusReserva.Concluida)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                status = "Concluída";
            }

            Console.Write("{0, -10}", status);

            Console.ResetColor();
            Console.WriteLine();
        }

        if (deveExibirCabecalho)
        {
            Console.WriteLine("---------------------------------");
            Console.WriteLine("Digite ENTER para continuar...");
            Console.ReadLine();
        }
    }

    private Reserva ObterDadosCadastrais()
    {
        // 1. Selecionar uma revista disponível
        VisualizarRevistas();

        Revista? revista = null;

        do
        {
            Console.Write("Digite o id da revista que deseja reservar: ");
            string? idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                revista = (Revista?)repositorioRevista.SelecionarPorId(idSelecionado);

        } while (revista == null);

        // 2. Selecionar um amigo disponível
        Console.WriteLine("---------------------------------");

        VisualizarAmigos();

        Amigo? amigo = null;

        do
        {
            Console.Write("Digite o id do amigo que reservará a revista: ");
            string? idSelecionado = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(idSelecionado) && idSelecionado.Length == 7)
                amigo = (Amigo?)repositorioAmigo.SelecionarPorId(idSelecionado);

        } while (amigo == null);

        // 3. Gerar um empréstimo
        return new Reserva(revista, amigo);
    }

    private void VisualizarRevistas()
    {
        Console.WriteLine(
            "{0, -7} | {1, -25} | {2, -6} | {3, -4} | {4, -15}",
            "Id", "Título", "Edição", "Ano", "Caixa"
        );

        EntidadeBase?[] revistas = repositorioRevista.SelecionarTodos();

        for (int i = 0; i < revistas.Length; i++)
        {
            Revista? r = (Revista?)revistas[i];

            if (r == null)
                continue;

            Console.Write("{0, -7} | ", r.Id);
            Console.Write("{0, -25} | ", r.Titulo);
            Console.Write("{0, -6} | ", r.NumeroEdicao);
            Console.Write("{0, -4} | ", r.AnoPublicacao);

            string corSelecionada = r.Caixa.Cor;

            if (corSelecionada == "Vermelho")
                Console.ForegroundColor = ConsoleColor.Red;

            else if (corSelecionada == "Verde")
                Console.ForegroundColor = ConsoleColor.Green;

            else if (corSelecionada == "Azul")
                Console.ForegroundColor = ConsoleColor.Blue;

            Console.Write("{0, -15}", r.Caixa.Etiqueta);

            Console.ResetColor();
            Console.WriteLine();
        }

        Console.WriteLine("---------------------------------");
    }

    private void VisualizarAmigos()
    {
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

        Console.WriteLine("---------------------------------");
    }

    private void ExibirCabecalho(string titulo)
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine($"Gestão de Empréstimos");
        Console.WriteLine("---------------------------------");
        Console.WriteLine(titulo);
        Console.WriteLine("---------------------------------");
    }

    private void ExibirMensagem(string mensagem)
    {
        Console.WriteLine("---------------------------------");
        Console.WriteLine(mensagem);
        Console.WriteLine("---------------------------------");
        Console.Write("Digite ENTER para continuar...");
        Console.ReadLine();
    }
}