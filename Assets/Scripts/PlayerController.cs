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
    [SerializeField] Transform bulletSpawn;
    [SerializeField] float shootForce;
    [SerializeField] float shootDamage;
    [SerializeField] float shootCooldown;
    float shootCooldownTimer;

    Rigidbody2D rb2d;
    Vector2 axis;
    SpriteRenderer spriteRenderer;
    HealthPlayer healthPlayer;
    public bool isFlipped { get; private set; }

    PhotonView photoView;
    Animator anim;
    struct EnemyTransform
    {
        public Vector3 position;
        public Quaternion rotation;
        public float currHealth;
    }
    
    EnemyTransform enemyTransform;
    public Class.ClassType classType;
    public User currentUserPlayer { get; private set; }
    private void Awake()
    {
        photoView = GetComponent<PhotonView>();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthPlayer = GetComponent<HealthPlayer>();
        
        PhotonNetwork.SendRate = 50;
        PhotonNetwork.SerializationRate = 50;
    }
    private void Start()
    {
        currentUserPlayer = NetworkManager.instance.GetCurrentUser();
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

    void Flip()
    {
        //Flip player
        isFlipped = !isFlipped;
        spriteRenderer.flipX = isFlipped;  
        
        bulletSpawn.localPosition = new Vector3(bulletSpawn.localPosition.x * -1, bulletSpawn.localPosition.y, bulletSpawn.localPosition.z);
    }

    void Jump()
    {
        rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
    void Shoot()
    {
        GameObject bullet = null;
        float rotationZ = isFlipped ? 180 : 0;
        if (PhotonNetwork.CurrentRoom != null)
        {
            switch (classType) {
                case Class.ClassType.LIGHT:
                    bullet = PhotonNetwork.Instantiate("LightBullet", bulletSpawn.position, Quaternion.Euler(0, 0, rotationZ));
                    break;
                    
                case Class.ClassType.HEAVY:
                    bullet = PhotonNetwork.Instantiate("HeavyBullet", bulletSpawn.position, Quaternion.Euler(0, 0, rotationZ));
                    break;
            }
        }
        if( bullet != null)
        {
            bullet.GetComponent<Bullet>().SetDamage(shootDamage);
        }
    }

    [PunRPC]
    public void Damage()
    {
        Debug.Log("Player vida: " + healthPlayer.GetHealth());
    }
    private void SmoothReplicate()
    {
        rb2d.position = enemyTransform.position;
        transform.rotation = enemyTransform.rotation;
    }
    
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
            stream.SendNext(spriteRenderer.flipX);         
            stream.SendNext(healthPlayer.GetHealth());
        }
        else if (stream.IsReading)
        {
            enemyTransform.position = (Vector3) stream.ReceiveNext();
            enemyTransform.rotation = (Quaternion) stream.ReceiveNext();
            spriteRenderer.flipX = (bool)stream.ReceiveNext();
            enemyTransform.currHealth = (float)stream.ReceiveNext();
        }
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance, transform.position.z));
    }
}
