using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boxfriend
{
    public class Pickups : MonoBehaviour, IInteractable
    {
        #region Fields
        [Header("Stat Changes")]
        [SerializeField, Range(-10, 10), Tooltip("Int representing the speed gain/loss the player will receive on pickup")]
        private float _speedChange;
        [SerializeField, Range(-10, 10), Tooltip("Int representing the health gain/loss the player will receive on pickup")]
        private int _healthChange;

        [Header("Components")]
        [SerializeField]
        private Collider2D _collider;
        [SerializeField]
        private SpriteRenderer _spriteRenderer;
        #endregion

        #region IInteractable
        public int SpeedChange { get { return _speedChange; } }
        public int HealthChange { get { return _healthChange; } }
        #endregion

        #region MonoBehaviours
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                StartCoroutine(Kill());
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                StartCoroutine(Kill());
            }
        }
        #endregion

        IEnumerator Kill()
        {
            _collider.enabled = false;
            _spriteRenderer.enabled = false;
            yield return new WaitForSeconds(0.2f);
            Destroy(gameObject);
        }
    }
}