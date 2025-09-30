using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustspeed;
    [SerializeField] float rotatespeed;
    [SerializeField] InputAction rotation;
    Rigidbody rb;
    [SerializeField] InputAction thrust;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    private void OnEnable()
    {
        thrust.Enable();
        rotation.Enable();
    }
    void FixedUpdate()
    {
        thrustprocess();
        rotationprocess();
    }
    void thrustprocess()
    {
        if (thrust.IsPressed())
        {

            rb.AddRelativeForce(Vector3.up * Time.fixedDeltaTime * thrustspeed);
           
           
        }

    }
    void rotationprocess()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            ApplyRotation(rotatespeed);
            
        }
        else if (rotationInput > 0)

        {
            ApplyRotation(-rotatespeed);
        }


    }
    void ApplyRotation(float rotateThisframe)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotateThisframe * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }


}


