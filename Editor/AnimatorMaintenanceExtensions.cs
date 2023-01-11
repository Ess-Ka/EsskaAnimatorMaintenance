using UnityEditor.Animations;
using UnityEngine;

namespace Esska.AnimatorMaintenance {

    public static class AnimatorControllerExtensions {

        public static bool IsStateMachineUsed(this AnimatorController controller, AnimatorStateMachine findStateMachine) {

            foreach (var layer in controller.layers) {

                if (layer.stateMachine == findStateMachine)
                    return true;

                if (layer.stateMachine.IsStateMachineUsed(findStateMachine))
                    return true;
            }

            return false;
        }

        public static bool IsStateUsed(this AnimatorController controller, AnimatorState findState) {

            foreach (var layer in controller.layers) {

                if (layer.stateMachine.IsStateUsed(findState))
                    return true;
            }

            return false;
        }

        public static bool IsBlendTreeUsed(this AnimatorController controller, BlendTree findBlendTree) {

            foreach (var layer in controller.layers) {

                if (layer.stateMachine.IsBlendTreeUsed(findBlendTree))
                    return true;
            }

            return false;
        }

        public static bool IsStateTransitionUsed(this AnimatorController controller, AnimatorStateTransition findStateTransition) {

            foreach (var layer in controller.layers) {

                if (layer.stateMachine.IsStateTransitionUsed(findStateTransition))
                    return true;
            }

            return false;
        }

        public static bool IsStateMachineBehaviourUsed(this AnimatorController controller, StateMachineBehaviour findStateMachineBehaviour) {

            foreach (var layer in controller.layers) {

                if (layer.stateMachine.IsStateMachineBehaviourUsed(findStateMachineBehaviour))
                    return true;
            }

            return false;
        }
    }

    public static partial class AnimatorStateMachineExtensions {

        public static bool IsStateMachineUsed(this AnimatorStateMachine stateMachine, AnimatorStateMachine findStateMachine) {

            foreach (var subStateMachine in stateMachine.stateMachines) {

                if (subStateMachine.stateMachine == findStateMachine)
                    return true;

                if (subStateMachine.stateMachine.IsStateMachineUsed(findStateMachine))
                    return true;
            }

            return false;
        }

        public static bool IsStateUsed(this AnimatorStateMachine stateMachine, AnimatorState findState) {

            foreach (var state in stateMachine.states) {

                if (state.state == findState)
                    return true;
            }

            foreach (var subStateMachine in stateMachine.stateMachines) {

                if (subStateMachine.stateMachine.IsStateUsed(findState))
                    return true;
            }

            return false;
        }

        public static bool IsBlendTreeUsed(this AnimatorStateMachine stateMachine, BlendTree findBlendTree) {

            foreach (var state in stateMachine.states) {

                if (state.state.motion == findBlendTree)
                    return true;

                if (state.state.motion is BlendTree)
                    return ((BlendTree)state.state.motion).IsBlendTreeUsed(findBlendTree);
            }

            foreach (var subStateMachine in stateMachine.stateMachines) {

                if (subStateMachine.stateMachine.IsBlendTreeUsed(findBlendTree))
                    return true;
            }

            return false;
        }

        public static bool IsStateTransitionUsed(this AnimatorStateMachine stateMachine, AnimatorStateTransition findStateTransition) {

            foreach (var transitions in stateMachine.entryTransitions) {

                if (transitions == findStateTransition)
                    return true;
            }

            foreach (var transitions in stateMachine.anyStateTransitions) {

                if (transitions == findStateTransition)
                    return true;
            }

            foreach (var state in stateMachine.states) {

                foreach (var transitions in state.state.transitions) {

                    if (transitions == findStateTransition)
                        return true;
                }
            }

            foreach (var subStateMachine in stateMachine.stateMachines) {

                if (subStateMachine.stateMachine.IsStateTransitionUsed(findStateTransition))
                    return true;
            }

            return false;
        }

        public static bool IsStateMachineBehaviourUsed(this AnimatorStateMachine stateMachine, StateMachineBehaviour findStateMachineBehaviour) {

            foreach (var state in stateMachine.states) {

                foreach (var behaviour in state.state.behaviours) {

                    if (behaviour == findStateMachineBehaviour)
                        return true;
                }
            }

            foreach (var subStateMachine in stateMachine.stateMachines) {

                if (subStateMachine.stateMachine.IsStateMachineBehaviourUsed(findStateMachineBehaviour))
                    return true;
            }

            return false;
        }
    }

    public static partial class BlendTreeExtensions {

        public static bool IsBlendTreeUsed(this BlendTree blendTree, BlendTree findBlendTree) {

            foreach (var item in blendTree.children) {

                if (item.motion == findBlendTree)
                    return true;

                if (item.motion is BlendTree)
                    return ((BlendTree)item.motion).IsBlendTreeUsed(findBlendTree);
            }

            return false;
        }
    }

}
