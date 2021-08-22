using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Boxfriend.Player
{
    /// <summary>
    /// Base class for PlayerState
    /// Controls state specific player actions
    /// </summary>
    public abstract class PlayerState
    {
        protected Rigidbody2D _rb;

        public PlayerState(Rigidbody2D body)
        {
            _rb = body;
        }


        #region MonoBehaviours
        public virtual void Update()
        {

        }

        public virtual void FixedUpdate(Vector2 moveDirection)
        {
            Vector2 playerVelocity = moveDirection * PlayerController.Instance.Speed;

            if (!(((int)playerVelocity.x ^ (int)_rb.velocity.x) <= 0) && Mathf.Abs(_rb.velocity.x) >= PlayerController.Instance.Speed)
            {
                playerVelocity.x = 0;
            }
            if (!(((int)playerVelocity.y ^ (int)_rb.velocity.y) <= 0) && Mathf.Abs(_rb.velocity.y) >= PlayerController.Instance.MaxSpeed)
            {
                playerVelocity.y = 0;
            }

            _rb.AddForce(playerVelocity);
            //_rb.velocity = moveDirection * PlayerController.Instance.Speed;
        }
        #endregion

        #region StateBehaviours
        public virtual IEnumerator StartState()
        {
            yield break;
        }

        public virtual IEnumerator ExitState()
        {
            yield break;
        }
        #endregion

    }
}
