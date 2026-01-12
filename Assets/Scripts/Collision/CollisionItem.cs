using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionItem : Collision
{
    private void Awake()
    {
        anim = GetComponentInChildren<Animation>();
    }
    public override void OnHit()
    {
        anim.Play();

        PlayerDataManager.Instance.HitDamage(1);
        Debug.Log("Ostacle collision");
    }
}
