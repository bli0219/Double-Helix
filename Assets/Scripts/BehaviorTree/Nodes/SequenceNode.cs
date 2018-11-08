using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {

    public class SequenceNode : ICompositeNode {

        public SequenceNode(string name, ITreeNode[] children, BehaviorTree BehaviorTree) : base(name, children, BehaviorTree) { }
        public SequenceNode(string name, BehaviorTree BehaviorTree) : base(name, BehaviorTree) { }

        public override void Tick() {

            if (activeChild == -1) { // no child ticked, ignore LastStatus, tick first child
                try {
                    activeChild = 0;
                    BehaviorTree.Path.Push(Children[activeChild]);
                } catch {
                    Debug.LogError("Empty Children[]");
                }

            } else { // some child already ticked, check result
                if (BehaviorTree.LastStatus == NodeStatus.Failure) { // fail if any failure
                    BehaviorTree.LastStatus = NodeStatus.Failure;
                    BehaviorTree.Finish();
                } else { // no failure yet
                    if (activeChild < Children.Length - 1) { // if last activeChild was not the last
                        activeChild++;
                        BehaviorTree.Path.Push(Children[activeChild]);
                    } else { // reached the last, still no failure
                        BehaviorTree.LastStatus = NodeStatus.Success;
                        BehaviorTree.Finish();
                    }
                }

            }
        }

    }
}
