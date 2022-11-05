using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]  private float speed = 10f;
    [SerializeField]  private float jumpForce = 2f;
    [SerializeField] private float dashSpeed = 20f;

    private CollisionHandler coll;

    //Booleans
    private bool groundTouch;
    public bool hasDashed;
    public bool isDashing = false;

    //animation
    [SerializeField] animateSprite aS;

    private BoxCollider2D collider;
    [SerializeField] private PhysicsMaterial2D bouncy;
    private PhysicsMaterial2D defaultPhysicsMaterial;

    private bool spriteDir;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        coll = GetComponent<CollisionHandler>();
        rb = GetComponent<Rigidbody2D>();
        defaultPhysicsMaterial = collider.sharedMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        //Get input
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 dir = new Vector2(x, y);

        //Walk in horizontal direction
        if (!isDashing) 
        {
            Walk(dir); 
        }
        
        //flipping the character
        if (x > 0)
        {
            spriteDir = true;
            aS.FlipSprite(spriteDir);
        }
        else if (x < 0)
        {
            spriteDir = false;
            aS.FlipSprite(spriteDir);
        }

        //jump if button is pressed
        if (Input.GetButtonDown("Jump"))
        {
            if (coll.isGrounded)
            {
                Jump();
            }
        }
        if(Input.GetButtonDown("Fire1") && !hasDashed)
        {
            Dash(dir);
        }

        ///////////// Ground touch/////////////
        
        if (coll.isGrounded && !groundTouch)
        {
            TouchedGround();
        }
        if (!coll.isGrounded && groundTouch)
        {
            groundTouch = false;
        }


    }

    private void Walk(Vector2 dir) 
    {
        rb.velocity = (new Vector2(dir.x * speed, rb.velocity.y));
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpForce;
    }

    private void TouchedGround() 
    {
        //only call when player has left the ground and then retouches the ground
        groundTouch = true;
        hasDashed = false; //reset dash

    }

    private void Dash(Vector2 dir)
    {
        hasDashed = true;
        rb.velocity = new Vector2(0, 0);
        Vector2 correctedDirection = Ratchet(dir);
        Debug.Log(correctedDirection*dashSpeed);
        rb.velocity = correctedDirection * dashSpeed;

        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        aS.FlashWhite();
        collider.sharedMaterial = bouncy;


        rb.gravityScale = 0;
        GetComponent<BetterJump>().enabled = false;
        isDashing = true;

        yield return new WaitForSeconds(.3f);

        aS.DefaultMaterial();
        collider.sharedMaterial = defaultPhysicsMaterial;
        rb.gravityScale = 3;
        GetComponent<BetterJump>().enabled = true;
        isDashing = false;
    }


    private Vector2 Ratchet(Vector2 dir)
    { //normalized ratchet direction

        if (dir.x == 0)
        {
            if (spriteDir)
            {
                if (dir.y >= 0)
                {
                    return new Vector2(Mathf.Sqrt(2) / 2, Mathf.Sqrt(2) / 2);
                }
                else
                {
                    return new Vector2(Mathf.Sqrt(2) / 2, -Mathf.Sqrt(2) / 2);
                }
            }

            else
            {
                if (dir.y >= 0)
                {
                    return new Vector2(-Mathf.Sqrt(2) / 2, Mathf.Sqrt(2) / 2);
                }
                else
                {
                    return new Vector2(-Mathf.Sqrt(2) / 2, -Mathf.Sqrt(2) / 2);
                }
            }
        }


        else if (dir.x > 0)
        {
            if (dir.y >= 0)
            {
                return new Vector2(Mathf.Sqrt(2) / 2, Mathf.Sqrt(2) / 2);
            }
            else
            {
                return new Vector2(Mathf.Sqrt(2) / 2, -Mathf.Sqrt(2) / 2);
            }
        }
        else
        {
            if (dir.y >= 0)
            {
                return new Vector2(-Mathf.Sqrt(2) / 2, Mathf.Sqrt(2) / 2);
            }
            else
            {
                return new Vector2(-Mathf.Sqrt(2) / 2, -Mathf.Sqrt(2) / 2);
            }

        }

    }
}
