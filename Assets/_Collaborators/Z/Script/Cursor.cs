using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class Cursor : MonoBehaviour {
        public static Cursor Main;
        public Slot CursorSlot;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        public void Update()
        {
            Process();
        }

        public void FixedUpdate()
        {
            Process();
        }

        public void Process()
        {
            Vector3 a = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(a.x, a.y, transform.position.z);
        }

        public Vector2 GetPosition()
        {
            return transform.position;
        }

        public static Slot GetCursorSlot()
        {
            return Main.CursorSlot;
        }
    }
}