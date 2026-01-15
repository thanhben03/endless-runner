using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Forward")]
    public float forwardSpeed = 7f;

    [Header("Lane")]
    public float laneDistance = 1.5f;
    public float laneChangeSpeed = 10f;
    private int currentLane = 0;

    [Header("Jump")]
    public float jumpForce = 7f;
    public float gravity = -20f;
    private float verticalVelocity;

    [Header("Slide")]
    public float slideDuration = 1f;
    private bool isSliding = false;

    private CharacterController controller;
    public Animator anim;

    private float originalHeight;
    private Vector3 originalCenter;

    public bool canMove = false;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        originalHeight = controller.height;
        originalCenter = controller.center;
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            PowerUpManager.Instance.Activate(PowerUpType.Magnet);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            PowerUpManager.Instance.Activate(PowerUpType.Invincible);

        if (Input.GetKeyDown(KeyCode.Alpha3))
            PowerUpManager.Instance.Activate(PowerUpType.DoubleScore);
        if (!canMove)
        {
            return;
        }
        HandleInput();
        Move();
    }

    void HandleInput()
    {
        var input = InputManager.Instance;

        if (input.SwipeLeft)
            ChangeLane(-1);

        if (input.SwipeRight)
            ChangeLane(1);

        if (input.SwipeUp)
            Jump();

        if (input.SwipeDown)
            StartCoroutine(Slide());
    }

    void Move()
    {
        Vector3 move = Vector3.forward * forwardSpeed;

        // Xác định vị trí lane
        float targetX = currentLane * laneDistance;
        float deltaX = targetX - transform.position.x;
        move.x = deltaX * laneChangeSpeed;

        // Gravity
        if (controller.isGrounded)
        {
            if (verticalVelocity < 0)
                verticalVelocity = -2f; // giữ chạm đất
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        move.y = verticalVelocity;

        controller.Move(move * Time.deltaTime);
    }

    void ChangeLane(int direction)
    {
        currentLane += direction;
        currentLane = Mathf.Clamp(currentLane, -1, 1);
    }

    void Jump()
    {
        if (!controller.isGrounded || isSliding) return;

        verticalVelocity = jumpForce;
        anim.SetTrigger("Jump");
    }

    IEnumerator Slide()
    {
        if (isSliding || !controller.isGrounded) yield break;

        isSliding = true;
        anim.SetBool("Slide", true);

        controller.height = originalHeight / 2;
        controller.center = new Vector3(
            originalCenter.x,
            originalCenter.y / 2,
            originalCenter.z
        );

        yield return new WaitForSeconds(slideDuration);

        controller.height = originalHeight;
        controller.center = originalCenter;

        anim.SetBool("Slide", false);
        isSliding = false;
    }
}
