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
            _rb.velocity = moveDirection * PlayerController.Instance.Speed;
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
