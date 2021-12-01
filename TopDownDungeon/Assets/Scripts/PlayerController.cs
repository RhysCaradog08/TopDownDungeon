using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController cc;

    [SerializeField] float moveSpeed;

    public bool stopped;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        stopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        cc.Move(move * Time.deltaTime * moveSpeed);

        if (stopped)
        {
            cc.enabled = false;
        }
        else cc.enabled = true;
    }
}
