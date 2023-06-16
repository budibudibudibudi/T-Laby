using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UWAK.GAME.ENEMY
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] EnemyState State = EnemyState.HIDEN;
        public delegate void OnEnemyStateChange(EnemyState newstate);
        public OnEnemyStateChange onEnemyStateChange;
        public EnemyState GetState() { return State; }
        public virtual void SetState(EnemyState newstate) { 
            State = newstate;
            onEnemyStateChange?.Invoke(State);
        }

        [SerializeField] private int Damage;
        public int GetDamageAmount() { return Damage; }
        public static Enemy Instance { get; private set; }

        private void Awake()
        {
            if (!Instance)
                Instance = this;
            else
                Destroy(gameObject);
        }
    }


}