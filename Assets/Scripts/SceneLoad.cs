using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoad : MonoBehaviour
{
    public GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player.transform.position = PlayerInteractions.posSaved;
        Debug.Log("positionrunned");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
