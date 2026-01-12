using System.Collections;
using UnityEngine;

public class RatMovement : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 3f;

    [Header("Delay")]
    public float minDelay = 1f;
    public float maxDelay = 3f;

    private int moveDir = 0;
    private bool canMove = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        // Pause animation
        animator.speed = 0f;

        // Delay ngẫu nhiên
        StartCoroutine(DelayStart());
    }

    IEnumerator DelayStart()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(minDelay, maxDelay));

        canMove = true;
        animator.speed = 1f; // Resume animation
    }

    // ===== ANIMATION EVENTS =====

    public void MoveLeft()
    {
        if (!canMove) return;
        moveDir = -1;
    }

    public void MoveRight()
    {
        if (!canMove) return;
        moveDir = 1;
    }

    public void StopMove()
    {
        moveDir = 0;
    }

    // ===== REALTIME MOVE =====

    void Update()
    {
        if (!canMove || moveDir == 0) return;

        transform.position += Vector3.right * moveDir * moveSpeed * Time.deltaTime;
    }
}
