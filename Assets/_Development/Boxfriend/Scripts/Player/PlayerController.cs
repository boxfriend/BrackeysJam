using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Boxfriend.Player
{
    //[RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : PlayerStateManager, IDestructible
    {
        #region Fields
        /// <summary>
        /// Static reference to the PlayerController
        /// (it's a singleton)
        /// </summary>
        [HideInInspector]
        public static PlayerController Instance;

        [Header("Player Stats"), Space(5)]
        [SerializeField, Range(0,100), Tooltip("Start health for the player")]
        private int _startHealth;
        [SerializeField, Range(0, 100), Tooltip("Start Damage for the player")]
        private int _startDamage;
        [SerializeField, Range(0, 100), Tooltip("Start Speed for the player")]
        private int _startSpeed;

        [Header("Max Stats"), Space(5)]
        [SerializeField, Range(0, 100), Tooltip("Maximum health for the player")]
        private int _maxHealth;
        [SerializeField, Range(0, 100), Tooltip("Maximum Damage for the player")]
        private int _maxDamage;
        [SerializeField, Range(0, 100), Tooltip("Maximum Speed for the player")]
        private int _maxSpeed;

        [Header("Player Components"), Space(5)]
        [SerializeField, Tooltip("Player's Rigidbody2D")]
        private Rigidbody2D _rb;

        public Vector2 vel;
        public float mag;

        //Non-Serialized Fields
        private int _currHealth, _currDamage, _currSpeed;
        #endregion

        #region Properties
        /// <summary>
        /// Get Player's current health.
        /// Cannot set health this way.
        /// </summary>
        public int Health 
        { 
            get { return _currHealth; }
            private set { return; }
        }

        /// <summary>
        /// Get Player's current damage.
        /// </summary>
        public int Damage 
        { 
            get { return _currDamage; }
        }

        /// <summary>
        /// Get Player's current speed.
        /// </summary>
        public int Speed
        {
            get { return _currSpeed; }
            private set { _currSpeed = Mathf.Clamp(_currSpeed + value, 0, MaxSpeed); }
        }

        public int MaxSpeed
        {
            get { return _maxSpeed; }
        }
        #endregion

        #region MonoBehaviours
        void Awake()
        {
            if(Instance != null)
            {
                Destroy(PlayerController.Instance.gameObject);
            }

            Instance = this;

            SetState(new PlayerStateBegin(_rb));
        }

        void Start()
        {
            //Sets player stats to their starting stats
            _currHealth = _startHealth;
            _currDamage = _startDamage;
            _currSpeed = _startSpeed;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            var destructible = col.GetComponent<IDestructible>(); //Checks if object is destructible then applies necessary damage
            if (destructible != null)
            {
                destructible.TakeDamage(Damage);
            }

            var interactable = col.GetComponent<IInteractable>();
            if(interactable != null)
            {
                _currSpeed += interactable.SpeedChange;
            }
        }

        #endregion

        #region IDestructable
        /// <summary>
        /// Will subtract damage value from current health.
        /// </summary>
        /// <param name="damage">Int to subtract from Health</param>
        public void TakeDamage(int damage)
        {
            _currHealth = Mathf.Clamp(_currHealth - damage, 0, _maxHealth); 

            if(_currHealth <= 0)
            {
                Kill();
            }
        }

        public void Kill()
        {
            //TODO: implement death
        }
        #endregion

        #region Input
        void OnMove(InputValue value)
        {
            _moveDirection = value.Get<Vector2>();
        }

        void OnEscape()
        {
            if(_state is PlayerStatePause)
            {
                PrevState();
                
                Time.timeScale = 1;
                Debug.Log("Player Unpause");
            } else
            {
                SetState(new PlayerStatePause(_rb));
                Time.timeScale = 0;
                Debug.Log("Player Paused");
            }
        }
        #endregion
    }
}
