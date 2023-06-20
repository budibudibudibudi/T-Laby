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

        Animator anim;
        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
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
                    anim.Play("run");
                    agent.isStopped = false;
                    agent.speed = sprintSpeed;
                    break;
                case EnemyState.PATROL:
                    anim.Play("walk");
                    agent.isStopped = false;
                    agent.speed = normalSpeed;
                    break;
                case EnemyState.ATTACKING:
                    StartCoroutine(Attacking());
                    break;
                case EnemyState.IDLE:
                    StartCoroutine(Idle());
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
                {
                    SetState(EnemyState.ATTACKING);
                }
            }
            if (Vector3.Distance(transform.position, player.transform.position) < patrolDistance && Vector3.Distance(transform.position, player.transform.position) > seeDistance)
            {
                SetState(EnemyState.PATROL);
            }
        }
        private IEnumerator Idle()
        {
            anim.Play("roar");
            while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                agent.isStopped = true;
                yield return null;
            }
            SetState(EnemyState.PATROL);
            agent.isStopped = false;
        }
        private IEnumerator Stunned()
        {
            agent.isStopped = true;
            anim.Play("gethit");
            while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                agent.isStopped = true;
                yield return null;
            }
            SetState(EnemyState.PATROL);
            agent.isStopped = false;
        }


        private IEnumerator Attacking()
        {
            anim.Play("attack");
            agent.Move(transform.forward);
            agent.speed = 8;
            yield return new WaitForSeconds(.5f);
            agent.speed = normalSpeed;
            yield return new WaitForSeconds(.5f);
            SetState(EnemyState.HIDEN);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Player"))
            {
                Player.Instance.HealthChange(-GetDamageAmount());
            }
        }
    }
}
