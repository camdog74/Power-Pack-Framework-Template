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
    public class HealerExamplePower : PowerBase
    {
        /// Team Wilson's Power Pack Framework: Power Template
        public Texture2D Healthy;
        public void Awake()
        {
            //We set our base info and values here
            PowerName = "Heal Pulse";
            Activation = "Chest";
        }
        public override void UsePower()
        {

            if (Enabled)
            {
               //Gets all objects in a certain radius
                Collider2D[] InTrigger = Physics2D.OverlapCircleAll(transform.position, 5);

                //Foreach of the objects found, we inject them with the health syringe and apply a particle effect
                foreach (var item in InTrigger)
                {
                    if (item.GetComponent<LimbBehaviour>() && !GetComponent<LimbBehaviour>().Person.Limbs.Contains(item.GetComponent<LimbBehaviour>()))
                    {
                        PoisonSpreadBehaviour poisonSpreadBehaviour = item.gameObject.AddComponent<LifePoison>() as PoisonSpreadBehaviour;
                        poisonSpreadBehaviour.Limb = item.GetComponent<LimbBehaviour>();

                        //The following code was generated with Camdog74's People Play-Tools (not ready for public use)
                        GameObject ParticleObject = new GameObject();
                        ParticleObject.transform.SetParent(item.transform, false);
                        // The following code was generated with Camdog74's People Play-Tools
                        ParticleSystem particleSystem = ParticleObject.AddComponent<ParticleSystem>();
                        ParticleObject.GetComponent<ParticleSystemRenderer>().material = new Material(ModAPI.FindMaterial("Sprites-Default"));
                        ParticleObject.GetComponent<ParticleSystemRenderer>().material.mainTexture = Healthy;
                        ParticleObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = "Foreground";
                        ParticleSystem.MainModule main = particleSystem.main;
                        ParticleSystem.ShapeModule shape = particleSystem.shape;
                        ParticleSystem.EmissionModule emission = particleSystem.emission;
                        ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule = particleSystem.velocityOverLifetime;
                        ParticleSystem.SizeOverLifetimeModule sizeOverLifetimeModule = particleSystem.sizeOverLifetime;
                        ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule = particleSystem.colorOverLifetime;
                        main.duration = 2.69f;
                        main.loop = false;
                        main.startDelay = 0f;
                        main.startLifetime = 0.75f;
                        main.startSpeed = 0f;
                        main.startSize3D = false;
                        main.startSize = 0.13f;
                        main.startRotation3D = false;
                        main.flipRotation = 0;
                        main.startColor = new Color(0.3030837f, 1f, 0f, 1f);
                        main.gravityModifier = 0f;
                        main.simulationSpace = ParticleSystemSimulationSpace.Local;
                        main.simulationSpeed = 1;
                        main.scalingMode = ParticleSystemScalingMode.Local;
                        main.playOnAwake = true;
                        main.emitterVelocityMode = ParticleSystemEmitterVelocityMode.Transform;
                        main.maxParticles = 1000;
                        main.stopAction = ParticleSystemStopAction.Destroy;
                        main.cullingMode = ParticleSystemCullingMode.Automatic;
                        main.ringBufferMode = ParticleSystemRingBufferMode.Disabled;
                        emission.rateOverTime = 10;
                        emission.rateOverDistance = 0;
                        shape.shapeType = ParticleSystemShapeType.Box;
                        shape.angle = 25;
                        shape.radius = 1;
                        shape.radiusThickness = 1;
                        shape.arc = 360;
                        shape.arcMode = ParticleSystemShapeMultiModeValue.Random;
                        shape.arcSpread = 0;
                        shape.length = 5;
                        shape.position = new Vector3(0f, 0f, -1.04f);
                        shape.rotation = new Vector3(0f, 0f, 0f);
                        shape.scale = new Vector3(0.35f, 0.32f, 0.17f);
                        shape.alignToDirection = false;
                        shape.randomDirection = false;
                        shape.sphericalDirectionAmount = 0;
                        shape.randomPositionAmount = 0;
                        sizeOverLifetimeModule.enabled = true;
                        sizeOverLifetimeModule.x.curve.AddKey(0f, 0.4923096f);
                        sizeOverLifetimeModule.x.curve.AddKey(1f, 1f);
                        sizeOverLifetimeModule.y.curve.AddKey(0f, 0f);
                        sizeOverLifetimeModule.y.curve.AddKey(1f, 1f);
                        colorOverLifetimeModule.enabled = true;
                        Gradient grad = new Gradient();
                        grad.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(1, 1, 1, 1), 0), new GradientColorKey(new Color(1, 1, 1, 1), 1) }, new GradientAlphaKey[] { new GradientAlphaKey(0f, 0f), new GradientAlphaKey(1f, 0.1353018f), new GradientAlphaKey(0f, 1f) });
                        colorOverLifetimeModule.color = grad;
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
            if (!body.GetComponent<HealerExamplePower>())
            {
                //CHANGE THIS TO YOUR NEW CLASS
                body.gameObject.AddComponent<HealerExamplePower>().PowerIcon = ResourceStorage.HealerPowerIcon;
                body.gameObject.GetComponent<HealerExamplePower>().Healthy = ResourceStorage.HealerParticles;
                

                //Sets the powers activation
                body.gameObject.AddComponent<UseEventTrigger>().Action = () =>
                {
                    body.GetComponent<HealerExamplePower>().UsePower();
                };
            }
        }
    }
}
