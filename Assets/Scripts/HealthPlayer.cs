using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Photon.Pun;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] float life;
    [SerializeField] Image imageLife;
    PhotonView photonView;

    float maxLife;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
    }
    private void Start()
    {
        maxLife = life;
    }
    public void AddHealth(float amount)
    {
        life += amount;
        if (life > maxLife)
        {
            life = maxLife;
        }
        imageLife.DOFillAmount(life / maxLife, 0.5f);
    }
    public void TakeDamage(float damage)
    {
        life -= damage;
        print(life);
        if (life <= 0)
        {
            imageLife.DOFillAmount(life / maxLife, 0.5f);
            life = 0;
            Die();
        }
        imageLife.DOFillAmount(life / maxLife, 0.5f);
        

    }

 
    public float GetHealth()
    {
        return life;
    }
    private void Die()
    {
        anim.SetTrigger("death");
    }

    public bool IsMíne()
    {
        return photonView.IsMine;
    }
}
