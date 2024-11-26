using System;
using FuncaoPrimo;
using System.Diagnostics;

class Program
{
  static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            
			int tamanhoVetor = args.Length > 0 ? int.Parse(args[0]) : 100;
			int numeroDeThreads =args.Length > 1 ? int.Parse(args[1]) : 4;             
            
            int[] vetor = Enumerable.Range(0, tamanhoVetor).ToArray();

            //conta numero primo
            var quantidadePrimos = ContadorPrimos.ContarPrimos(vetor, numeroDeThreads);
          
            Console.WriteLine($"Total numeros primos: {quantidadePrimos}");
            
            stopwatch.Stop();
            Console.WriteLine($"Tempo execucao: {stopwatch.Elapsed.TotalMilliseconds} ms");
        }
}
