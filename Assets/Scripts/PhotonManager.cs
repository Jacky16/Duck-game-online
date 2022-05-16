using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public static PhotonManager instance;
    public string nickEnemy;
    public Class playerClass;
    public Class enemyClass;
    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
            PhotonConnect();
            Debug.Log("Estoy en la escena de salas y soy " + PhotonNetwork.NickName + " : " + NetworkManager.instance.GetCurrentUser().GetNickName());
        }
    }

    private void PhotonConnect()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conexion realizada correctamente");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Conexion realizada correctamente");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Accedido al Lobby");        
    }

    public void CreateRoom(string nameRoom)
    {
        PhotonNetwork.CreateRoom(nameRoom, new RoomOptions { MaxPlayers = 2});        
    }
    public void JoinRoom(string nameRoom)
    {
        PhotonNetwork.JoinRoom(nameRoom);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Me he unido a la Sala" + PhotonNetwork.CurrentRoom.Name + "con" 
            + PhotonNetwork.CurrentRoom.PlayerCount + "Jugadores conectados a ellas");
        
      
        Debug.Log("Has joined: " + NetworkManager.instance.GetCurrentUser().GetNickName());

        foreach (Player player in PhotonNetwork.CurrentRoom.Players.Values)
        {
            if (player.NickName != PhotonNetwork.NickName)
            {
                nickEnemy = player.NickName;

                //Conseguir Query para obtneer clase enemigo
                NetworkManager.instance.SendNicknameToGetClass(nickEnemy);
            }
        }                 
    }
    
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Error al entrar en la sala, Error: " + returnCode + " que significa: " + message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("My nick is: " + NetworkManager.instance.GetCurrentUser().GetNickName());
        //Get all players in the room
       
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                nickEnemy = newPlayer.NickName;
                NetworkManager.instance.SendNicknameToGetClass(nickEnemy);
                Invoke("LoadGameplay", 5);
            }
        }     
    }

    void LoadGameplay()
    {
        PhotonNetwork.LoadLevel("GameplayScene");
    }

    public void LeaveCurrentRoom()
    {
        PhotonNetwork.LeaveRoom(true);
    }

   
}
