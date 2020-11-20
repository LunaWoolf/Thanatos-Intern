using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class Pair : MonoBehaviour {
        public Character C1;
        public Character C2;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public Character GetCharacter(int Index)
        {
            if (Index == 0)
                return C1;
            else if (Index == 1)
                return C2;
            return null;
        }
    }
}