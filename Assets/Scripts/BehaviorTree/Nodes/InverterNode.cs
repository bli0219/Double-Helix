using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class InverterNode : IDecoratorNode {

        bool started = false;
        public InverterNode(string name, ITreeNode child, Traverser traverser) : base(name, child, traverser) { }

        public override void Tick() {

            if (!started) {
                Traverser.Path.Push(Child);
            } else {
                Traverser.LastStatus = Traverser.LastStatus == NodeStatus.Success ? NodeStatus.Failure : NodeStatus.Success;
                Traverser.Finish();
            }
        }

    }
}