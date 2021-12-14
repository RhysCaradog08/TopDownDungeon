using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Basics.ObjectPool;

public class Arrow : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] bool hit;
    [SerializeField] float recallDelay;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hit = false;
    }

    private void Update()
    {
        if(hit)
        {
            rb.isKinematic = true;
            recallDelay -= Time.deltaTime;
        }

        if (recallDelay <= 0)
        {
            recallDelay = 0;
        }

        if(hit && recallDelay <= 0)
        {
            rb.isKinematic = false;
            ObjectPoolManager.instance.RecallObject(this.gameObject);
            hit = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit Something");
        recallDelay = 1;
        hit = true;
        //ObjectPoolManager.instance.RecallObject(this.gameObject);
    }
}
