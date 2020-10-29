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
            if (Index == 1)
            {
                if (!B.EC1)
                    Text.text = "";
                else
                    Text.text = B.EC1.Content;
            }
            else if (Index == 2)
            {
                if (!B.EC2)
                    Text.text = "";
                else
                    Text.text = B.EC2.Content;
            }
        }

        public void OnMouseDown()
        {
            if ((Index == 1 && !B.EC1) || (Index == 2 && !B.EC2))
                return;
            if (Index == 1)
                B.EC1.Effect(B);
            else if (Index == 2)
                B.EC2.Effect(B);
            B.EmptyEvent();
        }
    }
}