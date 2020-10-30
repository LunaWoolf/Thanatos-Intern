using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace THAN
{
    public class GlobalControl : MonoBehaviour {
        public static GlobalControl Main;
        public int CurrentTime;
        [Space]
        public List<Bound> Bounds;
        public List<Slot> IndividualSlots;
        public Slot DeathSlot;
        [Space]
        public List<EventChoice> Choices;
        public List<int> ChoiceRates;
        public List<Event> Events;
        [Space]
        public int DeathTime = 4;
        public TextMeshPro DeathTimeText;
        [Space]
        public int NewCharacterTime = 5;
        public GameObject CharacterPrefab;
        public CharacterGenerator Generator;

        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < 5; i++)
                NewCharacter(i + 1);
        }

        // Update is called once per frame
        void Update()
        {
            DeathTimeText.text = "- " + DeathTime + " -";
        }

        public void EndOfTurn()
        {
            foreach (Bound B in Bounds)
            {
                if (B.GetEvent())
                    return;
            }

            CurrentTime++;
            DeathTime--;
            NewCharacterTime--;
            foreach (Bound B in Bounds)
                B.Effect();
            if (DeathSlot.GetCharacter())
            {
                Character C = DeathSlot.GetCharacter();
                if (C.CanDie())
                {
                    Destroy(C.gameObject);
                    DeathSlot.Empty();
                    DeathTime = 6;
                }
            }
            if (NewCharacterTime <= 0)
            {
                NewCharacterTime = 7;
                NewCharacter(0);
            }
            GenerateEvent();
        }

        public void GenerateEvent()
        {
            List<Bound> Bs = new List<Bound>();
            foreach (Bound Bo in Bounds)
            {
                if (Bo.S1.GetCharacter() && Bo.S2.GetCharacter())
                    Bs.Add(Bo);
            }
            if (Bs.Count <= 0)
                return;
            Bound B = Bs[Random.Range(0, Bs.Count)];

            B.E = Events[Random.Range(0, Events.Count)];
        }

        /*public void GenerateEvent()
        {
            List<Bound> Bs = new List<Bound>();
            foreach (Bound Bo in Bounds)
            {
                if (Bo.S1.GetCharacter() && Bo.S2.GetCharacter())
                    Bs.Add(Bo);
            }
            if (Bs.Count <= 0)
                return;
            Bound B = Bs[Random.Range(0, Bs.Count)];
            List<EventChoice> ECs = new List<EventChoice>();
            for (int i = 0; i < Choices.Count; i++)
            {
                if (Choices[i].Pass(B))
                {
                    for (int j = 0; j < ChoiceRates[i]; j++)
                        ECs.Add(Choices[i]);
                }
            }
            if (ECs.Count <= 1)
                return;
            B.EC1 = ECs[Random.Range(0, ECs.Count)];
            for (int ii = ECs.Count - 1; ii >= 0; ii--)
            {
                if (ECs[ii] == B.EC1)
                    ECs.RemoveAt(ii);
            }
            if (ECs.Count <= 0)
            {
                B.EmptyEvent();
                return;
            }
            B.EC2 = ECs[Random.Range(0, ECs.Count)];
        }*/

        public bool CanEndTurn()
        {
            return true;
        }

        public void SlotExchange(Slot A, Slot B)
        {
            Character a = A.GetCharacter();
            Character b = B.GetCharacter();
            if (!a && !b)
                return;
            else if (!a)
            {
                A.AssignCharacter(b);
                B.Empty();
            }
            else if (!b)
            {
                B.AssignCharacter(a);
                A.Empty();
            }
            else
            {
                A.AssignCharacter(b);
                B.AssignCharacter(a);
            }
        }

        public Slot GetNextSlot()
        {
            for (int i = 0; i < IndividualSlots.Count; i++)
            {
                if (!IndividualSlots[i].GetCharacter())
                    return IndividualSlots[i];
            }
            return null;
        }

        public void NewCharacter(int Seed)
        {
            if (!GetNextSlot())
                return;
            GameObject G = Instantiate(CharacterPrefab);
            Character C = G.GetComponent<Character>();
            Generator.GenerateValue(C, Seed);
            GetNextSlot().AssignCharacter(C);
        }
    }
}