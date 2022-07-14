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

    public float movespeedmultipy = 1;
    private Vector3 finalvelocity;

    public float HoverHeight = 0.2f;
    private RaycastHit hhRayHit;

    private void Awake()
    {
        instance = this;
        r = GetComponent<Rigidbody>();
        c = Camera.main;
    }

    private void Update()
    {
        c.transform.localEulerAngles += Vector3.left * Input.GetAxisRaw("Mouse Y") * MouseSens;
        YLock += Input.GetAxis("Mouse X") * MouseSens;
        YLock = Mathf.Clamp(YLock, -89, 89);
        transform.eulerAngles = Vector3.up * YLock;

        finalvelocity = Vector3.zero;
        finalvelocity += transform.right * Input.GetAxis("Horizontal");
        finalvelocity += transform.forward * Input.GetAxis("Vertical");
        finalvelocity *= movespeedmultipy;
        if (Physics.Raycast(transform.position, transform.up * -1, out hhRayHit, HoverHeight + 0.1f))
        {
            if (hhRayHit.distance < HoverHeight - 0.1f)
            {
                transform.position += Vector3.up * 0.1f;
            }
        }
        else
        {
            finalvelocity += Vector3.down * 9.8f;
        }
    }

    private void FixedUpdate()
    {
        r.velocity = finalvelocity;
    }
}
