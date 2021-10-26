# NOTE: THIS IS IN DEVELOPMENT AND WILL CHANGE CONSTANTLY
![](https://i.imgur.com/l2pe9l4.png)
# Power Pack Framework Template
This is an example of what you can do with the Power Pack Framework (PPF).
You can look through all the heavily commented code and get an understanding on how making a power works.

# Navigation
1. Introduction
2. Setup
3. Power Creation
4. Functions


# 1. Introduction
The Power Pack Framework's (PPF) purpose is to give the community features that the Marvel Power Pack has like the power menu, capes, etc.

There are two parts to the PPF as a whole, your local mod and the PPF mod on the workshop.


![](https://i.imgur.com/sK5g0RE.png)

These mods are not initally connected but under the correct circumstances, they can communicate.

The creation of powers is entirely in your mod using the template (power creation [PART HERE]), what the PPF workshop mod serves as is a conduit for multiple mods to use the same systems like the power menu.



Your mod is able to create new powers with the ability to be enabled/disabled and be given to people, however you cannot enable/disable powers in your own mod without coding it, this is where the Framework comes in.

![](https://i.imgur.com/mZ5ZIyd.png)
This is flow of how the mods work together.


The framework can talk to your mod and powers by using messages which allows it to call functions and send data, meaning the menu can tell your power to turn off or even be removed.

## Why seperate the menu from the template?


The reason why the framework being its own mod is so importaint is so updates to the system are synced to every mod automatically via the workshop, meaning you don't have to update your code everytime theres a new update (the exeption being PowerPackFrameworkFunctions.cs but thats not required).
It also allows two mods using the framework to have their powers in the same menu together, like DC powers with Marvel powers.

NOTE: Your mod is playable without the framework installed but there will be missing features (power menu, cape, etc).
# 2. Setup
Download the template mod and change the " Mod.json " to your likings.
![](https://i.imgur.com/Zxz9bbn.png)
Thats pretty much all you need to do.

NOTE: if there are any new functions added to the PPF, you will need to redownload the "PowerPackFrameworkFunctions.cs" file.
# 3. Power Creation
In order to use the template to make powers, copy and past it and rename it to what your power is, make sure you rename the class throughout the template.

Creating a syringe for your power
There are two ways to make syringes, using the old method and the new method. The old method uses the first syringe system that was in the game and the new one is liquid based, use whichever suits your needs. 

In order to give a character powers using a syringe, just get the person behaviour on the limb and use your powers setup on the limb you want


The script itself is heavily commented and should have all the info you need to start your own script.

# 4. Functions
## Adding capes to a character

| Varriable Name  | Varriable Type|Usage|
| ------------- | ------------- | ------------- |
| person  | PersonBehaviour  | Specifies who will get the cape.|
| Cape Texture  | Sprite  |The texture of the cape.|
| Cape Base Texture  | Sprite  |The texture of the base of the cape.|

Code example

    PPF.PowerPackFrameworkFunctions.CreateCape(person, ResourceStorage.Sprite_SupermanCape, 0.078f, ResourceStorage.Sprite_BatmanCapeBase);
This function is used to create capes, you can currently only change their appearance. 

## Adding skins to a character

| Varriable Name  | Varriable Type|Usage|
| ------------- | ------------- | ------------- |
| person  | PersonBehaviour  | Specifies who will get the new skin.|
| Skin Texture  | Texture2D  |The texture of the skin.|
| Description  | String  |The description of the new skin.|

Code example

    PPF.PowerPackFrameworkFunctions.AddSkin(person, ModAPI.LoadTexture("Assets/People/Superman/skins/Bizzaro.png"), "Bizzaro, Superman's odd clone.");
This function is used to add new skins to characters, you can't to any complex things yet but you can swap the actual skins.
