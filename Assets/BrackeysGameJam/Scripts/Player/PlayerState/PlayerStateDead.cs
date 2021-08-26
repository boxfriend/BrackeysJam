using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JuJu;

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
            try
            {
                GameManager.instance.GameOver();
            }
            catch (System.NullReferenceException)
            {
                Debug.LogWarning("Game Manager not active in hierarchy");
            }
            return base.StartState();
        }

        public override void Update()
        {
            return;
        }

        public override void OnEscape()
        {
            return;
        }
    }
}