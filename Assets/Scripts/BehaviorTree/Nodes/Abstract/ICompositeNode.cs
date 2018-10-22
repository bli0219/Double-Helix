using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public abstract class ICompositeNode : ITreeNode {

        // inherit Name from ITreeNode
        protected ITreeNode[] Children;
        protected int activeChild = -1; 

        protected ICompositeNode (string name, ITreeNode[] children, Traverser traverser) {
            Name = name;
            Children = children;
            Traverser = traverser;
        }


    }
}
