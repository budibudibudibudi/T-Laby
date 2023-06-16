using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UWAK.GAME.PLAYER;

namespace UWAK.GAME.ENEMY
{
    public class EnemyController : Enemy
    {
        NavMeshAgent agent;
        [SerializeField] private float normalSpeed, sprintSpeed;
        [SerializeField] private float seeDistance, attackDistance,patrolDistance;
        [SerializeField] Transform player;
        public float space = 0.7f;

        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {

        }
        public override void SetState(EnemyState newstate)
        {
            base.SetState(newstate);
            switch (newstate)
            {
                case EnemyState.HIDEN:
                    gameObject.SetActive(false);
                    break;
                case EnemyState.STUNTED:
                    StartCoroutine(Stunned());
                    break;
                case EnemyState.SHOWED:
                    SetState(EnemyState.PATROL);
                    break;
                case EnemyState.NULL:
                    break;
                case EnemyState.CHASE:
                    agent.speed = sprintSpeed;
                    break;
                case EnemyState.PATROL:
                    agent.speed = normalSpeed;
                    break;
                case EnemyState.ATTACKING:
                    break;
                default:
                    break;
            }
        }

        private void Update()
        {
            agent.SetDestination(player.position);
            var front = transform.forward;
            var dirToPlayer = (player.position - transform.position).normalized;


            var dot = Vector3.Dot(front, dirToPlayer);

            if (dot > space && Vector3.Distance(transform.position, player.transform.position) < seeDistance)
            {
                SetState(EnemyState.CHASE);
            }
            if (dot > space && Vector3.Distance(transform.position, player.transform.position) < attackDistance)
            {
                if (GetState().Equals(EnemyState.CHASE))
                    StartCoroutine(Attacking());
            }
            if (Vector3.Distance(transform.position, player.transform.position) < patrolDistance && Vector3.Distance(transform.position, player.transform.position) > seeDistance)
            {
                SetState(EnemyState.PATROL);
            }
        }
        private IEnumerator Stunned()
        {
            agent.isStopped = true;
            yield return new WaitForSeconds(3);
            SetState(EnemyState.PATROL);
        }


        private IEnumerator Attacking()
        {
            print("jalan");
            SetState(EnemyState.NULL);
            //animasi nyerang
            agent.Move(transform.forward);
            agent.speed = 20;
            yield return new WaitForSeconds(.5f);
            agent.speed = normalSpeed;
            SetState(EnemyState.STUNTED);
            yield return new WaitForSeconds(1);
            SetState(EnemyState.HIDEN);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                Player.Instance.HealthChange(GetDamageAmount());
            }
        }
    }
}
