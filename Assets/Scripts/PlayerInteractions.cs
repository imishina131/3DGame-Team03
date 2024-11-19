using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteractions : MonoBehaviour
{
    Rigidbody rb;
    public GameObject FBanner;
    bool inKeyArea;
    public GameObject key;
    bool hasKey;
    bool inDoorArea;
    public GameObject door;
    public KnifeClownBehavior knifeClown;
    bool inButtonArea;
    bool inNoteArea;
    bool noteDisplayed;
    public GameObject noteDisplay;
    public Player player;
    bool inRangeArea;

    AudioSource audioSource;
    public AudioClip creepyMessage;
    public AudioClip doorLocked;
    public AudioClip faceAnotherWay;
    public AudioClip stageHigh;
    public AudioClip twistedFunHouse;

    public GameObject button;
    AudioSource doorAudio;
    AudioSource buttonAudio;
    public AudioClip doorOpen;
    public AudioClip buttonSound;

    public GameObject gun;

    Scene scene;

    Vector3 gunPos;


    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        rb = GetComponent<Rigidbody>();

        if(scene.name == "Level01")
        {
            doorAudio = door.GetComponent<AudioSource>();
            buttonAudio = button.GetComponent<AudioSource>();
        }
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.F))
        {
            if(inKeyArea == true)
            {
                key.SetActive(false);
                hasKey = true;
                inKeyArea = false;
            }

            if(inDoorArea == true && hasKey == true)
            {
                door.SetActive(false);
                doorAudio.clip = doorOpen;
                doorAudio.Play();
                hasKey = false;
                knifeClown.ShootFirstKnife();
                Invoke("PlayHint", 1.0f);
            }

            if(inButtonArea == true)
            {
                knifeClown.ChangeTarget();
                inButtonArea = false;
                buttonAudio.clip = buttonSound;
                buttonAudio.Play();
            }

            if(inNoteArea == true)
            {
                noteDisplayed = true;
                noteDisplay.SetActive(true);
                FBanner.SetActive(false);
                audioSource.clip = twistedFunHouse;
                audioSource.Play();
            }

            if(inRangeArea == true)
            {
                SceneManager.LoadScene("ShootingRange");
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape) && noteDisplayed == true)
        {
            noteDisplay.SetActive(false);
            audioSource.clip = creepyMessage;
            audioSource.Play();
        }

    }

    void PlayHint()
    {
        audioSource.clip = faceAnotherWay;
        audioSource.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Trampoline"))
        {
            rb.AddForce(new Vector3(0.0f, 300.0f, 0.0f));
        }

        if(other.gameObject.CompareTag("KeyArea") && hasKey == false)
        {
            FBanner.SetActive(true);
            inKeyArea = true;
        }

        if(other.gameObject.CompareTag("DoorArea"))
        {
            inDoorArea = true;
            if(hasKey)
            {
                FBanner.SetActive(true);
            }
            else
            {
                audioSource.clip = doorLocked;
                audioSource.Play();
            }
        }

        if(other.gameObject.CompareTag("ButtonArea"))
        {
            inButtonArea = true;
            FBanner.SetActive(true);
        }

        if(other.gameObject.CompareTag("NoteArea"))
        {
            inNoteArea = true;
            FBanner.SetActive(true);
        }

        if(other.gameObject.CompareTag("NextLevel"))
        {
            SceneManager.LoadScene("TransitionScene");
        }

        if(other.gameObject.CompareTag("Snake"))
        {
            player.TakeDamage(20);
        }

        if(other.gameObject.CompareTag("StageArea"))
        {
            audioSource.clip = stageHigh;
            audioSource.Play();
        }

        if(other.gameObject.CompareTag("RangeArea"))
        {
            inRangeArea = true;
            FBanner.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("KeyArea"))
        {
            FBanner.SetActive(false);
            inKeyArea = false;
        }

        if(other.gameObject.CompareTag("DoorArea"))
        {
            FBanner.SetActive(false);
            inDoorArea = false;
        }

        if(other.gameObject.CompareTag("ButtonArea"))
        {
            inButtonArea = false;
            FBanner.SetActive(false);
        }

        if(other.gameObject.CompareTag("NoteArea"))
        {
            inNoteArea = false;
            FBanner.SetActive(false);
        }

        if(other.gameObject.CompareTag("RangeArea"))
        {
            inRangeArea = false;
            FBanner.SetActive(false);
        }
    }

    
}
