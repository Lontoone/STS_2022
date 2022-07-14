using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public static Player instance;
    private Rigidbody r;
    private Camera c;

    public Transform USD_Point;

    public float MouseSens = 3.1f;
    private float YLock;
    private bool mLock;

    [Header("Player Movement")]
    public float movespeedmultipy = 1;
    private Vector3 finalvelocity;

    public float HoverHeight = 0.2f;
    private RaycastHit hhRayHit;

    [Header("Player HeadBob Animation")]
    public Animator HeadAnimator;

    [Header("Player States")]
    public static bool isUpSideDown = false;
    public static int Lifes = 3;

    private void Awake()
    {
        instance = this;
        r = GetComponent<Rigidbody>();
        c = Camera.main;

        mLock = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        PlayerHUD.UpdateLifes();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isUpSideDown = !isUpSideDown;
            if (isUpSideDown)
            {
                c.transform.DOLocalRotate(new Vector3(c.transform.eulerAngles.x, c.transform.eulerAngles.y, 180), 1);
                c.transform.DOLocalMoveY(0.25f, 1);
            }
            else
            {
                c.transform.DOLocalRotate(new Vector3(c.transform.eulerAngles.x, c.transform.eulerAngles.y, 180), 1);
                c.transform.DOLocalMoveY(1.75f, 1);
            }
        }

        #region Cursor Lock/Release
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
        #endregion

        #region Player Camera Up/Down
        YLock += Input.GetAxisRaw("Mouse Y") * MouseSens;
        YLock = Mathf.Clamp(YLock, -89, 89);
        c.transform.localEulerAngles = new Vector3(YLock, c.transform.localEulerAngles.y, c.transform.localEulerAngles.z);
        #endregion

        #region Player Rotate and Move
        transform.eulerAngles += Input.GetAxisRaw("Mouse X") * MouseSens * (isUpSideDown == false ? Vector3.up : Vector3.down);

        finalvelocity = Vector3.zero;
        finalvelocity += transform.right * Input.GetAxisRaw("Horizontal");
        finalvelocity += transform.forward * Input.GetAxisRaw("Vertical");
        finalvelocity *= movespeedmultipy;
        #endregion

        if (finalvelocity.x != 0 && finalvelocity.z != 0)
        {
            HeadAnimator.SetBool("isMoving", true);
        }
        else
        {
            HeadAnimator.SetBool("isMoving", false);
        }

        if (Input.GetKeyDown(KeyCode.K)) Damage();
    }

    private void FixedUpdate()
    {
        if (isUpSideDown)
        {
            if (Physics.Raycast(USD_Point.position + Vector3.down, transform.up, out hhRayHit, HoverHeight + 1.1f))
            {
                if (hhRayHit.distance < HoverHeight + 1)
                {
                    transform.position -= Vector3.up * 0.05f;
                }
            }
            else
            {
                finalvelocity -= Vector3.down * 9.8f;
            }
        }
        else
        {
            if (Physics.Raycast(transform.position + Vector3.up, transform.up * -1, out hhRayHit, HoverHeight + 1.1f))
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
        }
        r.velocity = finalvelocity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + Vector3.up, hhRayHit.point);
    }

    public static void Damage()
    {
        Lifes--;
        PlayerHUD.UpdateLifes();
    }
}
