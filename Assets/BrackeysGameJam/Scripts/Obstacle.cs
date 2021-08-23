using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boxfriend
{
    public class Obstacle : MonoBehaviour, IInteractable
    {
        [SerializeField,Range(-10,10),Tooltip("Change in player speed when interacting with object")]
        private int _speedChange;
        [SerializeField, Range(-10, 10), Tooltip("Change in player health when interacting with object")]
        private int _healthChange;



        public int SpeedChange { get { return _speedChange; } }

        public int HealthChange { get { return _healthChange; } }

        
    }
}