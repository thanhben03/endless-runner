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

    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("Ready", true);
        currentDistance = maxDistance;
        targetDistance = maxDistance;
        lastPlayerPos = player.position;

    }

    void Update()
    {
        float delta = Vector3.Distance(player.position, lastPlayerPos);
        if (delta > 0.01f)
        {
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
}
