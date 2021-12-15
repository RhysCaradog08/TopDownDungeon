using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Destructable")
        {
            Destroy(other.gameObject);
        }

        if(other.tag == "Switch")
        {
            Debug.Log("Hit Switch");

            Animator switchAnim = other.GetComponentInParent<Animator>();
            switchAnim.SetTrigger("Pressed");
        }
    }
}
