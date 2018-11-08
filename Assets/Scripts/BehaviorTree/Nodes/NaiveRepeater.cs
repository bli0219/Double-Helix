using System.Collections;
using System.Collections.Generic;

namespace BehaviorTree  {

    public class NaiveRepeaterNode : IDecoratorNode  {

        public NaiveRepeaterNode(string name, ITreeNode child, BehaviorTree BehaviorTree) : base (name, child, BehaviorTree) { }
        public NaiveRepeaterNode(string name, BehaviorTree BehaviorTree) : base(name, BehaviorTree) { }

        public override void Tick() {
            BehaviorTree.Path.Push(Child);
        }
    }

}