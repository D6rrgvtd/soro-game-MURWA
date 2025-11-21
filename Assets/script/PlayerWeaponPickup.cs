using UnityEngine;

public class PlayerWeaponPickup : MonoBehaviour
{
    public Transform weaponHolder; // ŽèŒ³ˆÊ’u
    private GameObject currentWeapon;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            PickupWeapon(other.gameObject);
            
        }
    }

    void PickupWeapon(GameObject weapon)
    {
        var floatScript = weapon.GetComponent<FloatingItem>();
        if ((floatScript))
        {
            floatScript.StopFloating();
        }

        currentWeapon = weapon;
        weapon.transform.SetParent(weaponHolder);
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;

        // “–‚½‚è”»’è•s—v
        var col = weapon.GetComponent<Collider>();
        if (col) col.enabled = false;
        var rb = weapon.GetComponent<Rigidbody>();
        if (rb) rb.isKinematic = true;

        
    }
}
