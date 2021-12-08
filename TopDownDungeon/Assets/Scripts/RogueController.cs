using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Basics.ObjectPool;

public class RogueController : MonoBehaviour
{
    CharacterController cc;
    Animator anim;

    public float moveSpeed;
    public float turnSpeed;

    Vector2 input;
    float angle;

    Quaternion targetRotation;
    Transform cam;

    public GameObject arrow;
    public Transform shootPoint;
    [SerializeField] float shootForce;
    [SerializeField] float shotDelay;
    [SerializeField] bool shotTaken;

    private void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        cam = Camera.main.transform;

        shotTaken = false;
    }

    private void Update()
    {
        GetInput();

        if (Mathf.Abs(input.x) < 1 && Mathf.Abs(input.y) < 1) return;

        CalculateDirection();
        RotatePlayer();
        MovePlayer();
    }

    void GetInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");

        if (Mathf.Abs(input.x) > 0 || Mathf.Abs(input.y) > 0)
        {
            anim.SetBool("Moving", true);
        }
        else anim.SetBool("Moving", false);

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            arrow = ObjectPoolManager.instance.CallObject("Arrow", shootPoint, shootPoint.position, shootPoint.rotation);
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Debug.Log("Aiming");
            anim.SetBool("Aiming", true);

            Rigidbody rb = arrow.GetComponent<Rigidbody>();
            rb.isKinematic = true;
        }
        else anim.SetBool("Aiming", false);

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            Debug.Log("Shoot");
            ShootArrow();
        }
    }

    void CalculateDirection()
    {
        angle = Mathf.Atan2(input.x, input.y);
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

    void ShootArrow()
    {
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.isKinematic = false;

        rb.AddForce(transform.forward * shootForce, ForceMode.Impulse);
        arrow.transform.parent = null;
    }
}
