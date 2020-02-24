using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.SpellSystem
{
    public class Spell_CircleArea : MonoBehaviour
    {
        SphereCollider collider;
        [SerializeField]
        private float maxRadius = 10f;
        private float maxScale = 10f;
        private float speedScale = 3f;
        private float lifetime = 3f;


        private void Start()
        {
            collider = GetComponent<SphereCollider>();
            Destroy(this.gameObject, lifetime);
        }
        private void Update()
        {
            Vector3 initialScale = this.transform.localScale;
            if (initialScale.x < maxScale)
            {
                this.transform.localScale = initialScale * (maxScale / speedScale) * Time.deltaTime;
            }
            
            
        }
    }
}

