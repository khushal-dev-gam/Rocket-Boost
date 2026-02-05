using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    Vector3 StartPosition;
   Vector3 EndPosition;
   [SerializeField] float Speed;
   [SerializeField] Vector3 MovementVector;
   float MovementFactor;
    void Start()
    {
     StartPosition = transform.position;
     EndPosition = StartPosition +MovementVector;
        
    }

   
    void Update()
    {
        MovementFactor = Mathf.PingPong(Time.time*Speed,1f);
        transform.position = Vector3.Lerp(StartPosition,EndPosition,MovementFactor);
    }
}
