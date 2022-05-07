using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform spawnPlayer1;
    [SerializeField] Transform spawnPlayer2;
    private void Awake()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("Player", spawnPlayer1.position, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("Player",spawnPlayer2.position, Quaternion.identity);
        }
    }
}
