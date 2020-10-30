using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace THAN
{
    public class EventChoiceRenderer : MonoBehaviour {
        public Bound B;
        public int Index;
        public TextMeshPro Text;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (!GetChoice())
            {
                Text.text = "";
            }
            else
            {
                EventChoice EC = GetChoice();
                Text.text = "[" + EC.GetContent() + "]";
            }
        }

        public EventChoice GetChoice()
        {
            if (!B.GetEvent())
                return null;
            return B.GetEvent().GetChoice(Index);
        }

        public void OnMouseDown()
        {
            if (!GetChoice())
                return;
            GetChoice().Effect(B);
            B.EmptyEvent();
        }
    }
}