using System.Diagnostics;
using UnityEngine;
using TMPro;



public class PlayerMovement : MonoBehaviour
{



    [SerializeField]
    private Transform camera;

    public TextMeshProUGUI Text;


    float movementSpeed = 5;
    float rotationspeed = 1000;


    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {


        //basic input
        if (Input.GetKey("w"))
        {
            transform.position += transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey("a"))
        {
            transform.position += transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey("s"))
        {
            transform.position -= transform.TransformDirection(Vector3.forward) * Time.deltaTime * movementSpeed;
        }
        if (Input.GetKey("d"))
        {
            transform.position -= transform.TransformDirection(Vector3.left) * Time.deltaTime * movementSpeed;
        }

        //rotate player on x
        //Yaw rotates the camera around its local Up axis
        transform.Rotate(Vector3.up * Time.deltaTime * Input.GetAxis("Mouse X") * rotationspeed);

        //this is for the camera
        //Pitch rotates the camera around its local Right axis

        // if ( camera.eulerAngles.y  > 300)
        // {

        // }
        // else
        // {
        //     camera.eulerAngles = new Vector3(0, 90, 0); //this is not working
        // }


            camera.Rotate(Vector3.left * Time.deltaTime * Input.GetAxis("Mouse Y") * rotationspeed);
        




   
            Text.text = camera.eulerAngles.y.ToString();
        

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = 20;
        }
        else
        {
            movementSpeed = 5;
        }



    }

    public void OnKey()
    {

    }




}
