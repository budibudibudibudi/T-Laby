using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UWAK.ITEM
{
    public class Senter :Item
    {
        [SerializeField] GameObject Light;
        [SerializeField] private float bateraiValue;
        public float GetBateraiValue() { return bateraiValue; }
        public override void Use()
        {
            base.Use();
            if (bateraiValue > 0)
               Light.SetActive(State);
        }
        private void Update()
        {
            if(State)
            {
                bateraiValue -= 1 * Time.deltaTime;
            }
        }
    }
}