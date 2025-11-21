using UnityEngine;
using System.Collections.Generic;


public class MinimapMarker : MonoBehaviour
{
    public Transform player; // プレイヤーのTransform
    public RectTransform marker; // UI上のマーカー
    public Camera minimapCamera; // ミニマップカメラ

    void Update()
    {
        // プレイヤーの位置を座標に変換
        Vector3 screenPos = minimapCamera.WorldToViewportPoint(player.position);


        marker.anchorMin = screenPos;
        marker.anchorMax = screenPos;
        marker.anchoredPosition = Vector2.zero;
    }
}

