using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public static User currentUser = new User();
    [SerializeField]
    List<Class> avaiableAvatars = new List<Class>();
    private void Awake()
    {
        if (instance != null && instance != this)
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

    #region Main Functions
    private void ManageData(string data)
    {
        //Si recibo ping devuelvo 1 como respuesta al servidor
        if (data == "ping")
        {
            Debug.Log("Recibo ping");
            writer.WriteLine("1");
            writer.Flush();
        }
        else if (data.Split('/')[0] == "2")
        {
            Debug.Log("Logeo Correcto");
            //Obtenemos el nombre de usuario y el ID que enviamos desde el servidor
            //SetNewUser(data);
            
            LoadClassesScene();
            writer.Flush();
        }
        else if (data == "3")
        {
            Debug.Log("Logeo Incorrecto");
            writer.Flush();
        }
        else if(data == "UserNick")
        {
            Debug.Log(data.Split('/')[1]);
            writer.Flush();
        }
        else if (data.Split('|')[0] == "GetAllClasses")
        {
            string [] classes = data.Split('|');
            SetAvaiableClasses(classes);
        }
        else if(data.Split('|')[0] == "4"){
            string[] userAndClasses = data.Split('|');

            string[] dataUser = userAndClasses[1].Split('/');
            
            //Obtener datos del usuario
            SetNewUserWithClass(dataUser);

            LoadRoomsScene();
            
            Debug.Log("Logeo con clase asignada");
        }else if(data.Split('|')[0] == "Class")
        {
            //Enviamos el clase enemigo a PhotonManager
            PhotonManager.instance.enemyClass = GetClassByString(data.Split('|')[1]);
            print($"La clase del enemigo es {data.Split('|')[1]} y yo soy {PhotonNetwork.NickName}");
            
        }
    }
    public void LogIn(string nick, string password)
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
    public void Register(string nick, string password, string email)
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
            writer.WriteLine("Register" + "/" + nick + "/" + password + "/" + email);

            //Limpio el writer de datos
            writer.Flush();

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
    
    //Enviar al servidor la clase para añadirla a la lista de clases del usuario en la BDD
    public void SendInfoToAddClassAndPlayer(string className)
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
            writer.WriteLine("AddClassUser" + "/" + currentUser.GetId() + "/" + className);

            //Limpio el writer de datos
            writer.Flush();

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }

    }

    public void SendNicknameToGetClass(string nickname)
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
            writer.WriteLine("GetClassByNickName" + "/" + nickname);

            //Limpio el writer de datos
            writer.Flush();

        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    #endregion

    #region Getters
    public User GetCurrentUser()
    {
        return currentUser;
    }

    public List<Class> GetAvaiableClasses()
    {
        return avaiableAvatars;
    }

    #endregion
    
    #region Setters
    void SetNewUserWithClass(string [] userWithClass)
    {
        string nick = userWithClass[0];
        string id = userWithClass[1];
        string idClassAssignded = userWithClass[2];
        PhotonNetwork.NickName = nick;

        currentUser.nick = nick;
        currentUser.idInDatabase = int.Parse(id);
        currentUser.idClassAssigned = int.Parse(idClassAssignded);

        

        Debug.Log("El usuario que ha iniciado sesion es: " + nick + " y su clase asignada es " + idClassAssignded);
   
    }

    //Recoger todas las clases que hay en el servidor
    void SetAvaiableClasses(string[] avatars)
    {
        //Elimino el identificador del tipo de datoa
        List<String> listStringsAvatars = new List<string>(avatars);
        if (listStringsAvatars[0].Contains("GetAllClasses"))
        {
            listStringsAvatars.RemoveAt(0);
        }
        //Recorro los campos de cada avatar
        for (int i = 0; i < listStringsAvatars.Count; i++)
        {
            //Los divido por '/' y despues los asigno en 'Avatar'
            string[] fieldsAvatar = listStringsAvatars[i].Split('/');

            string name = fieldsAvatar[0];
            float speed = float.Parse(fieldsAvatar[1]);
            float fireRate = float.Parse(fieldsAvatar[2]);
            float life = float.Parse(fieldsAvatar[3]);
            float damage = float.Parse(fieldsAvatar[4]);
            int idDatabase = int.Parse(fieldsAvatar[5]);

            Class avatar = new Class();
            avatar.name = name;
            avatar.speed = speed;
            avatar.fireRate = fireRate;
            avatar.life = life;
            avatar.damage = damage;
            avatar.idDatabase = idDatabase;


            avaiableAvatars.Add(avatar);
        }
        foreach (Class c in avaiableAvatars)
        {
            if (c.idDatabase == currentUser.GetIdClassAssigned())
            {
                currentUser.SetClass(c);
            }
        }
    }

    //Asignar una clase a un usuario que viene del servidor
    public Class GetClassByString(string _class)
    {
        string[] fieldsAvatar = _class.Split('/');
        string name = fieldsAvatar[0];
        float speed = float.Parse(fieldsAvatar[1]);
        float fireRate = float.Parse(fieldsAvatar[2]);
        float life = float.Parse(fieldsAvatar[3]);
        float damage = float.Parse(fieldsAvatar[4]);
        int idDatabase = int.Parse(fieldsAvatar[5]);

         Class classToReturn = new Class(name, speed, fireRate, life,damage,idDatabase);
        return classToReturn;
    }

    public Class GetClassByNickname(string nicknamePlayer)
    {
        foreach (Class c in avaiableAvatars)
        {
            if (c.GetIdBDD() == currentUser.GetIdClassAssigned())
            {
                if (nicknamePlayer == currentUser.GetNickName())
                {
                    currentUser.SetClass(c);
                    currentUser.SetClass(c);
                    Debug.Log("Mi raza es: " + c.GetNameClass());
                    return c;
                    
                }
            }
        }
        return null;
    }


    public void AssingClassToUser(Class _class)
    {
        currentUser.SetClass(_class);
    }

    #endregion

    public void LoadClassesScene()
    {
        SceneManager.LoadScene("ClassesScene");
    }
    public void LoadRoomsScene()
    {
        SceneManager.LoadScene("RoomScene");
    }

}

