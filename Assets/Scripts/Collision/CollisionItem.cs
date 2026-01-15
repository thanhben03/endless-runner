using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionItem : Collision
{
    private Vector3 originalPos;
    private Quaternion originalRot;

    private void Awake()
    {
        anim = GetComponentInChildren<Animation>();
        originalPos = transform.localPosition;
        originalRot = transform.localRotation;
    }
    public override void OnHit()
    {
        anim.Play();

        PlayerDataManager.Instance.HitDamage(1);
        StartCoroutine(ResetObstacle());
    }

    IEnumerator ResetObstacle()
    {
        transform.localPosition = originalPos;
        transform.localRotation = originalRot;

        yield return new WaitForSeconds(2);

        if (anim != null && PlayerDataManager.Instance.Health > 0)
        {
            anim.Stop();
            anim.Rewind();
            anim.Play();
            anim.Sample();   
            anim.Stop();
            GetComponent<BoxCollider>().enabled = true;

        }
    }
}
