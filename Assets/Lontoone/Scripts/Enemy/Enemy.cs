using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    public bool constantChasing = false;
    public AudioSource audioSource;
    public AudioClip runClip,discoverClip;
    public float moveSpeed;
    public float runSpeed;
    //怪物動作:(1)Idle (2)移動　(3)看到玩家後衝刺 (4)Jump Scare
    public Animator animator;
    public Transform moveTarget;
    public ColliderDetector sightCollider;
    public ColliderDetector fightCollider;
    public NavMeshAgent navAgent;
    public Collider moveableBounds;
    [SerializeField]
    private bool isReached = true;
    public float reachedCheckRadious = 1f;
    private float initFloorY;
    Coroutine idleTimeCoro;
    private string stateString = "idle";
    public LayerMask sightBlock;
    //private bool isInSight;
    public float loseSightDistance = 10;
    [SerializeField]
    private Transform chasingTarget;
    private float walkTime = 0f;
    public virtual void Start()
    {
        initFloorY = transform.position.y;
        navAgent.speed = moveSpeed;

        fightCollider.mOnTriggerEnter += CheckKillPleyer;
        ScoreManager.FEVER_TIME += OnRapid;
    }

    private void OnDestroy()
    {
        fightCollider.mOnTriggerEnter -= CheckKillPleyer;
        ScoreManager.FEVER_TIME -= OnRapid;
    }
    private void Update()
    {
        bool isInSight = SightCheck();

        isReached = Vector3.Distance(moveTarget.position, navAgent.transform.position) < reachedCheckRadious;

        if (isInSight)
        {
            //actionController.AddAction(run);

            Run();
        }
        else if (idleTimeCoro != null)
        {
            //idleing...
        }
        else if (isReached && !isInSight)
        {
            NotSeeTarget();
        }
        else
        {
            //actionController.AddAction(walk);
            Move(); //隨處走

            //行走超時
            walkTime += Time.deltaTime;
            if (walkTime > 5)
            {
                walkTime = 0;
                NotSeeTarget();
            }
        }
    }


    public virtual bool SightCheck()
    {

        //判斷距離 (是否甩開)
        if (chasingTarget != null && Vector3.Distance(chasingTarget.transform.position, transform.position) < loseSightDistance)
        {
            SetMoveTarget(chasingTarget.transform.position);
            return true;
        }

        //追丟了，尋找視線內新物件
        for (int i = 0; i < sightCollider.collidersInRange.Count; i++)
        {
            GameObject _chasingObj = sightCollider.collidersInRange[i];

            if (constantChasing) {
                SetMoveTarget(_chasingObj.transform.position);
                return true;
            }

            Vector3 dir = (_chasingObj.transform.position + new Vector3(0, 0.5f, 0) - transform.position).normalized;
            RaycastHit hit;
            //視覺
            Ray _ray = new Ray(transform.position, dir);
            Debug.DrawRay(_ray.origin, dir, Color.red);

            if (Physics.Raycast(_ray, out hit, 1000, sightBlock))
            {
                if (hit.collider.gameObject == sightCollider.collidersInRange[i].gameObject)
                {
                    SetMoveTarget(_chasingObj.transform.position);
                    //發現音效
                    if (_chasingObj!=chasingTarget && discoverClip!=null) {
                        audioSource.PlayOneShot(discoverClip);
                    }
                    chasingTarget = _chasingObj.transform;
                    return true;
                }
            }
        }

        //TEMP:
        //看到人=>追逐
        /*
        if (sightCollider.collidersInRange.Count > 0)
        {
            SetMoveTarget(sightCollider.collidersInRange[0].transform.position);
            return true;
        }*/

        chasingTarget = null;
        return false;
    }

    public virtual void NotSeeTarget()
    {
        int _rand = Random.Range(0, 100);
        if (_rand < 50)
        {
            //Idle ....
            Debug.Log("Idle");
            stateString = "Idle";
            animator.Play("Walk");
            audioSource.Stop();
            idleTimeCoro = StartCoroutine(WaitForIdle());
        }
        else
        {
            //Random set move Target
            Vector3 _newTargetPos = new Vector3(
                Random.Range(moveableBounds.bounds.min.x, moveableBounds.bounds.max.x),
                initFloorY,
                Random.Range(moveableBounds.bounds.min.z, moveableBounds.bounds.max.z)
                );
            SetMoveTarget(_newTargetPos);
        }
    }
    public virtual void OnRapid() { 
        //TODO....
    }
    public virtual void SetMoveTarget(Vector3 _pos)
    {
        moveTarget.position = _pos;
        navAgent.ResetPath();
        navAgent.SetDestination(_pos);
    }

    private void CheckKillPleyer(GameObject colliderEntered)
    {
        Player.Kill();

    }

    protected virtual void Move()
    {
        //eMove?.Invoke(transform.position);
        //animator....
        navAgent.speed = moveSpeed;
        navAgent.destination = moveTarget.position;
        stateString = "Move";

        animator.Play("Walk");
    }

    protected virtual void Run()
    {
        //eMove?.Invoke(transform.position);
        navAgent.speed = runSpeed;
        navAgent.destination = moveTarget.position;
        stateString = "Run";
        //animator....
        if (animator != null)
        {
            animator.Play("Run");
        }

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(runClip);
        }
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(moveableBounds.center, moveableBounds.size);
    }*/

    IEnumerator WaitForIdle()
    {
        yield return new WaitForSeconds(2.5f);
        idleTimeCoro = null;
    }


    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), stateString);
    }
}
