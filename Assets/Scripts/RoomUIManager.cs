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

    private void Awake()
    {
        createButton.onClick.AddListener(CreateRoom);
        joinButton.onClick.AddListener(JoinRoom);
    }

    void CreateRoom()
    {
        PhotonManager.instance.CreateRoom(createText.ToString());
    }

    void JoinRoom()
    {
        PhotonManager.instance.JoinRoom(joinText.ToString());
    }
}
