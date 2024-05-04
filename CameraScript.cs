using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float speed = 7.5f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    public bool menuOpen;


    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    float rotationY = 0;
    GameObject buttonPanel;

    // Start is called before the first frame update
    void Start()
    {
        buttonPanel = GameObject.Find("ButtonPanel");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
        buttonPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Q))&&(!menuOpen)){
            menuOpen=true;
            Cursor.lockState=CursorLockMode.None;
            Cursor.visible = true;
            buttonPanel.SetActive(true);
        }
        else if ((Input.GetKeyDown(KeyCode.Q))&&(menuOpen)){
            menuOpen=false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            buttonPanel.SetActive(false);
        }

        
        if(!menuOpen){
        
        
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        float curSpeedX =   speed * Input.GetAxis("Vertical") ;
        float curSpeedY =   speed * Input.GetAxis("Horizontal") ;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        characterController.Move(moveDirection * Time.deltaTime);
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            rotationY += Input.GetAxis("Mouse X") * lookSpeed;
            transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0);
      }      
    }
}
