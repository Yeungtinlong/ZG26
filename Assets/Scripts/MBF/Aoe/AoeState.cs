using System.Collections.Generic;
using UnityEngine;

namespace MBF
{
    public class AoeState : MonoBehaviour
    {
        public AoeModel model;
        public GameObject caster;
        public int side;
        public Vector3 targetPos;
        public int tickTime;
        public int duration;
        public float radius;
        public bool justCreated;
        public int tickElapsed;
        public List<CharacterState> charactersInRange;
        public ChaProp propWhileCast;
        public Dictionary<string, object> parameters;

        public void InitByLauncher(AoeLauncher launcher)
        {
            this.model = launcher.model;
            this.caster = launcher.caster;
            this.side = launcher.side;
            this.targetPos = launcher.targetPos;
            this.tickTime = Mathf.Max(1, launcher.tickTime);
            this.duration = launcher.duration;
            this.radius = launcher.radius;
            this.justCreated = true;
            this.tickElapsed = 0;
            this.propWhileCast = launcher.propWhileCast;
            this.charactersInRange = new List<CharacterState>();
            this.parameters = new Dictionary<string, object>();
            if (launcher.parameters != null)
                foreach(var p in launcher.parameters)
                    this.parameters.Add(p.Key, p.Value);
        }
    }
}