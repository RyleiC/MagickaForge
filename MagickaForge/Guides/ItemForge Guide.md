Variable Usage:
#############################

[ValueType] "VariableName": "Example"
- Description

#############################

[String] "Name": "example_name"
- The ID internally referenced by the game, must be the same name as the file name (without the .xnb extension)

[String] "LocalizedName": "#example_text"
- The language code that is displayed to the player as the item name. To be a properly displayed in game, it must be defined inside the Loctables inside of Content/Languages/*language*/

[String] "LocalizedDescription": "#example_textd"
- The language code that is displayed to the player as the item description. To be a properly displayed in game, it must be defined inside the Loctables inside of Content/Languages/*language*/

[Array] "Sounds": [{ "Cue":"amb_sheep_highlands", "Bank":"Ambience"}]
- Sounds that are played ambiently whenever the item is held. Ensure that the sound cue is from the proper bank!

[Boolean] "Grabbable": true
- Determines whether or not a player avatar can pick up this item if it's dropped

[Boolean] "Bound": false
- Determines whether or not the player avatar can swap out this item for another [Ex: Alucart's Gauntlet]

[Integer] "BlockStrength": 500
- How much Physical damage can be blocked by the player if they're holding block. Most items use 500, but shields have higher values

[WeaponClass] "WeaponClass": "Thrust_Medium"
- Determines the swing animation for the item. Most animations have a fast, medium, and slow variation. Setting WeaponClass to Staff makes it a staff and setting it to any gun will replace Block with Crouch

[Float] "CooldownTime": 4.5
- How long a player must wait after using this weapon to do so again

[Boolean] "HideModel": false
- Determines whether or not the model is rendered

[Boolean] "HideEffects": false
- Determines whether or not the effects are rendered

[Boolean] "PauseSounds": false
- Determines whether or not the item sounds are played

[Array] "Resistances": [{"Element": "Physical", "Multiplier": 0.3, "Modifier": 400, "StatusImmunity": false}]
- Whenever the weapon is a staff, resistances here will be applied to the wielder. Multiplier is the amount the damage is multiplied by and Modifier the added to the damage.

[PassiveAbilities] "PassiveAbilityType": "MoveSpeed"
- A passive ability that is applied to the player if they are wielding the item. To disable passive abilities set it to "None".

[Float] "PassiveAbilityStrength": "1.6"
- The magnitude of the previously mentioned PassiveAbilityType.

[Array] "Lights": [{"Radius": 8, "DiffuseColor": [0.4,0.4,0.4],"AmbientColor": [0.4,0.4,0.4], "SpecularAmount": 1, "LightVariationType": "None", "VariationAmount": 0, "VariationSpeed": 0}]
- An array that holds lights attached to the item. Despite being an array you may only have 1 light active, or the game will throw an exception.

[Boolean] "HasSpecialAbility": true
- Determines whether or not the item has an ability when you middle-click on a mouse or Y on a controller. Only applicable to staffs.

[Float] "SpecialAbilityCooldown": "8"
- The cooldown for any special abilities the item has. Only applicable to staffs.

[Object] "SpecialAbility": {"Type": "Rain", "Animation": "Cast_Self", "Hash":"", "Elements":[]}
- The special ability used by the item. Type is generally the name of a magick, Hash and Elements are best kept empty.

[Float] "MeleeRange": "1.4"
- The radius in which a melee weapon will hit a target.

[Boolean] "MeleeMultiHit": false
- Determines whether or not the melee weapon can hit multiple targets in one swing. Be careful as having multiple hits occur will trigger multiple hit events!

