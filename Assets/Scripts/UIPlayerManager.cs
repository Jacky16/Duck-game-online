using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class UIPlayerManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI namePlayerText;

    private void Start()
    {
        LoadNamePlayer();
    }

    void LoadNamePlayer()
    {
        
        foreach (var  player in PhotonNetwork.PlayerList)
        {
            if (player.ActorNumber == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                namePlayerText.text = player.NickName;
            }
        }
    }
}
