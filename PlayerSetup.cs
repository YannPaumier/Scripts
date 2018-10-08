using UnityEngine;

// Obliger l'utilisation du script Player
[RequireComponent(typeof(Player))]
public class PlayerSetup : MonoBehaviour
{ // Utilisation du NetworkBehaviour

    [Header("Player Options")]
    // Permet d'afficher la variable dans l'inspector
    [SerializeField] public AudioClip[] m_FootstepSounds;   // an array of footstep sounds that will be randomly selected from.
    [SerializeField] public AudioClip m_JumpSound;           // the sound played when character leaves the ground.
    [SerializeField] public AudioClip m_LandSound;           // the sound played when character touches back on ground.
    [SerializeField] public float m_StepInterval = 2f;
    [SerializeField] public float speed = 3f;
    [SerializeField] public float sensibility = 5f;
    [SerializeField] public float jumpForce = 250f;

    // Use this for initialization
    void Start () {
        Debug.Log("Lancement du player");
        GetComponent<Player>().Setup();
	}

    // Update is called once per frame
    void Update () {
		
	}
}
