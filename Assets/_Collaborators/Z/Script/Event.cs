using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class Event : MonoBehaviour {
        public string Title;
        public List<EventChoice> Choices;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public List<EventChoice> GetChoices()
        {
            return Choices;
        }

        public EventChoice GetChoice(int Index)
        {
            if (Choices.Count <= Index)
                return null;
            return Choices[Index];
        }

        public string GetTitle()
        {
            return Title;
        }
    }
}