using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(Exercice1))]
public class ButtonOnEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Exercice1 theexercice = (Exercice1) target;

        if (GUILayout.Button("Linear Transformate"))
        {
            theexercice.LinearTransformateVector();
        }
    }
}




[CustomEditor(typeof(MoveScript))]
public class PlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {

        MoveScript player = (MoveScript)target;

        if (GUILayout.Button("Change"))
        {
            player.enabled = !player.enabled;
        }
        DrawDefaultInspector();

    }
}



[CustomEditor(typeof(ExercicePlan))]
public class ExercicePlaneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ExercicePlan plane = (ExercicePlan)target;

        if (GUILayout.Button("Do you"))
        {
            plane.GOGO();
        }
    }
}
