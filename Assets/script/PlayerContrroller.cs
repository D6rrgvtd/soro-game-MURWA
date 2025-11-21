using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{

    // プレイヤー設定

    [Header("Movement Settings")]
    [Tooltip("移動速度 (m/s)")]
    public float moveSpeed = 6f;

    [Tooltip("マウスの視点移動速度")]
    public float lookSpeed = 1.2f;

    [Tooltip("ジャンプの高さ")]
    public float jumpHeight = 2f;

    [Tooltip("重力の強さ（マイナス値推奨）")]
    public float gravity = -9.81f;


    private CharacterController controller;
    private Vector3 velocity;
    private Camera playerCamera;
    private float xRotation = 0f;


    // 初期化

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // 子オブジェクトからカメラを取得
        playerCamera = GetComponentInChildren<Camera>();

        // マウスカーソルを固定してロック
        Cursor.lockState = CursorLockMode.Locked;
    }


    //  毎フレーム更新

    void Update()
    {
        HandleMovement();
        HandleLook();
    }


        // 移動処理

        void HandleMovement()
        {
            // 入力取得（WASD）
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            // プレイヤーの向きに基づく移動方向
            Vector3 move = transform.right * x + transform.forward * z;
            controller.Move(move * moveSpeed * Time.deltaTime);

            
            if (controller.isGrounded && velocity.y < 0)
                velocity.y = -2f;

            // ジャンプ入力
            if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            // 重力適用
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }


        //  視点操作

        void HandleLook()
        {
            float mouseX = Input.GetAxis("Mouse X") * lookSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * lookSpeed;

            // 垂直回転（上下）
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            // カメラの上下回転
            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // プレイヤー本体の水平回転
            transform.Rotate(Vector3.up * mouseX);
        }
    
}
