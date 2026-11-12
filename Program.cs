using SistemaTransporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

class Atividade
{
    static void Main()
    {
        List<Viagem> lista = new List<Viagem>();
        int opcao;
        opcao = 1;
        carregarDados(lista, "Viagem.txt");

        do
        {
            opcao = menu();
            switch (opcao)
            {
                case 1:
                    addViagem(lista);
                
                   break;
                case 2:
                    mostrarViagens(lista);

                    break;
                case 3:
                    // Leia a placa desejada e chame buscarPorPlaca (se retornar false, avise que não encontrou)
                    Console.WriteLine("Valor de busca da placa:");
                   string placaBusca =  Console.ReadLine();
                   bool placa = buscarPorPlaca(lista , placaBusca);
                    
                    if (placa == false)
                    {
                        Console.WriteLine("Nao encontrada!");
                    }
                    break;
                case 4:
                    // Leia o valor X em litros (double) e chame buscarPorConsumo
                    Console.WriteLine("Valor de busca do consumo:");
                    double consumo = double.Parse(Console.ReadLine());
                    bool verificarConsumo = buscarPorConsumo(lista, consumo);

                    if (verificarConsumo == false)
                    {
                        Console.WriteLine("Consumo nao encontrada!");
                    }

                    break;
                case 5:
                    // Leia o preço do litro de combustível (R$) e chame calcularGastoPorViagem


                    break;
                case 6:
                    // Leia o preço do litro de combustível (R$) e chame calcularTotalGeral


                    break;
                case 0:
                    Console.WriteLine("Saindo...");
                    // Chame salvarDados(listaViagens, "viagens.txt") antes de encerrar
                    salvarDados(lista , "Viagem.txt");
                    break;
            }
            Console.ReadKey();
            Console.Clear();

        } while (opcao != 0);
    }
    static int menu()
    {
        Console.WriteLine("=== SISTEMA DE CONTROLE DE VIAGENS - SETOR DE TRANSPORTES ===");
        Console.WriteLine("1 - Cadastrar Nova Viagem");
        Console.WriteLine("2 - Mostrar Todas as Viagens");
        Console.WriteLine("3 - Buscar Viagens por Placa do Veículo");
        Console.WriteLine("4 - Buscar Viagens que Consumiram mais que X Litros");
        Console.WriteLine("5 - Exibir Consumo (L) e Gasto (R$) de Cada Viagem");
        Console.WriteLine("6 - Exibir Consumo Total e Gasto Total de Todas as Viagens");
        Console.WriteLine("0 - Sair");
        Console.Write("Escolha uma opção: ");
        return int.Parse(Console.ReadLine());
    }

    static void addViagem(List<Viagem> listaViagens)
    {
        Viagem viagem = new Viagem();
        Console.WriteLine("Modelo do veiculo: ");
        viagem.modelo = Console.ReadLine();
        Console.WriteLine("Placa do veiculo: ");
        viagem.placa = Console.ReadLine();
        Console.WriteLine("Destino: ");
        viagem.destino = Console.ReadLine();
        Console.WriteLine("kmRodados: ");
        viagem.kmRodados = double.Parse(Console.ReadLine());
        Console.WriteLine("consumoMedio (km/L) ");
        viagem.consumoMedio = double.Parse(Console.ReadLine());

        listaViagens.Add(viagem);

    }

    static void mostrarViagens(List<Viagem> listaViagens)
    {
        foreach (Viagem v in listaViagens)
        {
            Console.WriteLine($"Modelo: {v.modelo}");
            Console.WriteLine($"Placa: {v.placa}");
            Console.WriteLine($"Destino: {v.destino}");
            Console.WriteLine($"kmRodados: {v.kmRodados}");
            Console.WriteLine($"consumoMedio: {v.consumoMedio}");


            // Percorra a listaViagens (for de 0 até listaViagens.Count)
        }  // Exiba modelo, placa, destino, kmRodados e consumoMedio de cada posição i

    }

    static bool buscarPorPlaca(List<Viagem> listaViagens, string placaBusca)
    {
        bool encontrada= false;
        foreach (Viagem v in listaViagens)
        {
            if (v.placa.ToUpper().Contains(placaBusca.ToUpper())) {
                Console.WriteLine($"Modelo: {v.modelo}");
                Console.WriteLine($"Placa: {v.placa}");
                Console.WriteLine($"Destino: {v.destino}");
                Console.WriteLine($"kmRodados: {v.kmRodados}");
                Console.WriteLine($"consumoMedio: {v.consumoMedio}");
            
                encontrada = true;
            }
        }
      
       return encontrada;
    }
    static bool buscarPorConsumo(List<Viagem> listaViagens, double litrosX)
    {
        bool encontrada = false;
        double consumo = 0;
        foreach (Viagem v in listaViagens)
        {
            consumo = v.kmRodados / v.consumoMedio;

            if(consumo > litrosX)
            {
                Console.WriteLine($"Modelo: {v.modelo}");
                Console.WriteLine($"Placa: {v.placa}");
                Console.WriteLine($"Destino: {v.destino}");
                Console.WriteLine($"kmRodados: {v.kmRodados}");
                Console.WriteLine($"consumoMedio: {v.consumoMedio}");
                Console.WriteLine($" Consumo total:{consumo}");

                encontrada = true;
            } 
           
        }
        // Percorra a lista, calcule os litros consumidos na viagem (kmRodados / consumoMedio)
        // Se o consumo em litros for maior que litrosX, mostre os dados da viagem e marque encontrado = true

        return encontrada;
    }
        
    
    
    static void salvarDados(List<Viagem> listaViagens, string nomeArquivo)
    {
        StreamWriter writer = new StreamWriter(nomeArquivo);
        foreach (Viagem v in listaViagens)
        {
            writer.WriteLine($"{v.modelo};{v.placa};{v.destino};{v.kmRodados};{v.consumoMedio}");
        }
        Console.WriteLine("Dados salvos com sucesso!");
        writer.Dispose();
    }

    static void carregarDados(List<Viagem> listaViagens, string nomeArquivo)
    {
        if (File.Exists(nomeArquivo))
        {
            string[] linhasDoArquivo = File.ReadAllLines(nomeArquivo);
            foreach (string linha in linhasDoArquivo)
            {
                string[] campos = linha.Split(';');
                Viagem v = new Viagem();
                v.modelo = campos[0];
                v.placa = campos[1];
                v.destino = campos[2];
                v.kmRodados = double.Parse(campos[3]);
                v.consumoMedio = double.Parse(campos[4]);
                listaViagens.Add(v);
            }
            Console.WriteLine("Dados carregados com sucesso!");
        }
        else
        {
            Console.WriteLine("Arquivo não encontrado :(");
        }
    }


}