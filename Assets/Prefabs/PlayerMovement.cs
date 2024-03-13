using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 15f;
    public float jumpPower = 5f;
    public float dashPower = 5f;

    private bool isRun;
    private bool isGround;
    public LayerMask layer;


    public float rotSpeed = 10f;

    Vector3 velocity = Vector3.zero;

    Vector3 dir = Vector3.zero;

    Animator animator;
    Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
        if (Input.GetButtonDown("Jump") && isGround/*&& animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")*/)
        {
            Vector3 jump = Vector3.up * jumpPower;
            rb.AddForce(jump, ForceMode.VelocityChange);
            //animator.SetTrigger("Jump");
        }

        dir.x = Input.GetAxisRaw("Horizontal");
        dir.z = Input.GetAxisRaw("Vertical");
        isRun = Input.GetButton("Dash");

        animator.SetBool("Walk", dir != Vector3.zero);
        animator.SetBool("Run", isRun);

        //if (Input.GetButtonDown("Dash"))
        //{
        //    Vector3 dash = transform.forward * dashPower;
        //    rb.AddForce(dash, ForceMode.VelocityChange);
        //}
    }

    void FixedUpdate()
    {
        //float xMove = Input.GetAxisRaw("Horizontal");
        //float zMove = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = dir.normalized * (isRun ? runSpeed : walkSpeed);

        //Vector3 moveHorizontal = transform.right * xMove;
        //Vector3 moveVertical = transform.forward * zMove;
        //velocity = (moveHorizontal + moveVertical).normalized * (isRun ? runSpeed : walkSpeed);

        if (dir != Vector3.zero)
        {
            if (Mathf.Sign(transform.forward.x) != Mathf.Sign(dir.x) ||
                Mathf.Sign(transform.forward.z) != Mathf.Sign(dir.z))
            {
                transform.Rotate(0, 1, 0);
            }
            transform.forward = Vector3.Lerp(transform.forward, dir, rotSpeed * Time.fixedDeltaTime);
        }

        rb.MovePosition(transform.position + moveDirection * Time.fixedDeltaTime);

        //AnimatorClipInfo[] clips = animator.GetCurrentAnimatorClipInfo(0);
        //LogManager.Log(nextPos.magnitude / runSpeed);

        //Debug.Log(velocity.magnitude);


        //rb.velocity = velocity;
        //MoveCharacter(velocity);
    }
    void CheckGround()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position + Vector3.up * 0.2f, Vector3.down, out hit, 0.4f, layer))
        {
            isGround = true;
        }
        else
        {
            isGround = false;
        }
    }
}
