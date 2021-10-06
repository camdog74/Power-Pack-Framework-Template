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



    public class PPF
    {
        public static void Main() 
        {
            //This is where you store all of your assets like your sprites, textures and sounds. (power icons too)
            //Make sure to add new ones when you need to use them instead of using ModAPI.Load in AfterSpawn.
            GameObject ResourceManager = new GameObject();
            ResourceManager.AddComponent<ResourceStorage>();

            //SPRITES

            ResourceManager.GetComponent<ResourceStorage>().Sprites.Add("Healer Power Icon", ModAPI.LoadSprite("Art/Icons/Healer.png"));
            ResourceManager.GetComponent<ResourceStorage>().Sprites.Add("Head Exploder Power Icon", ModAPI.LoadSprite("Art/Icons/Head Exploder.png"));
            ResourceManager.GetComponent<ResourceStorage>().Sprites.Add("Laser Eyes Power Icon", ModAPI.LoadSprite("Art/Icons/Laser.png"));
            ResourceManager.GetComponent<ResourceStorage>().Sprites.Add("Flying Fist Icon", ModAPI.LoadSprite("Art/Icons/Flying Fist.png"));
            ResourceManager.GetComponent<ResourceStorage>().Sprites.Add("Grenadier Icon", ModAPI.LoadSprite("Art/Icons/Grenadier.png"));
            ResourceManager.GetComponent<ResourceStorage>().Sprites.Add("Thick Skin Icon", ModAPI.LoadSprite("Art/Icons/Thick Skin.png"));
            ResourceManager.GetComponent<ResourceStorage>().Sprites.Add("Big Fist", ModAPI.LoadSprite("Art/Particles/Fist - Copy.png"));
            ResourceManager.GetComponent<ResourceStorage>().Sprites.Add("Cape Base", ModAPI.LoadSprite("Art/Cape/cape01.png"));
            ResourceManager.GetComponent<ResourceStorage>().Sprites.Add("Cape Texture", ModAPI.LoadSprite("Art/Cape/cape02.png"));

            //TEXTURES

            ResourceManager.GetComponent<ResourceStorage>().Textures.Add("Healer Particles", ModAPI.LoadTexture("Art/Particles/Health.png"));
            ResourceManager.GetComponent<ResourceStorage>().Textures.Add("Fist Particles", ModAPI.LoadTexture("Art/Particles/Fist.png"));

            //SOUNDS

            ResourceManager.GetComponent<ResourceStorage>().Sounds.Add("Laser Shoot", ModAPI.LoadSound("Sounds/LazerBlast.wav"));
            ResourceManager.GetComponent<ResourceStorage>().Sounds.Add("Fist Shoot", ModAPI.LoadSound("Sounds/Fist Shot.wav"));
            ResourceManager.GetComponent<ResourceStorage>().Sounds.Add("Fist Amb", ModAPI.LoadSound("Sounds/Fist_Amb.wav"));


            //Feel free to replace all these items/characters with your own.

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

                            //Gets all the characters limbs
                            foreach (var body in person.Limbs)
                        {
                                //Sets up power on specific bodypart. supported bodyparts are (Head,LowerArm,UpperBody)
                                if (body.gameObject.name.Contains("Head"))
                                LaserEyesPowerExample.SetUpPower(body, person, Instance,Color.green);
                        }

                            PowerPackFrameworkFunctions.CreateCape(person, ResourceManager.GetComponent<ResourceStorage>().Sprites["Cape Texture"], 0.15f, ResourceManager.GetComponent<ResourceStorage>().Sprites["Cape Base"]);
                        }
                }
            );
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


        }
    }

    public class ResourceStorage : MonoBehaviour
    {
        public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();
        public Dictionary<string, AudioClip> Sounds = new Dictionary<string, AudioClip>();

        public static Sprite GetSpriteResource(string SpriteName)
        {
            if (GameObject.FindObjectOfType<ResourceStorage>().Sprites[SpriteName] != null)
                return GameObject.FindObjectOfType<ResourceStorage>().Sprites[SpriteName];
            else return null;
        }
        public static Texture2D GetTextureResource(string TextureName)
        {
            if (GameObject.FindObjectOfType<ResourceStorage>().Textures[TextureName] != null)
                return GameObject.FindObjectOfType<ResourceStorage>().Textures[TextureName];
            else return null;
        }
        public static AudioClip GetSoundResource(string SoundName)
        {
            if (GameObject.FindObjectOfType<ResourceStorage>().Sounds[SoundName] != null)
                return GameObject.FindObjectOfType<ResourceStorage>().Sounds[SoundName];
            else return null;
        }
    }


}