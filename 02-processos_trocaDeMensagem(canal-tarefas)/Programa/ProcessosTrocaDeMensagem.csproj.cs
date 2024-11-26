using System;
using System.Threading.Tasks;
using FuncaoPrimo;
using System.Diagnostics;

class Program
{
    static async Task Main(string[] args)
    {
		Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();		
        
        int numeroDeProcessos = args.Length > 0 ? int.Parse(args[0]) : 1;
        int tamanhoVetor = args.Length > 1 ? int.Parse(args[1]) : 100;

        ContagemPrimos contagemPrimos = new ContagemPrimos();

        int totalPrimos = await contagemPrimos.ContagemPrimosAsync(numeroDeProcessos, tamanhoVetor);

        Console.WriteLine($"Total numeros primos: {totalPrimos}");
		
				Console.WriteLine($"Tempo de execucao: {stopwatch.ElapsedMilliseconds} ms");
    }
}
