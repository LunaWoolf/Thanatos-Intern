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
        public List<List<Slot>> Grid;
        public List<Slot> Slots;
        public Slot SelectingSlot;
        public Character HoldingCharacter;
        [Space]
        public List<Event> Events;
        public bool BoardActive;
        public bool IndividualEventActive;
        public bool TownEventActive;
        [Space]
        public EventRenderer IER;
        public EventRenderer TER;
        [Space]
        public int DeathTime;
        public TextMeshPro DeathTimeText;
        [Space]
        public int NewCharacterTime;
        public int NewCharacterIndex = 6;
        public GameObject CharacterPrefab;
        public CharacterGenerator Generator;
        [Space]
        public List<Character> Characters;
        public List<Pair> Pairs;
        [Space]
        public int VitalityLimit = 10;
        public int PassionLimit = 10;
        public int ReasonLimit = 10;

        public void Awake()
        {
            Grid = new List<List<Slot>>();
        }

        // Start is called before the first frame update
        void Start()
        {
            for (int i = 1; i < 6; i++)
                NewCharacter(i);
        }

        // Update is called once per frame
        void Update()
        {
            DeathTimeText.text = "- " + DeathTime + " -";
        }

        public void EndOfTurn()
        {
            StartCoroutine("EndOfTurnIE");
        }

        public IEnumerator EndOfTurnIE()
        {
            IndividualEventActive = false;
            TownEventActive = false;
            /*foreach (Bound B in Bounds)
                B.Effect();*/
            float a = 0;
            yield return new WaitForSeconds(1.6f);
            // TownEvent
            yield return new WaitForSeconds(1.2f);
            GenerateEvent();
            while (a < 0.4f || IndividualEventActive)
            {
                a += Time.deltaTime;
                yield return 0;
            }
            
            CurrentTime++;
            DeathTime--;
            NewCharacterTime--;

            /*if (DeathSlot.GetCharacter())
            {
                Character C = DeathSlot.GetCharacter();
                if (C.CanDie())
                {
                    Destroy(C.gameObject);
                    DeathSlot.Empty();
                    DeathTime = 6;
                }
            }*/
            /*if (NewCharacterTime <= 0)
            {
                NewCharacterTime = 6;
                NewCharacter(NewCharacterIndex);
                NewCharacterIndex++;
            }*/
        }

        public void GenerateEvent()
        {
            List<Character> Cs = new List<Character>();
            foreach (Character c in Characters)
                if (c.GetEvent())
                    Cs.Add(c);
            if (Cs.Count <= 0)
                return;
            Character C = Cs[Random.Range(0, Cs.Count)];
            Event E = C.GetEvent();
            C.OnTriggerEvent(E);
            IndividualEventActive = true;
            IER.Activate(E, C.GetPair());
        }

        public void GenerateEvent_Legacy()
        {
            /*List<Bound> Bs = new List<Bound>();
            foreach (Bound Bo in Bounds)
            {
                if (Bo.S1.GetCharacter() && Bo.S2.GetCharacter())
                    Bs.Add(Bo);
            }
            if (Bs.Count <= 0)
                return;
            Bound B = Bs[Random.Range(0, Bs.Count)];

            B.E = Events[Random.Range(0, Events.Count)];*/
        }

        public void ResolveEvent(int Index)
        {
            if (Index == 0)
                TownEventActive = false;
            else if (Index == 1)
                IndividualEventActive = false;
        }

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
            foreach (List<Slot> L in Grid)
            {
                foreach (Slot S in L)
                {
                    if (!S.GetCharacter())
                        return S;
                }
            }
            return null;
        }

        public void NewCharacter(int Seed)
        {
            if (Seed > 10)
                return;
            if (!GetNextSlot())
                return;
            GameObject G = Instantiate(CharacterPrefab);
            Character C = G.GetComponent<Character>();
            Generator.GenerateValue(C, Seed);
            GetNextSlot().AssignCharacter(C);
        }

        public void StartCharacters()
        {
            List<int> Stats = Generator.GenerateStartStats();

            List<bool> HiddenStats = new List<bool>();
            for (int asd = 0; asd < 15; asd++)
                HiddenStats.Add(false);
            int a = Random.Range(0, HiddenStats.Count);
            int b = a;
            while (b == a)
                b = Random.Range(0, HiddenStats.Count);
            HiddenStats[a] = false;
            HiddenStats[b] = false;

            for (int i = 0; i < 5; i++)
            {
                GameObject G = Instantiate(CharacterPrefab);
                Character C = G.GetComponent<Character>();
                C.IniStat(Stats[i * 3], Stats[i * 3] + 1, Stats[i * 3 + 2]);
                C.SetHidden_Vitality(HiddenStats[i * 3]);
                C.SetHidden_Passion(HiddenStats[i * 3 + 1]);
                C.SetHidden_Reason(HiddenStats[i * 3 + 2]);
                GetNextSlot().AssignCharacter(C);
            }
        }

        public void AddSlot(Slot S)
        {
            Slots.Add(S);
            for (int y = Grid.Count; y <= S.Position.y; y++)
                Grid.Add(new List<Slot>());
            for (int x = Grid[S.Position.y].Count; x <= S.Position.x; x++)
                Grid[S.Position.y].Add(null);
            Grid[S.Position.y][S.Position.x] = S;
        }

        public Character GetSelectingCharacter()
        {
            if (!SelectingSlot)
                return null;
            return SelectingSlot.GetCharacter();
        }

        public float GetVitalityLimit()
        {
            return VitalityLimit;
        }

        public float GetPassionLimit()
        {
            return PassionLimit;
        }

        public float GetReasonLimit()
        {
            return ReasonLimit;
        }

        public bool GetBoardActive()
        {
            return BoardActive;
        }
    }
}