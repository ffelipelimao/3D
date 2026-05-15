using UnityEditor;
using System.Linq;
using UnityEngine;


[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GameManager gm = (GameManager)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");
        if (gm.stateMachine == null) return;
        if (gm.stateMachine.CurrentState != null)
        {
            EditorGUILayout.LabelField("Current State: ", gm.stateMachine.CurrentState.ToString());
        }

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Availiable States");

        if (showFoldout)
        {
            if (gm.stateMachine.dictionaryStates != null)
            {
                var keys = gm.stateMachine.dictionaryStates.Keys.ToArray();
                var values = gm.stateMachine.dictionaryStates.Values.ToArray();

                for (int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField($"{keys[i]} :: {values[i]}");
                }
            }
        }
    }
}
