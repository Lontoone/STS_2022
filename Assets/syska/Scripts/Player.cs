using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public AudioClip impact;
    AudioSource audiosource;

    public static Player instance;
    private Rigidbody r;
    private Camera c;
    public Camera mc;

    public Transform USD_Point;

    public float MouseSens = 3.1f;
    private float YLock;
    private bool mLock;
    public Transform SpawnPointTransform;

    [Header("Player Camera")]
    public float Player_Walk_FOV = 60;
    public float Player_Run_FOV = 90;
    public float Player_Camera_FOV_ChangeTime = 2f;
    private bool dotweenlerprun = false;

    [Header("Player Movement")]
    public KeyCode Player_Jump_Key = KeyCode.Space;
    public KeyCode Player_Run_Key = KeyCode.LeftShift;
    public float Player_Walk_Speed = 7;
    public float Player_Run_Speed = 10;
    public float Player_Gravity = 9.81f;
    [Header("Player RayCast !!Danger!!")]
    public float Player_RayCast_HoverHeight = 0.2f;
    [Tooltip("Hover Height + Player_RayCast_Distance")]
    public float Player_RayCast_Distance = 1.1f;
    public float Player_RayCast_Fallin_Check = 1f;
    public float Player_RayCast_UpLift = 0.05f;
    private Vector3 finalvelocity;

    private RaycastHit hhRayHit;
    [Tooltip("Select What Ray Will Hit, Deselect What Ray Will Ignore")]
    public LayerMask RayIgnore;

    [Header("Player HeadBob Animation")]
    public Animator HeadAnimator;

    [Header("Player States")]
    public static bool isUpSideDown = false;
    public static bool isRunning = false;
    public static bool isFinished { get; private set; } = false;

    [Header("DEBUG")]
    public bool DEBUG_RAYCAST_TARGET = false;

    private void Awake()
    {
        instance = this;
        r = GetComponent<Rigidbody>();
        c = Camera.main;
        isUpSideDown = false;
        isRunning = false;
        isFinished = false;

        mLock = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
        PlayerHUD.UpdateLifes();
    }

    private void Update()
    {
        if (isFinished) return;
        if (Input.GetKeyDown(KeyCode.K))
        {
            SpawnPoint.End();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            ScoreManager.Code_Percent = 200;
            SpawnPoint.End();
        }
        if (Input.GetKeyDown(Player_Jump_Key))
        {
            audiosource.PlayOneShot(impact);
            isUpSideDown = !isUpSideDown;
            if (isUpSideDown)
            {
                c.transform.DOLocalRotate(new Vector3(c.transform.localEulerAngles.x, c.transform.localEulerAngles.y, 180), 1);
                c.transform.DOLocalMoveY(0.05f, 1);
                mc.transform.localEulerAngles = new Vector3(-90, 0, 180);
                mc.nearClipPlane = -0.4f;
                mc.farClipPlane = 1.4f;
            }
            else
            {
                c.transform.DOLocalRotate(new Vector3(c.transform.localEulerAngles.x, c.transform.localEulerAngles.y, 0), 1);
                c.transform.DOLocalMoveY(0.45f, 1);
                mc.transform.localEulerAngles = new Vector3(90, 0, 0);
                mc.nearClipPlane = -1.4f;
                mc.farClipPlane = 0.4f;
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
        if (isFinished) return;
        if (isUpSideDown)
        {
            if (Physics.Raycast(USD_Point.position , transform.up, out hhRayHit, Player_RayCast_HoverHeight + Player_RayCast_Distance, RayIgnore))
            {
                if (hhRayHit.distance < Player_RayCast_HoverHeight + Player_RayCast_Fallin_Check)
                {
                    transform.position -= Vector3.up * Player_RayCast_UpLift;
                }
            }
            else
            {
                finalvelocity -= Vector3.down * Player_Gravity;
            }
        }
        else
        {
            if (Physics.Raycast(transform.position , transform.up * -1, out hhRayHit, Player_RayCast_HoverHeight + Player_RayCast_Distance, RayIgnore))
            {
                if (hhRayHit.distance < Player_RayCast_HoverHeight + Player_RayCast_Fallin_Check)
                {
                    transform.position += Vector3.up * Player_RayCast_UpLift;
                }
            }
            else
            {
                finalvelocity += Vector3.down * Player_Gravity;
            }
        }
        if (DEBUG_RAYCAST_TARGET && hhRayHit.collider != null) Debug.Log(hhRayHit.collider.gameObject.name);
        r.velocity = finalvelocity;
    }

    public static void Kill()
    {
        instance.transform.position = instance.SpawnPointTransform.position;
        //if (isUpSideDown) 
        ScoreManager.Life--;
        PlayerHUD.UpdateLifes();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (ScoreManager.Life <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0); /*???o?????O?????????`*/
        }else
        {
            PlayerHUD.ShowRemainLife();
        }
    }

    public static void DisablePlayer()
    {
        isFinished = true;
        instance.finalvelocity = Vector3.zero;
        instance.r.velocity = Vector3.zero;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public static void EnablePlayer()
    {
        isFinished = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up, hhRayHit.point);
    }
}
