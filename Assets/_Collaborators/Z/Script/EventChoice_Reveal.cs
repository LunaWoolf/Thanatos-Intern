using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class EventChoice_Reveal : EventChoice {

        public override bool Pass(Bound B)
        {
            Character I = B.S1.GetCharacter();
            Character II = B.S2.GetCharacter();

            if (I.Hidden.x == 0 && I.Hidden.y == 0 && I.Hidden.z == 0
                && II.Hidden.x == 0 && II.Hidden.y == 0 && II.Hidden.z == 0)
                return false;
            return true;
        }

        public override void Effect(Bound B)
        {
            Character I = B.S1.GetCharacter();
            Character II = B.S2.GetCharacter();

            I.Hidden = new Vector3();
            II.Hidden = new Vector3();
        }
    }
}