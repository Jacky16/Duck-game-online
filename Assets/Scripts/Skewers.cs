using Photon.Pun;
using UnityEngine;

public class Skewers : MonoBehaviour
{
    [SerializeField] float forceKnockBack = 10;
    [SerializeField] float damage = 10;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Damage player
        if (collision.gameObject.tag == "Player")
        {
            //Apply knockback
            Vector2 dir = collision.transform.position - transform.position;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(dir.normalized * forceKnockBack, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<HealthPlayer>().photonView.RPC("TakeDamage", RpcTarget.All, damage);
        }
    }
}
