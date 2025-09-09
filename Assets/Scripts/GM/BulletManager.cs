using System.Linq;
using Common;
using MBF;
using TheGame.ResourceManagement;
using UnityEngine;

namespace TheGame.GM
{
    public class BulletManager : MonoBehaviour
    {
        private SceneVariants _sceneVariants;

        public void Set(SceneVariants sceneVariants)
        {
            _sceneVariants = sceneVariants;
        }

        private bool CheckBulletHit(Vector3 prevPos, Vector3 currentPos, float bulletRadius,
            Vector3 enemyPos, float enemyRadius)
        {
            // 计算子弹移动方向向量
            Vector3 bulletDirection = currentPos - prevPos;
            float bulletDistance = bulletDirection.magnitude;

            // 如果子弹没有移动，直接检查当前位置
            if (bulletDistance <= Mathf.Epsilon)
            {
                return Vector3.Distance(currentPos, enemyPos) <= (bulletRadius + enemyRadius);
            }

            // 计算子弹路径上离敌人最近的点
            Vector3 bulletPath = bulletDirection.normalized;
            Vector3 enemyToPrevPos = prevPos - enemyPos;

            float dot = Vector3.Dot(enemyToPrevPos, bulletPath);
            float closestPoint = Mathf.Clamp(dot, 0f, bulletDistance);
            Vector3 closestPos = prevPos + bulletPath * closestPoint;

            // 检查最近点与敌人的距离
            float distance = Vector3.Distance(closestPos, enemyPos);

            return distance <= (bulletRadius + enemyRadius);
        }

        public void CreateBullet(BulletLauncher bulletLauncher)
        {
            GameObject prefab = ResLoader.LoadAsset<GameObject>($"Prefabs/Bullets/{bulletLauncher.model.prefab}.prefab");
            BulletState bs = Instantiate(prefab, bulletLauncher.launchPos, Quaternion.identity).GetComponent<BulletState>();
            bs.InitByLauncher(bulletLauncher, _sceneVariants.characters.Select(c=>c.gameObject).ToList());
            _sceneVariants.bullets.Add(bs);
        }

        public void LogicTick()
        {
            bool hasBulletRemoved = false;

            for (int i = 0; i < _sceneVariants.bullets.Count; i++)
            {
                BulletState bs = _sceneVariants.bullets[i];

                // 更新命中记录
                for (int j = 0; j < bs.hitRecords.Count; j++)
                {
                    if (bs.hitRecords[j].tickToCanHit == 0)
                    {
                        bs.hitRecords[j] = null;
                        continue;
                    }

                    bs.hitRecords[j].tickToCanHit--;
                }

                bs.hitRecords.RemoveNullElements();

                // 处理移动
                Vector3 willMoveVec = bs.tween.Invoke(bs.tickElapsed + 1, bs, bs.followingTarget);
                Vector3 willMoveToPos = willMoveVec + bs.transform.position;
                bs.SetPos(willMoveToPos);

                // 特殊子弹只能追踪目标，目标阵亡且子弹到达时，子弹消失
                if (bs.justHitTarget
                    && (bs.followingTarget == null || (bs.followingTarget.TryGetComponent(out CharacterState cs) && cs.IsDead))
                    && (bs.targetPos - bs.transform.position).sqrMagnitude <= 0.01f
                   )
                {
                    Destroy(bs.gameObject);
                    _sceneVariants.bullets[i] = null;
                    hasBulletRemoved = true;
                    continue;
                }

                // 检查碰撞
                for (int j = 0; j < _sceneVariants.characters.Count; j++)
                {
                    cs = _sceneVariants.characters[j];
                    if (bs.justHitTarget && cs.gameObject != bs.followingTarget)
                        continue;

                    if (!bs.CanHit(cs.gameObject))
                        continue;

                    if ((bs.side == cs.side && !bs.model.hitAlly) || !bs.model.hitFoe)
                        continue;

                    // if ((bs.transform.position - cs.transform.position).sqrMagnitude > bs.radius * bs.radius)
                    //     continue;
                    // TODO: 敌人碰撞半径应该配置化
                    if (!CheckBulletHit(bs.transform.position, willMoveToPos, bs.radius, cs.transform.position, 0.1f))
                        continue;

                    bs.model.onHit.Invoke(bs, cs.gameObject);

                    if (bs.hp > 0)
                    {
                        bs.AddHitRecord(cs.gameObject);
                        bs.hp--;
                    }
                }

                // 更新tick
                bs.tickElapsed++;

                if (bs.hp <= 0 || bs.tickElapsed >= bs.duration)
                {
                    Destroy(bs.gameObject);
                    _sceneVariants.bullets[i] = null;
                    hasBulletRemoved = true;
                }
            }

            if (hasBulletRemoved)
                _sceneVariants.bullets.RemoveNullElements();
        }
    }
}