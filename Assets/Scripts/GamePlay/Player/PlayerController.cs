using Gameplay.Items;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Player
{
    public sealed class PlayerController : MonoBehaviour, IPlayer
    {
        public event Action Died;

        private readonly IList<IItem> _items = new List<IItem>();

        [SerializeField]
        private int _maxHealth = 1;
        private int _currentHealth;

        private bool _isDead;

        private Animator myAnim;

        internal PlayerMovement playerMovement;

        [Header("GameObjects Ref's")]
        [SerializeField] private GameObject[] _trails;

        public bool IsDead
        {
            get => _isDead;

            private set
            {
                if (_isDead == value)
                {
                    return;
                }

                _isDead = value;
                if (_isDead)
                {
                    OnPlayerDied();
                }
            }
        }

        public int Health
        {
            get => _currentHealth;
            set
            {
                value = Mathf.Clamp(value, 0, _maxHealth);
                if (_currentHealth == value)
                {
                    return;
                }

                _currentHealth = value;
                if (_currentHealth == 0)
                {
                    IsDead = true;
                }
            }
        }

        Vector2 IPlayer.Position => transform.position;

        public bool HasQuestItem { get; private set; }

        bool IPlayer.TakeItem(IItem item)
        {
            _items.Add(item);
            CheckForRing();
            return true;
        }

        void EnableTrails(bool isEnabled)
        {
            foreach (var trail in _trails)
            {
                trail.SetActive(isEnabled);
            }
        }

        void CheckForRing()
        {
            HasQuestItem = _items.Any(any => any.Keys.Contains(ItemKeys.QuestItem));
            var hasJumpBooster = _items.Any(any => any.Keys.Contains(ItemKeys.JumpBooster));
            playerMovement.JumpForce = hasJumpBooster ? 20 : 12;
            EnableTrails(hasJumpBooster);
        }

        private void Awake()
        {
            myAnim = GetComponent<Animator>();
            playerMovement = GetComponent<PlayerMovement>();

            IsDead = false;
            Health = _maxHealth;

            _items.Clear();
            CheckForRing();
        }

        private void Update()
        {
            RunAnimation();
            JumpAnimation();
        }

        #region Animations
        private void RunAnimation()
        {
            myAnim.SetFloat("MoveSpeed", playerMovement._rb2d.velocity.magnitude);
        }

        private void JumpAnimation()
        {
            if (playerMovement._rb2d.velocity.y != 0)
            {
                myAnim.SetBool("isJumping", true);
            }
            else
            {
                myAnim.SetBool("isJumping", false);
            }
        }
        #endregion

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Spikes") || col.gameObject.CompareTag("Enemy"))
            {
                --Health;
                return;
            }

            CheckInteraction(col.collider);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            CheckInteraction(col);
        }

        private void CheckInteraction(Collider2D col)
        {
            var interactable = col.gameObject.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.ApplyTo(this);
            }
        }

        private void OnPlayerDied()
        {
            playerMovement._rb2d.bodyType = RigidbodyType2D.Static;
            AudioManager_script.Instance.HurtSound();
            myAnim.Play("Death");
            Died?.Invoke();
        }
    }
}