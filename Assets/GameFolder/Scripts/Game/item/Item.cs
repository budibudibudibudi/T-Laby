using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UWAK.AUDIO;

namespace UWAK.ITEM
{
    public class Item : MonoBehaviour
    {
        protected AudioSource source;
        [SerializeField] protected AudioClip sfx;

        private void OnEnable()
        {
            source = GetComponent<AudioSource>();
            if(source == null)
                source = gameObject.AddComponent<AudioSource>();
        }
        public virtual void Use()
        {
            Debug.Log("used");
        }
        public virtual void Use(bool value)
        {
            Debug.Log("Used");
        }
    }
}
