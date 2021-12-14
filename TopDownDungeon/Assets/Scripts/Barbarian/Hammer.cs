using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);

        if (other.tag == "Destructable")
        {
            Debug.Log("Hit Destructable");
            Destroy(other.gameObject);
        }

    }
}
