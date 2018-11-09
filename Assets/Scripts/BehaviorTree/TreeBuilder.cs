using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MyBehaviorTree {
    public class TreeBuilder {
        public BehaviorTree bt;

        NodeStatus Function1 () {
            return NodeStatus.Success;
        }

        void Main() {

            var root = new NaiveRepeater("root", bt);
            var sel1 = new SelectorNode("Sel1", bt);
            var seq1 = new SequenceNode("Seq1", bt);
            var act1 = new ActionNode("act1", Function1, bt);
            var act2 = new ActionNode("act2", Function1, bt);
            Func<NodeStatus> fn1 = Function1;
            Func<NodeStatus> fn2 = Function1;

            root.Build(
                seq1.Build(
                    act1, 
                    act2,
                    sel1.Build(
                        act1,
                        act2
                    )
                )

            );

        }

    }
}