using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour {

    [SerializeField] private RectTransform healthFill;

    [SerializeField] private Player player;

    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        setHealthAmout(player.getHealth / 100f);
    }

    void setHealthAmout(float _amout)
    {
        healthFill.localScale = new Vector3(1f, _amout, 1f);
    }
}
