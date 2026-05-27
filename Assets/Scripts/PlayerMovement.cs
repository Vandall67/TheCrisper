using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine;





public class PlayerMovement : MonoBehaviour
{



    [SerializeField]
    private Transform camera;
    [SerializeField]
    private float forcaEmpurrao = 8f;


    float movementSpeed = 5;
    float rotationspeed = 500f;

    private Vector3 movimento;

    [SerializeField]
    private float jumpPower;

    private float xRotation = 0f;
    private float minX = -10f;  // how far up
    private float maxX = 10f;


    public Animator playerAnim;

    private Rigidbody playerRb;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>(); //get ridgbody
    }

    // Update is called once per frame

    bool walking;



    void FixedUpdate()
    {
        //basic input
        if (Input.GetKey("w")) //Keycode.w
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey("s"))
        {
            transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey("d"))
        {
            transform.position -= transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey("a"))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(jump());
        }



        // if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S)) // this needs testing
        // {
            //Sprint
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
            {
                movementSpeed = 10;
            }
            else
            {
                movementSpeed = 5;
            }


       // }
    }



    void Update()
    {

        //aniamtion
        // Front --- BACK
        if (Input.GetKeyDown(KeyCode.W)) //Keycode.w
        {
            playerAnim.SetTrigger("jog");
            playerAnim.ResetTrigger("idle");
            walking = true;

        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.ResetTrigger("jog");
            playerAnim.SetTrigger("idle");
            walking = false;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            playerAnim.SetTrigger("jogback");
            playerAnim.ResetTrigger("idle");
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.ResetTrigger("jogback");
            playerAnim.SetTrigger("idle");
        }




        //LEFT --- RIGHT
        if (Input.GetKeyDown(KeyCode.A)) //Keycode.w
        {
            playerAnim.SetTrigger("jogleft");
            playerAnim.ResetTrigger("idle");
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            playerAnim.ResetTrigger("jogleft");
            playerAnim.SetTrigger("idle");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            playerAnim.SetTrigger("jogright");
            playerAnim.ResetTrigger("idle");
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            playerAnim.ResetTrigger("jogright");
            playerAnim.SetTrigger("idle");
        }


        //jump
        if (Input.GetKeyUp(KeyCode.Space))
        {
            playerAnim.SetTrigger("jump");
            playerAnim.ResetTrigger("idle");
        }
        // if (Input.GetKeyUp(KeyCode.Space))
        // {
        //     playerAnim.SetTrigger("idle");
        //     playerAnim.ResetTrigger("jump");

        // }

        //Sprint
        if (walking == true)
        {

            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) // this needs testing
            {

                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    playerAnim.SetTrigger("run");
                    playerAnim.ResetTrigger("jog");
                }

                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    playerAnim.SetTrigger("jog");
                    playerAnim.ResetTrigger("run");
                }
            }
        }

        //rotate player on x
        //Yaw rotates the camera around its local Up axis
        transform.Rotate(Vector3.up * Time.deltaTime * Input.GetAxis("Mouse X") * rotationspeed);



        float mouseY = Input.GetAxis("Mouse Y") * rotationspeed * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minX, maxX);

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);





        //this needs better implementation
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movimento = new Vector3(horizontal, 0f, vertical).normalized;
    }




    private IEnumerator jump()
    {
        yield return new WaitForSeconds(0.6f);
        //transform.position += transform.TransformDirection(Vector3.up) * Time.deltaTime * movementSpeed;
        playerRb.AddForce(Vector2.up * jumpPower);

    }

    //Colisions
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Empurravel"))
        {
            Rigidbody rbObjeto = collision.rigidbody;

            if (rbObjeto != null && !rbObjeto.isKinematic)
            {
                Vector3 direcaoEmpurrao = new Vector3(movimento.x, 0f, movimento.z);

                if (direcaoEmpurrao.magnitude > 0.1f)
                {
                    rbObjeto.AddForce(direcaoEmpurrao.normalized * forcaEmpurrao, ForceMode.Force);
                }
            }
        }
    }

}
