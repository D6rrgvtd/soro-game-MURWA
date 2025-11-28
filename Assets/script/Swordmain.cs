using UnityEngine;

public class Swordmain : MonoBehaviour, IWeapon
{
    public Camera playerCamera;
    public float damage = 35f;
    public float attackRange = 5f;
  

   
    void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
      
    }

    public void Attack()
    {
        
        if (playerCamera == null)
        {
            Debug.LogWarning("Sword is not equipped yet!");
            return;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, attackRange))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyHealth>()?.TakeDamage(damage);
                Debug.Log("Œ•ƒqƒbƒgI");
            }
        }
    }
}
