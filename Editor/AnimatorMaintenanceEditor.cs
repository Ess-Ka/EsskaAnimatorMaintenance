using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace Esska.AnimatorMaintenance.Editor {

    [CustomEditor(typeof(AnimatorMaintenance))]
    public class AnimatorMainteneanceEditor : UnityEditor.Editor {

        static bool confirmBackup;

        AnimatorMaintenance maintenance;

        void OnEnable() {
            maintenance = (AnimatorMaintenance)target;
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            if (!maintenance.isActiveAndEnabled)
                return;

            GUILayout.Space(10f);

            if (Application.isPlaying) {
                EditorGUILayout.HelpBox("Controls are disabled during play mode", MessageType.Info, true);
                return;
            }

            if (maintenance.controller == null) {
                EditorGUILayout.HelpBox("Please select an Animator Controller", MessageType.Info, true);
                return;
            }

            RenderGUI((AnimatorController)maintenance.controller);
        }

        public static void RenderGUI(AnimatorController controller) {

            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("List Content By Type"))
                    ListContentByType(controller);

                if (GUILayout.Button("List Unused Content"))
                    AnalyzeRemoveContent(controller);
            }
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Show All Content"))
                    ShowContent(controller);

                if (GUILayout.Button("Show Unused Content"))
                    ShowContent(controller, true);

                if (GUILayout.Button("Hide Content"))
                    HideContent(controller);
            }
            GUILayout.EndHorizontal();

            GUILayout.Space(10f);

            confirmBackup = GUILayout.Toggle(confirmBackup, "I confirm, I've made a BACKUP of the controller!");

            GUILayout.Space(5f);

            GUI.enabled = confirmBackup;

            if (GUILayout.Button("Remove Unused Content")) {
                AnalyzeRemoveContent(controller, true);
                confirmBackup = false;
            }

            GUI.enabled = true;
        }

        public static void ListContentByType(AnimatorController controller) {
            Object[] objects = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(controller));
            Dictionary<System.Type, int> results = new Dictionary<System.Type, int>();
            int nullCount = 0;

            Debug.Log(string.Format("<b>List of content by type ({0}):</b>", controller.name));

            foreach (var item in objects) {

                if (item != null) {
                    System.Type type = item.GetType();

                    if (!results.ContainsKey(type))
                        results.Add(type, 0);

                    results[type]++;
                }
                else {
                    nullCount++;
                }
            }

            foreach (var item in results) {
                Debug.Log(string.Format("{0}: {1}", item.Key, item.Value));
            }

            if (nullCount > 0)
                Debug.Log(string.Format("Null: {0}", nullCount));

            Debug.Log(string.Format("<b>Total count: {0}</b>", objects.Length));
        }

        public static void AnalyzeRemoveContent(AnimatorController controller, bool removeUnused = false) {
            Object[] objects = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(controller));
            string suffix = removeUnused ? "removed" : "not used";
            int counter = 0;

            if (!removeUnused)
                Debug.Log(string.Format("<b>List of unused content ({0}):</b>", controller.name));
            else
                Debug.Log(string.Format("<b>Remove unused content ({0}):</b>", controller.name));

            foreach (var item in objects) {

                if (item != null) {

                    if (item is AnimatorStateMachine) {
                        AnimatorStateMachine stateMachine = (AnimatorStateMachine)item;

                        if (!controller.IsStateMachineUsed(stateMachine)) {

                            if (removeUnused)
                                AssetDatabase.RemoveObjectFromAsset(item);

                            Debug.Log(string.Format("StateMachine '{0}' {1}", stateMachine.name, suffix));
                            counter++;
                        }
                    }
                    else if (item is AnimatorState) {
                        AnimatorState state = (AnimatorState)item;

                        if (!controller.IsStateUsed(state)) {

                            if (removeUnused)
                                AssetDatabase.RemoveObjectFromAsset(item);

                            Debug.Log(string.Format("State '{0}' {1}", state.name, suffix));
                            counter++;
                        }

                    }
                    else if (item is BlendTree) {
                        BlendTree blendTree = (BlendTree)item;

                        if (!controller.IsBlendTreeUsed(blendTree)) {

                            if (removeUnused)
                                AssetDatabase.RemoveObjectFromAsset(item);

                            Debug.Log(string.Format("BlendTree '{0}' {1}", blendTree.name, suffix));
                            counter++;
                        }

                    }
                    else if (item is AnimatorStateTransition) {
                        AnimatorStateTransition transition = (AnimatorStateTransition)item;

                        if (!controller.IsStateTransitionUsed(transition)) {

                            if (removeUnused)
                                AssetDatabase.RemoveObjectFromAsset(item);

                            Debug.Log(string.Format("StateTransition {0}", suffix));
                            counter++;
                        }

                    }
                    else if (item is AnimatorTransition) {
                        AnimatorTransition transition = (AnimatorTransition)item;

                        if (!((transition.destinationStateMachine != null && controller.IsStateMachineUsed(transition.destinationStateMachine)) || (transition.destinationState != null && controller.IsStateUsed(transition.destinationState)))) {

                            if (removeUnused)
                                AssetDatabase.RemoveObjectFromAsset(item);

                            Debug.Log(string.Format("Transition {0}", suffix));
                            counter++;
                        }

                    }
                    else if (item is StateMachineBehaviour) {
                        StateMachineBehaviour behaviour = (StateMachineBehaviour)item;

                        if (!controller.IsStateMachineBehaviourUsed(behaviour)) {

                            if (removeUnused)
                                AssetDatabase.RemoveObjectFromAsset(item);

                            Debug.Log(string.Format("StateMachineBehavior {0} {1}", item.GetType(), suffix));
                            counter++;
                        }

                    }
                }
            }

            if (removeUnused)
                AssetDatabase.SaveAssets();

            if (!removeUnused)
                Debug.Log(string.Format("<b>Found {0} unused objects</b>", counter));
            else
                Debug.Log(string.Format("<b>Removed {0} unused objects</b>", counter));
        }

        public static void ShowContent(AnimatorController controller, bool showUnusedObjectsOnly = false) {
            Object[] objects = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(controller));

            foreach (var item in objects) {

                if (item != null && !(item is AnimatorController)) {
                    bool show = !showUnusedObjectsOnly;

                    if (showUnusedObjectsOnly) {

                        if (item is AnimatorStateMachine && !controller.IsStateMachineUsed((AnimatorStateMachine)item))
                            show = true;
                        else if (item is AnimatorState && !controller.IsStateUsed((AnimatorState)item))
                            show = true;
                        else if (item is BlendTree && !controller.IsBlendTreeUsed((BlendTree)item))
                            show = true;
                        else if (item is AnimatorStateTransition && !controller.IsStateTransitionUsed((AnimatorStateTransition)item))
                            show = true;
                        else if (item is AnimatorTransition) {
                            AnimatorTransition transition = (AnimatorTransition)item;

                            if (!((transition.destinationStateMachine != null && controller.IsStateMachineUsed(transition.destinationStateMachine)) || (transition.destinationState != null && controller.IsStateUsed(transition.destinationState))))
                                show = true;
                        }
                        else if (item is StateMachineBehaviour && !controller.IsStateMachineBehaviourUsed((StateMachineBehaviour)item))
                            show = true;
                    }

                    if (show)
                        item.hideFlags = HideFlags.None;
                    else if (showUnusedObjectsOnly)
                        item.hideFlags = HideFlags.HideInHierarchy;

                    EditorUtility.SetDirty(item);
                }
            }

            AssetDatabase.SaveAssets();
            EditorGUIUtility.PingObject(controller);
        }

        public static void HideContent(AnimatorController controller) {
            Object[] objects = AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GetAssetPath(controller));

            foreach (var item in objects) {

                if (item != null && !(item is AnimatorController)) {
                    item.hideFlags = HideFlags.HideInHierarchy;
                    EditorUtility.SetDirty(item);
                }
            }

            AssetDatabase.SaveAssets();
        }
    }
}