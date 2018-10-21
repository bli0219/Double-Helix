using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class InverterNode : IDecoratorNode {

        public InverterNode (string name, ITreeNode child) {
            Name = name;
            Child = child;
        }

        public override NodeStatus Tick() {
            NodeStatus status = Child.Tick();
            if (status == NodeStatus.Success) return NodeStatus.Failure;
            else return NodeStatus.Failure;
        }

    }
}