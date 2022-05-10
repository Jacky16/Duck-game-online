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
    
    [Header("Ground Settins")]
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask whatIsGround;
    bool isGrounded;

    [Header("Shoot")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] float shootForce;

    Rigidbody2D rb2d;
    Vector2 axis;
    bool isFlipped;

    PhotonView photoView;
    Animator anim;
    struct EnemyTransform
    {
        public Vector3 position;
        public Quaternion rotation;
    }
    EnemyTransform enemyTransform;
    private void Awake()
    {
        photoView = GetComponent<PhotonView>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        PhotonNetwork.SendRate = 50;
        PhotonNetwork.SerializationRate = 50;
    }
    private void Update()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isMoving", axis.x != 0);
        if (photoView.IsMine)
        {
            CheckInputs();
        }
        else
        {
            SmoothReplicate();
        }
        
    }

    private void FixedUpdate()
    {     
        rb2d.velocity = (new Vector2(axis.x * speed, rb2d.velocity.y));
             
    }

    void CheckInputs()
    {
        axis.x = Input.GetAxis("Horizontal");
        
        if (axis.x < 0 && !isFlipped)
        {
            Flip();
        }
        else if (axis.x > 0 && isFlipped)
        {
            Flip();
        }
            //Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void Flip()
    {
        //Flip player
        isFlipped = !isFlipped;
        transform.Rotate(0f, 180f, 0f);
    }

    void Jump()
    {
        rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    void Shoot()
    {
        GameObject bullet;
        
        if(PhotonNetwork.CurrentRoom != null)
        {
            bullet = PhotonNetwork.Instantiate(bulletPrefab.name, bulletSpawn.position, transform.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * shootForce;
        }
    }
    private void SmoothReplicate()
    {
        rb2d.position = enemyTransform.position;
        transform.rotation = enemyTransform.rotation;
        
    }
    public void Damage()
    {
        photonView.RPC("NetworkDamage", RpcTarget.All);
    }
    [PunRPC]
    private void NetworkDamage()
    {
        Destroy(this.gameObject);
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);

        }
        else if (stream.IsReading)
        {
            enemyTransform.position = (Vector3) stream.ReceiveNext();
            enemyTransform.rotation = (Quaternion) stream.ReceiveNext();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance, transform.position.z));
    }
}
