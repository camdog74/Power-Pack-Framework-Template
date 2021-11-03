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
    
    public class PowerPackFrameworkFunctions
    {
        public static void CreateCape(PersonBehaviour person, Sprite CapeTexture,float CapeThickness = 0.15f, Sprite CapeBaseSprite = null)
        {
            List<object> CapeInfo = new List<object>() { person, CapeTexture,CapeThickness, CapeBaseSprite };

            if (GameObject.Find("PowerPackFrameworkManager"))
                GameObject.Find("PowerPackFrameworkManager").SendMessage("CreateCape", CapeInfo);
        }



        public static void AddSkin(PersonBehaviour person,Texture2D texture, string SkinName, string Description)
        {
            List<object> SkinInfo = new List<object>() {person, texture,SkinName,Description};

            if (GameObject.Find("PowerPackFrameworkManager"))
                GameObject.Find("PowerPackFrameworkManager").SendMessage("AddSkinToCharacter", SkinInfo);
        }
        public static void AddCustomSpriteToSkin(PersonBehaviour person ,int TargetLimb, string SkinName, Sprite Skin, Texture2D Flesh, Texture2D Bone)
        {
            List<object> CustomSpriteInfo = new List<object>() {person,TargetLimb,SkinName,Skin, Flesh,Bone };

            if (GameObject.Find("PowerPackFrameworkManager"))
                GameObject.Find("PowerPackFrameworkManager").SendMessage("AddCustomBodyPartToSkin", CustomSpriteInfo);
        }

        public static void SetCustomSprite(LimbBehaviour BodyPart,Sprite Skin,Texture2D Flesh, Texture2D Bone)
        {
            List<object> CustomSpriteInfo = new List<object>() { BodyPart,Skin,Flesh,Bone};

            if (GameObject.Find("PowerPackFrameworkManager"))
                GameObject.Find("PowerPackFrameworkManager").SendMessage("CustomLimb", CustomSpriteInfo);
        }
        public static void SetCustomSpriteActive(bool IsActive)
        {
            List<object> CustomSpriteInfo = new List<object>() {IsActive };
            GameObject.Find("PowerPackFrameworkManager").SendMessage("SetactiveOrNot", CustomSpriteInfo);
        }
    }
}
