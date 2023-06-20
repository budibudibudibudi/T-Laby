using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UWAK.AUDIO;
using UWAK.GAME.PLAYER;

namespace UWAK.ITEM
{
    public abstract class Item : MonoBehaviour
    {
        public NamaItem itemName;
        public bool rareItem;
        public bool isInventoryItem;
        public Sprite Icon;
        
        protected AudioSource source;
        [SerializeField] protected AudioClip useSfx;
        [SerializeField] protected AudioClip addItemSfx;

        private void OnEnable()
        {
            source = GetComponent<AudioSource>();
            if(source == null)
                source = gameObject.AddComponent<AudioSource>();
        }
        public virtual void Use()
        {
            source?.PlayOneShot(useSfx);
        }
        public virtual void Use(bool value)
        {
            source?.PlayOneShot(useSfx);
        }
        public virtual void AddToInventory()
        {
            source?.PlayOneShot(addItemSfx);
            InventoryManager.Instance.AddItem(this, 1);
        }
        public abstract Senter GetSenter();
    }
    public enum NamaItem
    {
        KUNCI,
        PINTU,
        KOPI,
        NASIBUNGKUS,
        ESTEH,
        BATERAI,
        SENTER
    }
}
