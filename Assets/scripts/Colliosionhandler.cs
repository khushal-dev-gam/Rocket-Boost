using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class Colliosionhandler : MonoBehaviour
{
    [SerializeField] float levelDelay=1.5f;
    [SerializeField] AudioClip levelUp;
     [SerializeField] AudioClip Crash;
     [SerializeField] ParticleSystem SuccessParticles;
      [SerializeField] ParticleSystem CrashParticles;
     AudioSource audioSource;
     bool isControllable=true;
     bool isCollidable=true;
     void Start() {
         audioSource = GetComponent<AudioSource>();
     }

     private void Update()
    {
     RespondToDebugKeys();   
     }
     void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        else if(Keyboard.current.pKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
            Debug.Log("Colliosion is skipped");
        }
    }
    private void OnCollisionEnter(Collision other)
    {
     if(!isControllable ||!isCollidable)
     {
        return;}
     
        switch (other.gameObject.tag)
        {
            case "freindly":
                Debug.Log("FINALLY YOU STARTED");
                break;
            case "Finish":
              playerReached(); 
                break;
            default:
              Crashhappens();
                break;
                    }
    }
    
         void playerReached()
    {
      isControllable=false;
      audioSource.Stop();
       audioSource.PlayOneShot(levelUp);
       SuccessParticles.Play(); 
       GetComponent<Movement>().enabled=false;   
        Invoke("LoadNextLevel",levelDelay);
         
    }

         
        void Crashhappens()
        {
            isControllable=false;
            audioSource.Stop();         
            audioSource.PlayOneShot(Crash);
            CrashParticles.Play();
            GetComponent<Movement>().enabled=false;   
            Invoke ("ReloadLevel",levelDelay);
             
           }
        void LoadNextLevel()
        {
            int CurrentScene = SceneManager.GetActiveScene().buildIndex;
            int nextScene = CurrentScene+1;
            if(nextScene == SceneManager.sceneCountInBuildSettings)
            {
                nextScene=0;
            }
            SceneManager.LoadScene(nextScene);    
        }
        void ReloadLevel()
        {
            int CurrentScene = SceneManager.GetActiveScene().buildIndex;
             SceneManager.LoadScene(CurrentScene);
        }
    }


