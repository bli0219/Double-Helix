using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyBehaviorTree {
    public class BehaviorTree : MonoBehaviour {

        public Stack<ITreeNode> path;
        public ITreeNode activeNode;
        public NodeStatus lastStatus;
        public bool actionTaken;
        public ITreeNode root;

        void FixedUpdate() {
            Tick();
        }

        public void Tick() {
            actionTaken = false;
            while (!actionTaken) {
                path.Peek().Tick();
            }
        }

        public void PrintPath() {
            string str = "";
            foreach(ITreeNode node in path) {
                str = str + node.Name + "(" + node.GetType() + ")" + " -> ";
            }
            Debug.Log(str);
        }

        IEnumerator Traverse() {
            while (path.Count > 0) {
                path.Peek().Tick();
                yield return null;
            }
            yield return null;
            Debug.Log("Traversal Ended.");
        }

        public void Finish(NodeStatus status) {
            path.Pop();
            lastStatus = status;
        }
    }
}
