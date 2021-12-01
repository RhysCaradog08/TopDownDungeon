using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShift : MonoBehaviour
{
    Camera cam;
    CameraController camControl;

    Transform player;
    Transform currentTarget;
    [SerializeField] Transform newTarget;

    bool playerOverlapping;

    private void Start()
    {
        cam = Camera.main;
        camControl = FindObjectOfType<CameraController>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (playerOverlapping)
        {
            Vector3 doorToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.forward, doorToPlayer);

            Debug.Log("Dot Product: " + dotProduct);

            if (dotProduct < 0)
            {
                cam.transform.position = newTarget.position + camControl.offset;

                camControl.target = newTarget;

                playerOverlapping = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerOverlapping = false;
        }
    }
}
