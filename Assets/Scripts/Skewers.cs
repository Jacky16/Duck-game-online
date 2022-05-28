using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skewers : MonoBehaviour
{
    [SerializeField] float forceKnockBack = 10;
    private void OnTriggerEnter(Collider other)
    {
        //Damage player
        if (other.gameObject.tag == "Player")
        {
            //Apply knockback
            Vector2 dir = other.transform.position - transform.position;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(dir.normalized * forceKnockBack, ForceMode2D.Impulse);
            
            other.gameObject.GetComponent<HealthPlayer>().photonView.RPC("TakeDamage", RpcTarget.All, 10);
            

        }
    }
}
