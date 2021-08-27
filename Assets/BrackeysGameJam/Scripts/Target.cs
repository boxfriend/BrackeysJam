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
        [SerializeField, Range(0, 5), Tooltip("Number of points to give to player on hit.")]
        private int _score;

        [SerializeField,Tooltip("Prefab that will be dropped on target death")]
        private GameObject[] _pickupDrop;

        
        private int _currHealth;

        //Components
        [Header("Components"), Space(5)]
        [SerializeField]
        private SpriteRenderer _spr;
        [SerializeField]
        private BoxCollider2D _col;
        [SerializeField]
        private Animator _anim;
        [SerializeField]
        private AudioSource _audio;
        [Space(5)]
        [SerializeField]
        private AudioClip _damaged;
        [SerializeField]
        private AudioClip _destroyed;
        
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

            yield return new WaitForSeconds(0.25f);
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
                _audio.PlayOneShot(_destroyed);
                StartCoroutine(Kill());
            } else
            {
                _audio.PlayOneShot(_damaged);
            }
        }
        #endregion

        #region MonoBehaviours

        private void Awake()
        {
           
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