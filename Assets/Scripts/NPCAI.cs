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


    public GameObject bullet;
    private void Awake()
    {
        _agent= GetComponent<NavMeshObstacle>();
    }
    private void Update()
    {
        enemyInAttackRange = Physics.CheckSphere(transform.position, attackRange, enemy);
        Vector3 targetPos = _enemy.position;
        targetPos.y = transform.position.y;

        if (enemyInAttackRange)
        {
            AttackEnemy(targetPos);
        }
    }
    private void AttackEnemy(Vector3 targetpos)
    {
        transform.LookAt(targetpos);
        if(!alreadyAttacked)
        {
            Rigidbody rb=Instantiate(bullet, bulletRange.position, Quaternion.identity).GetComponent<Rigidbody>();
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
