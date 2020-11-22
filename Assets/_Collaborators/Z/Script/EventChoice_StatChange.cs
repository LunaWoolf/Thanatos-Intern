using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class EventChoice_StatChange : EventChoice {
        public Vector3Int ChangeA;
        public Vector3Int ChangeB;

        public override bool Pass(Pair P)
        {
            Character I = P.GetCharacter(0);
            Character II = P.GetCharacter(1);

            if (ChangeA.x != 0 && I.GetHidden_Vitality())
                return false;
            if (ChangeA.y != 0 && I.GetHidden_Passion())
                return false;
            if (ChangeA.z != 0 && I.GetHidden_Reason())
                return false;
            if (ChangeB.x != 0 && II.GetHidden_Vitality())
                return false;
            if (ChangeB.y != 0 && II.GetHidden_Passion())
                return false;
            if (ChangeB.z != 0 && II.GetHidden_Reason())
                return false;
            return true;
        }

        public override void Effect(Pair P, out Event AddEvent)
        {
            AddEvent = null;

            Character I = P.GetCharacter(0);
            Character II = P.GetCharacter(1);

            I.ChangeVitality(ChangeA.x);
            I.ChangePassion(ChangeA.y);
            I.ChangeReason(ChangeA.z);
            II.ChangeVitality(ChangeB.x);
            II.ChangePassion(ChangeB.y);
            II.ChangeReason(ChangeB.z);
        }
    }
}