using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace THAN
{
    public class Character : MonoBehaviour {
        public CharacterInfo Info;
        [Space]
        public TextMeshPro VitalityText;
        public TextMeshPro PassionText;
        public TextMeshPro ReasonText;
        public Color Green;
        public Color Red;
        public Color Normal;
        [Space]
        public Slot CurrentSlot;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Render();
        }

        public void Render()
        {
            if (!GetHidden_Vitality())
            {
                VitalityText.text = GetVitality() + "";
                if (GetVitality() == 10)
                    VitalityText.color = Normal;
                else if (GetVitality() < 10)
                    VitalityText.color = Green;
                else
                    VitalityText.color = Red;
                if (GetPersist_Vitality())
                    VitalityText.text = "[" + VitalityText.text + "]";
            }
            else
            {
                VitalityText.text = "??";
                VitalityText.color = Normal;
            }

            if (!GetHidden_Passion())
            {
                PassionText.text = GetPassion() + "";
                if (GetPassion() == 10)
                    PassionText.color = Normal;
                else if (GetPassion() < 10)
                    PassionText.color = Green;
                else
                    PassionText.color = Red;
                if (GetPersist_Passion())
                    PassionText.text = "[" + PassionText.text + "]";
            }
            else
            {
                PassionText.text = "??";
                PassionText.color = Normal;
            }

            if (!GetHidden_Reason())
            {
                ReasonText.text = GetReason() + "";
                if (GetReason() == 10)
                    ReasonText.color = Normal;
                else if (GetReason() < 10)
                    ReasonText.color = Green;
                else
                    ReasonText.color = Red;
                if (GetPersist_Reason())
                    ReasonText.text = "[" + ReasonText.text + "]";
            }
            else
            {
                ReasonText.text = "??";
                ReasonText.color = Normal;
            }
        }

        public void AssignSlot(Slot S)
        {
            transform.parent = S.transform;
            transform.localPosition = new Vector3();

            CurrentSlot = S;
        }

        public void BoundValueChange(float V, float P, float R)
        {
            if (V > GetVitality() + 1)
            {
                if (GetPersist_Vitality())
                    ChangeVitality(Random.Range(0, 2));
                else
                    ChangeVitality(1);
            }
            else if (V < GetVitality() - 1)
            {
                if (GetPersist_Vitality())
                    ChangeVitality(Random.Range(0, -2));
                else
                    ChangeVitality(-1);
            }
            else if (GetHidden_Vitality())
            {
                SetHidden_Vitality(false);
            }

            if (P > GetPassion() + 1)
            {
                if (GetPersist_Passion())
                    ChangePassion(Random.Range(0, 2));
                else
                    ChangePassion(1);
            }
            else if (P < GetPassion() - 1)
            {
                if (GetPersist_Passion())
                    ChangePassion(Random.Range(0, -2));
                else
                    ChangePassion(-1);
            }
            else if (GetHidden_Passion())
            {
                SetHidden_Passion(false);
            }

            if (R > GetReason() + 1)
            {
                if (GetPersist_Reason())
                    ChangeReason(Random.Range(0, 2));
                else
                    ChangeReason(1);
            }
            else if (R < GetReason() - 1)
            {
                if (GetPersist_Reason())
                    ChangeReason(Random.Range(0, -2));
                else
                    ChangeReason(-1);
            }
            else if (GetHidden_Reason())
            {
                SetHidden_Reason(false);
            }
        }

        public bool CanDie()
        {
            return GetVitality() <= 10 && GetPassion() <= 10 && GetReason() <= 10;
        }

        public void IniStat(float V, float P, float R)
        {
            SetVitality(V);
            SetPassion(P);
            SetReason(R);
        }

        //-------------------------------------------------------------------------------------------------------------

        public void ChangeVitality(float Value)
        {
            float a = Info.Vitality + Value;
            if (a < 1)
                a = 1;
            SetVitality(a);
        }

        public void SetVitality(float Value)
        {
            Info.SetVitality(Value);
        }

        public void ChangePassion(float Value)
        {
            float a = Info.Passion + Value;
            if (a < 1)
                a = 1;
            SetPassion(a);
        }

        public void SetPassion(float Value)
        {
            Info.SetPassion(Value);
        }

        public void ChangeReason(float Value)
        {
            float a = Info.Reason + Value;
            if (a < 1)
                a = 1;
            SetReason(a);
        }

        public void SetReason(float Value)
        {
            Info.SetReason(Value);
        }

        public void SetHidden_Vitality(bool Value)
        {
            Info.SetHidden_Vitality(Value);
        }

        public void SetHidden_Passion(bool Value)
        {
            Info.SetHidden_Passion(Value);
        }

        public void SetHidden_Reason(bool Value)
        {
            Info.SetHidden_Reason(Value);
        }

        public void SetPersist_Vitality(bool Value)
        {
            Info.SetPersist_Vitality(Value);
        }

        public void SetPersist_Passion(bool Value)
        {
            Info.SetPersist_Passion(Value);
        }

        public void SetPersist_Reason(bool Value)
        {
            Info.SetPersist_Reason(Value);
        }

        public float GetVitality()
        {
            return Info.Vitality;
        }

        public float GetPassion()
        {
            return Info.Passion;
        }

        public float GetReason()
        {
            return Info.Reason;
        }

        public bool GetHidden_Vitality()
        {
            return Info.Hidden_Vitality;
        }

        public bool GetHidden_Passion()
        {
            return Info.Hidden_Passion;
        }

        public bool GetHidden_Reason()
        {
            return Info.Hidden_Reason;
        }

        public bool GetPersist_Vitality()
        {
            return Info.Persist_Vitality;
        }

        public bool GetPersist_Passion()
        {
            return Info.Persist_Passion;
        }

        public bool GetPersist_Reason()
        {
            return Info.Persist_Reason;
        }
    }
}