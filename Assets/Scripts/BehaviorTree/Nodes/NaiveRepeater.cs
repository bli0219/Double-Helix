using System.Collections;
using System.Collections.Generic;

namespace BehaviorTree  {

    public class NaiveRepeaterNode : IDecoratorNode  {

        public NaiveRepeaterNode(string name, ITreeNode child, Traverser traverser) : base (name, child, traverser) { }

        public override void Tick() {
            Traverser.Path.Push(Child);
        }
    }

}