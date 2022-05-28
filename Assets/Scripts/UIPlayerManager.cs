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
        //Get nickname
        string nickname = GetComponent<PhotonView>().Owner.NickName;
        //Set name
        namePlayerText.text = nickname;
    }
}
