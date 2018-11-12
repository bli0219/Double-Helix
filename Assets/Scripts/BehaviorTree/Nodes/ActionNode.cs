using System.Collections;
using MyBehaviorTree;
using System;

namespace MyBehaviorTree {

    public class ActionNode : ITreeNode {

        // The action to perform, passed by user when initialized
        // Needs to be defined wiht clear feedback (success, failure, running)
        Action Task;

        public ActionNode(string name, Action task, BehaviorTree tree) {
            Task = task;
            Name = name;
            BehaviorTree = tree;
            
            //hero = BehaviorTree.hero;
        }

        // Task() 
        public override void Tick() {
            BehaviorTree.actionStatus = NodeStatus.Running;
            Task();
            if (BehaviorTree.actionStatus != NodeStatus.Running) {
                BehaviorTree.Finish(BehaviorTree.actionStatus);
            }
        }

    }
}