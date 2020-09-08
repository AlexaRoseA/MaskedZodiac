using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    Vector3 characterScale;
    float characterScaleX;
    public bool isGrounded;

    public Sprite priorMask;
    public Sprite currentMask;
    public List<Sprite> Masks;
    public int maskNum = 0;

    public GameObject mask;

    Animator animate;

    void Start()
    {
        priorMask = Masks[0];
        currentMask = Masks[0];
        characterScale = transform.localScale;
        characterScaleX = characterScale.x;
        animate = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Jump();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animate.SetBool("Jumping", true);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
        }
        else
        {
            animate.SetBool("Jumping", false);
            Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            transform.position += movement * Time.deltaTime * moveSpeed;

            if (movement != Vector3.zero)
            {
                animate.SetBool("Walking", true);
            }
            else
            {
                animate.SetBool("Walking", false);
            }
        }

        if(!this.animate.GetCurrentAnimatorStateInfo(0).IsName("MaskSwap") && Input.GetButtonDown("Mask Switch"))
        {
            Debug.Log("mask switch");
            animate.SetBool("MaskSwitch", true);
        }

        // Flip the Character:
        Vector3 characterScale = transform.localScale;
        if (Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = -0.2f;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = 0.2f;
        }
        transform.localScale = characterScale;
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            animate.SetBool("Jumping", true);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 5f), ForceMode2D.Impulse);
        } else
        {
            animate.SetBool("Jumping", false);
        }
    }

    //consider when character is jumping .. it will exit collision.
    public void OnCollisionExit2D(Collision2D collision)
    {
         if (collision.gameObject.tag == "groundLayers")
        {
            isGrounded = false;
            Debug.Log("not grounded");
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "groundLayers")
        {
            isGrounded = true;
            Debug.Log("grounded");
        }
    }

    public void MaskSwitch()
    {
        priorMask = Masks[maskNum];
        if (maskNum < Masks.Count - 1)
        {
            maskNum++;
        } else
        {
            maskNum = 0;
        }
        mask.GetComponent<SpriteRenderer>().sprite = Masks[maskNum];
        currentMask = Masks[maskNum];
        animate.SetBool("MaskSwitch", false);
    }
}
