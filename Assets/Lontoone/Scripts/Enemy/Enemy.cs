using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    public float moveSpeed;
    public float runSpeed;
    //怪物動作:(1)Idle (2)移動　(3)看到玩家後衝刺 (4)Jump Scare
    //public ActionController.mAction idle ,walk, run ;
    private ActionController actionController;

    public Transform moveTarget;
    public ColliderDetector sightCollider;
    public ColliderDetector fightCollider;
    public NavMeshAgent navAgent;
    public Bounds moveableBounds;
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
        actionController = gameObject.GetComponent<ActionController>();
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

    private void FixedUpdate()
    {

        //檢查
        /*
        if (Mathf.Abs(navAgent.transform.position.y - moveTarget.transform.position.y ) > 3) {
            Vector3 _sameSideY = moveTarget.transform.position;
            _sameSideY.y = navAgent.transform.position.y;
            moveTarget.transform.position = _sameSideY; 
        }*/

    }


    public bool SightCheck()
    {

        for (int i = 0; i < sightCollider.collidersInRange.Count; i++)
        {
            GameObject _chasingObj = sightCollider.collidersInRange[i];
            //TODO: Ray 穿牆設定
            Vector3 dir = (_chasingObj.transform.position+ new Vector3(0, 0.5f, 0) - transform.position).normalized ;
            RaycastHit hit;
            //看到敵人
            Debug.Log("See "+ _chasingObj.gameObject.name);

            //判斷距離 (是否甩開)
            if (chasingTarget!=null && Vector3.Distance(_chasingObj.transform.position , transform.position) < loseSightDistance) {
                SetMoveTarget(_chasingObj.transform.position);
                return true;
            }

            //視覺
            Debug.Log("Do Ray");
            Ray _ray = new Ray(transform.position , dir);
            Debug.DrawRay(_ray.origin,dir ,Color.red );

            if (Physics.Raycast(_ray, out hit, 10000, sightBlock))
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
                Random.Range(moveableBounds.min.x, moveableBounds.max.x),
                initFloorY,
                Random.Range(moveableBounds.min.z, moveableBounds.max.z)
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(moveableBounds.center, moveableBounds.size);
    }

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
