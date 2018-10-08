using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanAi : MonoBehaviour
{
    // Récupération du gameManager
    private GameManager gameManager;

    //Distance entre le joueur et l'ennemi
    private float Distance;

    public string[] targetNames;

    // Cible de l'ennemi
    private Transform Target;

    public BoxCollider WeaponCollider;

    //Distance de poursuite
    public float chaseRange = 10;

    //Distance de poursuite
    public float chaseSpeed = 1;

    // Portée des attaques
    public float attackRange = 2.2f;

    // Cooldown des attaques
    public float attackRepeatTime = 2.5f;
    private float attackTime;
    public float attackDelay = 1;

    // Montant des dégâts infligés
    public int TheDammage;

    // Agent de navigation
    private UnityEngine.AI.NavMeshAgent agent;

    // Animations de l'ennemi
    private bool waitForNextAnimation = false;
    private Animation animations;

    // Vie de l'ennemi
    public float enemyHealth;
    private bool isDead = false;

    // Blood effect
    [SerializeField] GameObject bloodFx;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.speed = chaseSpeed;
        animations = gameObject.GetComponent<Animation>();
        attackTime = Time.time;
    }


    void Update()
    {

        if (!isDead)
        {
            // On cherche le joueur en permanence
            if ( FindClosestTarget(targetNames))
            {
               // Debug.Log("trouvé");
                Target = FindClosestTarget(targetNames).transform;
            }
            else
            {
               // Debug.Log("pas trouvé_");
               // idle();
                return;
            }

            // On calcule la distance entre le joueur et l'ennemi, en fonction de cette distance on effectue diverses actions
            Distance = Vector3.Distance(Target.position, transform.position);

            // Quand l'ennemi est loin = idle
            if (Distance > chaseRange)
            {
                //idle();
            }

            // Quand l'ennemi est proche mais pas assez pour attaquer
            if (Distance < chaseRange && Distance > attackRange)
            {
                chase();
            }

            // Quand l'ennemi est assez proche pour attaquer
            if (Distance < attackRange)
            {
               // attack();
            }

        }
    }

    GameObject FindClosestTarget(string[] trgt)
    {
        
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (string tag in trgt)
        {
            GameObject[] gos;
            gos = GameObject.FindGameObjectsWithTag(tag);

            foreach (GameObject go in gos)
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
        }

        return closest;
    }

    // poursuite
    void chase()
    {
        if (!waitForNextAnimation && (Time.time > attackTime))
        {
            //animations.Play("Walk");
        }
        
        agent.destination = Target.position;
    }

    // Combat
    void attack()
    {
        // empeche l'ennemi de traverser le joueur
        agent.destination = transform.position;

        //Si pas de cooldown
        if (Time.time > attackTime)
        {
            //Debug.Log("ATTACK");
           // if (!waitForNextAnimation)
            //   {
                animations.Play("Attack");
                Player player = GameObject.Find("Player").GetComponent<Player>();
                player.TakeDamage(TheDammage);
            //  }
               
           // Target.GetComponent<PlayerInventory>().ApplyDamage(TheDammage);
            //Debug.Log("L'ennemi a envoyé " + TheDammage + " points de dégâts");
            attackTime = Time.time + attackRepeatTime;
        }
    }


    // idle
    void idle()
    {
        animations.Play("Idle");
    }

    public void ApplyDammage(float TheDammage, Vector3 hitPoint)
    {
        if (!isDead)
        {
  
            StartCoroutine(WaitAnimation());
            animations.Play("Damage");
            enemyHealth = enemyHealth - TheDammage;
            print(gameObject.name + "a subit " + TheDammage + " points de dégâts.");

            GameObject.Instantiate(bloodFx, hitPoint, transform.rotation);
           
            if (enemyHealth <= 0)
            {
                Dead();
            }
        }
    }

    IEnumerator WaitAnimation()
    {
        waitForNextAnimation = true;
        yield return new WaitForSeconds(0.9f);
        waitForNextAnimation = false;
    }

    public void Dead()
    {
        isDead = true;
        animations.Play("Death");
        
        gameManager.newDeath();
        Destroy(gameObject, 5);
    }
}