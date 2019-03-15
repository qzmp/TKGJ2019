using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    [CreateAssetMenu(menuName = "PluggableAI/EnemyStats")]
    public class EnemyStats : ScriptableObject
    {
        public float runningSpeed;
        public float chaseDuration;

        public float lightRadius;
        public float growLightDuration;
    }
}
