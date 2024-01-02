using System;

class Program
{
    private static bool cancelarCiclo = false;

    static void Main()
    {
        Console.WriteLine("Pomodoro do André");

        while (true)
        {
            MostrarOpcoes();
            Console.WriteLine("\nDigite uma opção:");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Console.WriteLine("Você escolheu a opção padrão POMODORO");
                    ExecutarCicloDeTrabalho(25);
                    ExecutarPausaCurta(5);
                    break;

                case "2":
                    Console.WriteLine("Você escolheu a opção personalizada:");
                    Console.Write("\nDigite o tempo desejado para o ciclo de estudos (em minutos): ");
                    int tempoCicloTrabalho = ObterTempoDoUsuario();

                    ExecutarCicloDeTrabalho(tempoCicloTrabalho);

                    Console.Write("Digite o tempo desejado para a pausa curta (em minutos): ");
                    int tempoPausaCurta = ObterTempoDoUsuario();

                    ExecutarPausaCurta(tempoPausaCurta);
                    break;

                case "0":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Opção inválida. Por favor, escolha uma opção válida.");
                    break;
            }
        }
    }

    static void MostrarOpcoes()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("************************************");
        Console.WriteLine("* Bem-vindo ao Pomodoro do André!  *");
        Console.WriteLine("************************************");
        Console.WriteLine("*         Opções Disponíveis        *");
        Console.WriteLine("*                                  *");
        Console.WriteLine("* 1 - Pomodoro Padrão              *");
        Console.WriteLine("*      (25 minutos de trabalho,    *");
        Console.WriteLine("*       5 minutos de pausa)        *");
        Console.WriteLine("*                                  *");
        Console.WriteLine("* 2 - Configurar tempos personal.  *");
        Console.WriteLine("*                                  *");
        Console.WriteLine("* 0 - Sair                         *");
        Console.WriteLine("*                                  *");
        Console.WriteLine("************************************");
        Console.ResetColor();
    }

    static void ExecutarCicloDeTrabalho(int minutos)
    {
        Console.WriteLine($"Iniciando ciclo de estudos ({minutos} minutos)...");
        cancelarCiclo = false;
        SimularContagemRegressiva(minutos);
        if (!cancelarCiclo)
        {
            Console.WriteLine("Ciclo de estudos concluído. Hora de uma pausa!");
        }
        else
        {
            Console.WriteLine("Ciclo de estudos cancelado. Retornando para a área principal.");
        }
    }

    static void ExecutarPausaCurta(int minutos)
    {
        Console.WriteLine($"Iniciando pausa curta ({minutos} minutos)...");
        cancelarCiclo = false;
        SimularContagemRegressiva(minutos);
        if (!cancelarCiclo)
        {
            Console.WriteLine("Pausa curta concluída. Pronto para o próximo ciclo de estudos!");
        }
        else
        {
            Console.WriteLine("Pausa curta cancelada. Retornando para a área principal.");
        }
    }

    static int ObterTempoDoUsuario()
    {
        while (true)
        {
            string entrada = Console.ReadLine();

            if (int.TryParse(entrada, out int minutos) && minutos > 0)
            {
                return minutos;
            }
            else
            {
                Console.WriteLine("Por favor, digite um valor válido maior que zero.");
            }
        }
    }

    static void SimularContagemRegressiva(int minutos)
    {
        int segundosTotais = minutos * 60;

        for (int segundos = segundosTotais; segundos > 0; segundos--)
        {
            Console.Write($"\rTempo restante: {TimeSpan.FromSeconds(segundos)}");

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Escape)
                {
                    cancelarCiclo = true;
                    Console.WriteLine("\nCiclo cancelado. Retornando para a área principal.");
                    return;  // Retorna para a área principal do software
                }
            }

            System.Threading.Thread.Sleep(1000);
        }

        Console.WriteLine();
    }
}
