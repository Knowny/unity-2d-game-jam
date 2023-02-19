using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    public KeyCode left;
    public KeyCode right;
    public KeyCode jump;
    public KeyCode shoot;
    
    public float shotCooldown;
    public bool canShoot;
    private float CD;

    public GameObject projectile;
    public Transform shootPoint;

    private Rigidbody2D theRB;

    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public bool isGrounded;
    private bool isMoving;

    private Animator anim;

    public AudioSource shootSound;
    public AudioSource walkSound;
    public AudioSource bounce;

    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, whatIsGround);

        //SHOOTING COOLDOWN
        CD -= Time.deltaTime;
        if(CD < 0){
            canShoot = true;
        }

        //WALKING
        if(Input.GetKey(left)){
            theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
            isMoving = true;
        }else if(Input.GetKey(right)){
            theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
            isMoving = true;
        }else{
            theRB.velocity = new Vector2(0, theRB.velocity.y);
            isMoving = false;
        }

        //WALKING SOUND
        if(isMoving && isGrounded){
            if(!walkSound.isPlaying){
                walkSound.Play();
            }
        }else{
            walkSound.Stop();
        }

        //Rotating while changing from left to right
        if(theRB.velocity.x < 0){
            transform.localScale = new Vector3(-1,1,1);
            
        }else if(theRB.velocity.x > 0){
            transform.localScale = new Vector3(1,1,1);
        }

        //JUMPING
        if(Input.GetKeyDown(jump) && isGrounded){
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }

        anim.SetFloat("Speed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("Grounded", isGrounded);

        //SHOOTING
        if(Input.GetKeyDown(shoot) && canShoot == true){
            GameObject projectileClone = (GameObject)Instantiate(projectile, shootPoint.position, shootPoint.rotation);
            projectileClone.transform.localScale = transform.localScale;

            anim.SetTrigger("Shoot");

            canShoot = false;
            CD = shotCooldown;

            shootSound.Play();
        }
    }

    //BOUNCE SOUNDS
    public void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Bounce"){
            bounce.Play();
        }
    }
}