using System.Collections;
using System.Collections.Generic;
using System.Net.Cache;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{
    Rigidbody rb;
    public GameObject FBanner;
    bool inKeyArea;
    public GameObject key;
    public static bool hasKey;
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
    bool dollDead;
    bool inLastShapesArea;
    bool inXArea;
    bool inSquareArea;
    public GameObject cage;

    public AudioSource audioSource;
    public AudioClip creepyMessage;
    public AudioClip doorLocked;
    public AudioClip faceAnotherWay;
    public AudioClip stageHigh;
    public AudioClip twistedFunHouse;
    public AudioClip freddyTalk;

    public GameObject button;
    public GameObject vendor;
    public GameObject doll;
    public GameObject ghostGirl;
    public GameObject fourShapesText;
    GameObject fireballClone;
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
    public AudioClip aKnife;

    
    public Transform fireballOrigin;
    public GameObject fireball;
    static int numberOfVisits = 0;

    public GameObject gun;

    Scene scene;

    Vector3 gunPos;

    public PlayerMovement playerStats;

    public GameObject playerObject;
    public static Vector3 posSaved;

    bool hasCircle;
    bool hasTriangle;
    bool hasX;
    bool hasSquare;
    bool gaveCookie;
    bool hasKnife;
    bool inBossArea;
    bool bossAttackArea;
    public bool dollClose;
    public static bool startBoss;

    public Animator boxAnimator;

    public GameObject knife;

    public int fireballspeed;

    public Animator animator;

    public Collider collider;

    public GameObject cookiePanel;

    public BossHealth boss;

    public GameObject bossHealthBar;

    bool delay;


    // Start is called before the first frame update
    void Start()
    {
        if(TaskCompleted.riddleCompleted == true)
        {
            hasKnife = true;
            audioSource.clip = aKnife;
            audioSource.Play();
            StartCoroutine(Disable());
        }

        if(TaskCompleted.rangeCompleted == true)
        {
            cookiePanel.SetActive(true);
            StartCoroutine(TakeOffCookiePanel());
        }
        Debug.Log("visit: " + numberOfVisits);
        scene = SceneManager.GetActiveScene();
        rb = GetComponent<Rigidbody>();
        transform.position = posSaved;
        if (scene.name == "Level02" && numberOfVisits > 0)
        {
            if(numberOfVisits > 0)
            {
                HideSpeech();
                
                dollAudio = doll.GetComponent<AudioSource>();
            }
        }
        else if(scene.name == "Level02")
        {
            knife.SetActive(false);
            Invoke("HideSpeech", 35f);
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

    IEnumerator TakeOffCookiePanel()
    {
        yield return new WaitForSeconds(5);
        cookiePanel.SetActive(false);
        TaskCompleted.rangeCompleted = false;
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(2);
        TaskCompleted.riddleCompleted = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("Pos: " + posSaved);
        /*
        if(scene.name == "Level02")
        {
            posSaved = playerObject.transform.position;
        }
        */

        Debug.Log("Speechfinish = " + finishedSpeech);
        if(scene.name == "Level02")
        {
            if(playerStats.crouching == true && inIceCreamArea == true)
            {
                FBanner.SetActive(true);
            }

            if(hasCircle == true && hasSquare == true && hasTriangle == true && hasX == true)
            {
                fourShapesText.SetActive(true);
            }
            else
            {
                fourShapesText.SetActive(false);
            }
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(scene.name == "Level02" && player.hasPotions == true && dollClose == true)
            {
                Vector3 direction = (new Vector3(ghostGirl.transform.position.x, ghostGirl.transform.position.y + 8, ghostGirl.transform.position.z) - (fireballOrigin.transform.position));
                animator.SetTrigger("throw");
                fireballClone = Instantiate(fireball, fireballOrigin.position, Quaternion.identity);
                fireballClone.GetComponent<Rigidbody>().AddForce(direction * fireballspeed, ForceMode.Impulse);
                player.usePotion();
            }

            if(scene.name == "Level02" && hasKnife && delay == false)
            {
                playerStats.moveSpeed = 0;
                animator.SetTrigger("stab");
                knife.SetActive(true);
                Invoke("SetMoveSpeed", 2.0f);
                Invoke("TakeAwayKnife", 2.5f);
                if(bossAttackArea == true)
                {
                    boss.TakeDamage();
                }
                delay = true;
                StartCoroutine(TakeOffDelay());
            }
        }

        if(Input.GetMouseButtonDown(1) && hasKnife)
        {
            animator.SetTrigger("roll");
            player.Roll();
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
                StartCoroutine(SavePosition("ShootingRange"));
            }

            if(inIceCreamArea == true && playerStats.crouching == true)
            {
                hasbullets = true;
                numberOfVisits += 1;
                StartCoroutine(SavePosition("FloorUnderVan"));
                //posSaved = playerObject.transform.position;
                //SceneManager.LoadScene("FloorUnderVan");
            }

            if(inLastShapesArea == true)
            {
                Destroy(GameObject.FindGameObjectWithTag("TriangleShape"));
                hasTriangle = true;
                Destroy(GameObject.FindGameObjectWithTag("CircleShape"));
                hasCircle = true;
                FBanner.SetActive(false);
            }

            if(inXArea == true)
            {
                Destroy(GameObject.FindGameObjectWithTag("XShape"));
                hasX = true;
                FBanner.SetActive(false);
            }

            if(inSquareArea == true)
            {
                Destroy(GameObject.FindGameObjectWithTag("SquareShape"));
                hasSquare = true;
                FBanner.SetActive(false);
            }

            if(inBossArea == true)
            {
                collider.isTrigger = true;
                boxAnimator.SetTrigger("open");
                startBoss = true;
                FBanner.SetActive(false);
                bossHealthBar.SetActive(true);
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

    IEnumerator TakeOffDelay()
    {
        yield return new WaitForSeconds(4);
        delay = false;
    }

    void SetMoveSpeed()
    {
        playerStats.moveSpeed = 7;
    }

    void TakeAwayKnife()
    {
        knife.SetActive(false);
    }

    public void LeaveMessage()
    {
        noteDisplay.SetActive(false);
        audioSource.clip = creepyMessage;
        audioSource.Play();
    }

    IEnumerator SavePosition(string scene)
    {
        posSaved = playerObject.transform.position;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(scene);

    }

    void PlayHint()
    {
        audioSource.clip = faceAnotherWay;
        audioSource.Play();
    }


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("BossArea") && hasKey == true)
        {
            FBanner.SetActive(true);
            inBossArea = true;
        }
        if(other.gameObject.CompareTag("Trampoline"))
        {
            rb.AddForce(new Vector3(0.0f, 300.0f, 0.0f));
        }

        if(other.gameObject.CompareTag("HealthPotion"))
        {
            if (Player.health < 100)
            {
                player.GainHealth();
                Destroy(other.gameObject);
            }
        }

        if(other.gameObject.CompareTag("Boss"))
        {
            bossAttackArea = true;
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
            inIceCreamArea = true;
            if(finishedSpeech && numberOfVisits < 1)
            {
                vendorAudio.clip = vendorMessage;
                vendorAudio.Play();
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
                else if(hasCircle == true && hasSquare == true && hasTriangle == true && hasX == true)
                {
                    Invoke("DropCage", 2.0f);
                    StartCoroutine(LoadPuzzleLevel());
                }
                else if(player.hasCookies == false && gaveCookie == false)
                {
                    dollAudio.clip = noCookieNoChat;
                    dollAudio.Play();
                }
            }
        }

        if(other.gameObject.CompareTag("LastShapesArea"))
        {
            if(player.ghostGirlDead == true)
            {
                FBanner.SetActive(true);
                inLastShapesArea = true;
            }
        }

        if(other.gameObject.CompareTag("SquareShape"))
        {
            FBanner.SetActive(true);
            inSquareArea = true;
        }

        if(other.gameObject.CompareTag("XShape"))
        {
            FBanner.SetActive(true);
            inXArea = true;
        }

    }

    void DropCage()
    {
        cage.SetActive(true);
        StartCoroutine(LoadPuzzleLevel());
    }

    IEnumerator LoadPuzzleLevel()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("PuzzleGame");
    }

    void SayHowDoYouKnow()
    {
        player.useCookie();
        gaveCookie = true;
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
        if(other.gameObject.CompareTag("BossArea") && hasKey == true)
        {
            FBanner.SetActive(false);
            inBossArea = false;
        }

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

        if(other.gameObject.CompareTag("Boss"))
        {
            bossAttackArea = false;
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

        if(other.gameObject.CompareTag("LastShapesArea"))
        {
            if(player.ghostGirlDead == true)
            {
                FBanner.SetActive(false);
                inLastShapesArea = false;
            }
        }

        if(other.gameObject.CompareTag("SquareShape"))
        {
            FBanner.SetActive(false);
            inSquareArea = false;
        }

        if(other.gameObject.CompareTag("XShape"))
        {

            FBanner.SetActive(false);
            inXArea = false;

        }
    }

    
}
