using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace THAN
{
    public class Bound : MonoBehaviour {
        public Slot S1;
        public Slot S2;
        [Space]
        public Event E;
        public GameObject EventBase;
        public TextMeshPro TitleText;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Render();
        }

        public void Render()
        {
            if (!GetEvent())
            {
                TitleText.text = "";
                EventBase.SetActive(false);
            }
            else
            {
                TitleText.text = GetEvent().GetTitle();
                EventBase.SetActive(true);
            }
        }

        public void Effect()
        {
            if (!S1.GetCharacter() || !S2.GetCharacter())
                return;
            Character C1 = S1.GetCharacter();
            float V1 = C1.GetVitality();
            float P1 = C1.GetPassion();
            float R1 = C1.GetReason();
            Character C2 = S2.GetCharacter();
            float V2 = C2.GetVitality();
            float P2 = C2.GetPassion();
            float R2 = C2.GetReason();
            C1.BoundValueChange(V2, P2, R2);
            C2.BoundValueChange(V1, P1, R1);
        }

        public void EmptyEvent()
        {
            E = null;
        }

        public Event GetEvent()
        {
            return E;
        }
    }
}