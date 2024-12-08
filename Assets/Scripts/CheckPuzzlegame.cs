using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CheckPuzzlegame : MonoBehaviour
{
    public SlotScript slot01;
    public SlotScript slot02;
    public SlotScript slot03;
    public SlotScript slot04;

    int lives = 3;
    public TMP_Text livesText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "Lives: " + lives;
    }

    public void ClickSubmit()
    {
        if(slot01.rightSlot == true && slot02.rightSlot == true && slot03.rightSlot == true && slot04.rightSlot == true)
        {
            TaskCompleted.riddleCompleted = true;
            SceneManager.LoadScene("Level02");
        }
        else
        {
            lives = lives - 1;

            if(lives <= 0)
            {
                SceneManager.LoadScene("LossScene");
            }
        }
    }
}
