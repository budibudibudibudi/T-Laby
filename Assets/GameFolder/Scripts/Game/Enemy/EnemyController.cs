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
        [SerializeField] private float seeDistance, attackDistance, patrolDistance;
        [SerializeField] Transform player;
        public float space = 0.7f;


        Animator anim;
        private void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            gameObject.SetActive(false);
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
                case EnemyState.PATROL:
                    anim.Play("walk");
                    agent.isStopped = false;
                    agent.speed = normalSpeed;
                    StartCoroutine(Patrolling());
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


        private IEnumerator Patrolling()
        {
            bool playerInArea = CheckPlayerInArea();
            if(playerInArea)
            {
                agent.SetDestination(player.transform.position);
            }
            while (agent.pathStatus == NavMeshPathStatus.PathComplete)
            {
                playerInArea = CheckPlayerInArea();
                if (playerInArea)
                {
                    agent.SetDestination(player.transform.position);
                }
                else
                {
                    SetState(EnemyState.HIDEN);
                }
                yield return null;
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
            agent.isStopped = false;
            SetState(EnemyState.PATROL);
        }
        private IEnumerator Stunned()
        {
            agent.isStopped = true;
            anim.Play("gethit");
            while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
            {
                yield return null;
            }
            bool cekArea = CheckPlayerInArea();
            if (cekArea)
                SetState(EnemyState.PATROL);
            else
                SetState(EnemyState.HIDEN);
        }

        private bool CheckPlayerInArea()
        {
            return Vector3.Distance(transform.position, player.transform.position) < patrolDistance;
        }
        private IEnumerator Attacking()
        {
            anim.Play("attack");
            agent.Move(transform.forward);
            agent.speed = 8;
            yield return new WaitForSeconds(.5f);
            agent.speed = normalSpeed;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                print("hited");
                Player.Instance.HealthChange(-GetDamageAmount());
                GameManager.Instance.ChangeState(GameState.GAMERESUME);
                SetState(EnemyState.HIDEN);
            }
        }
    }
}
