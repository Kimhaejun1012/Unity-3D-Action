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

    public float finalSpeed;

    private bool isRun;
    private bool isGround;
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
        animator = GetComponent<Animator>();
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


        isRun = Input.GetButton("Dash");

        animator.SetBool("Walk", dir != Vector3.zero);
        animator.SetBool("Run", isRun);

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            toggleCamRotation = true;
        }
        else
        {
            toggleCamRotation = false;
        }

        Debug.DrawRay(_cam.transform.position, new Vector3(_cam.transform.forward.x, 0, _cam.transform.forward.z).normalized, Color.red);
    }

    private void LateUpdate()
    {

    }

    /*캐릭터의 w키는 카메라의 forward방향이다
    캐릭터의 d키는 카메라의 right방향이다

    즉 카메라의 forward와 right를 가져와서 움직여야되고
    캐릭터 회전도 카메라에 따라서 회전시켜야된다 */

    void FixedUpdate()
    {
        Vector3 moveH = Vector3.Scale(_cam.transform.right, new Vector3(1,0,1)) * dir.x;
        Vector3 moveV = Vector3.Scale(_cam.transform.forward, new Vector3(1,0,1)) * dir.z;
        Vector3 moveVec = (moveH + moveV).normalized;

        rb.MovePosition(transform.position + moveVec * (isRun ? runSpeed : walkSpeed) * Time.fixedDeltaTime);


        /*
         캐릭터 회전은 wasd키 눌렀을 때 카메라의 forward 받아와서
         */

        if (dir != Vector3.zero)
        {
            Vector3 lookForward = _cam.transform.forward * dir.z;
            Vector3 lookRight = _cam.transform.right * dir.x;
            Vector3 rotate = Vector3.Scale((lookForward + lookRight).normalized, new Vector3(1, 0, 1));

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(rotate), Time.fixedDeltaTime * rotSpeed);
        }

        //if (!toggleCamRotation)
        //{
        //    Vector3 playerRotate = Vector3.Scale(_cam.transform.forward, new Vector3(1, 0, 1));
        //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(playerRotate), Time.deltaTime * rotSpeed);
        //}

        //Vector3 moveHorizontal = transform.right * dir.x;
        //Vector3 moveVertical = transform.forward * dir.z;
        //Vector3 movePos = (moveHorizontal + moveVertical).normalized * (isRun ? runSpeed : walkSpeed);

        //if (dir != Vector3.zero)
        //{
        //    if (Mathf.Sign(transform.forward.x) != Mathf.Sign(dir.x) ||
        //        Mathf.Sign(transform.forward.z) != Mathf.Sign(dir.z))
        //    {
        //        transform.Rotate(0, 1, 0);
        //    }
        //    transform.forward = Vector3.Lerp(transform.forward, dir, rotSpeed * Time.fixedDeltaTime);
        //}

        //velocity = (moveHorizontal + moveVertical).normalized * (isRun ? runSpeed : walkSpeed);



        //rb.velocity = velocity;

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
