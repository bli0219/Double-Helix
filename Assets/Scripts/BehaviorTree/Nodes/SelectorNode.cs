using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class SelectorNode : ICompositeNode {

        public SelectorNode(string name, ITreeNode[] children) {
            Children = children;
            Name = name;
        }

        public override NodeStatus Tick() {
            foreach (ITreeNode node in Children) {
                if (node.Tick()!= NodeStatus.Failure) {
                    return NodeStatus.Success;
                }
            }
            return NodeStatus.Failure;
        }

    }
}