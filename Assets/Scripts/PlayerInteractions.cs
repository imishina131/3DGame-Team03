using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    static bool finishedSpeech;
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
    public GameObject vendor;
    public GameObject doll;
    AudioSource doorAudio;
    AudioSource buttonAudio;
    AudioSource keyAudio;
    AudioSource vendorAudio;
    AudioSource dollAudio;
    public AudioClip doorOpen;
    public AudioClip buttonSound;
    public AudioClip keyPickupSound;
    public AudioClip vendorMessage;
    public AudioClip noBullets;
    public AudioClip howDoYouKnow;
    public AudioClip takeCookie;
    public AudioClip getMoving;
    public AudioClip noCookieNoChat;

    

    static int numberOfVisits = 0;

    public GameObject gun;

    Scene scene;

    Vector3 gunPos;

    public PlayerMovement playerStats;

    public GameObject playerObject;
    static Vector3 posSaved;


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
                playerObject.transform.position = posSaved;
                dollAudio = doll.GetComponent<AudioSource>();
            }
        }
        else if(scene.name == "Level02")
        {
            Invoke("HideSpeech", 40f);
            vendorAudio = vendor.GetComponent<AudioSource>();
            dollAudio = doll.GetComponent<AudioSource>();
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
        Debug.Log("Speechfinish = " + finishedSpeech);
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
                player.GetKey();
                key.SetActive(false);
                hasKey = true;
                inKeyArea = false;
                audioSource.clip = keyPickupSound; 
                audioSource.Play();
            }

            if(inDoorArea == true && hasKey == true)
            {
                door.SetActive(false);
                player.UseKey();
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
                audioSource.clip = noBullets;
                audioSource.Play();
                letDollFinish.SetActive(false);
                instructions.SetActive(true);
            }

            if(inRangeArea == true  && hasbullets == true)
            {
                posSaved = playerObject.transform.position;
                SceneManager.LoadScene("ShootingRange");
            }

            if(inIceCreamArea == true && playerStats.crouching == true)
            {
                hasbullets = true;
                numberOfVisits += 1;
                posSaved = playerObject.transform.position;
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
            if(finishedSpeech)
            {
                inRangeArea = true;
                FBanner.SetActive(true);
            }
        }

        if(other.gameObject.CompareTag("IceCreamArea"))
        {
            if(finishedSpeech && numberOfVisits < 1)
            {
                vendorAudio.clip = vendorMessage;
                vendorAudio.Play();
                inIceCreamArea = true;
                bulletsInstructions.SetActive(true);
            }
        }

        if(other.gameObject.CompareTag("DollArea"))
        {
            if(finishedSpeech)
            {
                if(player.hasCookies == true)
                {
                    dollAudio.clip = takeCookie;
                    dollAudio.Play();
                    Invoke("SayHowDoYouKnow", 16.0f);
                    Invoke("HearGetMoving", 20.0f);
                }
                else
                {
                    dollAudio.clip = noCookieNoChat;
                    dollAudio.Play();
                }
            }
        }

    }

    void SayHowDoYouKnow()
    {
        audioSource.clip = howDoYouKnow;
        audioSource.Play();
    }

    void HearGetMoving()
    {
        dollAudio.clip = getMoving;
        dollAudio.Play();
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
