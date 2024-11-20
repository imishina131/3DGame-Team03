using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    Camera mainCamera;
    public LayerMask layerMask;
    float maxSpeed = 10f;
    bool killzoneSpider01;
    bool killzoneSpider02;

    public GameObject spider01;
    public GameObject spider02;

    public int numberOfSpiders;

    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        numberOfSpiders = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, float.MaxValue, layerMask))
        {
            transform.position = hit.point;
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(killzoneSpider01 == true)
            {
                Destroy(spider01);
                killzoneSpider01 = false;
                numberOfSpiders += 1;
            }

            if(killzoneSpider02 == true)
            {
                Destroy(spider02);
                killzoneSpider02 = false;
                numberOfSpiders += 1;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("HeadSpider"))
        {
            player.TakeDamage(40);
        }

        if(other.gameObject.CompareTag("BottomSpider01"))
        {
            killzoneSpider01 = true;
        }

        if(other.gameObject.CompareTag("BottomSpider02"))
        {
            killzoneSpider02 = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("BottomSpider01"))
        {
            killzoneSpider01 = false;
        }

        if(other.gameObject.CompareTag("BottomSpider02"))
        {
            killzoneSpider02 = false;
        }
    }


    
}
