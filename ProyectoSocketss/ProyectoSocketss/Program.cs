using ServidorSocketUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSocketss
{
    public class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);

            Console.WriteLine("Iniciando Servidor en puerto {0}", puerto);
            ServerSocket servidor = new ServerSocket(puerto);

            if (servidor.Iniciar())
            {
                //OK, puede conectar
                Console.WriteLine("Servidor iniciado");
                while (true)
                {
                    Console.WriteLine("Esperando Cliente...");
                    Socket socketCliente = servidor.ObtenerCliente();
                    
                    ClienteCom cliente = new ClienteCom(socketCliente);
                    
                    Console.WriteLine("------------------------------------------------------------------------");
                    Console.WriteLine("Se ha conectado un cliente. El empezara a hablarte en breve");
                    Console.WriteLine("Nota: Cuando quieras cerrar la conexión con el cliente, escribe 'chao'");
                    Console.WriteLine("------------------------------------------------------------------------");


                    bool mantenerConexion = true;
                    while (mantenerConexion)
                    {

                        string respuesta = cliente.Leer();
                        Console.WriteLine("");
                        Console.WriteLine("CLIENTE: {0}", respuesta);

                        if (respuesta == "chao")
                        {
                            Console.WriteLine("--------------------Se cerró la conexión--------------------");
                            cliente.Escribir(respuesta);
                            mantenerConexion = false;
                            cliente.Desconectar();
                        }
                        else
                        {
                            
                            Console.WriteLine("");
                            Console.Write("Tu: ");
                            string nombre = Console.ReadLine().Trim();
                            cliente.Escribir(nombre);
                        }


                    }
                }

            }
            else
            {
                Console.WriteLine("ERRRORR!!!, el puerto {0} esta en uso", puerto);
            }
        }
    }
}
