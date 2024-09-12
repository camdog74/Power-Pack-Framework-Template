using UnityEngine;
namespace PPF
{
    //An empty class for you to copy paste and fill with your own powers!
    public class FlyingFistPowerExample : PowerBase
    {
        /// Team Wilson's Power Pack Framework: Power Template
        
        public float hitForce = 100;
        public float ProjectileSpeed = 6;
        public bool ParticleEffect = true;
        public string UseMessage = "Kerblam!";
        public Color FistColor = new Color(1f, 0.1336182f, 0f, 1f);

        public void Awake()
        {
            //We set our base info and values here
            PowerName = "Flying Fists";
            PowerDescription = "Shoots a fist the user's your hand";
            //We set our power activation type (Head,Chest,Hand)
            Activation = "Hand";
            //We add the hitForce variable to the power settings so we can change it in the power menu
            AddSliderPowerSetting("Hit Force","Changes the force of the fist",25,500,100,false,"SetHitForce");
            //We add the ProjectileSpeed variable to the power settings so we can change it in the power menu
            AddSliderPowerSetting("Projectile Speed","Changes the speed of the fist",2,20,6,false,"SetProjectileSpeed");
            //We add the ParticleEffect variable to the power settings so we can change it in the power menu
            AddTogglePowerSetting("Particle Effect","Enables or disables the particle effect",true,"SetParticleEffect");
            //we add the UseMessage variable to the power settings so we can change it in the power menu
            AddTextPowerSetting("Use Message","Changes the message that appears when you use the power","Kerblam!","SetUseMessage");
            //we add the FistColor variable to the power settings so we can change it in the power menu
            AddColorPowerSetting("Fist Color","Changes the color of the fist",new Color(1f, 0.1336182f, 0f, 1f),"SetFistColor");
        }
        public override void UsePower()
        {

            if (Enabled)
            {
                Debug.Log(UseMessage);
               //Creates an empty game object
                GameObject Projectile = new GameObject("Fist");
                //Add an audio source so we can play sounds from the object
                Projectile.AddComponent<AudioSource>().spatialBlend = 1;
                Projectile.GetComponent<AudioSource>().clip = ResourceStorage.FistAmb;
                Projectile.GetComponent<AudioSource>().loop = true;
                Projectile.GetComponent<AudioSource>().Play();
                Projectile.GetComponent<AudioSource>().PlayOneShot(ResourceStorage.FistShoot);
                //set the position to the user's hand
                Projectile.transform.position = transform.position;
                Projectile.transform.eulerAngles = transform.eulerAngles;
                //add a sprite to the object
                Projectile.AddComponent<SpriteRenderer>().sprite = ResourceStorage.BigFist;
                Projectile.GetComponent<SpriteRenderer>().color = FistColor;
                //Add a collider to the object
                Projectile.AddComponent<PolygonCollider2D>();
                Projectile.AddComponent<Rigidbody2D>().gravityScale = 0;
                //Add constant force to the object
                Projectile.AddComponent<ConstantForce2D>().force = -transform.up * ProjectileSpeed;
                //set its initial force so it spawns flying
                Projectile.GetComponent<Rigidbody2D>().velocity = -transform.up * ProjectileSpeed;
                //Sets the objects layer to the objects layer so it can interact with everything
                Projectile.layer = 9;
                //changes the scale
                Projectile.transform.localScale = Vector3.one * 0.4f;
                GameObject ParticleObject = new GameObject();
                ParticleObject.transform.SetParent(Projectile.transform, false);
                ParticleObject.transform.position = Projectile.transform.position;
                ParticleObject.transform.eulerAngles = Projectile.transform.eulerAngles;
                Projectile.AddComponent<FistProjectile>().ParticleObject = Projectile.gameObject;
                Projectile.GetComponent<FistProjectile>().HitForce = hitForce;
                foreach (var item in GetComponent<LimbBehaviour>().Person.Limbs)
                {
                    Physics2D.IgnoreCollision(item.GetComponent<Collider2D>(), Projectile.GetComponent<PolygonCollider2D>());
                }




                if(ParticleEffect)
                {
                    //The following code was generated with Camdog74's People Play-Tools (currently not for public use)
                ParticleSystem particleSystem = ParticleObject.AddComponent<ParticleSystem>();
                ParticleObject.GetComponent<ParticleSystemRenderer>().material = new Material(ModAPI.FindMaterial("Sprites-Default"));
                ParticleObject.GetComponent<ParticleSystemRenderer>().material.mainTexture = ResourceStorage.FistParticles;
                ParticleObject.GetComponent<ParticleSystemRenderer>().sortingLayerName = "Foreground";
                ParticleSystem.MainModule main = particleSystem.main;
                ParticleSystem.ShapeModule shape = particleSystem.shape;
                ParticleSystem.EmissionModule emission = particleSystem.emission;
                ParticleSystem.VelocityOverLifetimeModule velocityOverLifetimeModule = particleSystem.velocityOverLifetime;
                ParticleSystem.SizeOverLifetimeModule sizeOverLifetimeModule = particleSystem.sizeOverLifetime;
                ParticleSystem.ColorOverLifetimeModule colorOverLifetimeModule = particleSystem.colorOverLifetime;
                main.duration = 2.69f;
                main.loop = true;
                main.startDelay = 0f;
                main.startLifetime = 0.48f;
                main.startSpeed = 0f;
                main.startSize3D = false;
                main.startRotationX = 3.141593f;
                main.startRotationY = 0f;
                main.startRotationZ = 4.712389f;
                main.startSize = 2.56f;
                main.startRotation3D = true;
                main.flipRotation = 0;
                main.startColor = FistColor;
                main.gravityModifier = 0f;
                main.simulationSpace = ParticleSystemSimulationSpace.World;
                main.simulationSpeed = 1;
                main.scalingMode = ParticleSystemScalingMode.Local;
                main.playOnAwake = true;
                main.emitterVelocityMode = ParticleSystemEmitterVelocityMode.Transform;
                main.maxParticles = 1000;
                main.stopAction = ParticleSystemStopAction.Destroy;
                main.cullingMode = ParticleSystemCullingMode.Automatic;
                main.ringBufferMode = ParticleSystemRingBufferMode.Disabled;
                emission.rateOverTime = 29.47f;
                emission.rateOverDistance = 22.4f;
                shape.shapeType = ParticleSystemShapeType.Box;
                shape.angle = 25;
                shape.radius = 1;
                shape.radiusThickness = 1;
                shape.arc = 360;
                shape.arcMode = ParticleSystemShapeMultiModeValue.Random;
                shape.arcSpread = 0;
                shape.length = 5;
                shape.position = new Vector3(0f, -0.77f, -1.04f);
                shape.rotation = new Vector3(0f, 0f, 0f);
                shape.scale = new Vector3(1.76f, 1.03f, 0.17f);
                shape.alignToDirection = true;
                shape.randomDirection = false;
                shape.sphericalDirectionAmount = 0;
                shape.randomPositionAmount = 0;
                sizeOverLifetimeModule.enabled = true;
                sizeOverLifetimeModule.x.curve.AddKey(0f, 0.6671295f);
                sizeOverLifetimeModule.x.curve.AddKey(1f, 1f);
                sizeOverLifetimeModule.y.curve.AddKey(0f, 0f);
                sizeOverLifetimeModule.y.curve.AddKey(1f, 1f);
                colorOverLifetimeModule.enabled = true;
                //fade from transparent to the color of the fist variable and then fade back to transparent
                colorOverLifetimeModule.color = new ParticleSystem.MinMaxGradient(new Gradient()
                {
                    alphaKeys = new GradientAlphaKey[] { new GradientAlphaKey(0, 0), new GradientAlphaKey(1, 0.5f), new GradientAlphaKey(0, 1) },
                    colorKeys = new GradientColorKey[] { new GradientColorKey(FistColor, 0), new GradientColorKey(FistColor, 0.5f), new GradientColorKey(FistColor, 1) }
                });
                }
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
            if (!body.GetComponent<FlyingFistPowerExample>())
            {
                body.gameObject.AddComponent<FlyingFistPowerExample>();

                body.GetComponent<FlyingFistPowerExample>().PowerIcon = ResourceStorage.FlyingFistIcon;

                body.gameObject.AddComponent<UseEventTrigger>().Action = () =>
                {
                    body.gameObject.GetComponent<FlyingFistPowerExample>().UsePower();
                };
            }
        }

        public void SetHitForce(float value)
        {
            hitForce = value;
            Debug.Log("Hit Force set to " + value);
        }
        public void SetProjectileSpeed(float value)
        {
            ProjectileSpeed = value;
            Debug.Log("Projectile Speed set to " + value);
        }
        public void SetParticleEffect(bool value)
        {
            ParticleEffect = value;
            Debug.Log("Particle Effect set to " + value);
        }
        public void SetUseMessage(string value)
        {
            UseMessage = value;
            Debug.Log("Use Message set to " + value);
        }
        public void SetFistColor(Color value)
        {
            FistColor = value;
            Debug.Log("Fist Color set to " + value);
        }
    }

    public class FistProjectile : MonoBehaviour
    {
        public float HitForce = 100;
        public GameObject ParticleObject;
        public void OnCollisionEnter2D(Collision2D collision2D)
        {
            if (collision2D.gameObject.GetComponent<Rigidbody2D>())
            {
                if (collision2D.gameObject.GetComponent<LimbBehaviour>())
                {
                    foreach (var item in collision2D.gameObject.GetComponent<LimbBehaviour>().Person.Limbs)
                    {
                        item.PhysicalBehaviour.rigidbody.AddForce(-transform.up * (HitForce / 4));
                        item.Damage(5);
                    }
                }
                    else collision2D.gameObject.GetComponent<Rigidbody2D>().AddForce(-transform.up * HitForce);
                ModAPI.CreateParticleEffect("BigExplosion", transform.position);
                Destroy(gameObject);
            }
        }
    }
}
