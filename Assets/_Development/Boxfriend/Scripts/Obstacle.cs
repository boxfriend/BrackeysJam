using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boxfriend
{
    public class Obstacle : MonoBehaviour, IDestructable
    {

        #region Fields

        [Header("Obstacle Stats"), Space(5)]
        [SerializeField, Range(0, 100), Tooltip("Start health for the obstacle")]
        private int _startHealth;


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
            return;
        }

        public void TakeDamage(int damage)
        {
            return;
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

        }
        #endregion
    }
}