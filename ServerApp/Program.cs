using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
namespace ServerApp
{
    class Program
    {
        private const int SERVER_PORT = 3535;
        private const int MAXIMUM_CLIENT_QUEUE = 5;
        static void Main(string[] args)
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), SERVER_PORT);

            serverSocket.Bind(endPoint);

            try
            {
                serverSocket.Listen(MAXIMUM_CLIENT_QUEUE);
                Console.WriteLine("СЕрвер Запущен...");
                while (true)
                {
                    Socket clientSocket = serverSocket.Accept();

                    Console.WriteLine("У нас гости");
                    
                    int bytes;
                    byte[] buffer = new byte[1024];

                    StringBuilder stringBuilder = new StringBuilder();

                    do
                    {
                        bytes = clientSocket.Receive(buffer);
                        stringBuilder.Append(Encoding.Default.GetString(buffer));
                    }
                    while (clientSocket.Available > 0);
                    Console.WriteLine(stringBuilder);

                    clientSocket.Shutdown(SocketShutdown.Receive);
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                serverSocket.Close();
            }
        }
    }
}
