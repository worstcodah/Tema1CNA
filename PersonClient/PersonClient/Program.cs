using Grpc.Net.Client;
using System;

namespace PersonClient
{
    class Program
    {
        
        static void Main(string[] args)
        {
            const int NULL = 0;


            var channel = GrpcChannel.ForAddress("https://localhost:5001");


            var client = new GetPersonDataService.GetPersonDataServiceClient(channel);

            Console.WriteLine("Doriti sa va trimiteti datele personale catre server si sa primiti un raspuns? Y/y -> da / N/n -> nu");
            char choice = Console.ReadKey().KeyChar;
            while (choice != 'N' && choice != 'n')
            {
                Console.WriteLine("\nNumele:");
                var name = Console.ReadLine();
                Console.WriteLine("\nCNP:");
                var cnp = Console.ReadLine();

               var data = client.GetPersonData(new Person { Name = name, Cnp = cnp });

                if (data.Age != NULL)
                {
                    Console.WriteLine("\n" + name + " are varsta de " + data.Age + " ani si e de " + data.Gender);
                }
                else
                {
                    Console.WriteLine("Datele introduse nu sunt valide ! (mai multe informatii in consola serverului)");
                }
                Console.WriteLine("\n\nDoriti sa va trimiteti datele personale catre server si sa primiti un raspuns? Y/N");
                choice = Console.ReadKey().KeyChar;
                
            }

            channel.ShutdownAsync();
        }
    }
}
