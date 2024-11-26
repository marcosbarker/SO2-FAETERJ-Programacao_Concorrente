using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace FuncaoPrimo
{
    public class ContagemPrimos
    {
        public async Task<int> ContagemPrimosAsync(int numeroDeProcessos, int tamanhoVetor)
        {
            //cria canal para comunicacao
            var channel = Channel.CreateUnbounded<int>();

            //tarefa para produzir numeros e enviar para o canal
            _ = Task.Run(async () =>
            {
                for (int i = 2; i <= tamanhoVetor; i++)
                {
                    await channel.Writer.WriteAsync(i);
                }
                channel.Writer.Complete();
            });

            //lista que armazena as tarefas 
            List<Task<int>> tasks = new List<Task<int>>();

            for (int i = 0; i < numeroDeProcessos; i++)
            {
                tasks.Add(Task.Run(async () =>
                {
                    int contagemPrimos = 0;

                    //consumir os numeros do canal
                    await foreach (var numero in channel.Reader.ReadAllAsync())
                    {
                        if (ePrimo(numero))
                        {
                            contagemPrimos++;
                        }
                    }

                    return contagemPrimos;
                }));
            }

            //aguarda as tarefas terminarem
            var results = await Task.WhenAll(tasks);

            //soma resultados
            return results.Sum();
        }
        
        private bool ePrimo(int numero)
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
