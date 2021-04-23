using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerObjectController : NetworkBehaviour
{
    //Public Game Objects
    public CharacterController controller;
    public Camera cam;
    public Transform groundCheck;
    public LayerMask groundMask;
    public Transform headbone;
    //Ragdoll ragdoll;

    //Public Variables
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;
    public float gravity = -0.5f;
    public float jumpHeight = 0.1f;
    public float groundDis = 0.05f;
    public GameObject quickCastAbilityObject;
    public GameObject longCastAbilityObject;

    //Inspector Variables
    float turnSmoothVelocity;
    Vector3 velocity;
    float yaw = 0f;
    float camDir = 0.0f;
    CameraFollow tf = null;
    public bool isGrounded;
    public bool isFloating;
    public bool isJumping = false;
    private bool inVehicle = false;
    private bool quickCastAbilityOffCoolDown = true;
    private bool longCastAbilityOffCoolDown = true;
    private bool currentlyCasting = false;

    // Start is called before the first frame update
    void Start()
    {
        //Grab Camera Target
        isGrounded = false;
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
        //Get Camera for each Seperate Player
        if(hasAuthority)
        {
            if(cam == null)
            {
                cam = Camera.main;
                tf = cam.GetComponent<CameraFollow>();
                return;
            }
            Debug.Log(cam);
            // yaw += Input.GetAxis("Mouse X") * Time.deltaTime * 100.0f;
            // transform.eulerAngles = new Vector3(0, yaw, 0);
            camDir = cam.transform.eulerAngles.y;
            serverUpdate();

            //Vehicle Camera
            if(inVehicle)
            {
                Transform offset = headbone;
                offset.position = new Vector3(headbone.position.x - 10, headbone.position.y + 8, headbone.position.z);
                tf.setTarget(offset);
            }
            else
            {
                tf.setTarget(headbone);
            }
        }

        //Quick Attack
        if(Input.GetMouseButton(0) && quickCastAbilityOffCoolDown == true && this.tag == "Swordsman" && currentlyCasting == false)
        {
            quickCastAbilityOffCoolDown = false;
            currentlyCasting = true;
            StartCoroutine(QuickCastAbility());
            StopCoroutine(QuickCastAbility());
        }

        //Long Attack
        if(Input.GetMouseButton(1) && longCastAbilityOffCoolDown == true && this.tag == "Swordsman" && currentlyCasting == false)
        {
            longCastAbilityOffCoolDown = false;
            currentlyCasting = true;
            StartCoroutine(LongCastAbility());
            StopCoroutine(LongCastAbility());
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
            else if (isGrounded)
            {
                isJumping = false;
            }

            //Debug Ragdoll
            /*if (Input.GetKeyDown("g") && this.tag == "Swordsman")
            {
                ragdoll.die();
            }*/

            //Gravitational force
            if (!isGrounded)
            {
                velocity.y += gravity * Time.deltaTime;
            }

            //Directional movement inputs
            controller.Move(velocity * Time.deltaTime);
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            //Directional Movement Independant of Camera
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

    //Enter Vehicle when Collided and Pressing "E"
    void OnTriggerStay(Collider other)
    {
        if (this.tag == "Player" && other.gameObject.tag == "Vehicle" && Input.GetKey(KeyCode.E))
        {
            inVehicle = true;
        }
        else
        {
            inVehicle = false;
        }

        //Swordsman Gets Hit by Car
        if(this.tag == "Swordsman" && other.gameObject.tag == "Vehicle")
        {
            //5 is the vehicle speed that needs to be normalized
            float numberFromDistribution = Probabilities.X2PDF(1, 5);
            float randomNumber = Random.value;

            if(randomNumber <= numberFromDistribution)
            {
                this.GetComponent<Rigidbody>().velocity = Vector3.Scale(other.gameObject.GetComponent<Rigidbody>().velocity, new Vector3(1.3f, 1.3f, 1.3f));
            }
            else
            {
                Destroy(this.gameObject);
                //ragdoll.die();
            }
        }
    }

    IEnumerator QuickCastAbility()
    {
        yield return new WaitForSeconds(.4f);
        GameObject ability = Instantiate(quickCastAbilityObject) as GameObject;
        ability.transform.position = this.transform.position;
        ability.GetComponent<Rigidbody>().velocity = cam.transform.forward * 50;
        ability.transform.rotation = cam.transform.rotation;
        yield return new WaitForSeconds(.2f);
        currentlyCasting = false;
        yield return new WaitForSeconds(2f);
        quickCastAbilityOffCoolDown = true;
        yield return new WaitForSeconds(4f);
        Destroy(ability, 1.0f);
    }

    IEnumerator LongCastAbility()
    {
        yield return new WaitForSeconds(1f);
        GameObject ability = Instantiate(longCastAbilityObject) as GameObject;
        ability.transform.position = this.transform.position;
        ability.GetComponent<Rigidbody>().velocity = cam.transform.forward * 70;
        ability.transform.rotation = cam.transform.rotation;
        yield return new WaitForSeconds(.5f);
        currentlyCasting = false;
        yield return new WaitForSeconds(3f);
        quickCastAbilityOffCoolDown = true;
        yield return new WaitForSeconds(4f);
        Destroy(ability, 1.0f);
    } 
}
