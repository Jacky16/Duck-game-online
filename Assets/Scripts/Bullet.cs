using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PhotonView photonView;
    Rigidbody2D rb2d;
    float damage = 10;
    HealthPlayer healthPlayerEnemy;
    [SerializeField] GameObject vfx_hit;
    [SerializeField] Transform spawnVFX;
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        rb2d.velocity = transform.right * 10;
    }

    public void SetDamage(float _damage)
    {
        damage = _damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthPlayer healthPlayer))
        {
            healthPlayer.photonView.RPC("TakeDamage", RpcTarget.All, damage);
        }
        photonView.RPC("NetworkDestroy", RpcTarget.All);
        
    }
    
    [PunRPC]
    void NetworkDestroy()
    {
        //Instantiate Vfx
        Instantiate(vfx_hit, transform.position, transform.rotation,null);
        Destroy(gameObject);
    }
}


