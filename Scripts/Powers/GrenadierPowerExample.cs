using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Reflection;
namespace PPF
{
    //An empty class for you to copy paste and fill with your own powers!
    public class GrenadierPowerExample : PowerBase
    {
        /// Team Wilson's Power Pack Framework: Power Template

        public void Awake()
        {
            //We set our base info and values here
            PowerName = "Grenadier";
            //We set our power activation type (Head,Chest,Hand)
            Activation = "Hand";
        }
        public override void UsePower()
        {

            if (Enabled)
            {
                GameObject Projectile = Instantiate(ModAPI.FindSpawnable("Handgrenade").Prefab);
                Projectile.GetComponent<PhysicalBehaviour>().SpawnSpawnParticles = false;
                Projectile.AddComponent<AudioSource>().spatialBlend = 1;
                Projectile.transform.position = transform.position;
                Projectile.transform.eulerAngles = transform.eulerAngles;
                Projectile.GetComponent<Rigidbody2D>().velocity = -transform.up * 12;
                foreach (var item in GetComponent<LimbBehaviour>().Person.Limbs)
                {
                    Physics2D.IgnoreCollision(Projectile.GetComponent<Collider2D>(),item.Collider);
                }
                Projectile.GetComponent<PhysicalBehaviour>().ForceSendUse();
            }
        }

        /// <summary>
        /// Set up the power on a character.
        /// </summary>
        /// <param name="body">The selected limb of the user.</param>
        /// <param name="person">The PersonBehaviour of the user. (body.Person)</param>
        /// <param name="Instance">The overall instance of the user. ( body.Person.gameObject)</param>
        public static void SetUpPower(LimbBehaviour body, PersonBehaviour person, GameObject Instance)
        {
            if (!body.GetComponent<GrenadierPowerExample>())
            {
                body.gameObject.AddComponent<GrenadierPowerExample>();

                     body.GetComponent<GrenadierPowerExample>().PowerIcon = ResourceStorage.GetSpriteResource("Grenadier Icon");

                body.gameObject.AddComponent<UseEventTrigger>().Action = () =>
                {
                    body.gameObject.GetComponent<GrenadierPowerExample>().UsePower();
                };
            }
        }
    }
}
