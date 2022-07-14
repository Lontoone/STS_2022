using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
{
    public float moveSpeed;
    //public ActionController.mAction idle ,walk;

    public Transform moveTarget;
    public ColliderDetector sightCollider;
    public ColliderDetector fightCollider;
    public NavMeshAgent navAgent;
    public Bounds moveableBounds;
    [SerializeField]
    private bool isReached = true;
    public float reachedCheckRadious = 1f;
    //public event Action<Vector3> eMove;
    private void Start()
    {

        navAgent.speed = moveSpeed;
    }
    private void Update()
    {
        //Move();
    }

    private void FixedUpdate()
    {

        bool isInSight = SightCheck();
        isReached = Vector3.Distance(moveTarget.position, navAgent.transform.position) < reachedCheckRadious;

        if (isInSight)
        {
            Move();
        }
        else if (isReached && !isInSight)
        {// && !isInSight) {
            NotSeeTarget();
        }
        else { Move(); }
    }


    public bool SightCheck()
    {
        for (int i = 0; i < sightCollider.collidersInRange.Count; i++)
        {
            //TODO: Ray 穿牆設定
            //sightCollider.collidersInRange[i];
        }
        //TEMP:
        //看到人=>追逐
        if (sightCollider.collidersInRange.Count > 0)
        {
            SetMoveTarget(sightCollider.collidersInRange[0].transform.position);
            return true;
        }
        /*
        //沒看到人 => 閒逛
        else
        {
            NotSeeTarget();
            return false;
        }*/
        return false;
    }

    public virtual void NotSeeTarget()
    {
        int _rand = Random.Range(0, 100);
        if (_rand < 0)
        {
            //Idle ....
            Debug.Log("Idle");
        }
        else
        {
            //Random set move Target
            Vector3 _newTargetPos = new Vector3(
                Random.Range(moveableBounds.min.x, moveableBounds.max.x),
                transform.position.y,
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
        navAgent.destination = moveTarget.position;


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(moveableBounds.center, moveableBounds.size);
    }
}
