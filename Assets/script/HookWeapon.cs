using UnityEngine;
using System.Collections;
using Unity.VisualScripting.Dependencies.NCalc;


public class HookWeapon : MonoBehaviour, IWeapon
{
    [Header("Hook Settings")]
    public Camera playerCamera;
    public float range = 30f;
    public int pullDamage = 30;
    public int bonusDamage = 70;
    public float pullSpeed = 10f;
    public KeyCode bonusKey = KeyCode.LeftShift;

    private GameObject pulledEnemy;
    private bool canBonus = false;
    

  
    void Start()
    {
        if (playerCamera == null) 
            playerCamera = Camera.main;
        
    }

    public void Attack()
    {

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray,out RaycastHit hit, range))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("hit");
                pulledEnemy = hit.collider.gameObject;

                
                StartCoroutine(PullEnemy(pulledEnemy));

                // èââÒÉ_ÉÅÅ[ÉW
                var enemyHP = pulledEnemy.GetComponent<EnemyHealth>();
                if (enemyHP != null)
                    enemyHP.TakeDamage(pullDamage);

                // 2ïbä‘ShiftÇ≈í«â¡çUåÇ
                canBonus = true;
                StartCoroutine(BonusTimer());
            }
        }
    }

    private IEnumerator PullEnemy(GameObject enemy)
    {
        Vector3 targetPos = transform.position + playerCamera.transform.forward * 3f;

        while ( enemy != null && Vector3.Distance(enemy.transform.position,targetPos) >0.1f)
        {
            enemy.transform.position = Vector3.MoveTowards(
                enemy.transform.position,
                targetPos,
                pullSpeed * Time.deltaTime);
        }

        yield return null;

    }

    private IEnumerator BonusTimer()
    {
        float timer = 0f;
        while (timer < 0.2f)
        {
            timer += Time.deltaTime;

            if (canBonus && Input.GetKeyDown(bonusKey) && pulledEnemy != null)
            {
                var enemyHP = pulledEnemy.GetComponent<EnemyHealth>();
                if (enemyHP != null)
                    enemyHP.TakeDamage(bonusDamage);

                canBonus = false;
                break;
            }

            yield return null;
        }

        canBonus = false;
    }
}
