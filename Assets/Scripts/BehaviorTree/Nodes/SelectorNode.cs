﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class SelectorNode : ICompositeNode {

        // activeChild = -1 on declaration
        public SelectorNode(string name, ITreeNode[] children, Traverser traverser) : base (name, children, traverser) {}

        // use global variable to pass status
        public override void Tick() {

            if (activeChild == -1) {
                // no child ticked, ignore LastStatus, tick first child
                // assuming Children.Length > 0
                try {
                    activeChild = 0;
                    Traverser.Path.Push(Children[activeChild]);
                    
                } catch {
                    Debug.LogError("Empty Children[]");
                }

            } else {
                // some child ticked
                if (Traverser.LastStatus == NodeStatus.Success) {
                    // succeed if any success
                    Traverser.LastStatus = NodeStatus.Success;
                    Traverser.Finish();
                } else {
                    // no success yet
                    if (activeChild < Children.Length-1) {
                        // if last activeChild was not the last
                        activeChild++;
                        Traverser.Path.Push(Children[activeChild]);
                    } else {
                        // reached the last, still no success
                        Traverser.LastStatus = NodeStatus.Failure;
                        Traverser.Finish();
                    }
                }

            }
        }
    }
}