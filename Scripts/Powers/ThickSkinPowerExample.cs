using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
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
            PowerDescription = "Makes the user invincible";
            //We set our power activation type (Head,Chest,Hand)
            Activation = "Passive";
        }

        public List<GameObject> ProtectorObjects = new List<GameObject>();

        //this is to show if its on or off
        LightSprite Light;
        //Since its a toggle power, gotta override DisablePower and include turning off the power if its on
        public override void DisablePower()
        {
            foreach (var limb in GetComponent<LimbBehaviour>().Person.Limbs)
            {
                limb.ImmuneToDamage = true;
            }
            base.DisablePower();
        }

        public override void EnablePower()
        {
            foreach (var limb in GetComponent<LimbBehaviour>().Person.Limbs)
            {
                limb.ImmuneToDamage = false;
            }
            base.EnablePower();
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

                body.GetComponent<ThickSkinPowerExample>().PowerIcon = ResourceStorage.ThickSkinIcon;

                body.gameObject.AddComponent<UseEventTrigger>().Action = () =>
                {
                    body.gameObject.GetComponent<ThickSkinPowerExample>().UsePower();
                };
            }
        }
    }

}
