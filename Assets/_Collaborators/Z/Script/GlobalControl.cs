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
        public List<TownEvent> TownEvents;
        public bool BoardActive;
        public bool IndividualEventActive;
        public bool TownEventActive;
        [Space]
        public EventRenderer IER;
        public EventRenderer TER;
        [Space]
        public List<Character> StartCharacters;
        [Space]
        public List<Character> Characters;
        public List<Pair> Pairs;
        [Space]
        public GameObject PairPrefab;
        [Space]
        public int VitalityLimit = 10;
        public int PassionLimit = 10;
        public int ReasonLimit = 10;

        public void Awake()
        {
            Grid = new List<List<Slot>>();
        }

        public void StartCharacterIni()
        {
            foreach (Character C in StartCharacters)
                GetNextSlot().AssignCharacter(C);
        }

        // Start is called before the first frame update
        void Start()
        {
            StartCharacterIni();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && CanEndTurn())
                EndOfTurn();
        }

        public void EndOfTurn()
        {
            SelectingSlot = null;
            StartCoroutine("EndOfTurnIE");
        }

        public IEnumerator EndOfTurnIE()
        {
            BoardActive = false;
            IndividualEventActive = false;
            TownEventActive = false;
            foreach (Pair P in Pairs)
                P.Effect();
            float a = 0;

            yield return new WaitForSeconds(1.2f);

            yield return GenerateTownEvent();
            while (a < 0.4f || TownEventActive)
            {
                a += Time.deltaTime;
                yield return 0;
            }

            yield return new WaitForSeconds(1.2f);

            yield return GenerateEvent();
            while (a < 0.4f || IndividualEventActive)
            {
                a += Time.deltaTime;
                yield return 0;
            }

            yield return new WaitForSeconds(1.6f);

            CurrentTime++;
            BoardActive = true;
        }

        public bool CanEndTurn()
        {
            return GetBoardActive() && !HoldingCharacter;
        }

        public IEnumerator GenerateTownEvent()
        {
            Event E = null;
            foreach (TownEvent TE in TownEvents)
            {
                if (TE.CanTrigger())
                    E = TE;
            }
            if (!E)
                yield break;
            TownEventActive = true;
            TER.Activate(E, null);
        }

        public IEnumerator GenerateEvent()
        {
            List<Character> Cs = new List<Character>();
            foreach (Character c in Characters)
                if (c.GetEvent())
                    Cs.Add(c);
            if (Cs.Count <= 0)
                yield break;
            Character C = Cs[Random.Range(0, Cs.Count)];
            Event E = C.GetEvent();
            C.OnTriggerEvent(E);
            IndividualEventActive = true;
            IER.Activate(E, C.GetPair());
        }

        public void ResolveEvent(int Index)
        {
            if (Index == 0)
                TownEventActive = false;
            else if (Index == 1)
                IndividualEventActive = false;
        }

        public void AddPair(Pair P)
        {
            P.C1.CurrentPair = P;
            P.C2.CurrentPair = P;
            Pairs.Add(P);
        }

        public void RemovePair(Pair P)
        {
            P.C1.CurrentPair = null;
            P.C2.CurrentPair = null;
            Pairs.Remove(P);
            P.gameObject.SetActive(false);
            Destroy(P.gameObject, 3f);
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

        public Slot GetNextSlot(Slot Ori)
        {
            if (!Ori)
                GetNextSlot();
            bool a = false;
            foreach (List<Slot> L in Grid)
            {
                foreach (Slot S in L)
                {
                    if (S == Ori)
                        a = true;
                    if (a && !S.GetCharacter())
                        return S;
                }
            }
            return GetNextSlot();
        }

        public Slot GetSlot(int x, int y)
        {
            if (y >= Grid.Count)
                return null;
            if (x >= Grid[y].Count)
                return null;
            return Grid[y][x];
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