using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCompleted : MonoBehaviour
{
    public static bool riddleCompleted;
    public static bool rangeCompleted;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("range: " + rangeCompleted);
    }
}
