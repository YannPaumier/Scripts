using UnityEngine;

// Permet d'obliger l'utilisation d'un composant RigidBody
[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    // Récupéaration de la camera du joueur
    [SerializeField] private Camera cam;
    [SerializeField] private float cameraRotationLimit = 85f;

    private Vector3 velocity;
    private Vector3 rotation;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0f;

    private float m_StepCycle;
    private float m_NextStep;

    private Vector3 jumpForce;

    private Rigidbody rb;

    private PlayerSetup playerSetup;
    public AudioSource m_AudioSource;

    // Use this for initialization
    void Start () {
        playerSetup = GetComponent<PlayerSetup>();
        m_AudioSource = GetComponent<AudioSource>();
        m_StepCycle = 0f;
        m_NextStep = m_StepCycle / 2f;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
	}

    public void Move(Vector3 _Velocity) {
        velocity = _Velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    public void ApplyJump(Vector3 _jumpForce)
    {
        jumpForce = _jumpForce;
        // Gestion du saut
        if (CanJump())
        {
            rb.AddForce(jumpForce * Time.fixedDeltaTime, ForceMode.Impulse);
            PlayJumpSound();
        }
    }

    bool CanJump()
    {
        // Create Ray
        Ray ray = new Ray(transform.position, transform.up * -1);

        // Create Hit Info
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, transform.localScale.y + 0.2f))
        {
            return true;
        }

        // Nothing so return false
        return false;
    }

    public void ApplyDash()
    {
        Debug.Log("dash");
        rb.MovePosition(rb.position + velocity * 50 * Time.fixedDeltaTime);
    }
    /*
     * FixedUpdate should be used instead of Update when dealing with Rigidbody. 
     * For example when adding a force to a rigidbody, you have to apply the force every fixed frame inside FixedUpdate instead of every frame inside Update.
     */
    private void FixedUpdate() {
        PerformMovement();
        PerformRotation();
        
    }
   
    // Déplacer le joueur
    private void PerformMovement()
    {
        if (velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
            ProgressStepCycle(playerSetup.speed);
        }
        
    }

    // Rotation du joueur
    private void PerformRotation()
    {
        // Récupération de la rotation + Clamp la rotation haute et basse
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

        // Applique les changements à la camera après le Clamp
        cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    /*
     * Cycle des pas
     */
    private void ProgressStepCycle(float speed)
    {
   
        m_StepCycle += (velocity.magnitude + speed ) * Time.fixedDeltaTime;
     

        if (!(m_StepCycle > m_NextStep))
        {
            return;
        }

        m_NextStep = m_StepCycle + playerSetup.m_StepInterval;

        PlayFootStepAudio();
    }

    // Step audio
    private void PlayFootStepAudio()
    {
        
        if (!CanJump())
        {
            return;
        }
        
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, playerSetup.m_FootstepSounds.Length);
        m_AudioSource.clip = playerSetup.m_FootstepSounds[n];
        m_AudioSource.PlayOneShot(m_AudioSource.clip, 0.2f);
        // move picked sound to index 0 so it's not picked next time
        playerSetup.m_FootstepSounds[n] = playerSetup.m_FootstepSounds[0];
        playerSetup.m_FootstepSounds[0] = m_AudioSource.clip;
    }

    private void PlayJumpSound()
    {
      //  m_AudioSource.clip = playerSetup.m_JumpSound;
        //m_AudioSource.Play();
         m_AudioSource.PlayOneShot(playerSetup.m_JumpSound, 0.2f);

    }
}
