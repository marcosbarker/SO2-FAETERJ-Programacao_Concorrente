using System;
using System.IO.MemoryMappedFiles;
using System.Threading;

class FuncaoPrimo
{
    static void Main(string[] args)
    {
        int a = int.Parse(args[0]);
        int b = int.Parse(args[1]);

        //conecta memoria compartilhada
        using (var memoriaCompartilhada = MemoryMappedFile.OpenExisting("memoriaCompartilhadaReal"))
        {
            int contagemPrimos = 0;

            //verifica numero primo
            for (int i = a; i < b; i++)
            {
                if (ePrimo(i))
                {
                    contagemPrimos++;
                }
            }

            //atualiza contagem numeros primos na memoria compartilhada
            using (var acessoMemoria = memoriaCompartilhada.CreateViewAccessor())
            {
                //lock para que cada processo modifique a memoria por vez
                bool bloqueioMemoria = false;
                try
                {
                    Monitor.Enter(acessoMemoria, ref bloqueioMemoria);

                    //le a contagem atual de primos
                    int contagemPrimosAtual = acessoMemoria.ReadInt32(0);

                    //incrementa contagm dos primos do processo atual
                    acessoMemoria.Write(0, contagemPrimosAtual + contagemPrimos);
                }
                finally
                {
                    if (bloqueioMemoria)
                    {
                        //libera lock
                        Monitor.Exit(acessoMemoria);
                    }
                }
            }
        }
    }

    static bool ePrimo(int number)
    {
        if (number < 2) return false;
        for (int i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0)
                return false;
        }
        return true;
    }
}
