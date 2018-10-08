using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // private static Dictionary<string, Player> ennemies = new Dictionary<string, Player>();
    // Settings
    public bool darkMod = false;

    private int failleBuffer = 0;

    [SerializeField] public Faille faille;
    public Transform[] spwanPointFailles;
    public Transform[] spwanPointHumains;


    // Use this for initialization
    void Start () {
        Debug.Log("Starting Game...");
        //GameObject.Find("Directional Light").GetComponent<Light>().enabled = false;
        //GameObject.Find("Spot Light").GetComponent<Light>().enabled = true;
        newFaille();
    }
	
	// Update is called once per frame
	void Update () {
		if(failleBuffer >= 1)
        {
            newFaille();
        }
	}

    public void newDeath()
    {
        failleBuffer++;
    }

    private void newFaille()
    {
        failleBuffer = 0;
        GameObject.Instantiate(faille, spwanPointFailles[0].position, spwanPointFailles[0].rotation);
    }

    public void switchMod()
    {
        if (darkMod)
        {
            darkMod = false;
            GameObject.Find("Directional Light").GetComponent<Light>().enabled = true;
            GameObject.Find("Spot Light").GetComponent<Light>().enabled = false;
        }
        else
        {
            darkMod = true;
            GameObject.Find("Directional Light").GetComponent<Light>().enabled = false;
            GameObject.Find("Spot Light").GetComponent<Light>().enabled = true;
        }
    }
}
