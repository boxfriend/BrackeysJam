using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boxfriend.Player
{
    public class PlayerStateDead : PlayerState
    {

        public PlayerStateDead(Rigidbody2D body) : base(body)
        {

        }

        public override IEnumerator ExitState()
        {
            return base.ExitState();
        }

        public override void FixedUpdate(Vector2 moveDirection)
        {
            return;
        }

        public override IEnumerator StartState()
        {
            return base.StartState();
        }

        public override void Update()
        {
            return;
        }
    }
}