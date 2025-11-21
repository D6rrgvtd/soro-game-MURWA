using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public ProceduralTerrain terrain;
    public float chaseRange = 20f;
    public float moveSpeed = 3f;
    public float returnSpeed = 2f;
    public float heightOffset = 0.5f;
    public float heightSmoothSpeed = 5f;

    private Vector3 startPosition;
    private bool isChasing = false;
    private Rigidbody rb;

    void Start()
    {
        startPosition = transform.position;

        
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.useGravity = false;
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        if (player == null || terrain == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // プレイヤーを追う or 元の位置に戻る
        Vector3 target = (distance < chaseRange)
            ? player.position
            : startPosition;

        MoveTowards(target, (distance < chaseRange) ? moveSpeed : returnSpeed);
    }

    void MoveTowards(Vector3 target, float speed)
    {
        Vector3 direction = (target - transform.position).normalized;
        Vector3 nextPos = transform.position + direction * speed * Time.deltaTime;

        // ★地形の高さをスムーズに追従
        float terrainHeight = terrain.GetHeightAtPosition(nextPos.x, nextPos.z) + heightOffset;
        float smoothY = Mathf.Lerp(transform.position.y, terrainHeight, Time.deltaTime * heightSmoothSpeed);
        nextPos.y = smoothY;

        transform.position = nextPos;

        // 向きもスムーズに補間
        Vector3 flatDir = new Vector3(direction.x, 0, direction.z);
        if (flatDir.sqrMagnitude > 0.001f)
        {
            Quaternion targetRot = Quaternion.LookRotation(flatDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * 5f);
        }
    }
}
