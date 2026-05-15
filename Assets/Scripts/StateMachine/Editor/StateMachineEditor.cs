using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FiniteStateMachine))]
public class StateMachineEditor : Editor
{
    public bool showFoldout;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        FiniteStateMachine fsm = (FiniteStateMachine)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");
        if (fsm.stateMachine == null) return;
        if (fsm.stateMachine.CurrentState != null)
        {
            EditorGUILayout.LabelField("Current State: ", fsm.stateMachine.CurrentState.ToString());
        }

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Availiable States");

        if (showFoldout)
        {
            if (fsm.stateMachine.dictionaryStates != null)
            {
                var keys = fsm.stateMachine.dictionaryStates.Keys.ToArray();
                var values = fsm.stateMachine.dictionaryStates.Values.ToArray();

                for (int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField($"{keys[i]} :: {values[i]}");
                }
            }
        }
    }
}
