using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class EventChoice : MonoBehaviour {
        [TextArea]
        public string Content;

        public virtual void Effect(Pair P)
        {

        }

        public virtual bool Pass(Pair P)
        {
            return true;
        }

        public string GetContent()
        {
            return Content;
        }
    }
}