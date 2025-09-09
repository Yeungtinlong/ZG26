using System.Collections.Generic;
using UnityEngine;

namespace MBF.UnitBehaviors
{
    public class UnitAnim : MonoBehaviour
    {
        public struct PriorityInfo
        {
            public int ticks;
            public int priority;
            public bool canReplaceBySelf;

            public PriorityInfo(int ticks, int priority, bool canReplaceBySelf)
            {
                this.ticks = ticks;
                this.priority = priority;
                this.canReplaceBySelf = canReplaceBySelf;
            }
        }

        public readonly Dictionary<string, PriorityInfo> _animPriorities = new Dictionary<string, PriorityInfo>()
        {
            { "idle", new PriorityInfo(1, 0, false) },
            { "move", new PriorityInfo(1, 0, false) },
            { "die", new PriorityInfo(1, 99, false) },
            { "attack", new PriorityInfo(1, 0, true) },
            { "skill_0", new PriorityInfo(1, 0, true) },
            { "skill_1", new PriorityInfo(1, 0, true) },
        };

        private Animator _animator;
        private SpriteRenderer _spriteRenderer;
        private string _playingAnimName;
        private int _capturedTicks;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
            for (int i = 0; i < clips.Length; i++)
            {
                string clipName = clips[i].name;
                if (_animPriorities.ContainsKey(clipName))
                {
                    PriorityInfo info = _animPriorities[clipName];
                    info.ticks = Mathf.RoundToInt(clips[i].length / Time.fixedDeltaTime);
                    _animPriorities[clipName] = info;
                }
            }
        }

        public void Play(string animName, float speed = 1.0f)
        {
            if (string.IsNullOrEmpty(animName) || !_animPriorities.ContainsKey(animName)) return;

            if (
                _playingAnimName != null &&
                _animPriorities[animName].priority < _animPriorities[_playingAnimName].priority
            )
                return;

            _animator.speed = speed;
            if (/*_playingAnimName == animName && */_animPriorities[animName].canReplaceBySelf)
                _animator.Play(animName, 0, 0f);
            else
                _animator.Play(animName, 0);

            _capturedTicks = Mathf.RoundToInt(_animPriorities[animName].ticks / speed);
            _playingAnimName = animName;
        }

        private void FixedUpdate()
        {
            if (_capturedTicks <= 0) return;

            _capturedTicks--;
            if (_capturedTicks <= 0)
            {
                _playingAnimName = null;
                _animator.speed = 1.0f;
            }
        }

        public void SetDir(Vector2 dir)
        {
            _animator.SetFloat("dir_x", dir.x);
            _animator.SetFloat("dir_y", dir.y);
            _spriteRenderer.flipX = dir.x < 0f;
        }
    }
}