using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class SuccessRepeaterNode : IDecoratorNode {

        bool started = false;

        public SuccessRepeaterNode(string name, ITreeNode child, BehaviorTree BehaviorTree) : base(name, child, BehaviorTree) { }
        public SuccessRepeaterNode(string name, BehaviorTree BehaviorTree) : base(name, BehaviorTree) { }
        public override void Tick() {

            // this condition is most frequently used, so put it first
            if (started && BehaviorTree.LastStatus == NodeStatus.Success) { 
                BehaviorTree.Path.Push(Child);
            } else if (!started) {
                started = true;
                BehaviorTree.Path.Push(Child);
            } else {
                BehaviorTree.LastStatus = NodeStatus.Failure;
                BehaviorTree.Finish();
            }
        }
    }
}