using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class HealthPlayer : MonoBehaviour
{
    float life;
    [SerializeField] Image imageLife;
    public PhotonView photonView {  get;  private set; }
    public bool isDeath;
    float maxLife;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        photonView = GetComponent<PhotonView>();
    }
    private void Start()
    {
        isDeath = false;
    }

    public void AddHealth(float amount)
    {
        life += amount;
        if (life > maxLife)
        {
            life = maxLife;
        }
        UpdateUI();
    }
    [PunRPC]
    public void TakeDamage(float damage)
    {
        print("Vida antes del damage: " + life);
        life -= 10;
        Debug.Log("TakeDamage");
        if (life <= 0)
        {            
            life = 0;
            Die();
        }
        
        UpdateUI();
        print(life);

    }

    void UpdateUI()
    {
        imageLife.DOFillAmount(life / maxLife, 0.5f);
    }


    public float GetHealth()
    {
        return life;
    }
    private void Die()
    {
        anim.SetTrigger("death");
        GetComponent<PlayerController>().BlockMovement();
        isDeath = true;
        GameManager.singletone.FinishGame();
    }

    public bool IsMíne()
    {
        return photonView.IsMine;
    }

    public void SetLife(float v)
    {
        life = v;
        maxLife = life;     
        UpdateUI();
    }
}
