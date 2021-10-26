using UnityEngine;
namespace PPF
{
    //An empty class for you to copy paste and fill with your own powers!
    public class FlyingFistPowerExample : PowerBase
    {
        /// Team Wilson's Power Pack Framework: Power Template

        public void Awake()
        {
            //We set our base info and values here
            PowerName = "Flying Fists";
            //We set our power activation type (Head,Chest,Hand)
            Activation = "Hand";
        }
        public override void UsePower()
        {

            if (Enabled)
            {
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
                Projectile.GetComponent<SpriteRenderer>().color = Color.clear;
                //Add a collider to the object
                Projectile.AddComponent<PolygonCollider2D>();
                Projectile.AddComponent<Rigidbody2D>().gravityScale = 0;
                //Add constant force to the object
                Projectile.AddComponent<ConstantForce2D>().force = -transform.up * 6;
                //set its initial force so it spawns flying
                Projectile.GetComponent<Rigidbody2D>().velocity = -transform.up * 6;
                //Sets the objects layer to the objects layer so it can interact with everything
                Projectile.layer = 9;
                //changes the scale
                Projectile.transform.localScale = Vector3.one * 0.4f;
                GameObject ParticleObject = new GameObject();
                ParticleObject.transform.SetParent(Projectile.transform, false);
                ParticleObject.transform.position = Projectile.transform.position;
                ParticleObject.transform.eulerAngles = Projectile.transform.eulerAngles;
                Projectile.AddComponent<FistProjectile>().ParticleObject = Projectile.gameObject;
                foreach (var item in GetComponent<LimbBehaviour>().Person.Limbs)
                {
                    Physics2D.IgnoreCollision(item.GetComponent<Collider2D>(), Projectile.GetComponent<PolygonCollider2D>());
                }




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
                main.startColor = new Color(1f, 0.1336182f, 0f, 1f);
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
                Gradient grad = new Gradient();
                grad.SetKeys(new GradientColorKey[] { new GradientColorKey(new Color(1, 1, 1, 1), 0), new GradientColorKey(new Color(1, 1, 1, 1), 1) }, new GradientAlphaKey[] { new GradientAlphaKey(0f, 0f), new GradientAlphaKey(0.98063f, 0.4382391f), new GradientAlphaKey(0f, 1f) });
                colorOverLifetimeModule.color = grad;
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

        public void Update()
        {
            GetComponent<SpriteRenderer>().color = Color.Lerp(GetComponent<SpriteRenderer>().color, Color.red, 0.5f);
        }
    }
}
