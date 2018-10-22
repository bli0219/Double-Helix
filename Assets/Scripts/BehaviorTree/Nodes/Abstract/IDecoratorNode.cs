using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public abstract class IDecoratorNode : ITreeNode  {
        // inherit Name
        public ITreeNode Child;

        protected IDecoratorNode (string name, ITreeNode child, Traverser traverser) {
            Name = name;
            Child = child;
            Traverser = traverser;
        }
    }
}