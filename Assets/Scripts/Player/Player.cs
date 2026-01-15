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
        anim = GetComponentInChildren<Animator>();
        UpdateCameraReference();
    }

    private void OnDestroy()
    {
        PlayerDataManager.Instance.OnHitDamaged -= HitDamage;
    }

    void OnEnable()
    {
        UpdateCameraReference();
        PlayerDataManager.Instance.ResetPlayerData();
    }

    private void UpdateCameraReference()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HitDamage(int health)
    {
        
        if (mainCam == null)
        {
            UpdateCameraReference();
        }
        
        if (mainCam != null)
        {
            mainCam.GetComponent<Animator>().SetTrigger("HitWall");

        }
        
        if (health <= 0)
        {
            PlayerDataManager.Instance.SaveAfterRun();
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.GetComponent<PlayerMovement>().forwardSpeed = 0f;
            AudioManager.Instance.PlayMusicDeath();
            anim.SetInteger("Died", 3);
        } else
        {
            anim.SetTrigger("HitWall");
        }
    }

}
