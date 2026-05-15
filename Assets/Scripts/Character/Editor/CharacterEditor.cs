using UnityEditor;
using System.Linq;
using UnityEngine;

[CustomEditor(typeof(Character))]
public class CharacterEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Character ch = (Character)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");
        if (ch.stateMachine == null) return;
        if (ch.stateMachine.CurrentState != null)
        {
            EditorGUILayout.LabelField("Current State: ", ch.stateMachine.CurrentState.ToString());
        }

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Availiable States");

        if (showFoldout)
        {
            if (ch.stateMachine.dictionaryStates != null)
            {
                var keys = ch.stateMachine.dictionaryStates.Keys.ToArray();
                var values = ch.stateMachine.dictionaryStates.Values.ToArray();

                for (int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField($"{keys[i]} :: {values[i]}");
                }
            }
        }
    }
}
