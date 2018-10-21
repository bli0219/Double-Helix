using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public abstract class ITreeNode {

        public string Name { get; set; }
        public abstract NodeStatus Tick();

        public void AddToObserver() {
            TreeObserver.Path.Push (this);
        }
        public void RemoveFromObserver() {
            TreeObserver.Path.Pop();
        }
    }
}
