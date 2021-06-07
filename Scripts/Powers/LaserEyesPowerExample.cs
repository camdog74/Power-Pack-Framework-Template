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
    public class LaserEyesPowerExample : PowerBase
    {
        /// Team Wilson's Power Pack Framework: Example
        /// This is an example script that allows characters to shoot laser's from their heads
        public AudioClip LaserSound;
        public AudioSource audio;
        public Color LaserColor;

        public void Awake()
        {
            //We set our base info and values here
            PowerName = "Laser Eyes";
            Activation = "Head";
            audio = gameObject.AddComponent<AudioSource>();
        }


        public override void UsePower()
        {

            if (Enabled)
            {
                //Create the laser which is the Blaser's bolt
                GameObject LaserObject = Instantiate(ModAPI.FindSpawnable("Blaster").Prefab.GetComponent<BlasterBehaviour>().Bolt);

                //Sets the laser's color
                LaserObject.GetComponent<BlasterboltBehaviour>().Trail.startColor = LaserColor;
                LaserObject.GetComponent<BlasterboltBehaviour>().Trail.endColor = LaserColor;

                //We create a little particle effect when the shot happens because it looks cool
                ModAPI.CreateParticleEffect("Vapor", gameObject.transform.position);


                //We set the audio clip and play it
                audio.clip = LaserSound;
                audio.Play();


                //Set the laser's postion is to the object that is shooting
                LaserObject.transform.position = gameObject.transform.position;


                //This is to check what direction the character is facing, so we can shoot it in the right direction
                if (gameObject.GetComponent<LimbBehaviour>().Person.transform.localScale.x < 0)
                    LaserObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z + 180);
                else LaserObject.transform.eulerAngles = gameObject.transform.eulerAngles;
            }
        }

        /// <summary>
        /// Set up the power on a character.
        /// </summary>
        /// <param name="body">The selected limb of the user.</param>
        /// <param name="person">The PersonBehaviour of the user. (body.Person)</param>
        /// <param name="Instance">The overall instance of the user. ( body.Person.gameObject)</param>
        /// <param name="sprite">The power's icon.</param>
        /// <param name="LaserColor">The color of the shot laser.</param>
        public static void SetUpPower(LimbBehaviour body, PersonBehaviour person, GameObject Instance, Color LaserColor)
        {
            if (!body.GetComponent<LaserEyesPowerExample>())
            {
                //Adds the power/script and sets the laser sound at the same time
                body.gameObject.AddComponent<LaserEyesPowerExample>().LaserSound = ResourceStorage.GetSoundResource("Laser Shoot");


                body.gameObject.GetComponent<LaserEyesPowerExample>().LaserColor = LaserColor;

                //Sets the powers activation
                body.gameObject.AddComponent<UseEventTrigger>().Action = () =>
                {
                    body.gameObject.GetComponent<LaserEyesPowerExample>().UsePower();
                };

                body.GetComponent<LaserEyesPowerExample>().PowerIcon = ResourceStorage.GetSpriteResource("Laser Eyes Power Icon"); ;
            }
        }
    }
}
