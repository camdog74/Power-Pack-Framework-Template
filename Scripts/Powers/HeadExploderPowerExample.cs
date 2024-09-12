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
    public class HeadExploderPowerExample : PowerBase
    {
        /// Team Wilson's Power Pack Framework: Power Template

        public void Awake()
        {
            //We set our base info and values here
            PowerName = "Head Exploder";
            PowerDescription = "Explodes all heads in a nearby";
            Activation = "Passive";
        }
        public override void UsePower()
        {

            if (Enabled)
            {
                //Gets all objects in a certain radius.
                Collider2D[] InTrigger = Physics2D.OverlapCircleAll(transform.position, 5);

                foreach (var item in InTrigger)
                {
                   //Checks if the object is a limb, is a head and doesn't belong to the user.
                    if (item.GetComponent<LimbBehaviour>() && item.GetComponent<LimbBehaviour>().HasBrain && !GetComponent<LimbBehaviour>().Person.Limbs.Contains(item.GetComponent<LimbBehaviour>()))
                    {
                        //Creates the pop effect.
                        ModAPI.CreateParticleEffect("FuseBlown", item.transform.position);
                        //Destroys the head.
                        item.GetComponent<LimbBehaviour>().Crush();
                    }
                }
            }
        }

        /// <summary>
        /// Set up the power on a character.
        /// </summary>
        /// <param name="body">The selected limb of the user.</param>
        /// <param name="person">The PersonBehaviour of the user. (body.Person)</param>
        /// <param name="Instance">The overall instance of the user. ( body.Person.gameObject)</param>
        /// <param name="sprite">The power's icon.</param>
        public static void SetUpPower(LimbBehaviour body, PersonBehaviour person, GameObject Instance)
        {
            if (!body.GetComponent<HeadExploderPowerExample>())
            {
                body.gameObject.AddComponent<HeadExploderPowerExample>();

                body.GetComponent<HeadExploderPowerExample>().PowerIcon = ResourceStorage.HeadExploderPowerIcon;

                body.gameObject.AddComponent<UseEventTrigger>().Action = () =>
                {
                    body.gameObject.GetComponent<HeadExploderPowerExample>().UsePower();
                };
            }
        }
    }
}
