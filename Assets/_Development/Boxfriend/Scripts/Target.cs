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

        [SerializeField, Tooltip("The healthbar attached to the obstacle, must be Fill type")]
        private Image _healthBar;


        private int _currHealth;
        
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
            Debug.Log("Object would have died here");
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
        // Start is called before the first frame update
        void Start()
        {
            _currHealth = _startHealth;
        }

        // Update is called once per frame
        void Update()
        {
            _healthBar.fillAmount = (float)_currHealth / _startHealth;
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