using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyBehaviorTree {
	public class LearnerNode : ICompositeNode {

		List<int> scores;
		bool ticked = false;
		int lastTicked = -1;

		public LearnerNode (string name, ITreeNode[] children, BehaviorTree bt) : base(name, children, bt) {
			scores = new List<int>(new int[children.Length]);
		}
		public LearnerNode (string name, BehaviorTree bt) : base(name, bt) { }

		public override void Tick () {

			if (scores == null) {
				scores = new List<int>(new int[Children.Length]);
			} else {
				Debug.LogWarning("Scores not initialized");
			}

			if (!ticked) {
				int largest = 0;
				for (int i = 1; i != scores.Count; i++) {
					if (scores[i] > scores[largest]) {
						largest = i;
					}
				}
				lastTicked = largest;
				BehaviorTree.path.Push(Children[largest]);
			} else {
				if (BehaviorTree.lastStatus == NodeStatus.Success)
					scores[lastTicked] += 1;
				if (BehaviorTree.lastStatus == NodeStatus.Failure)
					scores[lastTicked] -= 1;
				BehaviorTree.Finish(BehaviorTree.lastStatus);
			}
		}
	}
}