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
        Debug.Log("HEALTH: " + health);
        
        // Kiểm tra và cập nhật lại Camera reference nếu cần
        if (mainCam == null)
        {
            UpdateCameraReference();
        }
        
        // Kiểm tra lại sau khi cập nhật
        if (mainCam != null)
        {
            mainCam.GetComponent<Animator>().SetTrigger("HitWall");

        }
        
        if (health <= 0)
        {
            PlayerDataManager.Instance.SaveAfterRun();
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.GetComponent<PlayerMovement>().forwardSpeed = 0f;
            anim.SetInteger("Died", 3);
        } else
        {
            anim.SetTrigger("HitWall");
        }
    }

}
