using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRat : Collision
{
    public override void OnHit()
    {
        PlayerDataManager.Instance.HitDamage(2);
    }
}
