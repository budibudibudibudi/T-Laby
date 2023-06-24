using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UWAK.GAME.ENEMY
{
    public class EnemiesEyes : Enemy
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Senter"))
            {
                if(GetState()!= EnemyState.STUNTED)
                    SetState(EnemyState.STUNTED);
            }
        }
    }


}