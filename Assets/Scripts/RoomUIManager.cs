using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomUIManager : MonoBehaviour
{
    [SerializeField]
    Button createButton;

    [SerializeField]
    Button joinButton;

    [SerializeField]
    Text createText;

    [SerializeField]
    Text joinText;

    [SerializeField] GameObject canvasWait;
    [SerializeField] Button backButton;

    private void Awake()
    {
        createButton.onClick.AddListener(CreateRoom);
        joinButton.onClick.AddListener(JoinRoom);
        backButton.onClick.AddListener(Back);
    }

    private void Back()
    {
        PhotonManager.instance.LeaveCurrentRoom();
        canvasWait.SetActive(false);
    }

    void CreateRoom()
    {
        PhotonManager.instance.CreateRoom(createText.ToString());
        createText.text = "";
        canvasWait.SetActive(true);
    }

    void JoinRoom()
    {    
        PhotonManager.instance.JoinRoom(joinText.ToString());
        joinText.text = "";
    }
}
