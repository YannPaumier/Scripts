using UnityEngine;

// Obliger l'utilisation de mon script Player Motor
[RequireComponent(typeof(PlayerMotor)), 
 RequireComponent(typeof(PlayerSetup)),
 RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour {

    private PlayerMotor motor;
    private PlayerSetup playerSetup;
    private Player player;
    private GameManager gameManager;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        motor = GetComponent<PlayerMotor>();
        playerSetup = GetComponent<PlayerSetup>();
        player = GetComponent<Player>();
    }
	
	// Update is called once per frame
	void Update () {
        /* 
         *  On va calculer la vélocité du mouvement du joueur en un vecteur 3D
         *  -1 = gauche, 0 = bouge pas, 1 = droite
         */
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");
        
        Vector3 _movHorizontal = transform.right * _xMov; // (0, 0, 0)
        Vector3 _movVertical = transform.forward * _zMov;

        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * playerSetup.speed;

        motor.Move(_velocity);
      
        /*
         *  Calcul de la rotation du joueur en un vecteur 3D
         */
        float _yRot = Input.GetAxisRaw("Mouse X"); // Rotation autour des Y
        Vector3 _rotation = new Vector3(0, _yRot, 0) * playerSetup.sensibility;

        motor.Rotate(_rotation);

        /*
         *  Calcul du mouvement de la caméra (haut et bas)
         */
        float _xRot = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRot * playerSetup.sensibility;

        motor.RotateCamera(_cameraRotationX);

        /*
         * Jump
         */
        Vector3 _jumpForce = Vector3.zero;

        if (Input.GetButtonDown("Jump"))
        {
            _jumpForce = Vector3.up * playerSetup.jumpForce;
            motor.ApplyJump(_jumpForce);
        }

        /*
         * Dash
         */
        if (Input.GetButtonDown("Fire2"))
        {
            //_jumpForce = Vector3.up * playerSetup.jumpForce;
           // motor.ApplyDash();
        }

        /*
         * Switch weapon
         */
        if (Input.GetKeyDown("1"))
        {
            player.SwitchWeapon(1);
            //_jumpForce = Vector3.up * playerSetup.jumpForce;
            // motor.ApplyDash();
        }
        if (Input.GetKeyDown("2"))
        {
            player.SwitchWeapon(2);
            //_jumpForce = Vector3.up * playerSetup.jumpForce;
            // motor.ApplyDash();
        }
        if (Input.GetKeyDown("3"))
        {
            player.SwitchWeapon(3);
            //_jumpForce = Vector3.up * playerSetup.jumpForce;
            // motor.ApplyDash();
        }

        if (Input.GetKeyDown("m"))
        {
            gameManager.switchMod();
        }

    }
}
