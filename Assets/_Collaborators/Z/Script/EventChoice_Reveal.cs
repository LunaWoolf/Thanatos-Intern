using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class EventChoice_Reveal : EventChoice {

        public override bool Pass(Pair P)
        {
            Character I = P.GetCharacter(0);
            Character II = P.GetCharacter(1);

            if (!I.GetHidden_Vitality() && !I.GetHidden_Passion() && !I.GetHidden_Reason()
                && !II.GetHidden_Vitality() && !II.GetHidden_Passion() && !II.GetHidden_Reason())
                return false;
            return true;
        }

        public override void Effect(Pair P, out Event AddEvent)
        {
            AddEvent = null;

            Character I = P.GetCharacter(0);
            Character II = P.GetCharacter(1);

            List<int> Hids = new List<int>();
            if (I.GetHidden_Vitality())
                Hids.Add(1);
            if (I.GetHidden_Passion())
                Hids.Add(2);
            if (I.GetHidden_Reason())
                Hids.Add(3);
            if (II.GetHidden_Vitality())
                Hids.Add(4);
            if (II.GetHidden_Passion())
                Hids.Add(5);
            if (II.GetHidden_Reason())
                Hids.Add(6);

            int asd = Hids[Random.Range(0, Hids.Count)];

            if (asd == 1)
                I.SetHidden_Vitality(false);
            else if (asd == 2)
                I.SetHidden_Passion(false);
            else if (asd == 3)
                I.SetHidden_Reason(false);
            else if (asd == 4)
                II.SetHidden_Vitality(false);
            else if (asd == 5)
                II.SetHidden_Passion(false);
            else if (asd == 6)
                II.SetHidden_Reason(false);
        }
    }
}