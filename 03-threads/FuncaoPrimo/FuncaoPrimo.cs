using System;
using System.Threading;
using System.Threading.Tasks;

namespace FuncaoPrimo
{
    public class ContadorPrimos
    {
        private static int quantidadePrimos;
        private static readonly object bloqueoiMultithread = new object();

        public static int ContarPrimos(int[] vetor, int numeroDeThreads)
        {
            quantidadePrimos = 0;

			//ParallelOptions - MaxDegreeOfParallelism define qantas threads podem ser usadas simultaneamente
            Parallel.For(0, vetor.Length, new ParallelOptions { MaxDegreeOfParallelism = numeroDeThreads }, i =>
            {
                if (ePrimo(vetor[i]))
                {
                   lock (bloqueoiMultithread)
                   {
                       quantidadePrimos++;
                   }
                }
            });

            return quantidadePrimos;
        }

        private static bool ePrimo(int numero)
        {
            if (numero < 2) return false;
            for (int i = 2; i <= Math.Sqrt(numero); i++)
            {
                if (numero % i == 0) return false;
            }
            return true;
        }
    }
}
