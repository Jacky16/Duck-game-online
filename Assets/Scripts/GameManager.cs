using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Spawners")]
    [SerializeField] Transform spawnPlayer1;
    [SerializeField] Transform spawnPlayer2;

    [SerializeField] GameObject canvasWin;
    [SerializeField] GameObject canvasDefeat;
    public static Class playerClass;
    public static GameManager singletone;
    
    private void Awake()
    {
        //Cursor.visible = false;

        if (singletone == null)
        {
            singletone = this;
        }
        else
        {
            Destroy(gameObject);
        }
        InitPlayers();
    }

    private void InitPlayers()
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

    public void FinishGame()
    {
        //Get all HealthPlayer of scene insade of tag player
        HealthPlayer[] healthPlayers = GameObject.FindObjectsOfType<HealthPlayer>();

        foreach (HealthPlayer healthPlayer in healthPlayers)
        {
            bool isOwner = healthPlayer.GetComponent<PhotonView>().IsMine;
            if (isOwner)
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                if (healthPlayer.isDeath)
                {
                    canvasDefeat.SetActive(true);
                }
                else
                {
                    canvasWin.SetActive(true);
                }               
            }
            
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}
