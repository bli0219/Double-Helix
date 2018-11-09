using System.Collections;
using MyBehaviorTree;
using System;

namespace MyBehaviorTree {

    public class ConditionNode : ITreeNode {

        // Fn returns only success & failure, and finishes immediately
        Func<NodeStatus> Fn;
        float param;
        ITreeNode Child;
        bool condition;
        bool started;

        public ConditionNode(string name, Func<NodeStatus> fn, ITreeNode child, BehaviorTree tree) {
            Fn = fn;
            Name = name;
            BehaviorTree = tree;
            Child = child;
        }

        public ConditionNode(string name, Func<NodeStatus> fn, BehaviorTree tree) {
            Fn = fn;
            Name = name;
            BehaviorTree = tree;
        }

        public ConditionNode Build(ITreeNode child) {
            Child = child;
            return this;
        }

        // If condition met, push child and after child finishes return success regardless
        // If condition not met, return failure
        // Supposed to provide branching with Selector, not Sequence
        public override void Tick() {
            if (!started) {
                NodeStatus status = Fn();
                started = true;
                if (status == NodeStatus.Success) {
                    BehaviorTree.path.Push(Child);
                }
            } else {
                BehaviorTree.Finish();
            }
        }

    }
}