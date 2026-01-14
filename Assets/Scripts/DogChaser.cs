using UnityEngine;

public class DogChaser : MonoBehaviour
{
    public Transform player;

    [Header("Distance")]
    public float maxDistance = 8f;   // xa nhất
    public float minDistance = 2.5f; // gần nhất (cảm giác sắp bắt)
    private float currentDistance;

    [Header("Speed")]
    public float followSpeed = 5f;
    public float catchUpSpeed = 8f;

    private float targetDistance;
    private Vector3 lastPlayerPos;

    private Animator anim;

    private bool isStart = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        currentDistance = maxDistance;
        targetDistance = maxDistance;
        lastPlayerPos = player.position;
        PlayerDataManager.Instance.OnHitDamaged += PlayerHitDamage;
        anim.SetBool("Ready", true);

    }

    void Update()
    {
        float delta = Vector3.Distance(player.position, lastPlayerPos);
        if (delta > 0.01f && !isStart)
        {
            isStart = true;
            anim.SetBool("Ready", false);
        }
        Vector3 targetPos =
            player.position - player.forward * currentDistance;

        // Chọn tốc độ
        float speed = currentDistance > targetDistance
            ? catchUpSpeed
            : followSpeed;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            Time.deltaTime * speed
        );

        // Nhìn theo player
        transform.LookAt(player);
    }

    // 👉 Gọi khi player mắc lỗi
    public void DogAlmostCatch()
    {
        targetDistance = minDistance;
    }

    // 👉 Gọi khi player chơi tốt lại
    public void DogFallBack()
    {
        targetDistance = maxDistance;
    }

    private void PlayerHitDamage(int health)
    {
        Debug.Log("Player Hit Damage on Dog chase: " + health);
        if (health <= 0)
        {
            anim.SetBool("Ready", true);
            catchUpSpeed = 0;
            followSpeed = 0;

            return;
        }
    }
}
