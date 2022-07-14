using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoofEnemy : Enemy
{
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

        //Copy rotation
        transform.rotation = Quaternion.Euler( navAgent.transform.rotation.eulerAngles);

    }
}
