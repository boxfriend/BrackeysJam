using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Boxfriend.Player;

namespace Boxfriend
{
    public class Target : MonoBehaviour, IDestructible, IScorable
    {

        #region Fields

        [Header("Target Stats"), Space(5)]
        [SerializeField, Range(0, 100), Tooltip("Start health for the target")]
        private int _startHealth;

        [SerializeField]
        private ObstacleSprite _sprites;

        [SerializeField,Tooltip("Prefab that will be dropped on target death")]
        private GameObject[] _pickupDrop;

        [SerializeField, Range(0, 5), Tooltip("Number of points to give to player on hit.")]
        private int _score;
        private int _currHealth;

        //Components
        private SpriteRenderer _spr;
        private BoxCollider2D _col;
        private Animator _anim;
        
        #endregion

        #region Properties
        public int Health 
        {
            get { return _currHealth; }
            set { } 
        }

        public int Score 
        { 
            get 
            { 
                if(PlayerController.Instance.Velocity.magnitude > PlayerController.Instance.MaxSpeed * 0.9f)
                {
                    return _score * 3;
                } else
                {
                    return _score;
                }
            } 
        }

        #endregion

        #region IDestructable
        public IEnumerator Kill()
        {
            _col.enabled = false;

            yield return new WaitForSeconds(0.5f);
            if(_pickupDrop != null)
            {
                foreach (GameObject p in _pickupDrop)
                {
                    GameObject pickup = Instantiate(p, transform.position, Quaternion.identity);
                    pickup.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)), ForceMode2D.Impulse);
                }
            }
        }

        public void TakeDamage(int damage)
        {
            _currHealth -= damage;

            _anim.SetFloat("HealthPerc", (float)_currHealth / _startHealth);
            if(_currHealth <= 0)
            {
                StartCoroutine(Kill());
            }
        }
        #endregion

        #region MonoBehaviours

        private void Awake()
        {
            _spr = GetComponent<SpriteRenderer>();
            _col = GetComponent<BoxCollider2D>();
            _anim = GetComponent<Animator>();
        }
        // Start is called before the first frame update
        void Start()
        {
            _currHealth = _startHealth;
            _anim.SetFloat("HealthPerc", 1);
        }


        void OnTriggerEnter2D(Collider2D col)
        {
            
            if (col.CompareTag("Player"))
            {
                //_healthBar.gameObject.SetActive(true);
            }
        }

        void OnTriggerExit2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                //_healthBar.gameObject.SetActive(false);
            }
        }
        #endregion
    }
}