using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace THAN
{
    public class EventRenderer : MonoBehaviour {
        public bool Active;
        public bool Deciding;
        public bool Activating;
        [Space]
        public int Index;
        public Animator Anim;
        public TextMeshPro ContentText;
        public TextMeshPro AddContentText;
        public TextMeshPro NameText;
        public ChoiceRenderer CRI;
        public ChoiceRenderer CRII;
        public ChoiceRenderer CRIII;
        [Space]
        public Event CurrentEvent;
        public Event CurrentAddEvent;
        public Pair CurrentPair;

        // Start is called before the first frame update
        void Start()
        {
            if (Active)
                Activate(CurrentEvent, CurrentPair);
        }

        // Update is called once per frame
        void Update()
        {
            Render();
        }

        public void Render()
        {
            if (!GetEvent())
            {
                ContentText.text = "";
                AddContentText.text = "";
                NameText.text = "";
                CRI.Render(null);
                CRII.Render(null);
                CRIII.Render(null);
                return;
            }
            ContentText.text = GetEvent().GetContent();
            if (GetAddEvent())
                AddContentText.text = GetAddEvent().GetContent();
            else
                AddContentText.text = "";
            if (!GetEvent().GetSource() || !GetEvent().DisplaySource)
                NameText.text = "";
            else
                NameText.text = GetEvent().GetSource().GetName();
            CRI.Render(GetEvent().GetChoices()[0]);
            CRII.Render(GetEvent().GetChoices()[1]);
            if (GetAddEvent())
                CRIII.Render(GetAddEvent().GetChoices()[0]);
            else
                CRIII.Render(null);
        }

        public void Decide(int Index)
        {
            StartCoroutine(DecideBuffer(Index));
        }

        public IEnumerator DecideBuffer(int Index)
        {
            while (Activating)
                yield return 0;
            Effect(Index);
        }

        public void Effect(int Index)
        {
            if (Index == 2)
            {
                GetAddEvent().GetChoices()[Index - 2].Effect(GetSourcePair(), out Event AddEvent);
                Disable();
            }
            else
            {
                GetEvent().GetChoices()[Index].Effect(GetSourcePair(), out Event AddEvent);
                if (!AddEvent)
                    Disable();
                else
                    AddActivate(AddEvent);
            }
        }

        public void Activate(Event E, Pair P)
        {
            CurrentEvent = E;
            CurrentAddEvent = null;
            CurrentPair = P;
            StartCoroutine("ActivateIE");
        }

        public IEnumerator ActivateIE()
        {
            Anim.SetBool("Active", true);
            Activating = true;
            yield return new WaitForSeconds(1f);
            CRI.Activate(GetEvent().GetChoices()[0]);
            CRII.Activate(GetEvent().GetChoices()[1]);
            yield return new WaitForSeconds(0.25f);
            Activating = false;
        }

        public void AddActivate(Event E)
        {
            CurrentAddEvent = E;
            StartCoroutine("AddActivateIE");
        }

        public IEnumerator AddActivateIE()
        {
            Anim.SetTrigger("Add");
            Activating = true;
            CRI.Disable();
            CRII.Disable();
            yield return new WaitForSeconds(0.15f);
            CRIII.Activate(GetAddEvent().GetChoice(0));
            yield return new WaitForSeconds(0.25f);
            Activating = false;
        }

        public void Disable()
        {
            Anim.SetBool("Active", false);
            CRI.Disable();
            CRII.Disable();
            CRIII.Disable();
        }

        public Event GetEvent()
        {
            return CurrentEvent;
        }

        public Event GetAddEvent()
        {
            return CurrentAddEvent;
        }

        public Pair GetSourcePair()
        {
            return CurrentPair;
        }
    }
}