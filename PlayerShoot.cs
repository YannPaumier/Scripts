using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour
{

    private GameObject weapon;
    private WeaponSetup weaponSetup;

    [SerializeField]
    private Camera cam;

    [SerializeField]
    private LayerMask mask;

 
    public AudioSource m_AudioSource;
    private Player player;

    private bool canShoot = true;

    // Bullet
    private GameObject bulletPrefab;
    private Transform bulletSpawn;

    // Use this for initialization
    void Start()
    {
        if (cam == null)
        {
            // s'il n'y a pas de camera, désactive le script
            Debug.LogError("Pas de caméra référencée.");
            this.enabled = false;
            return;
        }
       player = GetComponent<Player>();
      
       m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        weaponSetup = player.getWeapon.GetComponent<WeaponSetup>();

        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    private void Shoot()
    {

        if (!canShoot)
        {
            return;
        }

        if (weaponSetup.bullet)
        {
            Debug.Log("bullet");
            fireBullet();
        }
        else
        {
            fireLazer();
        }
        
    }

    private void fireLazer()
    {
        GameObject.Find("Camera/Weapon/Equiped").transform.GetComponent<WeaponSetup>().shoot();
   
        StartCoroutine(WaitShoot(weaponSetup.speed));
        m_AudioSource.PlayOneShot(weaponSetup.sound, 0.9f);

        RaycastHit _hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _hit, weaponSetup.range, mask))
        {

            if (_hit.collider.tag.Contains("Enemy"))
            {
                Debug.Log("hit ennemy!");
                _hit.transform.GetComponent<EnemyAi>().ApplyDammage(weaponSetup.damage, _hit.point);
            }
        }
    }

    private void fireBullet()
    {
        bulletSpawn = GameObject.Find("Camera/Weapon").transform;
      
        bulletPrefab = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        bulletPrefab.AddComponent<Rigidbody>();
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 100;
        bullet.GetComponent<MeshRenderer>().material.color = Color.blue;

        // Destroy the bullet after 2 seconds
       // Destroy(bullet, 3.0f);
    }

    IEnumerator WaitShoot(float seconds)
    {
        canShoot = false;
        yield return new WaitForSeconds(seconds);
        canShoot = true;
    }


}
