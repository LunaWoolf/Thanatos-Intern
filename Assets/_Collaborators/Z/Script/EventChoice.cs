using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class EventChoice : MonoBehaviour {
        public string Content;

        public virtual bool Pass(Bound B)
        {
            return true;
        }

        public virtual void Effect(Bound B)
        {

        }
    }
}