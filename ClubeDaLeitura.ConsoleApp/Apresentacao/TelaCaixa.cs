namespace ClubeDaLeitura.ConsoleApp.Apresentacao;

public class TelaCaixa
{
    public string? ObterOpcaoMenu()
    {
        Console.Clear();
        Console.WriteLine("---------------------------------");
        Console.WriteLine("Gestão de Caixas");
        Console.WriteLine("---------------------------------");
        Console.WriteLine("1 - Cadastrar caixa");
        Console.WriteLine("2 - Editar caixa");
        Console.WriteLine("3 - Excluir caixa");
        Console.WriteLine("4 - Visualizar caixas");
        Console.WriteLine("S - Voltar para o início");
        Console.WriteLine("---------------------------------");
        Console.Write("> ");

        string? opcaoMenu = Console.ReadLine()?.ToUpper();

        return opcaoMenu;
    }

    public void Cadastrar()
    {
    }

    public void Editar()
    {
    }

    public void Excluir()
    {
    }

    public void VisualizarTodos()
    {
    }
}
;