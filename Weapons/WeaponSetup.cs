using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponSetup : MonoBehaviour
{

    [SerializeField] public AudioClip sound;
    [SerializeField] public string name = "Pompe";
    [SerializeField] public int damage = 20;
    [SerializeField] public float range = 50f;
    [SerializeField] public float speed = 1f;
    [SerializeField] public bool bullet = false;

    private ParticleSystem particule;
    
    void OnTriggerEnter(Collider other)
    {

        Destroy(gameObject);
        //other.GetComponent<Player>();
        //Destroy(other.gameObject);
    }

    private void Start()
    {
        particule = transform.Find("Particule").gameObject.GetComponent<ParticleSystem>();
    }

    public void shoot()
    {
        particule.Play();
    }
}
