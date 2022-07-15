using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofEnemy : Enemy
{
    private float ralativeY;

    public override void Start()
    {
        base.Start();
        ralativeY = navAgent.transform.position.y - transform.position.y;
    }
    public override void SetMoveTarget(Vector3 _pos)
    {
        moveTarget.position = new Vector3(_pos.x , navAgent.transform.position.y, _pos.z);
    }

    protected override void Move()
    {
        base.Move();

        Vector3 _copyPositionToRoof = navAgent.transform.position;
        _copyPositionToRoof.y = transform.position.y;
        transform.position = _copyPositionToRoof;
        //transform.localPosition = new Vector3(0,ralativeY,0);

        //Copy rotation
        transform.rotation = Quaternion.Euler( navAgent.transform.rotation.eulerAngles);

    }

    protected override void Run()
    {
        base.Run();

        Vector3 _copyPositionToRoof = navAgent.transform.position;
        _copyPositionToRoof.y = transform.position.y;
        transform.position = _copyPositionToRoof;
        //transform.localPosition = new Vector3(0,ralativeY,0);

        //Copy rotation
        transform.rotation = Quaternion.Euler(navAgent.transform.rotation.eulerAngles);

    }

    public override void OnRapid()
    {
        //ÂÅ©Ç=>ÅÜ¦¨«í°lÂÜ¼Ò¦¡
        Bounds _fullSceneBound = sightCollider.GetComponent<Collider>().bounds;
        _fullSceneBound.size = new Vector3(10,0.5f,10);

        constantChasing = true;
    }
}
