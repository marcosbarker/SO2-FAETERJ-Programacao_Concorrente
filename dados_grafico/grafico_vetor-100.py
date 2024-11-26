import matplotlib.pyplot as plt

# Dados
tipos = ['Threads', 'Memória Compartilhada', 'Troca de Mensagens']
n_procs = [1, 2, 3, 4]
medias_threads = [27, 20, 22, 18]
medias_memoria = [434.5377, 396.7503, 519.8942, 462.9119]
medias_troca = [34, 37.4, 42, 44.8]

# Plotagem
plt.plot(n_procs, medias_threads, label='Threads', marker='o')
plt.plot(n_procs, medias_memoria, label='Memória Compartilhada', marker='o')
plt.plot(n_procs, medias_troca, label='Troca de Mensagens', marker='o')

# Detalhes do gráfico
plt.xlabel('Nº Threads/Processos')
plt.ylabel('Média')
plt.title('Gráfico de Linhas - Tipos de Execução')
plt.legend()
plt.grid(True)

# Mostrar gráfico
plt.show()
