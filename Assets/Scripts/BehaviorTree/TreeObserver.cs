using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class TreeObserver : MonoBehaviour {

        public static TreeObserver Instance;
        public static Stack<ITreeNode> Path;

        void Awake() {
            Instance = this;
        }

        public void Observe(ITreeNode node) {
            Path.Push(node);
        }

        public void Remove() {
            Path.Pop();
        }

        public void PrintPath() {
            string str = "";
            foreach(ITreeNode node in Path) {
                str = str + node.Name + "(" + node.GetType() + ")" + " -> ";
            }
            Debug.Log(str);
        }
    }
}
