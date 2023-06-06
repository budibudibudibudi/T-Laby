using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UWAK.GAME.PLAYER;

namespace UWAK.GAME.ENEMY
{
    public class EnemyController : MonoBehaviour
    {
        NavMeshAgent agent;
        [SerializeField] private float normalSpeed, sprintSpeed;
        [SerializeField] private float seeDistance, attackDistance,patrolDistance;
        [SerializeField] Transform player;
        public float space = 0.7f;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            Enemy.Instance.onEnemyStateChange += OnEnemyStateChange;
        }

        private void OnDisable()
        {
            Enemy.Instance.onEnemyStateChange -= OnEnemyStateChange;

        }
        private void OnEnemyStateChange(EnemyState newstate)
        {
            switch (newstate)
            {
                case EnemyState.HIDEN:
                    gameObject.SetActive(false);
                    break;
                case EnemyState.STUNTED:
                    StartCoroutine(Stunned());
                    break;
                case EnemyState.SHOWED:
                    Enemy.Instance.SetState(EnemyState.PATROL);
                    break;
                case EnemyState.NULL:
                    break;
                case EnemyState.CHASE:
                    agent.speed = sprintSpeed;
                    agent.SetDestination(player.position);
                    break;
                case EnemyState.PATROL:
                    agent.speed = normalSpeed;
                    agent.SetDestination(player.position);
                    break;
                case EnemyState.ATTACKING:
                    break;
                default:
                    break;
            }
        }

        private void Update()
        {
            var front = transform.forward;
            var dirToPlayer = (player.position - transform.position).normalized;


            var dot = Vector3.Dot(front, dirToPlayer);

            if (dot > space && Vector3.Distance(transform.position, player.transform.position) < seeDistance)
            {
                Enemy.Instance.SetState(EnemyState.CHASE);
            }
            if (dot > space && Vector3.Distance(transform.position, player.transform.position) < attackDistance)
            {
                if (Enemy.Instance.GetState().Equals(EnemyState.CHASE))
                    StartCoroutine(Attacking());
            }
            if (Vector3.Distance(transform.position, player.transform.position) < patrolDistance && Vector3.Distance(transform.position, player.transform.position) > seeDistance)
            {
                Enemy.Instance.SetState(EnemyState.PATROL);
            }
            if (Vector3.Distance(transform.position, player.transform.position) > patrolDistance)
            {
                Enemy.Instance.SetState(EnemyState.HIDEN);
            }
        }
        private IEnumerator Stunned()
        {
            agent.isStopped = true;
            yield return new WaitForSeconds(3);
            Enemy.Instance.SetState(EnemyState.PATROL);
        }


        private IEnumerator Attacking()
        {
            print("jalan");
            Enemy.Instance.SetState(EnemyState.NULL);
            //animasi nyerang
            agent.Move(transform.forward);
            agent.speed = 20;
            yield return new WaitForSeconds(.5f);
            agent.speed = normalSpeed;
            Enemy.Instance.SetState(EnemyState.STUNTED);
        }
    }
}
