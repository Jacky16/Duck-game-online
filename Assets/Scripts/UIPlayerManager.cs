using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class UIPlayerManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI namePlayerText;

    private void Start()
    {
        LoadNamePlayer();
    }

    void LoadNamePlayer()
    {
        foreach (Player player in PhotonNetwork.PlayerList)
        {
            if (player.NickName == PhotonNetwork.NickName)
            {
                namePlayerText.text = player.NickName;
            }
        }
    }
}
