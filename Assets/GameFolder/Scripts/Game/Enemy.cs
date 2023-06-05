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
        public void SetState(EnemyState newstate) { State = newstate; }

        public static Enemy Instance { get; private set; }

        private void Awake()
        {
            if (!Instance)
                Instance = this;
            else
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);
        }
    }


}