using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    
    // VARIABLES
    [SerializeField] private float speed = 6f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    
    // REFERENCES
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform cam;  
    
    
    
    // Start is called before the first frame update
    // void Start()
    // {
    //     
    // }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // a:-1 or d:+1
        float vertical = Input.GetAxisRaw("Vertical"); // w: +1 or s: -1
        
        // .normalized prevents increasing of speed if we press two keys to move diagonally
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;


        if (direction.magnitude >= 0.1 )
        {
            // THIS WILL ROTATE THE PLAYER IN THE DIRECTION HE'S GOING TO
            // ATAN2 returns an angle in radians so we convert it into degrees 
            // float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            // connecting to camera 
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            
            // smoothing rotation
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            
            // applying rotation
            // transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
            
            // now with smoothed angle
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            
            // we multiply to Vector3.forward to transform a rotation into a direction

            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            
            // {* Time.deltaTime} makes it frame rate independent
            // controller.Move(direction * speed * Time.deltaTime);
            controller.Move(moveDirection.normalized * speed * Time.deltaTime);
            
            
            
            // apply MAIN CAMERA to CAM for PLAYER
            
            
        }
    }
}
