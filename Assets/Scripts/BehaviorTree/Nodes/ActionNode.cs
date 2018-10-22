using System.Collections;
using BehaviorTree;
using System;

namespace BehaviorTree {

    public class ActionNode : ITreeNode {

        // The action to perform, passed by user when initialized
        // Needs to be defined wiht clear feedback (success, failure, running)
        readonly Func<NodeStatus> Fn;

        public ActionNode(string name, Func<NodeStatus> fn, Traverser traverser) {
            Fn = fn;
            Name = name;
            Traverser = traverser;
        }

        // Tick() repeats over frames until it finishes
        public override void Tick() {
            NodeStatus status = Fn();
            Traverser.actionTaken = true;
            if (status != NodeStatus.Running) {
                Traverser.LastStatus = Fn();
                Traverser.Finish();
            }
        }

    }
}