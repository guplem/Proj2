using UnityEngine;
using System;
using UnityEditor;

[CustomEditor(typeof(StressEmitter))]
public class StressCustomGUI : Editor
{

    public override void OnInspectorGUI()
    {
        var myScript = target as StressEmitter;

        myScript.emitStress = GUILayout.Toggle(myScript.emitStress, "Emit Stress");   

        using (new EditorGUI.DisabledScope(!myScript.emitStress))
        {
            myScript.stressAmountPerSecond = EditorGUILayout.FloatField("Amount of Stress per second", myScript.stressAmountPerSecond);
            myScript.stressPerSecondDelay = EditorGUILayout.FloatField("Delay between stress", myScript.stressPerSecondDelay);
        }
    }
}
