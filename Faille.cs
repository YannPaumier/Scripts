using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faille : MonoBehaviour {


    [SerializeField] private GameObject[] enemys;
    //[SerializeField] private GameObject failleFX;
    // Use this for initialization
    void Start () {
        Debug.Log("Lancement d'une faille");
        if(enemys.Length > 0)
        {
           // Vector3 Vector3 = new Vector3(-2.8f, 0.1f, -76f);
           // GameObject.Instantiate(failleFX, Vector3, transform.rotation);

            GameObject.Instantiate(enemys[0], transform.position, transform.rotation);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
