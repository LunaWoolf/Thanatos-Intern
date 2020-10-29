using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class StaticAssign : MonoBehaviour {
        public GlobalControl GC;
        public Cursor MainCursor;

        public void Awake()
        {
            GlobalControl.Main = GC;
            Cursor.Main = MainCursor;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}