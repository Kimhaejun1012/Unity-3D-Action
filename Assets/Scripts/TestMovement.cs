using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 15f;
    public float jumpPower = 5f;
    public float dashPower = 5f;

    public float finalSpeed;

    private bool isRun;
    public LayerMask layer;

    public bool toggleCamRotation;
    Camera _cam;

    public float rotSpeed = 10f;
    Vector3 velocity = Vector3.zero;

    Vector3 dir = Vector3.zero;

    Animator animator;
    Rigidbody rb;

    //CharacterController characterController;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        dir.x = Input.GetAxisRaw("Horizontal");
        dir.z = Input.GetAxisRaw("Vertical");
        //dir.Normalize();

        //CheckGround();
        //if (Input.GetButtonDown("Jump") && isGround/*&& animator.GetCurrentAnimatorStateInfo(0).IsName("Jump")*/)
        //{
        //    Vector3 jump = Vector3.up * jumpPower;
        //    rb.AddForce(jump, ForceMode.VelocityChange);
        //    //animator.SetTrigger("Jump");
        //}


        if (Input.GetKey(KeyCode.LeftAlt))
        {
            toggleCamRotation = true;
        }
        else
        {
            toggleCamRotation = false;
        }
    }


    void FixedUpdate()
    {
        Vector3 moveH = Vector3.Scale(_cam.transform.right, new Vector3(1, 0, 1)) * dir.x;
        Vector3 moveV = Vector3.Scale(_cam.transform.forward, new Vector3(1, 0, 1)) * dir.z;
        Vector3 moveVec = (moveH + moveV).normalized;

        rb.MovePosition(transform.position + moveVec * (isRun ? runSpeed : walkSpeed) * Time.fixedDeltaTime);
    }
}
