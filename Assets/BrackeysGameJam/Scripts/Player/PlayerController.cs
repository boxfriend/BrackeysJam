using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using JuJu;

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

        public float speed;

        [Header("Player Components"), Space(5)]
        [SerializeField, Tooltip("Player's Rigidbody2D")]
        private Rigidbody2D _rb;
        [SerializeField, Tooltip("Arrow's Rigidbody2D")]
        private GameObject _windsArrow;
        [SerializeField,Tooltip("Speedometer image")]
        private Image _speedometer;
        [SerializeField, Tooltip("Score Text object")]
        private Text _scoreText;
        [SerializeField, Tooltip("Trail Renderer object")]
        private TrailRenderer _tr;
        [SerializeField, Range(10,100),Tooltip("Changes how quickly the speed on the speedometer changes")]
        private float _speedAdjustment;
        [SerializeField, Tooltip("The text for the speedometer speed display")]
        private TextMeshProUGUI _speedText;
        [SerializeField, Tooltip("The amount of time the player has")]
        private float timerCount = 60f;
        [SerializeField, Tooltip("The text for the timer")]
        private TextMeshProUGUI timerText;
        //Non-Serialized Fields
        private int _currHealth, _currDamage, _score = 0;
        private float _currSpeed;

        private bool timerIsStarted;
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
        public float Speed
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

        /// <summary>
        /// Get the player's Rigidbody2D Velocity
        /// </summary>
        public int Score
        {
            get { return _score; }
            private set { _score += value; }
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

            var interactable = col.GetComponent<IInteractable>(); //Checks if object is interactable then applies speed and health changes to player
            if(interactable != null)
            {
                Speed = interactable.SpeedChange;
                Health = interactable.HealthChange;
                Debug.Log(Health);
            }

            var scorable = col.GetComponent<IScorable>();
            if (scorable != null)
            {
                Score = scorable.Score;
            }
        }

        

        void OnCollisionEnter2D(Collision2D col)
        {
            var destructible = col.gameObject.GetComponent<IDestructible>(); //Checks if object is destructible then applies necessary damage
            if (destructible != null)
            {
                destructible.TakeDamage(Damage);
            }

            var interactable = col.gameObject.GetComponent<IInteractable>(); //Checks if object is interactable then applies speed and health changes to player
            if (interactable != null)
            {
                Speed = interactable.SpeedChange;
                Health = interactable.HealthChange;
                Debug.Log(Health);
            }
        }

        protected override void Update()
        {
            base.Update();
            //Sets player scale based on percentage of health compared to start health
            transform.localScale = Vector3.one * ((float)Health / _startHealth) * 1.5f; 
            _tr.widthMultiplier = transform.localScale.x / 3;

            _speedometer.fillAmount = _rb.velocity.magnitude / MaxSpeed;
            _scoreText.text = Score.ToString();


            var angle = Mathf.Atan2(_rb.velocity.y, _rb.velocity.x) * Mathf.Rad2Deg - 90;
            _windsArrow.transform.rotation = Quaternion.Euler(0, 0, angle);

            //Displays the speed on the speed text on the speedometer
            _speedText.text = $"{(int)(Velocity.magnitude * _speedAdjustment)} m/h";

            if(_rb.velocity.magnitude >= 0.5f){
                timerIsStarted = true;
            }

            if(timerIsStarted){
            timerCount -= Time.deltaTime;
            timerText.text = $"{(int)timerCount}";
            }

            if(timerCount <= 0){
                //Time.timeScale = 0;
                timerIsStarted = false;
                GameManager.instance.GameOver();
                GetComponent<PlayerController>().enabled = false;
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
            
            if (Health < 1)
            {
                StartCoroutine(Kill());
            }

        }


        /// <summary>
        /// Kills the player
        /// </summary>
        /// <returns></returns>
        public IEnumerator Kill()
        {
            SetState(new PlayerStateDead(_rb));
            yield return null;
        }
        #endregion

        /// <summary>
        /// Calls the Kill() coroutine. This is only to be used from non-MonoBehaviour scripts
        /// </summary>
        /// <param name="t"></param>
        public void Kill(bool t)
        {
            StartCoroutine(Kill());
        }

        #region Input
        void OnMove(InputValue value)
        {
            _moveDirection = value.Get<Vector2>();
        }

        void OnEscape()
        {
            _state.OnEscape();
        }
        #endregion
    }
}
