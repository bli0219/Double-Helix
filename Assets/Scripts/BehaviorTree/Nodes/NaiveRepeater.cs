using System.Collections;
using System.Collections.Generic;

namespace BehaviorTree  {

    public class NaiveRepeaterNode : IDecoratorNode  {

        public NaiveRepeaterNode(string name, ITreeNode child) {
            Child = child;
            Name = name;
        }

        public override NodeStatus Tick() {
            while (true) {
                Child.Tick();
            }
        }
    }

}