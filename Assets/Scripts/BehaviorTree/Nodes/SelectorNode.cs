using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class SelectorNode : ICompositeNode {

        // activeChild = -1 on declaration
        public SelectorNode(string name, ITreeNode[] children, BehaviorTree BehaviorTree) : base (name, children, BehaviorTree) {}
        public SelectorNode(string name, BehaviorTree BehaviorTree) : base(name, BehaviorTree) { }

        // use global variable to pass status
        public override void Tick() {

            if (activeChild == -1) {
                // no child ticked, ignore LastStatus, tick first child
                // assuming Children.Length > 0
                try {
                    activeChild = 0;
                    BehaviorTree.Path.Push(Children[activeChild]);
                    
                } catch {
                    Debug.LogError("Empty Children[]");
                }

            } else {
                // some child ticked
                if (BehaviorTree.LastStatus == NodeStatus.Success) {
                    // succeed if any success
                    BehaviorTree.LastStatus = NodeStatus.Success;
                    BehaviorTree.Finish();
                } else {
                    // no success yet
                    if (activeChild < Children.Length-1) {
                        // if last activeChild was not the last
                        activeChild++;
                        BehaviorTree.Path.Push(Children[activeChild]);
                    } else {
                        // reached the last, still no success
                        BehaviorTree.LastStatus = NodeStatus.Failure;
                        BehaviorTree.Finish();
                    }
                }

            }
        }
    }
}