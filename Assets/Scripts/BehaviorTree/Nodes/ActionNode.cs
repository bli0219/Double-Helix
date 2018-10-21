using System.Collections;
using BehaviorTree;
using System;

namespace BehaviorTree {

    public class ActionNode : ITreeNode {

        // The action to perform, passed by user when initialized
        // Needs to be defined wiht clear feedback (success, failure, running)
        Func<NodeStatus> fn;

        public ActionNode(string _name, Func<NodeStatus> _fn) {
            fn = _fn;
            Name = _name;
        }

        // TODO: Tick() continues over frames until it finishes
        // Current implementation finishes in a single frame
        // Consider coroutines
        public override NodeStatus Tick() {
            NodeStatus status = NodeStatus.Running;
            while (status == NodeStatus.Running) {
                status = fn();
            }
            return status;
        }
    }
}