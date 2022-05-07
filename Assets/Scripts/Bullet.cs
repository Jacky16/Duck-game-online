using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    private Rigidbody2D rb;
    PhotonView photonView;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        photonView = GetComponent<PhotonView>();
    }
    private void Start()
    {
        rb.velocity = new Vector2(speed, 0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<PlayerController>().Damage();
        
        photonView.RPC("NetworkDestroy", RpcTarget.All);
    }
    [PunRPC]
    void NetworkDestroy()
    {
        Destroy(gameObject);
    }
}


