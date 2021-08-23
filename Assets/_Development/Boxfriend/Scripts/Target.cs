using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Boxfriend
{
    public class Target : MonoBehaviour, IDestructible
    {

        #region Fields

        [Header("Obstacle Stats"), Space(5)]
        [SerializeField, Range(0, 100), Tooltip("Start health for the obstacle")]
        private int _startHealth;

        [SerializeField]
        private ObstacleSprite _sprites;

        private int _currHealth;

        //Components
        private SpriteRenderer _spr;
        private BoxCollider2D _col;
        
        #endregion

        #region Properties
        public int Health 
        {
            get { return _currHealth; }
            set { } 
        }

        #endregion

        #region IDestructable
        public void Kill()
        {
            _col.enabled = false;
        }

        public void TakeDamage(int damage)
        {
            _currHealth -= damage;


            if(_currHealth <= 0)
            {
                Kill();
            }
        }
        #endregion

        #region MonoBehaviours

        private void Awake()
        {
            _spr = GetComponent<SpriteRenderer>();
            _col = GetComponent<BoxCollider2D>();
        }
        // Start is called before the first frame update
        void Start()
        {
            _currHealth = _startHealth;
        }

        // Update is called once per frame
        void Update()
        {
            //_healthBar.fillAmount = (float)_currHealth / _startHealth;

            if(_currHealth <= 0)
            {
                _spr.sprite = _sprites.sprites[4];
            } else if ((float)_currHealth/_startHealth < 0.5)
            {
                _spr.sprite = _sprites.sprites[3];
            } else if ((float)_currHealth / _startHealth < 0.75)
            {
                _spr.sprite = _sprites.sprites[2];
            }
            else if ((float)_currHealth / _startHealth != 1)
            {
                _spr.sprite = _sprites.sprites[1];
            } else 
            {
                _spr.sprite = _sprites.sprites[0];
            }
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