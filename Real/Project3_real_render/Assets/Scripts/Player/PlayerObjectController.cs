using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerObjectController : NetworkBehaviour
{
    public CharacterController controller;
    public Camera cam;
    public Transform groundCheck;
    public LayerMask groundMask;

    //Public variables
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float gravity = -0.5f;
    public float jumpHeight = 0.1f;
    public float groundDis = 0.05f;
    float yaw = 0f;

    //Private variables
    float turnSmoothVelocity;
    Vector3 velocity;
    public bool isGrounded;
    public bool isFloating;
    public bool isJumping = false;
    float camDir = 0.0f;
    CameraFollow tf = null;
    // Update is called once per frame
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Reached Player Object Controller");
        transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        if (hasAuthority)
        {
            cam = Camera.main;
            tf = cam.GetComponent<CameraFollow>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(hasAuthority)
        {
            if(cam == null)
            {
                cam = Camera.main;
                tf = cam.GetComponent<CameraFollow>();
            }
            Debug.Log(cam);
            // yaw += Input.GetAxis("Mouse X") * Time.deltaTime * 100.0f;
            // transform.eulerAngles = new Vector3(0, yaw, 0);
            camDir = cam.transform.eulerAngles.y;
            serverUpdate();
            tf.setTarget(this.transform);

        }
    }

    public void serverUpdate()
    {
        if (hasAuthority)
        {



            //Check for ground
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDis, groundMask);
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -0.1f;
            }

            //Jump input
            if (Input.GetButtonDown("Jump") && isGrounded)
            {

                isJumping = true;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
            else if (isGrounded) isJumping = false;

            //Gravitational force

            if (!isGrounded)
            {
                velocity.y += gravity * Time.deltaTime;

            }

            controller.Move(velocity);

            //Directional movement inputs
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            //Directional movement with a third person camera
            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + camDir;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);

            }

            yaw += Input.GetAxis("Mouse X") * Time.deltaTime * 100.0f;
            transform.eulerAngles = new Vector3(0, yaw, 0);


        }
    }

 
}
