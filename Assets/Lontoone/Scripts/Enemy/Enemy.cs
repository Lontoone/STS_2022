using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    public bool constantChasing = false;
    public float moveSpeed;
    public float runSpeed;
    //怪物動作:(1)Idle (2)移動　(3)看到玩家後衝刺 (4)Jump Scare
    //public ActionController.mAction idle ,walk, run ;
    public Transform moveTarget;
    public ColliderDetector sightCollider;
    public ColliderDetector fightCollider;
    public NavMeshAgent navAgent;
    public Collider moveableBounds;
    [SerializeField]
    private bool isReached = true;
    public float reachedCheckRadious = 1f;
    private float initFloorY;
    Coroutine idleTimeColor;
    private string stateString = "idle";
    public LayerMask sightBlock;
    //private bool isInSight;
    public float loseSightDistance = 10;
    [SerializeField]
    private Transform chasingTarget;
    public virtual void Start()
    {
        initFloorY = transform.position.y;
        navAgent.speed = moveSpeed;
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
        else if (idleTimeColor != null)
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
            Vector3 dir = (_chasingObj.transform.position+ new Vector3(0, 0.5f, 0) - transform.position).normalized ;
            RaycastHit hit;
            //視覺
            Ray _ray = new Ray(transform.position , dir);
            Debug.DrawRay(_ray.origin,dir ,Color.red );

            if ( Physics.Raycast(_ray, out hit, 1000, sightBlock) || constantChasing)
            {
                Debug.Log("See " + hit.collider.gameObject.name);
                if (hit.collider.gameObject == sightCollider.collidersInRange[i].gameObject)
                {
                    SetMoveTarget(_chasingObj.transform.position);
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
            idleTimeColor = StartCoroutine(WaitForIdle());
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

    public virtual void SetMoveTarget(Vector3 _pos)
    {
        //TODO: y軸
        moveTarget.position = _pos;
    }

    protected virtual void Move()
    {
        //eMove?.Invoke(transform.position);
        //animator....
        navAgent.speed = moveSpeed;
        navAgent.destination = moveTarget.position;
        stateString = "Move";
    }

    protected virtual void Run()
    {
        //eMove?.Invoke(transform.position);
        navAgent.speed = runSpeed;
        navAgent.destination = moveTarget.position;
        stateString = "Run";
        //animator....
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(moveableBounds.center, moveableBounds.size);
    }*/

    IEnumerator WaitForIdle()
    {
        yield return new WaitForSeconds(5);
        idleTimeColor = null;
    }


    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), stateString);
    }
}
