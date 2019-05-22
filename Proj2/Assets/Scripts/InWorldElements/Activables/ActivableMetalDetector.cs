using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649
public class ActivableMetalDetector : Activable
{
    public MaterialPhysics[] detectedMaterials;
    [SerializeField] private ActivableAlarm linkedAlarm;

    public override ActivationType GetActivationType()
    {
        return ActivationType.Other;
    }

    protected override void SetState(bool state, CharacterManager characterActivating, bool alertAtActivate)
    {
        if (state)
            foreach (MaterialPhysics material in detectedMaterials)
            {
                try
                {
                    if (characterActivating != null)
                        if (material == ((PlayerManager)characterActivating).inventory.storedItem.GetComponent<MaterialWithSound>().materialPhysics)
                        {
                            //linkedAlarm.SwitchState(characterActivating);
                            linkedAlarm.ActivateAlarm();
                        }
                }
                catch (System.InvalidCastException)
                { }
                catch (System.NullReferenceException)
                { }

            }
    }
}
