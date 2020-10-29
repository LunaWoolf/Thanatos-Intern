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

            List<int> Hids = new List<int>();
            if (I.Hidden.x == 1)
                Hids.Add(1);
            if (I.Hidden.y == 1)
                Hids.Add(2);
            if (I.Hidden.z == 1)
                Hids.Add(3);
            if (II.Hidden.x == 1)
                Hids.Add(4);
            if (II.Hidden.y == 1)
                Hids.Add(5);
            if (II.Hidden.z == 1)
                Hids.Add(6);

            int asd = Hids[Random.Range(0, Hids.Count)];

            if (asd == 1)
                I.Hidden.x = 0;
            else if (asd == 2)
                I.Hidden.y = 0;
            else if (asd == 3)
                I.Hidden.z = 0;
            else if (asd == 4)
                II.Hidden.x = 0;
            else if (asd == 5)
                II.Hidden.y = 0;
            else if (asd == 6)
                II.Hidden.z = 0;
        }
    }
}