using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public static Player instance;
    private Rigidbody r;
    private Camera c;

    public float MouseSens = 3.1f;
    private float YLock;
    private bool mLock;

    public float movespeedmultipy = 1;
    private Vector3 finalvelocity;

    public float HoverHeight = 0.2f;
    private RaycastHit hhRayHit;

    private void Awake()
    {
        instance = this;
        r = GetComponent<Rigidbody>();
        c = Camera.main;

        mLock = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mLock = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            mLock = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        YLock += Input.GetAxisRaw("Mouse Y") * MouseSens;
        YLock = Mathf.Clamp(YLock, -89, 89);
        c.transform.localEulerAngles = Vector3.left * YLock;

        transform.eulerAngles += Vector3.up * Input.GetAxisRaw("Mouse X") * MouseSens;

        finalvelocity = Vector3.zero;
        finalvelocity += transform.right * Input.GetAxisRaw("Horizontal");
        finalvelocity += transform.forward * Input.GetAxisRaw("Vertical");
        finalvelocity *= movespeedmultipy;
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position+Vector3.up, transform.up * -1, out hhRayHit, HoverHeight + 1.1f))
        {
            if (hhRayHit.distance < HoverHeight + 1)
            {
                transform.position += Vector3.up * 0.05f;
            }
        }
        else
        {
            finalvelocity += Vector3.down * 9.8f;
        }
        r.velocity = finalvelocity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + Vector3.up, hhRayHit.point);
    }
}
