using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PhotonView photonView;
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
  

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            collision.GetComponent<PlayerController>().Damage();
        
        photonView.RPC("NetworkDestroy", RpcTarget.All);
    }
    [PunRPC]
    void NetworkDestroy()
    {
        Destroy(gameObject);
    }
}


