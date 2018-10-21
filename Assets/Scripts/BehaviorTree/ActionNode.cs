using System.Collections;
using BehaviorTree;
using System;

namespace BehaviorTree {

    public class ActionNode : ITreeNode {

        Func<NodeStatus> fn;

        public ActionNode(string _name, Func<NodeStatus> _fn) {
            fn = _fn;
            Name = _name;
        }

        public override NodeStatus Tick() {
            NodeStatus status = NodeStatus.Running;
            while (status == NodeStatus.Running) {
                status = fn();
            }
            return status;
        }
    }
}