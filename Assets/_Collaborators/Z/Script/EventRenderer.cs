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
        public TextMeshPro NameText;
        public ChoiceRenderer CRI;
        public ChoiceRenderer CRII;
        [Space]
        public Event CurrentEvent;
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
                NameText.text = "";
                CRI.Render(null);
                CRII.Render(null);
            }

            ContentText.text = GetEvent().GetContent();
            if (!GetEvent().GetSource())
                NameText.text = "";
            else
                NameText.text = GetEvent().GetSource().GetName();
            CRI.Render(GetEvent().GetChoices()[0]);
            CRII.Render(GetEvent().GetChoices()[1]);
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
            GetEvent().GetChoices()[Index].Effect(GetSourcePair());
            Disable();
        }

        public void Activate(Event E, Pair P)
        {
            CurrentEvent = E;
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

        public void Disable()
        {
            Anim.SetBool("Active", false);
            CRI.Disable();
            CRII.Disable();
        }

        public Event GetEvent()
        {
            return CurrentEvent;
        }

        public Pair GetSourcePair()
        {
            return CurrentPair;
        }
    }
}