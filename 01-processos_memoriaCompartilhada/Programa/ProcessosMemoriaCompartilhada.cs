using System;
using System.Diagnostics;
using System.IO.MemoryMappedFiles;

class ProcessosMemoriaCompartilhada
{
    static void Main(string[] args)
    {
		Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();		
        
        int tamanhoVetor = args.Length > 0 ? int.Parse(args[0]) : 100;
        int[] vetor = new int[tamanhoVetor];
        
        //preenche o vetor com números aleatórios
        Random rand = new Random(0);
        for (int i = 0; i < tamanhoVetor; i++)
        {
            vetor[i] = rand.Next(1, 10000);
        }

        //define o numero de processos
        int numeroDeProcessos = args.Length > 1 ? int.Parse(args[1]) : 4;

        //cria memoria compartilhada
        using (var memoriaCompartilhada = MemoryMappedFile.CreateNew("memoriaCompartilhadaReal", 1024))
        {
            int vetorDivididoPorProcesso= tamanhoVetor / numeroDeProcessos;
            Process[] processes = new Process[numeroDeProcessos];

            //inicializa a memoria compartilhada vazia
            using (var accessor = memoriaCompartilhada.CreateViewAccessor())
            {
				//inicializa contagem de primos
                accessor.Write(0, 0); 
            }

            //cria os processos
            for (int i = 0; i < numeroDeProcessos; i++)
            {
                int start = i * vetorDivididoPorProcesso ;
                int end = (i == numeroDeProcessos - 1) ? tamanhoVetor : start + vetorDivididoPorProcesso ;

                //criacao dos processos
				string FuncaoPrimo = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName, "FuncaoPrimo", "bin", "Debug", "net7.0", "FuncaoPrimo.dll");

                processes[i] = new Process();
                processes[i].StartInfo.FileName = "dotnet";
                processes[i].StartInfo.Arguments = $"{FuncaoPrimo} {start} {end}";
                processes[i].Start();
            }

            //espera todos os processos terminarem
            foreach (var process in processes)
            {
                process.WaitForExit();
            }

            //leitura do resultado da memoria compartilhada
            using (var accessor = memoriaCompartilhada.CreateViewAccessor())
            {
                int contagemPrimos = accessor.ReadInt32(0);
                Console.WriteLine($"Total numeros primos: {contagemPrimos}");
				
				stopwatch.Stop();
				Console.WriteLine($"Tempo de execucao: {stopwatch.Elapsed.TotalMilliseconds} ms");				
            }
        }
    }
}
