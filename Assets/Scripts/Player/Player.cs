using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator anim;
    public Camera mainCam;

    void Start()
    {
        PlayerDataManager.Instance.OnHitDamaged += HitDamage;
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HitDamage(int health)
    {
        mainCam.GetComponent<Animator>().SetTrigger("HitWall");
        if (health <= 0)
        {
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            anim.SetInteger("Died", 2);
        } else
        {
            anim.SetTrigger("HitWall");
        }
    }

}
