using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class SelectorNode : TreeNode {

        string name;
        TreeNode[] children;

        public SelectorNode(string _name, TreeNode[] _children) {
            children = _children;
            name = _name;
        }

        public NodeStatus Tick() {
            foreach (TreeNode node in children) {
                if (node.Tick()!= NodeStatus.Failure) {
                    return NodeStatus.Success;
                }
            }
            return NodeStatus.Failure;
        }

    }
}