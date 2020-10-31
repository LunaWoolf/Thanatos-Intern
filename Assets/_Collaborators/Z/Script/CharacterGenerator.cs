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

        public void GenerateValue(Character C, int Seed)
        {
            if (Seed == 0)
            {
                Vector3Int Temp = new Vector3Int();
                List<int> SP = new List<int>();
                foreach (Vector2 V in SumPool)
                {
                    for (int i = 0; i < V.y; i++)
                        SP.Add((int)V.x);
                }
                int Sum = SP[Random.Range(0, SP.Count)];
                int I = Random.Range(1, Sum);
                int II = Random.Range(1, Sum);
                if (I > II)
                {
                    int III = I;
                    I = II;
                    II = III;
                }
                Temp.x = I;
                Temp.y = II - I;
                if (Temp.y <= 0)
                    Temp.y = Random.Range(1, 4);
                Temp.z = Sum - II;
                C.IniStat(Temp.x, Temp.y, Temp.z);
            }
            else if (Seed == 1)
            {
                C.IniStat(6, 17, 3);
            }
            else if (Seed == 2)
            {
                C.IniStat(9, 9, 12);
            }
            else if (Seed == 3)
            {
                C.IniStat(15, 3, 8);
            }
            else if (Seed == 4)
            {
                C.IniStat(13, 8, 11);
            }
            else if (Seed == 5)
            {
                C.IniStat(12, 19, 1);
            }
            for (int i = 0; i < 3; i++)
            {
                int Index = i;
                bool Hidden = Random.Range(0.01f, 0.99f) < HiddenRate;
                bool Persist = Random.Range(0.01f, 0.99f) < PersistRate;
                if (Index == 0)
                {
                    if (Hidden)
                        C.SetHidden_Vitality(true);
                    if (Persist)
                        C.SetPersist_Vitality(true);
                }
                else if (Index == 1)
                {
                    if (Hidden)
                        C.SetHidden_Passion(true);
                    if (Persist)
                        C.SetPersist_Passion(true);
                }
                else if (Index == 2)
                {
                    if (Hidden)
                        C.SetHidden_Reason(true);
                    if (Persist)
                        C.SetPersist_Reason(true);
                }
            }
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

        public List<int> GenerateStartStats()
        {
            List<int> Temp = new List<int>();
            for (int b = 0; b < 15; b++)
                Temp.Add(0);
            for (int i = 0; i < 3; i++)
            {
                int Sum = Random.Range(46, 51);
                List<int> Indexs = new List<int>();
                Indexs.Add(0);
                Indexs.Add(Random.Range(1, Sum));
                Indexs.Add(Random.Range(1, Sum));
                Indexs.Add(Random.Range(1, Sum));
                Indexs.Add(Random.Range(1, Sum));
                Indexs.Add(Sum);
                bool a = true;
                while (a)
                {
                    a = false;
                    for (int j = 0; j < Indexs.Count - 1; j++)
                    {
                        if (Indexs[j] > Indexs[j + 1])
                        {
                            int asd = Indexs[j];
                            Indexs[j] = Indexs[j + 1];
                            Indexs[j + 1] = asd;
                            a = true;
                        }
                    }
                }
                List<int> TempIndex = new List<int>();
                for (int y = 0; y < 5; y++)
                    TempIndex.Add(y);
                for (int I = 0; I < 5; I++)
                {
                    int c = Random.Range(0, TempIndex.Count);
                    int x = TempIndex[c];
                    TempIndex.RemoveAt(c);
                    Temp[x * 3 + i] = Indexs[I + 1] - Indexs[I];
                    if (Temp[x * 3] <= 0)
                        Temp[x * 3] = 1;
                }
            }
            return Temp;
        }
    }
}