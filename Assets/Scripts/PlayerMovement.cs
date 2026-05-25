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



    private float xRotation = 0f;
    private float minX = -10f;  // how far up
    private float maxX = 10f;


    public Animator playerAnim;

    void Start()
    {

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

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = 10;
        }
        else
        {
            movementSpeed = 5;
        }
    }



    void Update()
    {

        //aniamtion
        if (Input.GetKeyDown("w")) //Keycode.w
        {
            playerAnim.SetTrigger("jog");
            playerAnim.ResetTrigger("idle");
            walking = true;

        }
        if (Input.GetKeyUp("w"))
        {
            playerAnim.ResetTrigger("jog");
            playerAnim.SetTrigger("idle");
            walking = false;
        }

        if (Input.GetKeyDown("s"))
        {
            playerAnim.SetTrigger("jogback");
            playerAnim.ResetTrigger("idle");
        }
        if (Input.GetKeyUp("s"))
        {
            playerAnim.ResetTrigger("jogback");
            playerAnim.SetTrigger("idle");
        }


        //rotate player on x
        //Yaw rotates the camera around its local Up axis
        transform.Rotate(Vector3.up * Time.deltaTime * Input.GetAxis("Mouse X") * rotationspeed);



        float mouseY = Input.GetAxis("Mouse Y") * rotationspeed * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, minX, maxX);

        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);



        if (walking == true)
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

        //Sprint



        //this needs better implementation
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movimento = new Vector3(horizontal, 0f, vertical).normalized;
    }

    public void OnKey()
    {

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
