using Cinemachine;
using Project.Manager;
using Project.SpellSystem;
using Project.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player
{
    public class PlayerManager : Singleton<PlayerManager>, IDamageable
    {
        Rigidbody rigidbody;
        Animator animator;
        private Vector3 _velocity;

        public CinemachineFreeLook cinemachine;

        Vector3 moveDirection = Vector3.zero;
        public float speed = 300f;
        [Header("Temporal Solution")]
        public BoxCollider collider;
        public GameObject groundedGO;

        [Header("Value for editor")]
        [SerializeField]
        private float timeBetweenSpell;
        public float rotationSpeed = 30f;
        [SerializeField]
        private float sprintMultiplier = 2f;
        [SerializeField]
        private float jumpForce = 50f;
        [Header("Control")]

        public KeyCode basicSpellInput = KeyCode.Alpha1;
        public KeyCode heavySpellInput = KeyCode.Alpha2;
        public KeyCode buffSpellInput = KeyCode.Alpha3;
        public KeyCode crowdControlSpellInput = KeyCode.Alpha4;

        [SerializeField]
        private bool isGrounded = true;
        [Header("Spawn Point/Effect")]
        public Transform spellSpawnPoint;

        [Header("Stats")]
        [SerializeField]
        private PlayerStats stats;
        
        [Header("Spell Book")]
        public SpellBook spellBook;

        Cooldown cooldownActualSpell;
        Cooldown cooldownBetweenWeaponAttack; //TODO : attack speed in the weapon class so change this value after equip weapon

        void Start()
        {
            cooldownActualSpell = new Cooldown(0);
            cooldownBetweenWeaponAttack = new Cooldown(1);
            Cursor.visible = false;
            this.collider = GetComponentInChildren<BoxCollider>();
            this.rigidbody = GetComponent<Rigidbody>();
            this.animator = GetComponent<Animator>();

            this.stats = new PlayerStats(100,100);
            this.spellBook = new SpellBook(this);
            UIManager.Instance.lifeBar.SetMaxLifeBar(this.stats.maxHealth);
            UIManager.Instance.manaBar.SetMaxManaBar(this.stats.maxMana);
        }

        void Update()
        {
            CheckMovement();
            CheckRotation();

            CheckWeaponAttack();
            CheckSpellAttack();
            
        }

        #region CheckMethods
        private void CheckMovement()
        {
            float vertical = Input.GetAxis("Vertical"); //Axis Z
            float horizontal = Input.GetAxis("Horizontal"); //Axis X

            Vector3 forward = Camera.main.transform.forward;
            Vector3 right = Camera.main.transform.right;

            _velocity = (forward * vertical) + (right * horizontal) * speed * Time.deltaTime;
            bool isRunning = Input.GetKey(KeyCode.LeftShift);
            if (isRunning)
            {
                _velocity *= sprintMultiplier;
            }

            _velocity.y = rigidbody.velocity.y;
            rigidbody.velocity = _velocity;
            this.animator.SetFloat("Speed", Mathf.Abs(rigidbody.velocity.magnitude));

            this.animator.SetBool("IsRunning", isRunning && rigidbody.velocity.magnitude > 0.05f);

        }
        private void CheckRotation()
        {
            Vector3 cameraDirection = new Vector3(Camera.main.transform.forward.x, 0f, Camera.main.transform.forward.z);
            Vector3 playerDirection = new Vector3(transform.forward.x, 0f, transform.forward.z);

            if (Vector3.Angle(cameraDirection, playerDirection) > 15f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(cameraDirection, transform.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }

        private void CheckSpellAttack()
        {
            cooldownActualSpell.CooldownUpdate();

            Spell spellSelected = this.spellBook.GetSpellSelected();

            if ( spellSelected != null && !cooldownActualSpell.IsOnCooldown()) //Fix the condition with cooldown
            {
                if(this.stats.currentMana >= spellSelected.manaCost)
                {
                    this.stats.UseMana(spellSelected.manaCost);

                    cooldownActualSpell.SetCooldown(spellSelected.castingTime);
                    cooldownActualSpell.StartCooldown();

                    this.animator.SetTrigger("SpellAttack");
                    this.FireSpell(spellSelected);
                }
            }
        }

        public void CheckWeaponAttack()
        {
            cooldownBetweenWeaponAttack.CooldownUpdate();
            if (Input.GetMouseButtonDown(0) & !cooldownBetweenWeaponAttack.IsOnCooldown())
            {
                cooldownBetweenWeaponAttack.StartCooldown();
                this.animator.SetTrigger("WeaponAttack");
                //Attack
            }
            //TODO: first make spell attack work and spell system
        }

        #endregion

        public void FireSpell(Spell spell)
        {
            UIManager.Instance.manaBar.SetManaBar(this.stats.currentMana);
            switch (spell.type)
            {
                case SpellType.PROJECTILE:
                    Instantiate(spell.vfx, spellSpawnPoint.position, transform.rotation);
                    break;
                case SpellType.CIRCLE_AREA:
                    Instantiate(spell.vfx, transform.position, Quaternion.identity);
                    break;
            }

        }

        #region IDamageable
        public void TakeDamage(int ammount)
        {
            this.stats.ReduceLife(ammount);
            UIManager.Instance.lifeBar.SetLifeBar(this.stats.currentHealth);
        }

        public bool IsDead()
        {
            return this.stats.currentHealth <= 0;
        }

        public void Die()
        {
            Destroy(this.gameObject);
            Debug.Log("Your Dead");
        }
        #endregion
    }
}

