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
    // _________ _______  _______  _______            _________ _        _______  _______  _       
    // \__   __/(  ____ \(  ___  )(       )  |\     /|\__   __/( \      (  ____ \(  ___  )( (    /|  
    //    ) (   | (    \/| (   ) || () () |  | )   ( |   ) (   | (      | (    \/| (   ) ||  \  ( |  
    //    | |   | (__    | (___) || || || |  | | _ | |   | |   | |      | (_____ | |   | ||   \ | |  
    //    | |   |  __)   |  ___  || |(_)| |  | |( )| |   | |   | |      (_____  )| |   | || (\ \) |  
    //    | |   | (      | (   ) || |   | |  | || || |   | |   | |            ) || |   | || | \   |  
    //    | |   | (____/\| )   ( || )   ( |  | () () |___) (___| (____/\/\____) || (___) || )  \  |  
    //    )_(   (_______/|/     \||/     \|  (_______)\_______/(_______/\_______)(_______)|/    )_)  
    //
    // This framework was created by Team Wilson and is allowed to be used in any mod as long as The Power Pack Framework is a required mod (otherwise may be errors).
    // Link to PPF: https://steamcommunity.com/sharedfiles/filedetails/?id=2506978276
    // Framework coded by 🥧 Camdog74 🥧
    // VERSION 3.0.0


    //This is where you store all of your assets like your sprites, textures and sounds. (power icons too)
    public class ResourceStorage : MonoBehaviour
    {
        public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();
        public Dictionary<string, AudioClip> Sounds = new Dictionary<string, AudioClip>();



        public static Sprite HealerPowerIcon;
        public static Sprite HeadExploderPowerIcon;
        public static Sprite LaserEyesPowerIcon;
        public static Sprite FlyingFistIcon;
        public static Sprite GrenadierIcon;
        public static Sprite ThickSkinIcon;
        public static Sprite BigFist;
        public static Sprite CapeBase;
        public static Sprite CapeTexture;

        public static Texture2D HealerParticles;
        public static Texture2D FistParticles;

        public static AudioClip LaserShoot;
        public static AudioClip FistShoot;
        public static AudioClip FistAmb;
    }

    public class PPF
    {
        public static void Main()
        {
            ResourceStorage.HealerPowerIcon = ModAPI.LoadSprite("Art/Icons/Healer.png");
            ResourceStorage.HeadExploderPowerIcon = ModAPI.LoadSprite("Art/Icons/Head Exploder.png");
            ResourceStorage.LaserEyesPowerIcon = ModAPI.LoadSprite("Art/Icons/Laser.png");
            ResourceStorage.FlyingFistIcon = ModAPI.LoadSprite("Art/Icons/Flying Fist.png");
            ResourceStorage.GrenadierIcon = ModAPI.LoadSprite("Art/Icons/Grenadier.png");
            ResourceStorage.ThickSkinIcon = ModAPI.LoadSprite("Art/Icons/Thick Skin.png");
            ResourceStorage.BigFist = ModAPI.LoadSprite("Art/Particles/Fist - Copy.png");
            ResourceStorage.CapeBase = ModAPI.LoadSprite("Art/Cape/cape01.png");
            ResourceStorage.CapeTexture = ModAPI.LoadSprite("Art/Cape/cape02.png");
            ResourceStorage.HealerParticles = ModAPI.LoadTexture("Art/Particles/Health.png");
            ResourceStorage.FistParticles = ModAPI.LoadTexture("Art/Particles/Fist.png");
            ResourceStorage.LaserShoot = ModAPI.LoadSound("Sounds/LazerBlast.wav");
            ResourceStorage.FistShoot = ModAPI.LoadSound("Sounds/Fist Shot.wav");
            ResourceStorage.FistAmb = ModAPI.LoadSound("Sounds/Fist_Amb.wav");



            //Make sure to add new ones when you need to use them instead of using ModAPI.Load in AfterSpawn.
            GameObject ResourceManager = new GameObject();
            ResourceManager.AddComponent<ResourceStorage>();

            //Feel free to replace all these items/characters with your own.

            #region People
            // Example Power Character
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Human"), //item to derive from
                    NameOverride = "Example Powered Person", //new item name with a suffix to assure it is globally unique
                    DescriptionOverride = "An example character that shoots lasers from the Power Pack Framework(PPF)", //new item description
                    CategoryOverride = ModAPI.FindCategory("Entities"), //new item category
                //   ThumbnailOverride = ModAPI.LoadSprite("blueMan.png"), //new item thumbnail (relative path)
                    AfterSpawn = (Instance) => //all code in the AfterSpawn delegate will be executed when the item is spawned
                    {

                        //get person
                        var person = Instance.GetComponent<PersonBehaviour>();

                        //Gives laser eyes power to head
                        LaserEyesPowerExample.SetUpPower(person.Limbs[0], person, Instance, Color.green);
                        //Gives Healer power to chest
                        HealerExamplePower.SetUpPower(person.Limbs[1], person, Instance);

                        //Creates a cape and gives it to the person and also stores it as a variable.
                        var Cape = PowerPackFrameworkFunctions.CreateCape(person, ResourceStorage.CapeTexture, 0.15f, ResourceStorage.CapeBase);

                        //The three lines below are examples of how to add custom skins to the character.
                        PowerPackFrameworkFunctions.AddSkin(person, ModAPI.LoadTexture("Art/Skins/Wilson/Skin Layer.png"), "TestSkin", "This is Wilson. He's a test skin in this case, but most importantly <color=red>Team Wilson's<color=white> mascot!");
                        PowerPackFrameworkFunctions.AddSkin(person, ModAPI.LoadTexture("Art/Skins/Wilson/skin_layer_1.png"), "BatmanSkin", "This is Wilson, except his parents are dead.");
                        PowerPackFrameworkFunctions.AddSkin(person, ModAPI.LoadTexture("Art/Skins/Wilson/Wilson.png"), "JediSkin", "This is Wilson, except he's green now.");
                        //These two lines of code are how you add a custom limb to the character, in this case, it's custom heads. (Added in v2)
                        PowerPackFrameworkFunctions.AddCustomizedLimbToSkin(person, 0, "BatmanSkin", ModAPI.LoadSprite("Art/Skins/Wilson/Head.png"));
                        PowerPackFrameworkFunctions.AddCustomizedLimbToSkin(person, 0, "JediSkin", ModAPI.LoadSprite("Art/Skins/Wilson/Head 2.png"));

                        //this is how you set a custom thumbnail for a skin (Added in v3)
                        PowerPackFrameworkFunctions.SetSkinCustomThumbnail(person, "BatmanSkin", ModAPI.LoadSprite("Art/Skins/Wilson/Custom Thumb2.png"));

                        //The following lines of code are examples of how to add a skin events to your skin which executes a UnityAction. (Added in v2)
                        //and runs code on selection and deselection.

                        //Here we set up the skin selection action.
                        UnityAction SkinSelectAction = () =>
                        {
                            foreach (var item in person.Limbs)
                            {
                                item.PhysicalBehaviour.MakeWeightless();
                            }
                            if (Cape.activeInHierarchy)
                            {
                                Cape.SetActive(false);
                            }
                            ModAPI.Notify("Skin Select Action Triggered :D");
                        };
                        //And here we set up the skin deselection action.
                        UnityAction SkinDeselectAction = () =>
                        {
                            foreach (var item in person.Limbs)
                            {
                                item.PhysicalBehaviour.MakeWeightful();
                            }
                            if (!Cape.activeInHierarchy)
                            {
                                Cape.SetActive(true);
                            }
                            ModAPI.Notify("Skin Deselect Action Triggered :D");
                        };
                        //And here we add the events to the skin.
                        PowerPackFrameworkFunctions.SetSkinEvent(person, "JediSkin", SkinSelectAction, SkinDeselectAction);
                    }
                }
            );
            #endregion

            #region Syringes
            //Laser eye syringe
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Life Syringe"), //item to derive from
                    NameOverride = "Laser Eye Syringe (PPF)", //new item name with a suffix to assure it is globally unique
                    DescriptionOverride = "An example power syringe that makes the user shoot lasers. from the Power Pack Framework(PPF)", //new item description
                    CategoryOverride = ModAPI.FindCategory("Biohazard"), //new item category
                                                                         //   ThumbnailOverride = ModAPI.LoadSprite("blueMan.png"), //new item thumbnail (relative path)
                    AfterSpawn = (Instance) => //all code in the AfterSpawn delegate will be executed when the item is spawned
                    {
                        Component.Destroy(Instance.GetComponent<LifeSyringe>());
                        Instance.AddComponent<LaserEyeExampleSyringe>();
                    }
                }
            );

            //Head Exploder syringe
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Life Syringe"), //item to derive from
                    NameOverride = "Head Exploder Syringe", //new item name with a suffix to assure it is globally unique
                    DescriptionOverride = "An example power syringe that makes the user explode peoples heads. from the Power Pack Framework(PPF)", //new item description
                    CategoryOverride = ModAPI.FindCategory("Biohazard"), //new item category
                                                                         //   ThumbnailOverride = ModAPI.LoadSprite("blueMan.png"), //new item thumbnail (relative path)
                    AfterSpawn = (Instance) => //all code in the AfterSpawn delegate will be executed when the item is spawned
                    {
                        Component.Destroy(Instance.GetComponent<LifeSyringe>());
                        Instance.AddComponent<HeadExploderExampleSyringe>();
                    }
                }
            );

            //Healer Syringe
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Life Syringe"), //item to derive from
                    NameOverride = "Healer Example Powered Syringe", //new item name with a suffix to assure it is globally unique
                    DescriptionOverride = "An example power syringe that makes the user Heal people. from the Power Pack Framework(PPF)", //new item description
                    CategoryOverride = ModAPI.FindCategory("Biohazard"), //new item category
                                                                         //   ThumbnailOverride = ModAPI.LoadSprite("blueMan.png"), //new item thumbnail (relative path)
                    AfterSpawn = (Instance) => //all code in the AfterSpawn delegate will be executed when the item is spawned
                    {
                        Component.Destroy(Instance.GetComponent<LifeSyringe>());
                        Instance.AddComponent<HealerExampleSyringe>();
                    }
                }
            );

            //Thick Skin Syringe
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Life Syringe"), //item to derive from
                    NameOverride = "Thick Skin Example Powered Syringe", //new item name with a suffix to assure it is globally unique
                    DescriptionOverride = "An example power syringe that makes the user almost impossible to kill. from the Power Pack Framework(PPF)", //new item description
                    CategoryOverride = ModAPI.FindCategory("Biohazard"), //new item category
                                                                         //   ThumbnailOverride = ModAPI.LoadSprite("blueMan.png"), //new item thumbnail (relative path)
                    AfterSpawn = (Instance) => //all code in the AfterSpawn delegate will be executed when the item is spawned
                    {
                        Component.Destroy(Instance.GetComponent<LifeSyringe>());
                        Instance.AddComponent<ThickSkinExampleSyringe>();
                    }
                }
            );

            //Flying Fist Syringe
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Life Syringe"), //item to derive from
                    NameOverride = "Flying Fist Example Powered Syringe", //new item name with a suffix to assure it is globally unique
                    DescriptionOverride = "An example power syringe that makes the user shoot powerful punches from their fists. from the Power Pack Framework(PPF)", //new item description
                    CategoryOverride = ModAPI.FindCategory("Biohazard"), //new item category
                                                                         //   ThumbnailOverride = ModAPI.LoadSprite("blueMan.png"), //new item thumbnail (relative path)
                    AfterSpawn = (Instance) => //all code in the AfterSpawn delegate will be executed when the item is spawned
                    {
                        Component.Destroy(Instance.GetComponent<LifeSyringe>());
                        Instance.AddComponent<FlyingFistExampleSyringe>();
                    }
                }
            );

            //Grenadier Syringe
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Life Syringe"), //item to derive from
                    NameOverride = "Grenadier Example Powered Syringe", //new item name with a suffix to assure it is globally unique
                    DescriptionOverride = "An example power syringe that makes the user throw grenades. from the Power Pack Framework(PPF)", //new item description
                    CategoryOverride = ModAPI.FindCategory("Biohazard"), //new item category
                                                                         //   ThumbnailOverride = ModAPI.LoadSprite("blueMan.png"), //new item thumbnail (relative path)
                    AfterSpawn = (Instance) => //all code in the AfterSpawn delegate will be executed when the item is spawned
                    {
                        Component.Destroy(Instance.GetComponent<LifeSyringe>());
                        Instance.AddComponent<GrenadierExampleSyringe>();
                    }
                }
            );

            //Power Disabler Syringe
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Life Syringe"), //item to derive from
                    NameOverride = "Power Disabler Syringe", //new item name with a suffix to assure it is globally unique
                    DescriptionOverride = "An example power syringe that disables powers. from the Power Pack Framework(PPF)", //new item description
                    CategoryOverride = ModAPI.FindCategory("Biohazard"), //new item category
                                                                         //   ThumbnailOverride = ModAPI.LoadSprite("blueMan.png"), //new item thumbnail (relative path)
                    AfterSpawn = (Instance) => //all code in the AfterSpawn delegate will be executed when the item is spawned
                    {
                        Component.Destroy(Instance.GetComponent<LifeSyringe>());
                        Instance.AddComponent<PowerDisablerSyringe>();
                    }
                }
            );

            //Power Remover Syringe
            ModAPI.Register(
                new Modification()
                {
                    OriginalItem = ModAPI.FindSpawnable("Life Syringe"), //item to derive from
                    NameOverride = "Power Remover Syringe", //new item name with a suffix to assure it is globally unique
                    DescriptionOverride = "An example power syringe that removes powers. from the Power Pack Framework(PPF)", //new item description
                    CategoryOverride = ModAPI.FindCategory("Biohazard"), //new item category
                                                                         //   ThumbnailOverride = ModAPI.LoadSprite("blueMan.png"), //new item thumbnail (relative path)
                    AfterSpawn = (Instance) => //all code in the AfterSpawn delegate will be executed when the item is spawned
                    {
                        Component.Destroy(Instance.GetComponent<LifeSyringe>());
                        Instance.AddComponent<PowerRemoverSyringe>();
                    }
                }
            );


            #endregion
        }
    }


}