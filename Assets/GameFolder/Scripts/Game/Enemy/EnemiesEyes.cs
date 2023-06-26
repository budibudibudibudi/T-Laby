using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UWAK.GAME.ENEMY
{
    public class EnemiesEyes : MonoBehaviour
    {

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Senter"))
            {
                Enemy enemy = GetComponentInParent<Enemy>();
                if (enemy.GetState().Equals(EnemyState.PATROL)|| enemy.GetState().Equals(EnemyState.CHASE))
                    enemy.SetState(EnemyState.STUNTED);
            }
        }
    }


}