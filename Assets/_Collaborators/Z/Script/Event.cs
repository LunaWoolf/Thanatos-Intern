using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class Event : MonoBehaviour {
        public string Title;
        public string Source;
        [TextArea]
        public string Content;
        public bool DisplaySource;
        public List<EventChoice> Choices;

        // Start is called before the first frame update
        public void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {

        }
        
        public virtual bool Pass(Pair P)
        {
            return true;
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

        public Character GetSource()
        {
            return Character.Find(Source);
        }

        public string GetContent()
        {
            return Content;
        }
    }
}