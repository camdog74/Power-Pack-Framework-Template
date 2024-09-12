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
        /// Power Description.
        /// </summary>
        public string PowerDescription = "";
        /// <summary>
        /// Power activation type. (Head,Chest,Hand)
        /// </summary>
        public string Activation = "None";
        /// <summary>
        /// The icon that will appear for the power in the power menu.
        /// </summary>
        public List<List<object>> PowerSettings = new List<List<object>>();
        public Sprite PowerIcon;


        //When we turn off the GripBehaviour(grabbing) we need to remember the original grip hold location.

        public Vector2 OriginalHand = Vector2.zero;

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
            //6
            PowerInfo.Add(PowerDescription);
            //7
            PowerInfo.Add(PowerSettings);

            //This sends the power to the framework.
            if (GameObject.Find("PowerPackFrameworkManager"))
                GameObject.Find("PowerPackFrameworkManager").SendMessage("AddNewPowerToMenu", PowerInfo);
            else if (!GameObject.Find("PowerPackFrameworkManager"))
            {
                GameObject UrlObject = new GameObject();
                UrlObject.AddComponent<URLOpenBehaviour>();
                DialogBoxManager.Dialog("POWER PACK FRAMEWORK NOT INSTALLED/ENABLED\nYour mods won't work properly without it", new DialogButton("Close", true));
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
            enabled = false;
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
            DisablePower();
            Component.Destroy(this);
        }

        public void AddSliderPowerSetting(string SettingName, string SettingDescription, float MinValue, float MaxValue, float DefaultValue, bool WholeNumbers,string SetFunction)
        {
            List<object> SliderPowerSettingInfo = new List<object>() { "Slider", SettingName, SettingDescription, MinValue, MaxValue, DefaultValue, WholeNumbers, SetFunction };

            PowerSettings.Add(SliderPowerSettingInfo);
        }
        public void AddTogglePowerSetting(string SettingName, string SettingDescription, bool DefaultValue, string SetFunction)
        {
            List<object> TogglePowerSettingInfo = new List<object>() { "Toggle", SettingName, SettingDescription, DefaultValue, SetFunction };

            PowerSettings.Add(TogglePowerSettingInfo);
        }
        public void AddTextPowerSetting(string SettingName, string SettingDescription, string DefaultValue, string SetFunction)
        {
            List<object> TextPowerSettingInfo = new List<object>() { "Text", SettingName, SettingDescription, DefaultValue, SetFunction };

            PowerSettings.Add(TextPowerSettingInfo);
        }
        public void AddColorPowerSetting(string SettingName, string SettingDescription, Color DefaultValue, string SetFunction)
        {
            List<object> ColorPowerSettingInfo = new List<object>() { "Color", SettingName, SettingDescription, DefaultValue, SetFunction };

            PowerSettings.Add(ColorPowerSettingInfo);
        }
        public void AddOptionListPowerSetting(string SettingName, string SettingDescription,Vector2 ItemSize, List<OptionListOption> Options,int DefaultValue)
        {
            List<List<object>> OptionListOptions = new List<List<object>>();
            foreach (var item in Options)
            {
                List<object> OptionListOption = new List<object>() { item.Name, item.Description, item.Icon, item.UseGridBackground, item.Function };
                OptionListOptions.Add(OptionListOption);
            }
            List<object> OptionListPowerSettingInfo = new List<object>() { "OptionList", SettingName, SettingDescription, ItemSize, OptionListOptions, DefaultValue };

            PowerSettings.Add(OptionListPowerSettingInfo);
        }

        public class OptionListOption
        {
            public string Name;
            public string Description;
            public Sprite Icon;
            public bool UseGridBackground;
            public UnityAction Function;

            public OptionListOption(string name, string description, Sprite icon, bool useGridBackground, UnityAction function)
            {
                Name = name;
                Description = description;
                Icon = icon;
                UseGridBackground = useGridBackground;
                Function = function;
            }
        }

    }
}
