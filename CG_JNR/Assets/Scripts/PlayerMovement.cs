using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables
    [SerializeField] private float moveSpeed;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    
    private Vector3 moveDirection;
    private Vector3 velocity;

    [SerializeField] private bool isGrounded;
    [SerializeField] private float groundcheckDistance;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private float gravity;

    [SerializeField] private float jumpheight;

    private CharacterController controller;
    private Animator anim;
    


    // Start is called before the first frame update
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        //controller.center = new Vector3(0, 1, 0);
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(Attack());
        }
    }

    private void Move()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundcheckDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveZ = Input.GetAxis("Vertical");

        moveDirection = new Vector3(0, 0, moveZ);

        moveDirection = transform.TransformDirection(moveDirection);

        if(isGrounded)
        {
            if (moveDirection != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                //Walk
                Walk();
            }else if (moveDirection != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                //Run
                Run();
            }else if (moveDirection== Vector3.zero)
            {
                //Idle
                Idle();
            }
            moveDirection *= moveSpeed;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
       

        controller.Move(moveDirection * Time.deltaTime);
       


       

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); 
    }
    private void Walk()
    {
        moveSpeed = walkSpeed;
        anim.SetFloat("Speed", 0.5f, 0.1f, Time.deltaTime);
    }
    private void Run()
    {
        moveSpeed = runSpeed;
        anim.SetFloat("Speed", 1, 0.1f, Time.deltaTime);
    }
    private void Idle()
    {
        anim.SetFloat("Speed", 0, 0.1f, Time.deltaTime);
    }
    private void Jump()
    {

        velocity.y = Mathf.Sqrt(jumpheight * -2 * gravity);
    }
    private IEnumerator Attack()
    {
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 1);
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(0.9f);
        anim.SetLayerWeight(anim.GetLayerIndex("Attack Layer"), 0);
    }
}

