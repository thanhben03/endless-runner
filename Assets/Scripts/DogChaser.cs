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
        player = GamePlayManager.Instance.GetPlayer().transform;
        anim = GetComponent<Animator>();
        currentDistance = maxDistance;
        targetDistance = maxDistance;
        lastPlayerPos = player.position;
        PlayerDataManager.Instance.OnHitDamaged += PlayerHitDamage;
        anim.SetBool("Ready", true);

    }

    private void OnDestroy()
    {
        PlayerDataManager.Instance.OnHitDamaged -= PlayerHitDamage;

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

        if (isStart)
        {
            transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            Time.deltaTime * speed
        );
        }

        // Nhìn theo player
        transform.LookAt(player);
    }

    public void DogAlmostCatch()
    {
        targetDistance = minDistance;
    }

    public void DogFallBack()
    {
        targetDistance = maxDistance;
    }

    private void PlayerHitDamage(int health)
    {

        if (health <= 0)
        {
            anim.SetBool("Ready", true);
            catchUpSpeed = 0;
            followSpeed = 0;

            return;
        }
    }
}
