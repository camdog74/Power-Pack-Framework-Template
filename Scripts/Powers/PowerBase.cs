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
    public class PowerBase : MonoBehaviour
    {
        /// Team Wilson's Power Pack Framework Template
        /// This class is the base for any of your powers, any powers you make should be a subclass of this.

        ///If the power is activated
        public bool Enabled = true;
        /// <summary>
        /// Weather or not this power gets added to the power menu.
        /// </summary>
        public bool AddToPowerList = true;

        /// <summary>
        /// Power Name.
        /// </summary>
        public string PowerName = "Power";
        /// <summary>
        /// Power activation type. (Head,Chest,Hand)
        /// </summary>
        public string Activation = "None";
        /// <summary>
        /// The icon that will appear for the power in the power menu.
        /// </summary>
        public Sprite PowerIcon;

        //When we turn off the GripBehaviour(grabbing) we need to remember the original grip hold location.

        Vector2 OriginalHand = Vector2.zero;

        public virtual void Start()
        {
            //This is the power's infomation that we later send to the framework.
            List<object> PowerInfo = new List<object>();
            //0
            PowerInfo.Add(Enabled);
            //1
            PowerInfo.Add(AddToPowerList);
            //2
            PowerInfo.Add(PowerName);
            //3
            PowerInfo.Add(Activation);
            //4
            PowerInfo.Add(PowerIcon);
            //5
            PowerInfo.Add(this);

            //This sends the power to the framework.
            if (GameObject.Find("PowerPackFrameworkManager"))
                GameObject.Find("PowerPackFrameworkManager").SendMessage("AddNewPowerToMenu", PowerInfo);
            else if (!GameObject.Find("PowerPackFrameworkManager"))
            {
                GameObject UrlObject = new GameObject();
                UrlObject.AddComponent<URLOpenBehaviour>();
                DialogBoxManager.Dialog("POWER PACK FRAMEWORK NOT INSTALLED\nYour mods won't work properly without it", new DialogButton("Close", true), new DialogButton("Workshop Page", true, new UnityAction[1] { (UnityAction)(() => { UrlObject.GetComponent<URLOpenBehaviour>().OpenURL("https://steamcommunity.com/workshop/filedetails/?id=2506978276"); }) }));
            }
            EnablePower();
        }


        /// <summary>
        /// Uses the said power.
        /// </summary>
        public virtual void UsePower()
        {

        }

        /// <summary>
        /// Disables the power.
        /// </summary>
        public virtual void DisablePower()
        {
            if (GetComponent<GripBehaviour>())
                GetComponent<GripBehaviour>().GripPosition = OriginalHand;
            Enabled = false;

        }

        /// <summary>
        /// Enables the power.
        /// </summary>
        public virtual void EnablePower()
        {
            if (GetComponent<GripBehaviour>())
            {
                OriginalHand = GetComponent<GripBehaviour>().GripPosition;

                GetComponent<GripBehaviour>().GripPosition = Vector3.one * 999;
            }

            Enabled = true;
        }

        /// <summary>
        /// Removes the power.
        /// </summary>
        public virtual void RemovePower()
        {
            //This destroys the use trigger on the gameobject if it exists
            if (GetComponent<UseEventTrigger>())
            {
                Component.Destroy(GetComponent<UseEventTrigger>());
            }
            //this destroys the power/script
            Component.Destroy(this);
        }

    }
}
