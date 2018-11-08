using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class InverterNode : IDecoratorNode {

        bool started = false;
        public InverterNode(string name, ITreeNode child, BehaviorTree BehaviorTree) : base(name, child, BehaviorTree) { }
        public InverterNode(string name, BehaviorTree BehaviorTree) : base(name, BehaviorTree) { }

        public override void Tick() {

            if (!started) {
                BehaviorTree.Path.Push(Child);
            } else {
                BehaviorTree.LastStatus = BehaviorTree.LastStatus == NodeStatus.Success ? NodeStatus.Failure : NodeStatus.Success;
                BehaviorTree.Finish();
            }
        }

    }
}