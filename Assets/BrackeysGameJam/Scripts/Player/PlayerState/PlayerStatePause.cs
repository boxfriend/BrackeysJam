using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JuJu;

namespace Boxfriend.Player
{
    public class PlayerStatePause : PlayerState
    {
        private Vector2 _prevVelocity;
        public PlayerStatePause(Rigidbody2D body) : base(body)
        {
            
        }
        

        public override IEnumerator StartState()
        {
            _prevVelocity = _rb.velocity;
            _rb.velocity = Vector2.zero;
            yield break;
        }

        // Update is called once per frame
        public override void Update()
        {
            return;
        }

        public override void FixedUpdate(Vector2 moveDir)
        {
            return;
        }

        public override IEnumerator ExitState()
        {
            _rb.velocity = _prevVelocity;
            return base.ExitState();
        }

        public override void OnEscape()
        {
            try
            {
                GameManager.instance.Resume();
            }
            catch (System.NullReferenceException)
            {
                PlayerController.Instance.PrevState();
                Time.timeScale = 1;
                Debug.LogWarning("Game Manager not active in hierarchy");
            }
        }
    }
}