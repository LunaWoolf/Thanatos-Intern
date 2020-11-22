using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class EventChoice_Corrupt : EventChoice {

        public override void Effect(Pair P, out Event AddEvent)
        {
            AddEvent = null;
            KeyBase.Main.ChangeKey("Corrupt", 1);
            if (KeyBase.Main.GetKey("Corrupt") >= 3)
            {

            }
        }
    }
}