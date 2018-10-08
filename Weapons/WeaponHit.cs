using UnityEngine;
using System.Collections;

public class WeaponHit : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("l'arme touche : " + col.gameObject.name);
    }
}