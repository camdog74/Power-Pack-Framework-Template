using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
namespace PPF
{
    //An empty class for you to copy paste and fill with your own powers!
    public class GrenadierPowerExample : PowerBase, Messages.IUse
    {
        /// Team Wilson's Power Pack Framework: Power Template

        public float ThrowForce = 12;
        public GameObject GrenadePrefab;
        float ActivationDelay = 0;
        public void Awake()
        {
            //We set our base info and values here
            PowerName = "Grenadier";
            PowerDescription = "Throws a grenade";
            //We set our power activation type (Head,Chest,Hand)
            Activation = "Hand";

            //Set the default prefab for the grenade
            GrenadePrefab = ModAPI.FindSpawnable("Handgrenade").Prefab;

            //Add a slider to the power settings so we can change the throw force
            AddSliderPowerSetting("Throw Force", "The force of the throw", 0, 20, 12, false, "SetThrowForce");

            List<OptionListOption> options = new List<OptionListOption>();

            //Handgrenade
            options.Add(new OptionListOption("Handgrenade", ModAPI.FindSpawnable("Handgrenade").Description, ModAPI.FindSpawnable("Handgrenade").ViewSprite, false, new UnityAction(() =>
            {
                GrenadePrefab = ModAPI.FindSpawnable("Handgrenade").Prefab;
                ActivationDelay = 0;
                ThrowForce = 12;
            })));
            //Plastic Explosive
            options.Add(new OptionListOption("Plastic Explosive", ModAPI.FindSpawnable("Plastic Explosive").Description, ModAPI.FindSpawnable("Plastic Explosive").ViewSprite, false, new UnityAction(() =>
            {
                GrenadePrefab = ModAPI.FindSpawnable("Plastic Explosive").Prefab;
                ActivationDelay = 1.5f;
                ThrowForce = 20;
            })));
            //Sticky Grenade
            options.Add(new OptionListOption("Sticky Grenade", ModAPI.FindSpawnable("Sticky Grenade").Description, ModAPI.FindSpawnable("Sticky Grenade").ViewSprite, false, new UnityAction(() =>
            {
                GrenadePrefab = ModAPI.FindSpawnable("Sticky Grenade").Prefab;
                ActivationDelay = 0.1f;
                ThrowForce = 12;
            })));
            //Dynamite
            options.Add(new OptionListOption("Dynamite", ModAPI.FindSpawnable("Dynamite").Description, ModAPI.FindSpawnable("Dynamite").ViewSprite, false, new UnityAction(() =>
            {
                GrenadePrefab = ModAPI.FindSpawnable("Dynamite").Prefab;
                ActivationDelay = 2f;
                ThrowForce = 24;
            })));


            AddOptionListPowerSetting("Grenade Type", "The type of grenade to throw",new Vector2(300,300), options, 0);
        }
        public void Use(ActivationPropagation activation)
        {
            if (Enabled)
            {
                GameObject Projectile = Instantiate(GrenadePrefab);
                Projectile.GetComponent<PhysicalBehaviour>().SpawnSpawnParticles = false;
                Projectile.AddComponent<AudioSource>().spatialBlend = 1;
                Projectile.transform.position = transform.position;
                Projectile.transform.eulerAngles = transform.eulerAngles;
                Projectile.GetComponent<Rigidbody2D>().velocity = -transform.up * ThrowForce;
                foreach (var item in GetComponent<LimbBehaviour>().Person.Limbs)
                {
                    Physics2D.IgnoreCollision(Projectile.GetComponent<Collider2D>(), item.Collider);
                }
                Projectile.GetComponent<PhysicalBehaviour>().Invoke("ForceSendUse", ActivationDelay);
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

                body.GetComponent<GrenadierPowerExample>().PowerIcon = ResourceStorage.GrenadierIcon;

                body.gameObject.AddComponent<UseEventTrigger>().Action = () =>
                {
                    body.gameObject.GetComponent<GrenadierPowerExample>().UsePower();
                };
            }
        }

        public void SetThrowForce(float value)
        {
            ThrowForce = value;
        }
    }
}
