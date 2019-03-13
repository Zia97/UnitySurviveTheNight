using System.Collections.Generic;
using UnityEngine;

namespace Assets.HeroEditor.Common.CharacterScripts
{
    /// <summary>
    /// General behaviour for projectiles: bullets, rockets and other.
    /// </summary>
    public class Projectile : MonoBehaviour
    {
        public List<Renderer> Renderers;
        public GameObject Trail;
        public GameObject Impact;
	    public Rigidbody Rigidbody;
        private int health = 100;


		public void Start()
        {
            gameObject.layer = 9;
            Destroy(gameObject, 5);
        }

	    public void Update()
	    {
		    if (Rigidbody != null && Rigidbody.useGravity)
		    {
			    transform.right = Rigidbody.velocity.normalized;
		    }
	    }

        public void OnTriggerEnter(Collider other)
        {
            Bang(other.gameObject);
        }


        void OnCollisionEnter2D(Collision2D collision)
        {
            Bang(collision.gameObject);

            if (collision.transform.gameObject.name != "wall")
            {
                if(gameObject.name.Equals("SniperBullet"))
                {
                    health = health - 30;
                }
                if(health<=0)
                {
                    Destroy(this.gameObject);
                }
               
            }
            else if(collision.transform.gameObject.name == "wall")
            {
                Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collision.transform.gameObject.GetComponent<Collider2D>());
            }
        }

        private void Bang(GameObject other)
        {
            ReplaceImpactSound(other);
            Impact.SetActive(true);
            Destroy(GetComponent<SpriteRenderer>());
            Destroy(GetComponent<Rigidbody>());
            Destroy(GetComponent<Collider>());
            Destroy(gameObject, 1);

            foreach (var ps in Trail.GetComponentsInChildren<ParticleSystem>())
            {
                ps.Stop();
            }

	        foreach (var tr in Trail.GetComponentsInChildren<TrailRenderer>())
	        {
		        tr.enabled = false;
			}
		}

        private void ReplaceImpactSound(GameObject other)
        {
            var sound = other.GetComponent<AudioSource>();

            if (sound != null && sound.clip != null)
            {
                Impact.GetComponent<AudioSource>().clip = sound.clip;
            }
        }
    }
}