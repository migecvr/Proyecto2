using ClienteUsuario.Comunicacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ClienteUsuario
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            bool repetir = true;
            do
            {
                repetir = true;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("-------------------------------------------------------------------------------");
                Console.WriteLine("Ingresa el numero del puerto y direccion ip del servidor al cual vas a conectar");
                Console.WriteLine("ejemplo");
                Console.WriteLine("Puerto:2050");
                Console.WriteLine("IP:127.0.0.1");
                Console.WriteLine("-------------------------------------------------------------------------------");

                Console.Write("Puerto: ");
                int puerto = Convert.ToInt32(Console.ReadLine().Trim());
                Console.Write("Servidor: ");
                string servidor = Console.ReadLine().Trim();

                Console.WriteLine("Conectando a Servidor {0} en puerto {1}", servidor, puerto);
                ClienteSocket clienteSocket = new ClienteSocket(servidor, puerto);
                if (clienteSocket.Conectar())
                {
                    Console.WriteLine("Conectado...");
                    Console.WriteLine("------------------------------------------------------------------------");
                    Console.WriteLine("Ahora pueder hablar con el servidor");
                    Console.WriteLine("Nota: Cuando quieras cerrar la conexión con el servidor, escribe 'chao'");
                    Console.WriteLine("------------------------------------------------------------------------");
                    Console.WriteLine("");

                    
                    Console.WriteLine("Comienza a escribir al servidor");
                    Console.Write("Tu: ");
                    string nombre = Console.ReadLine().Trim();
                    clienteSocket.Escribir(nombre);

                    bool mantenerConexion = true;
                    while (mantenerConexion)
                    {

                        string respuesta = clienteSocket.Leer();

                        if (respuesta == "chao")
                        {
                            Console.WriteLine("--------------------Se cerró la conexión--------------------");
                            clienteSocket.Escribir(respuesta);
                            mantenerConexion = false;
                            clienteSocket.Desconectar();
                        }
                        else
                        {
                            Console.WriteLine("");
                            Console.WriteLine("SERVIDOR: {0}", respuesta);
                            Console.WriteLine("");
                            Console.Write("Tu: ");
                            nombre = Console.ReadLine().Trim();
                            clienteSocket.Escribir(nombre);
                        }


                    }

                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("¡¡¡Error de Comunicacion!!!. Revisa si ingresastes bien los datos");

                }

                bool repCiclo = true;

                do
                {
                    Console.WriteLine("¿Volver a conectar?");
                    Console.WriteLine("1. Sí");
                    Console.WriteLine("0. No");
                    Console.Write(": ");
                    string opcion = Console.ReadLine().Trim();
                    if (opcion == "0")
                    {
                        repetir = false;
                        repCiclo = false;
                    }
                    else if (opcion == "1")
                    {
                        repCiclo = false;
                    }
                    else
                    {
                        Console.WriteLine("¡Debes seleccionar unas de las opciones!");
                    }

                } while (repCiclo);

            } while (repetir);


        }
    }
}
