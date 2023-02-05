using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 2.0f;
    public float jump = 2.5f;
    public float stopMultiplier;
    private Rigidbody2D rb2d;
    private Collider2D col;
    private bool isGrounded = false;
    private bool flipped = false;
    public LayerMask m_LayerMask;
    public Vector3 punchboxPos = new Vector3(1, 1);
    public Vector3 punchboxDim = new Vector3(2, 3);
    public Vector3 kickboxPos = new Vector3(1, -1);
    public Vector3 kickboxDim= new Vector3(1.5f, 0.5f);
    public Transform fistPunch;
    public Transform footKick;
    public OnHit player2Hit;
    private Animation hitAnim;
    private float lastPunch;
    private float lastKick;
    private enum attacks {punch = 0, kick = 1}
    // Start is called before the first frame update
    void Start()
    {
        rb2d= GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        hitAnim = GetComponent<Animation>();
        lastPunch = Time.time;
        lastKick = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float translation = Input.GetAxisRaw("Horizontal") * speed;
        if (translation < 0)
        {
            if (!flipped)
            {
                punchboxPos.Set(-punchboxPos.x, punchboxPos.y, punchboxPos.z);
                kickboxPos.Set(-kickboxPos.x, kickboxPos.y, kickboxPos.z);
            }
            flipped = true;
        }
        else if (translation > 0)
        {
            if (flipped)
            {
                punchboxPos.Set(-punchboxPos.x, punchboxPos.y, punchboxPos.z);
                kickboxPos.Set(-kickboxPos.x, kickboxPos.y, kickboxPos.z);
            }
            flipped = false;
        }
        rb2d.AddForce(new Vector2(translation, 0));
        if (translation == 0 && isGrounded)
        {
            rb2d.AddForce(new Vector2(-rb2d.velocity.x * stopMultiplier, 0));
        }
    }

    void Update()
    {
        fistPunch.position = new Vector3(transform.position.x + punchboxPos.x, transform.position.y + punchboxPos.y);
        footKick.position = new Vector3(transform.position.x + kickboxPos.x, transform.position.y + kickboxPos.y);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb2d.AddForce(Vector2.up * jump, ForceMode2D.Impulse);
        }

        Collider2D[] punchCollider = Physics2D.OverlapBoxAll(transform.position + punchboxPos, punchboxDim, m_LayerMask);
        Collider2D[] kickCollider = Physics2D.OverlapBoxAll(transform.position + kickboxPos, kickboxDim, m_LayerMask);

        if (Input.GetButtonDown("Fire1") && Time.time - lastPunch >= 1.25)
        {
            lastPunch = Time.time;
            hitAnim.Play("Punch");
        }

        if (Input.GetButtonDown("Fire2") && Time.time - lastKick >= 0.75)
        {
            lastKick = Time.time;
            hitAnim.Play("Kick");
        }

        if (punchCollider.Length > 0 && Time.time - lastPunch < 0.05)
        {
            Debug.Log("AAAAAAAAAAAAAAAAAAAAA");
            player2Hit.imHit(flipped, ((int)attacks.punch));
            
        }

        if (kickCollider.Length > 0 && Time.time - lastKick < 0.05)
        {
            Debug.Log("KIIIIIIIIIIIIIIIIIIIIICK");
            player2Hit.imHit(flipped, ((int)attacks.kick));
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            Debug.Log("YOU ARE GROUNDED GROUNDED GROUNDED GROUNDED");
        }
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
        Gizmos.DrawWireCube(transform.position + punchboxPos, punchboxDim);
        Gizmos.DrawWireCube(transform.position + kickboxPos, kickboxDim);
    }
}