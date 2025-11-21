using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    // 装備中の武器（剣、銃など）
    public MonoBehaviour currentWeaponBehaviour; // Inspector 用（実行時に IWeapon にキャスト）
    public IWeapon currentWeapon;

    void Start()
    {
        if (currentWeaponBehaviour != null)
            currentWeapon = currentWeaponBehaviour as IWeapon;
    }

    // 外部から武器を差し替えるときはこれを呼ぶ
    public void EquipWeapon(MonoBehaviour weaponBehaviour)
    {
        currentWeaponBehaviour = weaponBehaviour;
        currentWeapon = weaponBehaviour as IWeapon;
    }

    void Update()
    {
        // 左クリックで発砲／攻撃
        if (Input.GetMouseButtonDown(0) && currentWeapon != null)
        {
            currentWeapon.Attack();
        }
    }
}
