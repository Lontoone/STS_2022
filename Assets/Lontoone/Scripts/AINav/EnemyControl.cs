using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class EnemyControl : Enemy
{
    public override void OnRapid() {
        runSpeed = 1.3f;
    }
}
