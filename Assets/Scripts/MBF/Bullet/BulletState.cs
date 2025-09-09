using System.Collections.Generic;
using UnityEngine;
using MBF.UnitBehaviors;

namespace MBF
{
    public class HitRecord
    {
        public GameObject target;
        public int tickToCanHit;

        public HitRecord(GameObject target, int tickToCanHit)
        {
            this.target = target;
            this.tickToCanHit = tickToCanHit;
        }
    }

    public class BulletState : MonoBehaviour
    {
        public BulletModel model;
        public GameObject caster;
        public int side;
        public Vector3 launchPos;
        public Vector3 launchDir;
        public float radius;
        public float speed;
        public int duration;
        public int hp;
        public bool justHitTarget;

        public BulletTargeting targeting;
        public BulletTween tween;

        public int tickElapsed;
        public List<HitRecord> hitRecords;
        public ChaProp propWhileCast;
        public Dictionary<string, object> parameters;

        public GameObject followingTarget;
        public Vector3 targetPos;

        private Vector3 _velocity;
        private UnitMove _unitMove;

        public void InitByLauncher(BulletLauncher launcher, List<GameObject> targets)
        {
            this.model = launcher.model;
            this.caster = launcher.caster;
            this.side = launcher.side;
            this.launchPos = launcher.launchPos;
            this.launchDir = launcher.launchDir;
            this.radius = launcher.radius;
            this.speed = launcher.speed;
            this.duration = launcher.duration;
            this.hp = launcher.hp;
            this.justHitTarget = launcher.justHitTarget;
            this.targeting = launcher.targeting;
            this.tween = launcher.tween;

            this.tickElapsed = 0;
            this.hitRecords = new List<HitRecord>();
            this.propWhileCast = launcher.propWhileCast;
            if (launcher.parameters != null)
                this.parameters = new Dictionary<string, object>(launcher.parameters);

            this.followingTarget = this.targeting?.Invoke(this, targets);
            this.targetPos = this.followingTarget != null ? followingTarget.transform.position : launcher.targetPos;
            
            this.transform.right = this.launchDir;
            
            SyncUnit();
        }

        private void SyncUnit()
        {
            _unitMove = GetComponent<UnitMove>();
        }

        public void AddMoveVelocity(Vector3 velocity)
        {
            _velocity += velocity;
            _unitMove.MoveBy(velocity);
            _velocity = Vector3.zero;
        }
        
        public void SetMoveVelocity(Vector3 velocity)
        {
            _unitMove.MoveBy(velocity);
            _velocity = Vector3.zero;
        }

        public void SetPos(Vector3 pos)
        {
            _unitMove.SetPos(pos);
        }

        public bool CanHit(GameObject target)
        {
            for (int i = 0; i < hitRecords.Count; i++)
            {
                if (hitRecords[i].target == target)
                    return false;
            }

            return true;
        }

        public void AddHitRecord(GameObject target)
        {
            hitRecords.Add(new HitRecord(target, model.sameTargetDelay));
        }
    }
}