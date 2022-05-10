using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] float life;
    [SerializeField] Image imageLife;
    float maxLife;
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
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
    public void DoDamage(float damage)
    {
        life -= damage;
        if (life <= 0)
        {
            life = 0;
            Die();
        }
        imageLife.DOFillAmount(life / maxLife, 0.5f);
    }

    private void Die()
    {
        anim.SetTrigger("death");
    }
}
