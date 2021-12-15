using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Switch : MonoBehaviour
{
    public GameObject door;
    public bool pressed;

    Vector3 doorStartPos;
    float doorMovement;
    [SerializeField] float doorSpeed;

    // Start is called before the first frame update
    void Start()
    {
        doorStartPos = door.transform.position;
        pressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(pressed)
        {
            doorMovement = Vector3.Distance(doorStartPos, door.transform.position);

            door.transform.position -= new Vector3(0,4,0) * doorSpeed * Time.deltaTime;

        }

        if (doorMovement >= 4)
        {
            door.SetActive(false);
        }
    }
}
