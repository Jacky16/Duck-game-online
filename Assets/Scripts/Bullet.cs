using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    PhotonView photonView;
    Rigidbody2D rb2d;
    Vector3 dir;
    float damage = 10;
    HealthPlayer healthPlayerEnemy;
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
        if (collision.TryGetComponent(out HealthPlayer _healthPlayerEnemy))
        {
            _healthPlayerEnemy.GetComponent<PlayerController>().photonView.RPC("Damage", RpcTarget.All);
        }
        photonView.RPC("NetworkDestroy", RpcTarget.All);
    }

    [PunRPC]
    void UpdateLifePlayer()
    {
        HealthPlayer[] healthPlayers = FindObjectsOfType<HealthPlayer>();
        foreach (HealthPlayer hp in healthPlayers)
        {
            if (healthPlayerEnemy == hp)
            {
                hp.TakeDamage(damage);
            }
        }
    }
    [PunRPC]
    void NetworkDestroy()
    {
        Destroy(gameObject);
    }
}


