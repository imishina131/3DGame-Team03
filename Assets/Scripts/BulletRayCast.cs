using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BulletRayCast : MonoBehaviour
{
    Camera camera;
    public LayerMask mask;
    int bullets = 0;

    public Player player;

    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = camera.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position, Color.blue);


        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("cast");

            if(Physics.Raycast(ray, out hit, 100, mask))//destroys the trap object if its in the way of the ray once the player left clicks
            {
                Destroy(hit.collider.gameObject);
                player.AddBullet();
                bullets += 1;

                if(bullets == 5)
                {
                    SceneManager.LoadScene("Level02");
                }
            }
        }
    }
}
