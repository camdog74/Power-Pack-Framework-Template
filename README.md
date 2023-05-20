
![](https://i.imgur.com/l2pe9l4.png)
# Power Pack Framework Template
This is an example of what you can do with the Power Pack Framework (PPF).
You can look through all the heavily commented code and get an understanding on not only how making a power works but how all the other features work too.

# Navigation
1. Introduction
2. Setup
3. Power Creation
4. Functions


# 1. Introduction
The Power Pack Framework's (PPF) purpose is to give the community features that the Marvel Power Pack has like the power menu, capes, etc.

There are two parts to the PPF as a whole, your local mod and the PPF mod on the workshop.


![](https://i.imgur.com/sK5g0RE.png)

These mods are not initially connected but under the correct circumstances, they can communicate.

The creation of powers is entirely in your mod using the template (3. Power Creation), the PPF workshop mod serves as a conduit for multiple mods to use the same systems as the power menu and update automatically to fix bugs and add features.



Your mod is able to create new powers with the ability to be enabled/disabled and be given to people, however you cannot enable/disable powers in your own mod without coding it, this is where the Framework comes in.

![](https://i.imgur.com/mZ5ZIyd.png)
This is flow of how the mods work together.


The framework can talk to your mod and powers by using messages which allows it to call functions and send data, meaning the menu can tell your power to turn off or even be removed.

## Why seperate the menu from the template?


The reason why the framework being its own mod is so important is so updates to the system are synced to every mod automatically via the workshop, meaning you don't have to update your code everytime there's a new update (the exception being "PowerPackFrameworkFunctions.cs", but thats not required).
It also allows two mods using the framework to have their powers in the same menu together, like DC powers with Marvel powers.

>NOTE: Your mod is playable without the framework installed but there will be missing features (power menu, cape, etc).
# 2. Setup
You can either download the PowerPackFrameworkFunctions.cs file and use the functions that way or download the template.

if you download the template mod, change the " Mod.json " to your likings.
![](https://i.imgur.com/Zxz9bbn.png)
To be clear, you can change the Mod.json to whatever you want, i just filled it in myself in the picture. That's pretty much all you need to do.

>NOTE: if there are any new functions added to the PPF, you will need to re download the "PowerPackFrameworkFunctions.cs" file.
# 3. Power Creation
In order to use the template to make powers, copy and past it and rename it to what your power is, make sure you rename the class throughout the template.

Creating a syringe for your power
There are two ways to make syringes, using the old method and the new method. The old method uses the first syringe system that was in the game and the new one is liquid based, use whichever suits your needs. 

In order to give a character powers using a syringe, just get the person behaviour on the limb and use your powers setup on the limb you want.


The script itself is heavily commented and should have all the info you need to start your own script.

# 4. Functions
## Adding capes to a character
###### Added in Version 1
> Note: As of Version 2, this function now returns the GameObject of the cape, allowing you to modify it.

| Variable Name | Variable Type|Usage|
| ------------- | ------------- | ------------- |
| person | PersonBehaviour | Specifies who will get the cape.|
| Cape Texture | Sprite |The texture of the cape.|
| Cape Base Texture | Sprite |The texture of the base of the cape.|

Code example



    //Creates a cape and gives it to the person and also stores it as a variable.
    var Cape = PPF.PowerPackFrameworkFunctions.CreateCape(person, ResourceStorage.CapeTexture, 0.15f, ResourceStorage.CapeBase);

This function is used to create capes, you can currently only change their appearance. 

## Adding skins to a character
###### Added in Version 1

| Variable Name | Variable Type|Usage|
| ------------- | ------------- | ------------- |
| person | PersonBehaviour | Specifies who will get the new skin.|
| Skin Texture | Texture2D |The texture of the skin.|
| Skin Name | String |The ID/name of the new skin|
| Description | String |The description of the new skin.|

Code example



    //This line below is an example of how to add custom skins to a character.
    PPF.PowerPackFrameworkFunctions.AddSkin(person, ModAPI.LoadTexture("Art/Skins/Wilson/Skin Layer.png"),"TestSkin", "This is Wilson. He's a test skin in this case, but most importantly <color=red>Team Wilson's<color=white> mascot!");

This function is used to add new skins to characters which lets you change skins when selecting the skin selector option in the context menu (right click menu).
> Note: The skin ID/Name of the skin your character starts with is called Default.
## Adding custom body part to skins
###### Added in Version 2

| Variable Name | Variable Type|Usage|
| ------------- | ------------- | ------------- |
| person | PersonBehaviour | Specifies the person.|
| TargetLimb | int |Specifies the limb (in person.Limbs).|
| Skin | Sprite |The sprite of the skin.|
| Flesh | Texture2D |The texture of the Flesh.|
| Bone | Texture2D |The texture of the Bone.|

Code example



    //This line of code is how you add a custom limb to the character, in this case, it's a custom head. (Added in v2)
    PPF.PowerPackFrameworkFunctions.AddCustomizedLimbToSkin(person, 0 , "BatmanSkin", ModAPI.LoadSprite("Art/Skins/Wilson/Head.png", 10), ModAPI.LoadTexture("Art/Skins/Wilson/Head Flesh.png"), ModAPI.LoadTexture("Art/Skins/Wilson/Head Bone.png"));

This function lets you change sprites and allows you to make art outside the usual texture boundarys on characters by creating independant sprites and using them. This does require you to set a flesh and bone layer as well though. 
> Note:
If you notice the gore layer screwing up or being stretched, you might need to make your sprite's image size a higher resolution and scale up the sprite, you can see this in the example mod art folder (Art\Skins\Wilson\Head.png)

## Adding UnityAction events on the selection/deselection of a certain skin
###### Added in Version 2

| Variable Name | Variable Type|Usage|
| ------------- | ------------- | ------------- |
| person | PersonBehaviour | Specifies the person.|
| Skin Name | String |The ID/name of the new skin|
| SelectAction | UnityAction |The UnityAction with code to execute when the skin is selected.|
| DeselectAction | UnityAction |**(OPTIONAL)** The UnityAction with code to execute when the skin is deselected.|

Code example



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
                        PPF.PowerPackFrameworkFunctions.SetSkinEvent(person, "JediSkin", SkinSelectAction, SkinDeselectAction);

This function lets you execute code via a UnityAction allowing you to do cool effects and events when a skin is selected and deselected. 
