using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace THAN
{
    public class Character : MonoBehaviour {
        public float Vitality;
        public float Passion;
        public float Reason;
        public Vector3 Hidden;
        public Vector3 Persist;
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
            if (Hidden.x == 0)
            {
                VitalityText.text = GetVitality() + "";
                if (GetVitality() == 10)
                    VitalityText.color = Normal;
                else if (GetVitality() < 10)
                    VitalityText.color = Green;
                else
                    VitalityText.color = Red;
                if (Persist.x == 1)
                    VitalityText.text = "[" + VitalityText.text + "]";
            }
            else
            {
                VitalityText.text = "??";
                VitalityText.color = Normal;
            }

            if (Hidden.y == 0)
            {
                PassionText.text = GetPassion() + "";
                if (GetPassion() == 10)
                    PassionText.color = Normal;
                else if (GetPassion() < 10)
                    PassionText.color = Green;
                else
                    PassionText.color = Red;
                if (Persist.y == 1)
                    PassionText.text = "[" + PassionText.text + "]";
            }
            else
            {
                PassionText.text = "??";
                PassionText.color = Normal;
            }

            if (Hidden.z == 0)
            {
                ReasonText.text = GetReason() + "";
                if (GetReason() == 10)
                    ReasonText.color = Normal;
                else if (GetReason() < 10)
                    ReasonText.color = Green;
                else
                    ReasonText.color = Red;
                if (Persist.z == 1)
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
                if (Persist.x == 1)
                    ChangeVitality(Random.Range(0, 2));
                else
                    ChangeVitality(1);
            }
            else if (V < GetVitality() - 1)
            {
                if (Persist.x == 1)
                    ChangeVitality(Random.Range(0, -2));
                else
                    ChangeVitality(-1);
            }
            else if (Hidden.x == 1)
            {
                Hidden.x = 0;
            }

            if (P > GetPassion() + 1)
            {
                if (Persist.y == 1)
                    ChangePassion(Random.Range(0, 2));
                else
                    ChangePassion(1);
            }
            else if (P < GetPassion() - 1)
            {
                if (Persist.y == 1)
                    ChangePassion(Random.Range(0, -2));
                else
                    ChangePassion(-1);
            }
            else if (Hidden.y == 1)
            {
                Hidden.y = 0;
            }

            if (R > GetReason() + 1)
            {
                if (Persist.z == 1)
                    ChangeReason(Random.Range(0, 2));
                else
                    ChangeReason(1);
            }
            else if (R < GetReason() - 1)
            {
                if (Persist.z == 1)
                    ChangeReason(Random.Range(0, -2));
                else
                    ChangeReason(-1);
            }
            else if (Hidden.z == 1)
            {
                Hidden.z = 0;
            }
        }

        public void IniStat(float V, float P, float R)
        {
            Vitality = V;
            Passion = P;
            Reason = R;
        }

        public void ChangeVitality(float Value)
        {
            Vitality += Value;
        }

        public float GetVitality()
        {
            return Vitality;
        }

        public void ChangePassion(float Value)
        {
            Passion += Value;
        }

        public float GetPassion()
        {
            return Passion;
        }

        public void ChangeReason(float Value)
        {
            Reason += Value;
        }

        public float GetReason()
        {
            return Reason;
        }
    }
}