using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourPun, IPunObservable
{
    [Header("Stats")]
    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    Rigidbody2D rb2d;
    Vector2 axis;

    PhotonView photoView;
    Vector3 enemyPos = Vector3.zero;
    private void Awake()
    {
        photoView = GetComponent<PhotonView>();
        rb2d = GetComponent<Rigidbody2D>();

        PhotonNetwork.SendRate = 20;
        PhotonNetwork.SerializationRate = 20;
    }
    private void Update()
    {
        if (photoView.IsMine)
        {
            CheckInputs();
        }
        
    }

    

    private void FixedUpdate()
    {
        if (!photoView.IsMine)
        {
            SmoothReplicate();
        }
        rb2d.velocity = (new Vector2(axis.x * speed, rb2d.velocity.y));
    }

    void CheckInputs()
    {
        axis.x = Input.GetAxis("Horizontal");

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    void Jump()
    {
        rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    private void SmoothReplicate()
    {
        rb2d.position = Vector3.Lerp(transform.position, enemyPos, 20 * Time.fixedTime);
    }
    void SetAxis(Vector2 _axis)
    {
        axis = _axis;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(rb2d.position);

        }else if (stream.IsReading)
        {
            enemyPos = (Vector2) stream.ReceiveNext();
        }
    }
}
