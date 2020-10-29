using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class CharacterGenerator : MonoBehaviour {
        public List<Vector2> SumPool;
        public List<Vector2> ValuePool;
        public float HiddenRate;
        public float PersistRate;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GenerateValue(Character C)
        {
            Vector3Int Temp = new Vector3Int();
            List<int> SP = new List<int>();
            List<int> Indexs = new List<int>();
            Indexs.Add(0);
            Indexs.Add(1);
            Indexs.Add(2);
            foreach (Vector2 V in SumPool)
            {
                for (int i = 0; i < V.y; i++)
                    SP.Add((int)V.x);
            }
            int Sum = SP[Random.Range(0, SP.Count)];
            for (int i = 0; i < 3; i++)
            {
                int Index = Indexs[Random.Range(0, Indexs.Count)];
                for (int j = Indexs.Count - 1; j >= 0; j--)
                {
                    if (Indexs[j] == Index)
                        Indexs.RemoveAt(j);
                }
                int F = GenerateInidividualStat(Sum);
                bool Hidden = Random.Range(0.01f, 0.99f) < HiddenRate;
                bool Persist = Random.Range(0.01f, 0.99f) < PersistRate;
                if (Index == 0)
                {
                    Temp.x = F;
                    if (Hidden)
                        C.Hidden.x = 1;
                    if (Persist)
                        C.Persist.x = 1;
                }
                else if (Index == 1)
                {
                    Temp.y = F;
                    if (Hidden)
                        C.Hidden.y = 1;
                    if (Persist)
                        C.Persist.y = 1;
                }
                else if (Index == 2)
                {
                    Temp.z = F;
                    if (Hidden)
                        C.Hidden.z = 1;
                    if (Persist)
                        C.Persist.z = 1;
                }
                Sum -= F;
                if (Sum <= 0)
                    Sum++;
            }
            C.IniStat(Temp.x, Temp.y, Temp.z);
        }

        public int GenerateInidividualStat(int Sum)
        {
            List<int> SP = new List<int>();
            foreach (Vector2 V in ValuePool)
            {
                if (V.x <= Sum)
                {
                    for (int i = 0; i < V.y; i++)
                        SP.Add((int)V.x);
                }
            }
            return SP[Random.Range(0, SP.Count)];
        }
    }
}