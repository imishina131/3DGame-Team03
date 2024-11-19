using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastScript : MonoBehaviour
{
    Camera camera;
    Transform cameraTransform;
    public LayerMask mask;
    AudioSource audioSource;

    public Player player;

    public AudioClip gunShot;
    void Start()
    {
        cameraTransform = GameObject.Find("PlayerCamera").transform;
        camera = cameraTransform.GetComponent<Camera>();
        Debug.Log(camera.name);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = camera.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.blue);


        if(Input.GetMouseButtonDown(0))
        {
            audioSource.clip = gunShot;
            audioSource.Play();
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("cast");

            if(Physics.Raycast(ray, out hit, 100, mask))//destroys the trap object if its in the way of the ray once the player left clicks
            {
                Destroy(hit.collider.gameObject);
                player.DestroyTarget();
            }
            player.LoseBullet();
        }
    }
}
