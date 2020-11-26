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
        public Slot SacrificeSlot;
        public List<int> SacrificeTimes;
        [Space]
        public EndTurnButton EndTurnAnim;
        public SacrificeSlot SacrificeAnim;
        public Animator BoardShadeAnim;
        public bool BoardActive;
        public bool IndividualEventActive;
        public bool TownEventActive;
        public List<Character> MaskedCharacters;
        public List<Character> ChangedCharacters;
        public List<Pair> MaskedPairs;
        [Space]
        public EventRenderer IER;
        public EventRenderer TER;
        [Space]
        public List<string> StartCharacters;
        public List<TownEvent> TownEvents;
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
            foreach (string s in StartCharacters)
            {
                Character C = Character.Find(s);
                GetNextSlot().AssignCharacter(C);
                C.Active = true;
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            StartCharacterIni();
            if (GetSacrificeActive())
                SacrificeAnim.Active();
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

            if (GetSacrificeActive())
                yield return SacrificeProcess();

            PreGenerateEvent(out List<Character> NextList);
            PreGenerateTownEvent(out Event NextTownEvent);
            if (!NextTownEvent && NextList.Count <= 0)
            {
                EndTurnAnim.Next(1f);
                while (EndTurnAnim.Animating)
                    yield return 0;
            }
            else if (NextTownEvent)
            {
                EndTurnAnim.Next(0.33f);
                while (EndTurnAnim.Animating)
                    yield return 0;
                yield return TownEventProcess(NextTownEvent);
                if (NextList.Count <= 0)
                {
                    EndTurnAnim.Next(0.67f);
                    while (EndTurnAnim.Animating)
                        yield return 0;
                }
                else
                {
                    EndTurnAnim.Next(0.33f);
                    while (EndTurnAnim.Animating)
                        yield return 0;
                    yield return IndividualEventProcess(NextList);
                    EndTurnAnim.Next(0.34f);
                    while (EndTurnAnim.Animating)
                        yield return 0;
                }
            }
            else
            {
                EndTurnAnim.Next(0.66f);
                while (EndTurnAnim.Animating)
                    yield return 0;
                yield return IndividualEventProcess(NextList);
                EndTurnAnim.Next(0.34f);
                while (EndTurnAnim.Animating)
                    yield return 0;
            }
            CurrentTime++;
            BoardActive = true;
            if (GetSacrificeActive())
                SacrificeAnim.Active();
            foreach (Character C in Characters)
                C.EndOfTurn();
            foreach (Character C in Characters)
            {
                if (!C.Active && C.StartTime <= CurrentTime)
                {
                    GetNextSlot().AssignCharacter(C);
                    C.Active = true;
                }
            }
        }

        public IEnumerator SacrificeProcess()
        {
            SacrificeSlot.GetCharacter().Death();
            SacrificeAnim.Disable();
            yield return new WaitForSeconds(1f);
        }

        public IEnumerator TownEventProcess(Event NextTownEvent)
        {
            yield return GenerateTownEvent(NextTownEvent);
            if (TownEventActive)
            {
                while (TownEventActive)
                    yield return 0;
                StartCoroutine("DisableBoardShade");
            }
            //yield return new WaitForSeconds(0.8f);
        }

        public IEnumerator IndividualEventProcess(List<Character> Characters)
        {
            yield return GenerateEvent(Characters);
            if (IndividualEventActive)
            {
                while (IndividualEventActive)
                    yield return 0;
                StartCoroutine("DisableBoardShade");
            }
            //yield return new WaitForSeconds(0.8f);
        }

        public bool CanEndTurn()
        {
            return GetBoardActive() && !HoldingCharacter && (!GetSacrificeActive() || SacrificeSlot.GetCharacter());
        }

        public void PreGenerateTownEvent(out Event NE)
        {
            Event E = null;
            foreach (TownEvent TE in TownEvents)
            {
                if (TE.CanTrigger())
                    E = TE;
            }
            NE = E;
        }

        public IEnumerator GenerateTownEvent(Event E)
        {
            BoardShadeAnim.SetBool("Active", true);
            yield return 0;
            //yield return new WaitForSeconds(0.5f);
            TownEventActive = true;
            TER.Activate(E, null);
        }

        public void PreGenerateEvent(out List<Character> NL)
        {
            List<Character> Cs = new List<Character>();
            foreach (Character c in Characters)
                if (c.GetEvent())
                    Cs.Add(c);
            NL = Cs;
        }

        public IEnumerator GenerateEvent(List<Character> Cs)
        {
            Character C = Cs[Random.Range(0, Cs.Count)];
            Event E = C.GetEvent();
            C.OnTriggerEvent(E);

            BoardShadeAnim.SetBool("Active", true);
            if (E.FreeSources.Count == 0)
            {
                Pair P = C.GetPair();
                P.ActivateMask();
                if ((P.GetCharacter(0).CurrentSlot.GetPosition().x < P.GetCharacter(1).CurrentSlot.GetPosition().x && P.GetCharacter(0).CurrentSlot.ERPosition.x < 0)
                    || (P.GetCharacter(0).CurrentSlot.GetPosition().x > P.GetCharacter(1).CurrentSlot.GetPosition().x && P.GetCharacter(0).CurrentSlot.ERPosition.x > 0))
                {
                    Vector2 a = P.GetCharacter(0).CurrentSlot.GetPosition() + P.GetCharacter(0).CurrentSlot.ERPosition;
                    IER.transform.position = new Vector3(a.x, a.y, IER.transform.position.z);
                }
                else if ((P.GetCharacter(0).CurrentSlot.GetPosition().x > P.GetCharacter(1).CurrentSlot.GetPosition().x && P.GetCharacter(1).CurrentSlot.ERPosition.x < 0)
                    || (P.GetCharacter(0).CurrentSlot.GetPosition().x < P.GetCharacter(1).CurrentSlot.GetPosition().x && P.GetCharacter(1).CurrentSlot.ERPosition.x > 0))
                {
                    Vector2 a = P.GetCharacter(1).CurrentSlot.GetPosition() + P.GetCharacter(1).CurrentSlot.ERPosition;
                    IER.transform.position = new Vector3(a.x, a.y, IER.transform.position.z);
                }
                else if (P.GetCharacter(0).CurrentSlot.GetPosition().x > P.GetCharacter(1).CurrentSlot.GetPosition().x)
                {
                    Vector2 a = P.GetCharacter(0).CurrentSlot.ERPosition;
                    a.x = -a.x;
                    Vector2 b = P.GetCharacter(0).CurrentSlot.GetPosition();
                    IER.transform.position = new Vector3(a.x + b.x, a.y + b.y, IER.transform.position.z);
                }
                else
                {
                    Vector2 a = P.GetCharacter(1).CurrentSlot.ERPosition;
                    a.x = -a.x;
                    Vector2 b = P.GetCharacter(1).CurrentSlot.GetPosition();
                    IER.transform.position = new Vector3(a.x + b.x, a.y + b.y, IER.transform.position.z);
                }
                //yield return new WaitForSeconds(0.5f);
            }
            else
            {
                Slot S = Character.Find(E.FreeSources[0]).CurrentSlot;
                Vector2 a = S.ERPosition + S.GetPosition();
                IER.transform.position = new Vector3(a.x, 2, IER.transform.position.z);
                for (int i = 0; i < E.FreeSources.Count; i++)
                {
                    Character c = Character.Find(E.FreeSources[i]);
                    ChangedCharacters.Add(c);
                    c.ActivateMask();
                    c.Highlighted = true;
                }
                yield return new WaitForSeconds(0.5f);
                for (int i = 0; i < ChangedCharacters.Count; i++)
                {
                    ChangedCharacters[i].PositionChange(new Vector3(S.GetPosition().x, IER.AttachPositions[i], ChangedCharacters[i].transform.position.z));
                }
            }
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

        public IEnumerator DisableBoardShade()
        {
            BoardShadeAnim.SetBool("Active", false);
            foreach (Character C in ChangedCharacters)
                C.PositionChange(C.CurrentSlot.GetPosition());
            yield return new WaitForSeconds(0.6f);
            foreach (Character C in MaskedCharacters)
                C.DisableMask();
            foreach (Pair P in MaskedPairs)
                P.DisableMask();
            foreach (Character C in ChangedCharacters)
                C.Highlighted = false;
            MaskedCharacters.Clear();
            MaskedPairs.Clear();
            ChangedCharacters.Clear();
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
                    if (S == SacrificeSlot)
                        continue;
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
                    if (a && S && !S.GetCharacter())
                        return S;
                }
            }
            return GetNextSlot();
        }

        public Slot GetSlot(int x, int y)
        {
            if (y >= Grid.Count || y < 0)
                return null;
            if (x >= Grid[y].Count || x < 0)
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

        public bool GetSacrificeActive()
        {
            return SacrificeTimes.Contains(CurrentTime);
        }
    }
}