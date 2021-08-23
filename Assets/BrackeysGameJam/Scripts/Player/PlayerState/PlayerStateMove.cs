using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boxfriend.Player
{
    public class PlayerStateMove : PlayerState
    {
        public PlayerStateMove(Rigidbody2D body) : base(body)
        {

        }

        public override IEnumerator StartState()
        {
            return base.StartState();
        }

        public override void FixedUpdate(Vector2 moveDirection)
        {
            base.FixedUpdate(moveDirection);
            if (_rb.velocity.magnitude < 0.4)
            {
                Debug.Log("too slow!");
                //PlayerController.Instance.SetState(new PlayerStatePause(_rb));
                PlayerController.Instance.Kill();
            }
        }
    }
}