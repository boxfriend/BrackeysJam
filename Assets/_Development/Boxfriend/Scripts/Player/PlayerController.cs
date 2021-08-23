using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

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
        [SerializeField, Range(1,100), Tooltip("Start health for the player")]
        private int _startHealth;
        [SerializeField, Range(1, 100), Tooltip("Start Damage for the player")]
        private int _startDamage;
        [SerializeField, Range(3, 10), Tooltip("Start Speed for the player")]
        private int _startSpeed;

        [Header("Max Stats"), Space(5)]
        [SerializeField, Range(0, 100), Tooltip("Maximum health for the player")]
        private int _maxHealth;
        [SerializeField, Range(0, 100), Tooltip("Maximum Damage for the player")]
        private int _maxDamage;
        [SerializeField, Range(0, 20), Tooltip("Maximum Speed for the player")]
        private int _maxSpeed;

        [Header("Player Components"), Space(5)]
        [SerializeField, Tooltip("Player's Rigidbody2D")]
        private Rigidbody2D _rb;
        [SerializeField, Tooltip("Arrow's Rigidbody2D")]
        private GameObject _windsArrow;
        [SerializeField]
        private Image _speedometer;


        //Non-Serialized Fields
        private int _currHealth, _currDamage, _currSpeed;
        #endregion

        #region Properties
        /// <summary>
        /// Get Player's current health.
        /// </summary>
        public int Health 
        { 
            get { return _currHealth; }
            private set { _currHealth = Mathf.Clamp(_currHealth + value, 0, _maxHealth); }
        }

        /// <summary>
        /// Get Player's current attack damage.
        /// </summary>
        public int Damage 
        { 
            get { return _currDamage; }
            private set { _currDamage = Mathf.Clamp(_currDamage + value, 0, _maxDamage); }
        }

        /// <summary>
        /// Get Player's current movement speed.
        /// This is applied force per frame, not velocity.
        /// </summary>
        public int Speed
        {
            get { return _currSpeed; }
            private set { _currSpeed = Mathf.Clamp(_currSpeed + value, 0, MaxSpeed); }
        }

        /// <summary>
        /// The maximum magnitude of the player's velocity.
        /// </summary>
        public int MaxSpeed
        {
            get { return _maxSpeed; }
        }

        /// <summary>
        /// Get the player's Rigidbody2D Velocity
        /// </summary>
        public Vector2 Velocity
        {
            get { return _rb.velocity; }
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

            var interactable = col.GetComponent<IInteractable>(); //Checks if 
            if(interactable != null)
            {
                _currSpeed += interactable.SpeedChange;
                Health = interactable.HealthChange;
                Debug.Log(Health);
            }
        }

        protected override void Update()
        {
            base.Update();
            _speedometer.fillAmount = _rb.velocity.magnitude / MaxSpeed;

            var angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg + 90;
            //_windsArrow.rotation = angle;
            _windsArrow.transform.rotation = Quaternion.Euler(0, 0, angle);

            if (_currHealth <= 0)
            {
                Kill();
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

            Health = damage;
            Debug.Log(Health);
            
        }

        public void Kill()
        {
            //TODO: implement death
            Debug.Log("Ha u ded");
            SetState(new PlayerStateDead(_rb));
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
