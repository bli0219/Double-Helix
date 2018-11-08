using System.Collections;
using BehaviorTree;
using System;

namespace BehaviorTree {

    public class ActionNode : ITreeNode {

        // The action to perform, passed by user when initialized
        // Needs to be defined wiht clear feedback (success, failure, running)
        Func<NodeStatus> Fn;

        public ActionNode(string name, Func<NodeStatus> fn, BehaviorTree behaviorTree) {
            Fn = fn;
            Name = name;
            BehaviorTree = behaviorTree;
        }

        // Commmenting out the two functions because assigning beforehand is preferred

        //public ActionNode(string name, BehaviorTree behaviorTree) {
        //    Name = name;
        //    BehaviorTree = behaviorTree;
        //}

        //public ActionNode Build(Func<NodeStatus> fn) {
        //    Fn = fn;
        //    return this;
        //}

        // Tick() repeats over frames until it finishes
        public override void Tick() {
            NodeStatus status = Fn();
            BehaviorTree.actionTaken = true;
            if (status != NodeStatus.Running) {
                BehaviorTree.LastStatus = Fn();
                BehaviorTree.Finish();
            }
        }

    }
}