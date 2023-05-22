using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UWAK.ITEM
{
    public class Senter :Item
    {
        [SerializeField] GameObject Light;
        [SerializeField] private float bateraiValue;
        [SerializeField] private float maxBateraiValue;
        public float GetMaxBateraiValue() { return maxBateraiValue; }
        public float GetBateraiValue() { return bateraiValue; }
        public void AddBateraiValue(float amount) { Mathf.Clamp(bateraiValue += amount, 0, maxBateraiValue); }

        [SerializeField] private bool State;
        public bool GetState() { return State; }

        [SerializeField] Image bateraiSlider;

        public override void Use(bool state)
        {
            base.Use();
            State = state;
            if (bateraiValue > 0)
               Light.SetActive(State);
        }
        private void Update()
        {
            if(State)
            {
                bateraiValue -= 1 * Time.deltaTime;
            }
            if(bateraiValue <= 0)
            {
                bateraiValue = 0;
                State = false;
                Light.SetActive(State);
            }
            bateraiSlider.fillAmount = bateraiValue / maxBateraiValue;
        }
        public override Senter GetSenter()
        {
            return this;
        }
    }
}