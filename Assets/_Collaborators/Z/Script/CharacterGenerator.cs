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
            else if (Seed == 1) //败家子
            {
                C.IniStat(22, 6, 6);
            }
            else if (Seed == 2) //女巫
            {
                C.IniStat(7, 13, 2);
            }
            else if (Seed == 3) //邪恶小女孩
            {
                C.IniStat(8, 4, 20);
            }
            else if (Seed == 4) //贵族
            {
                C.IniStat(5, 19, 8);
                C.SetHidden_Reason(true);
            }
            else if (Seed == 5) //夫人
            {
                C.IniStat(2, 8, 7);
                C.SetHidden_Reason(true);
            }
            else if (Seed == 5) //工具人
            {
                C.IniStat(12, 10, 6);
                C.SetHidden_Vitality(true);
            }
            else if (Seed == 6) //小男孩
            {
                C.IniStat(8, 2, 4);
            }
            else if (Seed == 6) //小女孩
            {
                C.IniStat(12, 18, 16);
            }
            else if (Seed == 7) //神秘人
            {
                C.IniStat(14, 3, 13);
                C.SetHidden_Vitality(true);
                C.SetHidden_Passion(true);
                C.SetHidden_Reason(true);
            }
            else if (Seed == 8) //那个人
            {
                C.IniStat(1, 8, 24);
                C.SetHidden_Reason(true);
            }
            else if (Seed == 10)
            {
                C.IniStat(30, 28, 2);
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
                int Sum = Random.Range(44, 45);
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