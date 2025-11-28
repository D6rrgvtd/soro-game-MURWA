using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour, IWeapon
{
    [Header("Gun Settings")]
    public Camera playerCamera;
    public float damage = 25f;
    public float range = 15f;
    public int maxAmmo = 12;
    public int reloadcount = 0;
    public float reloadTime = 1.5f;
    public float fireRate = 0.2f; // 連射間隔（ここでは半自動なら0.2）

    /*[Header("Effects (optional)")]
    public ParticleSystem muzzleFlash;
    public GameObject impactPrefab;
    public AudioClip fireSound;
    public AudioClip reloadSound;*/

    private int currentAmmo;
    private bool isReloading = false;
    private float lastFireTime = 0f;
    private AudioSource audioSource;

    

  
    void Start()
    {
        currentAmmo = maxAmmo;
        if (playerCamera == null)
            playerCamera = Camera.main;
        audioSource = GetComponent<AudioSource>();
    }

   

    public void Attack()
    {
        if (isReloading) return;

        if (Time.time - lastFireTime < fireRate) return;

        if (currentAmmo <= 0)
        {
            if (!isReloading) 
                StartCoroutine(Reload());
            return;
        }

        lastFireTime = Time.time;
        currentAmmo--;


        /*if (muzzleFlash != null) muzzleFlash.Play();
        if (audioSource != null && fireSound != null) audioSource.PlayOneShot(fireSound);*/

       


        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        if (Physics.Raycast(ray, out RaycastHit hit, range))
        {
            


            /*if (impactPrefab != null)
            {
                Instantiate(impactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
            }*/

            if (hit.collider.CompareTag("Enemy"))
            {
                hit.collider.GetComponent<EnemyHealth>()?.TakeDamage(damage);
                
            }
        }


    }

    public IEnumerator Reload()
    {
         reloadcount++;
        if (isReloading) yield break;
        isReloading = true;
        Debug.Log("Reloading" + reloadcount + "回目");


        //if (audioSource != null && reloadSound != null) audioSource.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(reloadTime);
        Debug.Log("Reloading" + reloadcount + "回目完了");
        currentAmmo = maxAmmo;
        isReloading = false;
        // UI 更新
    }

    
    public int GetCurrentAmmo() => currentAmmo;
    public int GetMaxAmmo() => maxAmmo;
    public bool IsReloading() => isReloading;
}
