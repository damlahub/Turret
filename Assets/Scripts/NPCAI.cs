using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class NPCAI : MonoBehaviour
{

    [SerializeField] private Transform _enemy, bulletRange;

    public NavMeshObstacle _agent;
    public float attackRange;
    public bool enemyInAttackRange;
    public LayerMask enemy;
    public float timeBetweenAttacks;

    private bool alreadyAttacked;

    //Audio and Particle System
    public AudioSource audioSource;
    public ParticleSystem particle;


    public GameObject bullet;
    private void Awake()
    {
        _agent= GetComponent<NavMeshObstacle>();
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, enemy);

        if (enemyInAttackRange)
        {
            AttackEnemy();
        }
    }
    private void AttackEnemy()
    {
        Vector3 targetPos = _enemy.position;
        targetPos.y = transform.position.y;
        transform.LookAt(targetPos);
        if(!alreadyAttacked)
        {

            Rigidbody rb =Instantiate(bullet, bulletRange.position, Quaternion.identity).GetComponent<Rigidbody>();
            ParticleSystem particleInstance= Instantiate(particle, bulletRange.position, Quaternion.identity);
            particleInstance.Play();
            audioSource.Play();
            rb.AddForce(transform.forward*20,ForceMode.Impulse);
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
}
