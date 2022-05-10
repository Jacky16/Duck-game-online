using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PhotonView photonView;
    float damage = 10;
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
  
    public void SetDamage(float _damage)
    {
        damage = _damage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out HealthPlayer healthPlayer))
            healthPlayer.DoDamage(damage);

        photonView.RPC("NetworkDestroy", RpcTarget.All);
    }
    [PunRPC]
    void NetworkDestroy()
    {
        Destroy(gameObject);
    }
}


