using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform spawnPlayer1;
    [SerializeField] Transform spawnPlayer2;
    Class.ClassType playerClass;
    private void Awake()
    {

        playerClass = NetworkManager.instance.GetCurrentUser().GetClass().GetClassType();
      

        if (PhotonNetwork.IsMasterClient)
        {
            SpawnPlayer(spawnPlayer1.position);
        }
        else
        {
            SpawnPlayer(spawnPlayer2.position);
        }
    }

    void SpawnPlayer(Vector3 posSpawn)
    {
        switch (playerClass)
        {
            case Class.ClassType.LIGHT:
                PhotonNetwork.Instantiate("Player-Light", posSpawn, Quaternion.identity);
                break;
                
            case Class.ClassType.HEAVY:
                PhotonNetwork.Instantiate("Player-Heavy", posSpawn, Quaternion.identity);
                break;
        }
    }
}
