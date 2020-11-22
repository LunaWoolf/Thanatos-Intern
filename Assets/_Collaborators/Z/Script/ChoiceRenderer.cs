using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace THAN
{
    public class ChoiceRenderer : MonoBehaviour {
        public bool Active;
        public bool MouseOn;
        [Space]
        public int Index;
        public EventRenderer ER;
        public Animator Anim;
        public Collider C2D;
        public TextMeshPro EmptyText;
        public TextMeshPro SelectingText;

        // Start is called before the first frame update
        void Start()
        {
            if (Active)
                Activate(null);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void Render(EventChoice EC)
        {
            if (!EC)
            {
                EmptyText.text = "";
                SelectingText.text = "";
                return;
            }

            EmptyText.text = EC.GetContent();
            SelectingText.text = EC.GetContent();
        }

        public void Activate(EventChoice EC)
        {
            Render(EC);
            Anim.SetBool("Active", true);
            C2D.enabled = true;
            Active = true;
        }

        public void Disable()
        {
            if (!Active)
                return;
            Anim.SetBool("Active", false);
            C2D.enabled = false;
            OnMouseExit();
            Active = false;
        }

        public void OnMouseEnter()
        {
            MouseOn = true;
            Anim.SetBool("Selecting", true);
        }

        public void OnMouseExit()
        {
            MouseOn = false;
            Anim.SetBool("Selecting", false);
        }

        public void OnMouseDown()
        {
            if (Active)
                ER.Decide(Index);
        }
    }
}