![](https://i.imgur.com/l2pe9l4.png)
# Power Pack Framework Template
This is an example of what you can do with the Power Pack Framework (PPF).
You can look through all the heavily commented code and get an understanding on how making a power works.

# Navigation
1. Introduction




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
