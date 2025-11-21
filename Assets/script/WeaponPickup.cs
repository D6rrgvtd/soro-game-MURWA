using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private IWeapon weaponScript;
    private bool isPickedUp = false;

    void Awake()
    {
        // このオブジェクトにアタッチされている武器スクリプト（IWeaponを実装しているもの）を取得
        weaponScript = GetComponent<IWeapon>();

        if (weaponScript == null)
            Debug.LogError($"{gameObject.name} に IWeapon を実装したスクリプトが付いていません！");
    }

    void OnTriggerEnter(Collider other)
    {
        if (isPickedUp) return;

        if (other.CompareTag("Player"))
        {
            PlayerAttack playerAttack = other.GetComponent<PlayerAttack>();

            if (playerAttack != null)
            {
                // 現在の武器として登録
                playerAttack.currentWeapon = weaponScript;

                // モデルをプレイヤーの手に装備
                Transform weaponTransform = (weaponScript as MonoBehaviour).transform;
                weaponTransform.SetParent(other.transform);
                weaponTransform.localPosition = new Vector3(0.5f, -0.3f, 1f);
                weaponTransform.localRotation = Quaternion.identity;

                isPickedUp = true;
                Debug.Log($"{gameObject.name} を装備しました！");

                Destroy(GetComponent<Collider>()); // もう拾えないように
            }
            else
            {
                Debug.LogWarning("PlayerAttack がプレイヤーに付いていません。");
            }
        }
    }
}
