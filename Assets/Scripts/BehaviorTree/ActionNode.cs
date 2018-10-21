using System.Collections;
using BehaviorTree;
using System;

namespace BehaviorTree {

    public class ActionNode : TreeNode {

        Func<NodeStatus> fn;
        string name;

        public ActionNode(string _name, Func<NodeStatus> _fn) {
            fn = _fn;
            name = _name;
        }

        public NodeStatus Tick() {
            return fn();
        }
    }
}