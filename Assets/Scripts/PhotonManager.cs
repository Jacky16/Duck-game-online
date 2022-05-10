using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    public static PhotonManager instance;
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
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Error al entrar en la sala, Error: " + returnCode + " que significa: " + message);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.LoadLevel("GameplayScene");
            }
        }
    }

    public void LeaveCurrentRoom()
    {
        PhotonNetwork.LeaveRoom(true);
    }
}
