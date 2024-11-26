import matplotlib.pyplot as plt

numero_de_processos_100 = [1, 2, 3, 4]
medias_threads_100 = [27.47536, 20.8157, 22.72552, 18.91034]
medias_processo_memoria_compartilhada_100 = [116.4894, 199.0019, 170.78342, 203.42416]
medias_processos_troca_mensagens_100 = [34, 37.4, 42, 44.8]

numero_de_processos_1000 = [1, 2, 3, 4]
medias_threads_1000 = [28.4777, 20.51576, 18.85944, 19.85172]
medias_processo_memoria_compartilhada_1000 = [132.44982, 168.4038, 179.5461, 203.78372]
medias_processos_troca_mensagens_1000 = [39.6, 39.8, 38, 37.4]

plt.figure(figsize=(12, 6))

#vetor 100
plt.subplot(1, 2, 1)
plt.plot(numero_de_processos_100, medias_threads_100, label='Threads', marker='o')
plt.plot(numero_de_processos_100, medias_processo_memoria_compartilhada_100, label='Memória Compartilhada', marker='o')
plt.plot(numero_de_processos_100, medias_processos_troca_mensagens_100, label='Troca de Mensagens', marker='o')
plt.xlabel('Nº Threads/Processos')
plt.ylabel('Média (milissegundos)')
plt.title('Tamanho do Vetor 100')
plt.legend(loc='center right')
plt.grid(True)

#vetor 1000
plt.subplot(1, 2, 2)
plt.plot(numero_de_processos_1000, medias_threads_1000, label='Threads', marker='o')
plt.plot(numero_de_processos_1000, medias_processo_memoria_compartilhada_1000, label='Memória Compartilhada', marker='o')
plt.plot(numero_de_processos_1000, medias_processos_troca_mensagens_1000, label='Troca de Mensagens', marker='o')
plt.xlabel('Nº Threads/Processos')
plt.ylabel('Média (milissegundos)')
plt.title('Tamanho do Vetor 1000')
plt.legend(loc='center right')
plt.grid(True)

plt.tight_layout()
plt.show()