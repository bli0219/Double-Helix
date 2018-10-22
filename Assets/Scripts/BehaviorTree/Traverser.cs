using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {
    public class Traverser : MonoBehaviour {

        public Traverser Instance;
        public Stack<ITreeNode> Path;
        public ITreeNode ActiveNode;
        public NodeStatus LastStatus;
        public bool actionTaken;

        void Awake() {
            Instance = this;
        }

        void FixedUpdate() {
            actionTaken = false;
            while (!actionTaken) {
                Path.Peek().Tick();
            }
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

        //public void Traverse() {
        //    while (Path.Count > 0) {
        //        Path.Peek().Tick();
        //    }
        //    Debug.Log("Traversal Ended.");
        //}

        IEnumerator Traverse() {
            while (Path.Count > 0) {
                Path.Peek().Tick();
                yield return null;
            }
            yield return null;
            Debug.Log("Traversal Ended.");
        }

        void Complete() {
            Path.Pop();
        }

        public void Finish() {
            Path.Pop();
        }
        public void Finish(ITreeNode node) {
            Debug.Log("Popping " + node.Name);
            Path.Pop();
        }
    }
}
