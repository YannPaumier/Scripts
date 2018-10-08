using UnityEngine;
using System.Collections; // Permet l'utilisation de coroutine

public class Player : MonoBehaviour {

    [SerializeField] private int maxHealth = 100;

    [SerializeField] public GameObject[] weapons;

    // Sounds
    private AudioSource m_AudioSource;
    [SerializeField] public AudioClip deadSound;
    [SerializeField] public AudioClip hitSound;
    [SerializeField] public AudioClip[] randomSounds;

    // Selected Weapon
    private int selectedWeapon = 1;

    private int currentHealth;

    /* 
     * GETTER & SETTERS
     */
    public int getHealth
    {
        get { return currentHealth; }
    }

    public GameObject getWeapon
    {
        get { return weapons[selectedWeapon]; }
    }

    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    public void Setup()
    {
        SetDefaults();
    }

    private void Update()
    { 
    }

    public void SwitchWeapon(int _position)
    {
        selectedWeapon = _position -1;
        showWeapon();
    }

    public void TakeDamage(int _amout)
    {
        if (isDead)
        {
            return;
        }

        m_AudioSource.PlayOneShot(hitSound, 0.9f);

        currentHealth -= _amout;

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    
    private void Die()
    {
        isDead = true;

        GameObject player = GameObject.Find("Player");
        Destroy(player, 0);

        Debug.Log(transform.name + " est mort.");

        // Respawn
        StartCoroutine(Respawn());

    }
    private IEnumerator Respawn() // Coroutines
    {
        yield return new WaitForSeconds(5f); // Attendre X secondes
        //SetDefaults();

        Debug.Log(transform.name + " a respawn.");
    }


    public void SetDefaults()
    {
        isDead = false;
        m_AudioSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        showWeapon();
    }

    public void showWeapon()
    {
        // Si on a une arme, la détruit
        if (GameObject.Find("Camera/Weapon/Equiped"))
        {
            Destroy(GameObject.Find("Camera/Weapon/Equiped"), 0);
        }
        
        if(weapons.Length <= 0)
        {
            return;
        }
        /// Cré l'objet weapon
        GameObject WeaponSlot1 = Instantiate(weapons[selectedWeapon]);
        WeaponSlot1.name = "Equiped";
 
        //Attacher l'arme à la caméra
        WeaponSlot1.transform.parent = GameObject.Find("Camera/Weapon").transform;
        //Set de la position de l'arme
        WeaponSlot1.transform.localPosition = new Vector3(0, 0, 0);
        //set de la rotation de l'arme
        WeaponSlot1.transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, 0, 0);
    }
}
