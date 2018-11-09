using System.Collections;
using MyBehaviorTree;
using System;

namespace MyBehaviorTree {

    public class ActionNode : ITreeNode {

        // The action to perform, passed by user when initialized
        // Needs to be defined wiht clear feedback (success, failure, running)
        Func<NodeStatus> Fn;

        public ActionNode(string name, Func<NodeStatus> fn, BehaviorTree tree) {
            Fn = fn;
            Name = name;
            BehaviorTree = tree;
        }

        // Tick() repeats over frames until it finishes
        public override void Tick() {
            NodeStatus status = Fn();
            BehaviorTree.actionTaken = true; //work done for this frame
            if (status != NodeStatus.Running) {
                BehaviorTree.lastStatus = Fn();
                BehaviorTree.Finish();
            }
        }

    }
}