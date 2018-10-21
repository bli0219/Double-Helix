using System.Collections;
using BehaviorTree;

namespace BehaviorTree {
    using func = System.Func<NodeStatus>;

    public class ActionNode : TreeNode {

        private func fn;

        public ActionNode(func _fn) {
            fn = _fn;
        }

        public NodeStatus Tick() {
            return fn();
        }
    }

}