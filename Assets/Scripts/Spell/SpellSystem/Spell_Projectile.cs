using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.SpellSystem
{
    public class Spell_Projectile : MonoBehaviour
    {
        BoxCollider collider;
        [SerializeField]
        private float maxLifeTime;


        private void Start()
        {
            collider = GetComponent<BoxCollider>();
            Destroy(this.gameObject, maxLifeTime);
        }
        private void Update()
        {
            transform.position += transform.forward * (20 * Time.deltaTime);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(this.gameObject);
        }
    }
}

