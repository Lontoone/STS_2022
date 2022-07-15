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
    public Transform mc;

    public Transform USD_Point;

    public float MouseSens = 3.1f;
    private float YLock;
    private bool mLock;

    [Header("Player Camera")]
    public float Player_Walk_FOV = 60;
    public float Player_Run_FOV = 90;
    public float Player_Camera_FOV_ChangeTime = 2f;
    private bool dotweenlerprun = false;

    [Header("Player Movement")]
    public KeyCode Player_Jump_Key = KeyCode.Q;
    public KeyCode Player_Run_Key = KeyCode.LeftShift;
    public float Player_Walk_Speed = 7;
    public float Player_Run_Speed = 10;
    private Vector3 finalvelocity;

    public float HoverHeight = 0.2f;
    private RaycastHit hhRayHit;
    [Tooltip("Select What Ray Will Hit, Deselect What Ray Will Ignore")]
    public LayerMask RayIgnore;

    [Header("Player HeadBob Animation")]
    public Animator HeadAnimator;

    [Header("Player States")]
    public static bool isUpSideDown = false;
    public static bool isRunning = false;

    [Header("DEBUG")]
    public bool DEBUG_RAYCAST_TARGET = false;

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
        if (Input.GetKeyDown(Player_Jump_Key))
        {
            isUpSideDown = !isUpSideDown;
            if (isUpSideDown)
            {
                c.transform.DOLocalRotate(new Vector3(c.transform.localEulerAngles.x, c.transform.localEulerAngles.y, 180), 1);
                c.transform.DOLocalMoveY(0.25f, 1);
                mc.localEulerAngles = new Vector3(-90, 90, 90);
            }
            else
            {
                c.transform.DOLocalRotate(new Vector3(c.transform.localEulerAngles.x, c.transform.localEulerAngles.y, 0), 1);
                c.transform.DOLocalMoveY(1.75f, 1);
                mc.localEulerAngles = new Vector3(90, 90, 90);
            }
        }
        isRunning = Input.GetKey(Player_Run_Key);

        if (isRunning && !dotweenlerprun)
        {
            c.DOFieldOfView(Player_Run_FOV, Player_Camera_FOV_ChangeTime);
            dotweenlerprun = true;
        }
        else if (!isRunning && dotweenlerprun)
        {
            c.DOFieldOfView(Player_Walk_FOV, Player_Camera_FOV_ChangeTime);
            dotweenlerprun = false;
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
        YLock += Input.GetAxisRaw("Mouse Y") * MouseSens * (isUpSideDown == false ? -1 : 1);
        YLock = Mathf.Clamp(YLock, -89, 89);
        c.transform.localEulerAngles = new Vector3(YLock, c.transform.localEulerAngles.y, c.transform.localEulerAngles.z);
        #endregion

        #region Player Rotate and Move
        transform.eulerAngles += Input.GetAxisRaw("Mouse X") * MouseSens * (isUpSideDown == false ? Vector3.up : Vector3.down);

        finalvelocity = Vector3.zero;
        finalvelocity += transform.right * Input.GetAxisRaw("Horizontal") * (isUpSideDown ? -1 : 1);
        finalvelocity += transform.forward * Input.GetAxisRaw("Vertical");
        finalvelocity = Vector3.Normalize(finalvelocity) * (isRunning ? Player_Run_Speed : Player_Walk_Speed);
        #endregion

        #region Player HeadBob Animation
        HeadAnimator.speed = isRunning ? 1.5f : 1f;
        if (finalvelocity.x != 0 && finalvelocity.z != 0)
        {
            HeadAnimator.SetBool("isMoving", true);
        }
        else
        {
            HeadAnimator.SetBool("isMoving", false);
        }
        #endregion
    }

    private void FixedUpdate()
    {
        if (isUpSideDown)
        {
            if (Physics.Raycast(USD_Point.position + Vector3.down, transform.up, out hhRayHit, HoverHeight + 1.1f, RayIgnore))
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
            if (Physics.Raycast(transform.position + Vector3.up, transform.up * -1, out hhRayHit, HoverHeight + 1.1f, RayIgnore))
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
        if (DEBUG_RAYCAST_TARGET && hhRayHit.collider != null) Debug.Log(hhRayHit.collider.gameObject.name);
        r.velocity = finalvelocity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + Vector3.up, hhRayHit.point);
    }
}
