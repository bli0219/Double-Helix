using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class SuccessRepeaterNode : IDecoratorNode {

        bool started = false;

        public SuccessRepeaterNode(string name, ITreeNode child, Traverser traverser) : base(name, child, traverser) { }

        public override void Tick() {

            // this condition is most frequently used, so put it first
            if (started && Traverser.LastStatus == NodeStatus.Success) { 
                Traverser.Path.Push(Child);
            } else if (!started) {
                started = true;
                Traverser.Path.Push(Child);
            } else {
                Traverser.LastStatus = NodeStatus.Failure;
                Traverser.Finish();
            }
        }
    }
}