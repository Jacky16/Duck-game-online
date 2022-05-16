using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform spawnPlayer1;
    [SerializeField] Transform spawnPlayer2;
    public static Class playerClass;
    
    
    private void Awake()
    {

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.NickName == PhotonNetwork.NickName)
            {
                Class classPlayerInRoom = NetworkManager.instance.GetClassByNickname(player.NickName);
                
                Transform posSpawn;
                
                if (player.IsMasterClient)
                {
                    posSpawn = spawnPlayer1;
                    playerClass = classPlayerInRoom;
                }
                else
                {
                    posSpawn = spawnPlayer2;
                }
                
                switch (classPlayerInRoom.GetNameClass())
                {
                    case "Light":

                        PhotonNetwork.Instantiate("Player-Light", posSpawn.position, Quaternion.identity);
                        Debug.Log("El nombre del player es: " + player.NickName + " y tiene la clase " + classPlayerInRoom.GetNameClass());

                        break;

                    case "Heavy":

                        PhotonNetwork.Instantiate("Player-Heavy", posSpawn.position, Quaternion.identity);
                        Debug.Log("El nombre del player es: " + player.NickName + "y tiene la clase " + classPlayerInRoom.GetNameClass());
                        break;
                }
            }
        }         
    }
}
