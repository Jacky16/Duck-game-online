using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager instance;

    TcpClient socket;
    NetworkStream stream;
    StreamWriter writer;
    StreamReader reader;
    bool connected;
    const string host = "192.168.1.27";
    const int port = 6543;
    private void Awake()
    {
      if(instance != null && instance != this)
        {
            Destroy(instance);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    private void Update()
    {
        //Si estoy conectado reviso si existen datos
        if (connected)
        {
            //Si hay datos disponibles para leer
            if (stream.DataAvailable)
            {
                //Leo una linea de datos
                string data = reader.ReadLine();

                //Si los datos no son nulos los trabajo
                if (data != null)
                {
                    ManageData(data);
                }
            }
        }
    }

    public void ConnectToServer(string nick, string password)
    {
        try
        {
            //Instancia la clase para gestionar la conexion y el streaming de datos
            socket = new TcpClient(host, port);
            stream = socket.GetStream();

            //Si hay streaming de datos hay conexion
            connected = true;

            //Instancio clases de lectura y escritura
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);

            //Envio 0 con nick y ususario separados por / ya que son los valores que he definido en el servidor
            writer.WriteLine("Login" + "/" + nick + "/" + password);

            //Limpio el writer de datos
            writer.Flush();

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    private void ManageData(string data)
    {
        //Si recibo ping devuelvo 1 como respuesta al servidor
        if (data == "ping")
        {
            Debug.Log("Recibo ping");
            writer.WriteLine("1");
            writer.Flush();
        }
    }
}
