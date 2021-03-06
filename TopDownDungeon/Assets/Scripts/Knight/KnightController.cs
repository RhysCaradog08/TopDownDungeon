using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    CharacterController cc;
    Animator anim;

    public float moveSpeed;
    public float turnSpeed;

    Vector3 input;
    float angle;

    Quaternion targetRotation;
    Transform cam;

    public CapsuleCollider swordTrigger;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        cam = Camera.main.transform;
    }

    private void Update()
    {
        GetInput();

        if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.z) < 1) return;

        CalculateDirection();
        RotatePlayer();
        MovePlayer();
    }

    void GetInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.z = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(input.x) > 0 || Mathf.Abs(input.z) > 0)
        {
            anim.SetBool("Moving", true);
        }
        else anim.SetBool("Moving", false);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("Attack");
            anim.SetTrigger("Attack");
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            Debug.Log("Block");
            anim.SetBool("Blocking", true);
        }
        else anim.SetBool("Blocking", false);
    }

    void CalculateDirection()
    {
        angle = Mathf.Atan2(input.x, input.z);
        angle = Mathf.Rad2Deg * angle;
        angle += cam.eulerAngles.y;
    }

    void RotatePlayer()
    {
        targetRotation = Quaternion.Euler(0, angle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    void MovePlayer()
    {
        cc.Move(transform.forward * moveSpeed * Time.deltaTime);
    }

    public void EnableSwordTrigger()
    {
        swordTrigger.enabled = true;
    }

    public void DisableSwordTrigger()
    {
        swordTrigger.enabled = false;
    }
}
