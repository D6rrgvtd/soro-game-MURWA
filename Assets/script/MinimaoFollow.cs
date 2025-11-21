using UnityEngine;

public class MinimapFollow : MonoBehaviour
{
    public Transform player;
    public bool rotateWithPlayer = true;
    public float height = 50f;

    void LateUpdate()
    {
        if (player == null) return;

        // プレイヤー上空に位置を合わせる
        Vector3 newPos = player.position;
        newPos.y += height;
        transform.position = newPos;

        // プレイヤーの向きに合わせて回転
        if (rotateWithPlayer)
        {
            transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        }
        else
        {
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }
    }
}
