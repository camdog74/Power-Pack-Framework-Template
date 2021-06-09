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
    public class PowerTemplate : PowerBase
    {
        /// Team Wilson's Power Pack Framework: Power Template

        public void Start()
        {
            //We set our base info and values here
            PowerName = "Power name";
            //We set our power activation type (Head,Chest,Hand)
            Activation = "Head";

            //Always keep this
            base.Start();
        }
        
        //This is the actual power part of your power, this function will trigger when the activation limb is used.
        public override void UsePower()
        {
            //This if statement exists to check if your power is selected and enabled in the power menu
            if (Enabled)
            {

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
            //This checks if the person already has the power, if they don't we give it to them (duplicates are bad) NOTE:make sure to change the class to your new power class
            if (!body.GetComponent<PowerTemplate>())
            {
                //Adds your new power to the person NOTE:make sure to change the class to your new power class
                body.gameObject.AddComponent<PowerTemplate>();
                //This sets the power's icon, make sure you add the your new icon to the ResourceStorage NOTE:make sure to change the class to your new power class
                body.GetComponent<PowerTemplate>().PowerIcon = ResourceStorage.GetSpriteResource("PUT YOUR ICON HERE!");

                body.gameObject.AddComponent<UseEventTrigger>().Action = () =>
                {
                    //This triggers your power NOTE:make sure to change the class to your new power class
                    body.gameObject.GetComponent<PowerTemplate>().UsePower();
                };
            }
        }
    }
}
