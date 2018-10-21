using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class SuccessRepeaterNode : IDecoratorNode {


        public SuccessRepeaterNode (string name, ITreeNode child) {
            Name = name;
            Child = child;
        }

        public override NodeStatus Tick() {
            NodeStatus success = NodeStatus.Success;
            while (success == NodeStatus.Success) {
                success = Child.Tick();
            }
            return NodeStatus.Failure;
        }
    }
}