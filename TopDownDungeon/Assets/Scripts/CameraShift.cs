using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShift : MonoBehaviour
{
    Camera cam;
    CameraController camControl;

    Transform player;
    PlayerController playerControl;

    [SerializeField] Transform newTarget;

    private Vector3 velocity = Vector3.zero;
    public float lerpTime;

    bool playerOverlapping;

    private void Start()
    {
        cam = Camera.main;
        camControl = FindObjectOfType<CameraController>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerController playerControl = player.GetComponent<PlayerController>();
    }

    void LateUpdate()
    {
        if (playerOverlapping)
        {
            Vector3 doorToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.forward, doorToPlayer);

            //Debug.Log("Dot Product: " + dotProduct);

            if (dotProduct < 0)
            {
                MoveCamera();
            }
        }
    }

    void MoveCamera()
    {
        Debug.Log("Camera Distance: " + Vector3.Distance(cam.transform.position, newTarget.position + camControl.offset));

        cam.transform.position = Vector3.Lerp(cam.transform.position, newTarget.position + camControl.offset, lerpTime);

        camControl.target = newTarget;

        playerOverlapping = false;
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
