using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class Bark_SpecialPair : Bark {
        [Space]
        public string SpecialKey;
        public int CurrentSpecialBarkIndex;
        public List<string> SpecialBarks;

        public override void Effect()
        {
            if (C.GetPair() && C.GetPartner().GetName() == SpecialKey)
            {
                CurrentSpecialBarkIndex++;
                if (CurrentSpecialBarkIndex >= Barks.Count)
                    CurrentSpecialBarkIndex = 0;
                // Actual Bark Effect
            }
            else
            {
                CurrentBarkIndex++;
                if (CurrentBarkIndex >= Barks.Count)
                    CurrentBarkIndex = 0;
                // Actual Bark Effect
            }
        }
    }
}