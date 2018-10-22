using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {

    public class SequenceNode : ICompositeNode {
        
        public SequenceNode(string name, ITreeNode[] children, Traverser traverser) : base(name, children, traverser) { }

        public override void Tick() {

            if (activeChild == -1) { // no child ticked, ignore LastStatus, tick first child
                try {
                    activeChild = 0;
                    Traverser.Path.Push(Children[activeChild]);
                } catch {
                    Debug.LogError("Empty Children[]");
                }

            } else { // some child already ticked, check result
                if (Traverser.LastStatus == NodeStatus.Failure) { // fail if any failure
                    Traverser.LastStatus = NodeStatus.Failure;
                    Traverser.Finish();
                } else { // no failure yet
                    if (activeChild < Children.Length - 1) { // if last activeChild was not the last
                        activeChild++;
                        Traverser.Path.Push(Children[activeChild]);
                    } else { // reached the last, still no failure
                        Traverser.LastStatus = NodeStatus.Success;
                        Traverser.Finish();
                    }
                }

            }
        }

    }
}
