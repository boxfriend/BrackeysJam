using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boxfriend.Player
{
    public class PlayerStateBegin : PlayerState
    {
        public PlayerStateBegin(Rigidbody2D body) : base(body)
        {

        }

        public override void FixedUpdate(Vector2 moveDirection)
        {
            base.FixedUpdate(moveDirection);

            if (_rb.velocity.magnitude > 0.5)
            {
                PlayerController.Instance.SetState(new PlayerStateMove(_rb));
            }
        }
    }
}