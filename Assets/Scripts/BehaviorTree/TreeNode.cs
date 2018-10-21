using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree {

    public interface TreeNode {

        NodeStatus Tick();

    }
}
