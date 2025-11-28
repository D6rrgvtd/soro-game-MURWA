using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private IWeapon weaponScript;
    private bool isPickedUp = false;

    void Awake()
    {
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
                // 武器登録
                playerAttack.currentWeapon = weaponScript;

                // 武器モデルを手に装備
                Transform weaponTransform = (weaponScript as MonoBehaviour).transform;
                weaponTransform.SetParent(other.transform);
                weaponTransform.localPosition = new Vector3(0.5f, -0.3f, 1f);
                weaponTransform.localRotation = Quaternion.identity;

                // ★ この武器に付いている全ての攻撃スクリプトを有効化
                EnableWeaponScripts(weaponTransform.gameObject);

                isPickedUp = true;
                Destroy(GetComponent<Collider>());
            }
        }
    }

    // 子オブジェクトも含めて武器スクリプトをONにする
    void EnableWeaponScripts(GameObject weapon)
    {
        // MonoBehaviour 全てを取得して有効化
        MonoBehaviour[] scripts = weapon.GetComponentsInChildren<MonoBehaviour>(true);

        foreach (var s in scripts)
        {
            // IWeapon ではなく「攻撃に関わるスクリプトのみ」をONにしたい場合、
            // 名前などでフィルタ可能（例：GunRecoil, SwordAttack）
            if (s is GunRecoil || s is tateburi )
            {
                s.enabled = true;
            }
        }
    }
}
