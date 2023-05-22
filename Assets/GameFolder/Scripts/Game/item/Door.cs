using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UWAK.ITEM
{
    public class Door : Item
    {
        private bool state;
        public bool GetDoorState() { return state; }
        Collider col;
        Animator anim;
        void Start()
        {
            anim = GetComponent<Animator>();
            col = GetComponent<Collider>();
        }
        public override void Use(bool isOpen)
        {
            base.Use();
            state = isOpen;
            if (state)
            {
                anim.Play("Opening 1");
            }
            else
            {
                anim.Play("Closing 1");
            }
        }
        public override Senter GetSenter()
        {
            return null;
        }
        #region Animation Event
        public void ActivateCollider()
        {
            col.enabled = true;
        }
        public void DisableCollider()
        {
            col.enabled = false;
        }

        #endregion
    }
}