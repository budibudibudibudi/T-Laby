using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UWAK.AUDIO;

namespace UWAK.ITEM
{
    public class Item : MonoBehaviour
    {
        public NamaItem itemName;
        public bool isStackAble;
        public bool isInventoryItem;
        
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
            source.PlayOneShot(sfx);
            Debug.Log("used");
        }
        public virtual void Use(bool value)
        {
            source.PlayOneShot(sfx);
            Debug.Log("Used");
        }
    }
    public enum NamaItem
    {
        CAPSULE,
        KUNCI,
        KEJU,
        PINTU
    }
}
