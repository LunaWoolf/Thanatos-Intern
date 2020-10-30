using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class Slot : MonoBehaviour {
        public Bound B;
        public GameObject AnimBase;
        public Character CurrentCharacter;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AssignCharacter(Character C)
        {
            CurrentCharacter = C;
            C.AssignSlot(this);
            AnimBase.SetActive(false);
        }

        public void Empty()
        {
            CurrentCharacter = null;
            AnimBase.SetActive(true);
        }

        public Character GetCharacter()
        {
            return CurrentCharacter;
        }

        public void OnMouseDown()
        {
            if (B && B.GetEvent())
                return;
            GlobalControl.Main.SlotExchange(this, Cursor.GetCursorSlot());
        }
    }
}