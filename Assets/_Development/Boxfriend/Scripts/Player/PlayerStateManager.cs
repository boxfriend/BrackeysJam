using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boxfriend.Player
{
    /// <summary>
    /// Manages Player State
    /// Will automatically call PlayerState methods
    /// </summary>
    public class PlayerStateManager : MonoBehaviour
    {
        protected PlayerState _state, _prevState;
        protected Vector2 _moveDirection;

        #region StateManagement
        /// <summary>
        /// Caches current player state and calls state exit routine
        /// Changes current state of the player
        /// </summary>
        /// <param name="state">PlayerState</param>
        public void SetState(PlayerState state)
        {
            if(_state != null)
            {
                _prevState = _state;
                StartCoroutine(_state.ExitState());
            }

            _state = state;

            StartCoroutine(_state.StartState());
        }

        /// <summary>
        /// Returns player to previous state by calling SetState and passing previous state
        /// </summary>
        public void PrevState()
        {
            SetState(_prevState);
        }
        #endregion

        #region MonoBehaviours

        void Update()
        {
            _state.Update();
        }

        private void FixedUpdate()
        {
            _state.FixedUpdate(_moveDirection);
        }

        #endregion
    }
}
