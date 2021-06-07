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
    public class ThickSkinPowerExample : PowerBase
    {
        /// Team Wilson's Power Pack Framework: Power Template

        //This power is a good example to show powers that turn on and off


        public void Awake()
        {
            //We set our base info and values here
            PowerName = "Thick Skin";
            //We set our power activation type (Head,Chest,Hand)
            Activation = "Chest";
        }

        public List<GameObject> ProtectorObjects = new List<GameObject>();

        //this is to show if its on or off
        public bool PowerOn = false;

        float OgBreakingThreshold;
        float OgShotDamageMultiplier;
        GameObject OgShotImpact;
        float OgitemHealth;
        float OgInitialHealth;
        LightSprite Light;
        public override void UsePower()
        {

            if (Enabled)
            {
                if (!PowerOn)
                {
                    foreach (var item in GetComponent<LimbBehaviour>().Person.Limbs)
                    {
                        //Gather original limb info so we can revert them
                        OgShotDamageMultiplier = item.ShotDamageMultiplier;
                        OgShotImpact = item.PhysicalBehaviour.Properties.ShotImpact;
                        OgBreakingThreshold = item.BreakingThreshold;
                        OgitemHealth = item.Health;
                        OgInitialHealth = item.InitialHealth;

                        if (item.HasBrain)
                            //create a fancy light
                            Light = ModAPI.CreateLight(item.transform, Color.red, 3, 5);
                        //Make no blood particle appear when shot
                        item.PhysicalBehaviour.Properties.ShotImpact = ModAPI.FindPhysicalProperties("Bowling Pin").ShotImpact;
                        item.ShotDamageMultiplier = 0;
                        item.BreakingThreshold = 9999999;
                        item.Health = 9999;
                        item.InitialHealth = 9999;
                        item.gameObject.AddComponent<UnShootable>();

                    }

                    PowerOn = true;
                }
                else 
                {
                    foreach (var item in GetComponent<LimbBehaviour>().Person.Limbs)
                    {
                        //Set everything back to normal
                        if (Light != null)
                            Destroy(Light.gameObject);
                        item.PhysicalBehaviour.Properties.ShotImpact = OgShotImpact;
                        item.ShotDamageMultiplier = OgShotDamageMultiplier;
                        item.BreakingThreshold = OgBreakingThreshold;
                        item.Health = OgitemHealth;
                        item.InitialHealth = OgInitialHealth;
                        Destroy(item.gameObject.GetComponent<UnShootable>());
                        PowerOn = false;
                    }
                }

            }
        }
        //Since its a toggle power, gotta override DisablePower and include turning off the power if its on
        public override void DisablePower()
        {
            if (PowerOn)
            {
                foreach (var item in GetComponent<LimbBehaviour>().Person.Limbs)
                {
                    if (Light != null)
                        Destroy(Light.gameObject);
                    item.PhysicalBehaviour.Properties.ShotImpact = OgShotImpact;
                    item.ShotDamageMultiplier = OgShotDamageMultiplier;
                    item.BreakingThreshold = OgBreakingThreshold;
                    item.Health = OgitemHealth;
                    item.InitialHealth = OgInitialHealth;
                    Destroy(item.gameObject.GetComponent<UnShootable>());

                    PowerOn = false;
                }
            }
            base.DisablePower();
        }

        /// <summary>
        /// Set up the power on a character.
        /// </summary>
        /// <param name="body">The selected limb of the user.</param>
        /// <param name="person">The PersonBehaviour of the user. (body.Person)</param>
        /// <param name="Instance">The overall instance of the user. ( body.Person.gameObject)</param>
        public static void SetUpPower(LimbBehaviour body, PersonBehaviour person, GameObject Instance)
        {
            if (!body.GetComponent<ThickSkinPowerExample>())
            {
                body.gameObject.AddComponent<ThickSkinPowerExample>();

                body.GetComponent<ThickSkinPowerExample>().PowerIcon = ResourceStorage.GetSpriteResource("Thick Skin Icon");

                body.gameObject.AddComponent<UseEventTrigger>().Action = () =>
                {
                    body.gameObject.GetComponent<ThickSkinPowerExample>().UsePower();
                };
            }
        }
    }

    public class UnShootable : MonoBehaviour
    {
        float Health;
        float BloodAmount;

        public void Start()
        {
            Health = GetComponent<LimbBehaviour>().Health;
            BloodAmount = GetComponent<LimbBehaviour>().CirculationBehaviour.BloodAmount;
        }
        
        public void Update()
        {
            //Checks if any damage happens
            if(GetComponent<LimbBehaviour>().CirculationBehaviour.GunshotWoundCount > 0 || GetComponent<LimbBehaviour>().CirculationBehaviour.BleedingPointCount > 0 || GetComponent<LimbBehaviour>().CirculationBehaviour.StabWoundCount > 0 && GetComponent<LimbBehaviour>().BruiseCount > 0)
            {
                foreach (var item in GetComponent<LimbBehaviour>().Person.Limbs)
                {
                    if (item.GetComponent<UnShootable>())
                        item.GetComponent<UnShootable>().HealEverything();
                }

            }

        }
        //Literally heal almost everything
        public void HealEverything()
        {
            GetComponent<LimbBehaviour>().CirculationBehaviour.HealBleeding();
            GetComponent<LimbBehaviour>().CirculationBehaviour.IsPump = true;
            GetComponent<LimbBehaviour>().CirculationBehaviour.IsDisconnected = false;
            GetComponent<LimbBehaviour>().Person.Consciousness = 1;
            GetComponent<LimbBehaviour>().Health = Health;
            GetComponent<LimbBehaviour>().PhysicalBehaviour.BurnProgress = 0;
            GetComponent<LimbBehaviour>().Person.ShockLevel = 0;
            GetComponent<LimbBehaviour>().Person.PainLevel = 0;
            GetComponent<LimbBehaviour>().Numbness = 0;
            GetComponent<LimbBehaviour>().CirculationBehaviour.BleedingRate = 0;
            GetComponent<LimbBehaviour>().CirculationBehaviour.GunshotWoundCount = 0;
            GetComponent<LimbBehaviour>().CirculationBehaviour.StabWoundCount = 0;
            GetComponent<LimbBehaviour>().BruiseCount= 0;
            GetComponent<LimbBehaviour>().SkinMaterialHandler.ClearAllDamage();
        }
    }

}
