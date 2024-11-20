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
    bool inFreddyArea;
    public GameObject freddySpeech;
    bool finishedSpeech;
    public GameObject letDollFinish;
    static bool hasbullets;
    public GameObject instructions;
    public GameObject bulletsInstructions;
    bool inIceCreamArea;

    AudioSource audioSource;
    public AudioClip creepyMessage;
    public AudioClip doorLocked;
    public AudioClip faceAnotherWay;
    public AudioClip stageHigh;
    public AudioClip twistedFunHouse;
    public AudioClip freddyTalk;

    public GameObject button;
    AudioSource doorAudio;
    AudioSource buttonAudio;
    public AudioClip doorOpen;
    public AudioClip buttonSound;

    static int numberOfVisits = 0;

    public GameObject gun;

    Scene scene;

    Vector3 gunPos;

    public PlayerMovement playerStats;


    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
        rb = GetComponent<Rigidbody>();
        if(scene.name == "Level02" && numberOfVisits > 0)
        {
            if(numberOfVisits > 0)
            {
                HideSpeech();
            }
        }
        else if(scene.name == "Level02")
        {
            Invoke("HideSpeech", 40f);
        }

        if(scene.name == "Level01")
        {
            doorAudio = door.GetComponent<AudioSource>();
            buttonAudio = button.GetComponent<AudioSource>();
        }
        audioSource = GetComponent<AudioSource>();
    }

    void HideSpeech()
    {
        freddySpeech.SetActive(false);
        finishedSpeech = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(scene.name == "Level02")
        {
            if(playerStats.crouching == true && inIceCreamArea == true)
            {
                FBanner.SetActive(true);
            }
        }

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

            if(inRangeArea == true && finishedSpeech == false && numberOfVisits < 1)
            {
                letDollFinish.SetActive(true);
            }
            else if(inRangeArea == true && finishedSpeech == true && numberOfVisits < 1)
            {
                letDollFinish.SetActive(false);
                instructions.SetActive(true);
            }

            if(inRangeArea == true  && hasbullets == true)
            {
                SceneManager.LoadScene("ShootingRange");
            }

            if(inIceCreamArea == true && playerStats.crouching == true)
            {
                hasbullets = true;
                numberOfVisits += 1;
                SceneManager.LoadScene("FloorUnderVan");
            }
        }

        if(Input.GetKeyDown(KeyCode.Escape) && noteDisplayed == true)
        {
            noteDisplay.SetActive(false);
            audioSource.clip = creepyMessage;
            audioSource.Play();
        }

        Debug.Log(hasbullets);

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

        if(other.gameObject.CompareTag("IceCreamArea"))
        {
            inIceCreamArea = true;
            bulletsInstructions.SetActive(true);
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
            instructions.SetActive(false);
            letDollFinish.SetActive(false);
        }
        if(other.gameObject.CompareTag("IceCreamArea"))
        {
            inIceCreamArea = false;
            FBanner.SetActive(false);
            bulletsInstructions.SetActive(false);
        }
    }

    
}
