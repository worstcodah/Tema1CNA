using Grpc.Net.Client;
using System;

namespace PersonClient
{
    class Program
    {

        static void Main(string[] args)
        {



            var channel = GrpcChannel.ForAddress("https://localhost:5001");


            var client = new GetPersonDataService.GetPersonDataServiceClient(channel);

            Console.WriteLine("Doriti sa va trimiteti datele personale catre server si sa primiti un raspuns?\n N/n -> nu\n Orice alta tasta -> da");
            char choice = Console.ReadKey().KeyChar;
            while (choice != 'N' && choice != 'n')
            {
                Console.WriteLine("\nNumele:");
                var name = Console.ReadLine();
                Console.WriteLine("\nCNP:");
                var cnp = Console.ReadLine();

                var data = client.GetPersonData(new Person { Name = name, Cnp = cnp });

                if (!String.IsNullOrEmpty(data.Gender))
                {
                    Console.WriteLine("\n" + name + " are varsta de " + data.Age + " ani si e de " + data.Gender);
                }
                else
                {
                    Console.WriteLine("Datele introduse nu sunt valide ! (mai multe informatii in consola serverului)");
                }

                Console.WriteLine("Doriti sa va trimiteti datele personale catre server si sa primiti un raspuns?\n N/n -> nu\n Orice alta tasta -> da");
                choice = Console.ReadKey().KeyChar;

            }

            channel.ShutdownAsync();
        }
    }
}
