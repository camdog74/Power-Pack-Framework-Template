using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Reflection;
using UnityEngine.SceneManagement;

namespace PPF
{
    public class PowerPackFrameworkFunctions
    {
        public static bool DoingThings = false;

        public static void CreateCape(PersonBehaviour person, Sprite CapeTexture,float CapeThickness = 0.15f, Sprite CapeBaseSprite = null)
        {
            List<object> CapeInfo = new List<object>() { person, CapeTexture,CapeThickness, CapeBaseSprite };

            if (GameObject.Find("PowerPackFrameworkManager"))
                GameObject.Find("PowerPackFrameworkManager").SendMessage("CreateCape", CapeInfo);
            else
            {
                CheckforPPF();
            }

        }

        public static void AddSkin(PersonBehaviour person,Texture2D texture, string SkinName, string Description)
        {
            List<object> SkinInfo = new List<object>() {person, texture,SkinName,Description};

            if (GameObject.Find("PowerPackFrameworkManager"))
                GameObject.Find("PowerPackFrameworkManager").SendMessage("AddSkinToCharacter", SkinInfo);
            else
            {
                CheckforPPF();
            }
        }

        
        //This isn't finished yet and creates errors combined with the skin system
        
        //public static void SetCustomSprite(LimbBehaviour BodyPart, Sprite Skin,Texture2D Flesh, Texture2D Bone)
        //{
        //    List<object> CustomSpriteInfo = new List<object>() { BodyPart,Skin,Flesh,Bone};

        //    if (GameObject.Find("PowerPackFrameworkManager"))
        //        GameObject.Find("PowerPackFrameworkManager").SendMessage("CustomLimb", CustomSpriteInfo);
        //}


        public static void CheckforPPF()
        {
            if (!GameObject.Find("PowerPackFrameworkManager"))
            {
                if (!GameObject.Find("AThingForThingsPowerPackThing"))
                {
                    GameObject UrlObject = new GameObject("AThingForThingsPowerPackThing");
                    UrlObject.AddComponent<URLOpenBehaviour>();
                    foreach (var mod in ModLoader.LoadedMods)
                    {
                        if (mod.CreatorUGCIdentity == "2506978276")
                        {
                            DialogBoxManager.Dialog("POWER PACK FRAMEWORK NOT ENABLED\nYour mods won't work properly without it", new DialogButton("Close", true), new DialogButton("Enable and Reload", true, new UnityAction[1] { (UnityAction)(() =>
                            {
                                if (!DoingThings)
                                {
                                    DoingThings = true;
                                    ModLoader.SetModActive(mod);
                                    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex).completed += Done;
                                }
                            })}));
                            return;
                        }
                    }
                    DialogBoxManager.Dialog("POWER PACK FRAMEWORK NOT INSTALLED\nYour mods won't work properly without it", new DialogButton("Close", true), new DialogButton("Workshop Page", true, new UnityAction[1] { (UnityAction)(() => { UrlObject.GetComponent<URLOpenBehaviour>().OpenURL("https://steamcommunity.com/workshop/filedetails/?id=2506978276"); }) }));
                }
            }
        }
        private static void Done(AsyncOperation op)
        {
            DoingThings = false;
        }
    }
}
