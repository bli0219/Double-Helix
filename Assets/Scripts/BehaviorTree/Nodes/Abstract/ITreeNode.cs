using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public abstract class ITreeNode {

        public string Name;

        // Tick() modifies global variable LastStatus as message passing
        public abstract void Tick();
        protected Traverser Traverser;

        //public void AddToObserver() {
        //    TreeTraverser.Path.Push (this);
        //}
        //public void RemoveFromObserver() {
        //    TreeTraverser.Path.Pop();
        //}
    }
}
