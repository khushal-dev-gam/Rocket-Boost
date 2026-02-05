using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrustspeed;
    [SerializeField] float rotatespeed;
    [SerializeField] InputAction rotation;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem EngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    Rigidbody rb;
    AudioSource audioSource;

        [SerializeField] InputAction thrust;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            thrustStart();
            }
        else
            {
                audioSource.Stop();
                EngineParticles.Stop();
            }

    }
    void thrustStart()
    {
          rb.AddRelativeForce(Vector3.up * Time.fixedDeltaTime * thrustspeed);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if(!EngineParticles.isPlaying)
            {
             EngineParticles.Play();
            }
    }
    void rotationprocess()
    {
        
        rotationStuff();

    }

    void rotationStuff()
    {
        float rotationInput = rotation.ReadValue<float>();
        if (rotationInput < 0)
        {
            ApplyRotation(rotatespeed);
            if(!leftThrusterParticles.isPlaying)
            { leftThrusterParticles.Play();
            }
        }
        else if (rotationInput > 0)

        {
            ApplyRotation(-rotatespeed);
          if(!rightThrusterParticles.isPlaying)
          {
               rightThrusterParticles.Play();
          }
          }

        else
        {
            rightThrusterParticles.Stop();
            leftThrusterParticles.Stop();
        }
    }
    void ApplyRotation(float rotateThisframe)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotateThisframe * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }


}


