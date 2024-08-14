using MagickaForge.Enums;
using System.Data;
using System.Text.Json.Nodes;

namespace MagickaForge.Forges.Character
{
#pragma warning disable CS8602
#pragma warning disable CS8604
    public class CharacterForge : Forge
    {
        //This is contains the initial header for a character XNB before information is written down and is uniform across all files
        private readonly byte[] XNB_HEADER =
        {
            0x58, 0x4E, 0x42, 0x77, 0x04, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x59,
            0x4D, 0x61, 0x67, 0x69, 0x63, 0x6B, 0x61, 0x2E, 0x43, 0x6F, 0x6E, 0x74,
            0x65, 0x6E, 0x74, 0x52, 0x65, 0x61, 0x64, 0x65, 0x72, 0x73, 0x2E, 0x43,
            0x68, 0x61, 0x72, 0x61, 0x63, 0x74, 0x65, 0x72, 0x54, 0x65, 0x6D, 0x70,
            0x6C, 0x61, 0x74, 0x65, 0x52, 0x65, 0x61, 0x64, 0x65, 0x72, 0x2C, 0x20,
            0x4D, 0x61, 0x67, 0x69, 0x63, 0x6B, 0x61, 0x2C, 0x20, 0x56, 0x65, 0x72,
            0x73, 0x69, 0x6F, 0x6E, 0x3D, 0x31, 0x2E, 0x30, 0x2E, 0x30, 0x2E, 0x30,
            0x2C, 0x20, 0x43, 0x75, 0x6C, 0x74, 0x75, 0x72, 0x65, 0x3D, 0x6E, 0x65,
            0x75, 0x74, 0x72, 0x61, 0x6C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01
        };

        private bool modernMagicka;

        public CharacterForge(string InstructionPath, JsonObject JsonRoot, bool ModernMagicka) : base(InstructionPath, JsonRoot)
        {
            modernMagicka = ModernMagicka;
            InstructionsToXNB();
        }

        protected void InstructionsToXNB()
        {

            writer.Write(XNB_HEADER); //START

            writer.Write((string?)jsonRoot["Name"]);
            writer.Write((string?)jsonRoot["LocalizedName"]);
            writer.Write((int)Enum.Parse(typeof(Factions), (string?)jsonRoot["Faction"], true));
            writer.Write((int)Enum.Parse(typeof(BloodType), (string?)jsonRoot["BloodType"], true));
            writer.Write((bool)jsonRoot["IsEthereal"]);
            writer.Write((bool)jsonRoot["LooksEthereal"]);
            writer.Write((bool)jsonRoot["Fearless"]);
            writer.Write((bool)jsonRoot["Uncharmable"]);
            writer.Write((bool)jsonRoot["Nonslippery"]);
            writer.Write((bool)jsonRoot["HasFairy"]);
            writer.Write((bool)jsonRoot["CanSeeInvisible"]);

            JsonArray arraySounds = jsonRoot["Sounds"].AsArray();
            writer.Write(arraySounds.Count);

            foreach (JsonObject sounds in arraySounds)
            {
                writer.Write((string?)sounds["Cue"]);
                writer.Write((int)Enum.Parse(typeof(Banks), (string?)sounds["Wavebank"], true));
            }

            JsonArray arrayGibs = jsonRoot["Gibs"].AsArray();
            writer.Write(arrayGibs.Count);
            foreach (JsonObject gib in arrayGibs)
            {
                writer.Write((string?)gib["Model"]);
                writer.Write((float)gib["Mass"]);
                writer.Write((float)gib["Scale"]);
            }

            JsonArray arrayLights = jsonRoot["Lights"].AsArray();
            writer.Write(arrayLights.Count);
            if (arrayLights.Count > 4)
            {
                throw new ArgumentOutOfRangeException("Items may only have up to 4 lights! Please remove some lights or this will crash the game!");
            }
            foreach (JsonObject light in arrayLights)
            {
                writer.Write((string?)light["Bone"]);
                writer.Write((float)light["Radius"]);
                JsonArray array = light["DiffuseColor"].AsArray();

                for (int i = 0; i < array.Count; i++)
                    writer.Write((float)array[i]);

                array = light["AmbientColor"].AsArray();

                for (int i = 0; i < array.Count; i++)
                    writer.Write((float)array[i]);

                writer.Write((float)light["SpecularAmount"]);
                writer.Write((byte)Enum.Parse(typeof(LightVariationType), (string?)light["LightVariationType"], true));
                writer.Write((float)light["VariationAmount"]);
                writer.Write((float)light["VariationSpeed"]);
            }

            writer.Write((float)jsonRoot["MaxHitpoints"]);
            writer.Write((int)jsonRoot["NumberOfHealthbars"]);
            writer.Write((bool)jsonRoot["Undying"]);
            writer.Write((float)jsonRoot["UndieTime"]);
            writer.Write((float)jsonRoot["UndieHitpoints"]);
            writer.Write((int)jsonRoot["PainTolerance"]);
            writer.Write((float)jsonRoot["KnockdownTolerance"]);
            writer.Write((int)jsonRoot["ScoreValue"]);
            if (modernMagicka)
            {
                writer.Write((int)jsonRoot["XPValue"]);
                writer.Write((bool)jsonRoot["RewardOnKill"]);
                writer.Write((bool)jsonRoot["RewardOnOverkill"]);
            }
            writer.Write((int)jsonRoot["Regeneration"]);
            writer.Write((float)jsonRoot["MaxPanic"]);
            writer.Write((float)jsonRoot["ZapModifier"]);
            writer.Write((float)jsonRoot["Length"]);
            writer.Write((float)jsonRoot["Radius"]);
            writer.Write((float)jsonRoot["Mass"]);
            writer.Write((float)jsonRoot["Speed"]);
            writer.Write((float)jsonRoot["TurnSpeed"]);
            writer.Write((float)jsonRoot["BleedRate"]);
            writer.Write((float)jsonRoot["StunTime"]);
            writer.Write((int)Enum.Parse(typeof(Banks), (string?)jsonRoot["SummonElementBank"], true));
            writer.Write((string?)jsonRoot["SummonElementCue"]);

            JsonArray arrayResistances = (JsonArray)jsonRoot["Resistances"];
            writer.Write(arrayResistances.Count);
            foreach (JsonObject resistance in arrayResistances)
            {
                writer.Write((int)Enum.Parse(typeof(Elements), (string?)resistance["Element"], true));
                writer.Write((float)resistance["Multiplier"]);
                writer.Write((float)resistance["Modifier"]);
                writer.Write((bool)resistance["StatusImmunity"]);
            }

            JsonArray arrayModels = (JsonArray)jsonRoot["Models"];
            writer.Write(arrayModels.Count);
            foreach (JsonObject model in arrayModels)
            {
                writer.Write((string?)model["Model"]);
                writer.Write((float)model["Scale"]);
                JsonArray array = model["Tint"].AsArray();

                for (int i = 0; i < array.Count; i++)
                    writer.Write((float)array[i]);
            }
            writer.Write((string?)jsonRoot["Animation"]);

            JsonArray arrayEffects = jsonRoot["Effects"].AsArray();
            writer.Write(arrayEffects.Count);
            foreach (JsonObject effect in arrayEffects)
            {
                writer.Write((string?)effect["Bone"]);
                writer.Write((string?)effect["Effect"]);
            }

            JsonArray arrayAnimations = jsonRoot["AnimationClips"].AsArray();
            InterpretAnimations(arrayAnimations, writer);
            JsonArray arrayEquipment = jsonRoot["Equipment"].AsArray();
            writer.Write(arrayEquipment.Count);
            int x = 0;
            foreach (JsonObject equipment in arrayEquipment)
            {
                writer.Write(x);
                x++;
                writer.Write((string?)equipment["Bone"]);
                JsonArray jsA = equipment["Offset"].AsArray();
                foreach (float direction in jsA)
                {
                    writer.Write(direction);
                }
                writer.Write((string?)equipment["Item"]);
            }
            JsonArray characterConditions = jsonRoot["EventConditions"].AsArray();
            InterpretCondition(characterConditions);

            writer.Write((float)jsonRoot["AlertRadius"]);
            writer.Write((float)jsonRoot["GroupChase"]);
            writer.Write((float)jsonRoot["GroupSeperation"]);
            writer.Write((float)jsonRoot["GroupCohesion"]);
            writer.Write((float)jsonRoot["GroupAlignment"]);
            writer.Write((float)jsonRoot["GroupWander"]);
            writer.Write((float)jsonRoot["FriendlyAvoidance"]);
            writer.Write((float)jsonRoot["EnemyAvoidance"]);
            writer.Write((float)jsonRoot["SightAvoidance"]);
            writer.Write((float)jsonRoot["DangerAvoidance"]);
            writer.Write((float)jsonRoot["AngerWeight"]);
            writer.Write((float)jsonRoot["DistanceWeight"]);
            writer.Write((float)jsonRoot["HealthWeight"]);
            writer.Write((bool)jsonRoot["Flocking"]);
            writer.Write((float)jsonRoot["BreakFreeStrength"]);
            InterpretAbilities(jsonRoot["Abilities"].AsArray());
            JsonArray moveAbilities = jsonRoot["MoveAbilities"].AsArray();
            writer.Write(moveAbilities.Count);
            foreach (JsonObject move in moveAbilities)
            {
                writer.Write((byte)Enum.Parse(typeof(MovementProperties), (string?)move["MoveProperty"], true));
                JsonArray anims = move["Animations"].AsArray();
                writer.Write(anims.Count);
                foreach (string anim in anims)
                {
                    writer.Write(anim);
                }
            }
            InterpretBuff(jsonRoot["Buffs"].AsArray());
            InterpretAura(jsonRoot["Auras"].AsArray());

            writer.Close(); //END
        }

        private void InterpretAbilities(JsonArray abilityArray)
        {
            writer.Write(abilityArray.Count);
            foreach (JsonObject ability in abilityArray)
            {
                AbilityTypes type = (AbilityTypes)Enum.Parse(typeof(AbilityTypes), (string?)ability["AbilityType"], true);
                writer.Write((string?)ability["AbilityType"]);
                writer.Write((float)ability["Cooldown"]);
                writer.Write((byte)Enum.Parse(typeof(AbilityTarget), (string?)ability["AbilityTarget"], true));
                bool hasFuzzyE = (string?)ability["FuzzyExpression"] != string.Empty;
                writer.Write(hasFuzzyE);
                if (hasFuzzyE)
                {
                    writer.Write((string?)ability["FuzzyExpression"]);
                }
                JsonArray anim = ability["Animations"].AsArray();
                writer.Write(anim.Count);
                foreach (string x in anim)
                {
                    writer.Write(x);
                }
                if (type == AbilityTypes.Block)
                {
                    writer.Write((float)ability["Arc"]);
                    writer.Write((int)ability["Shield"]);
                }
                if (type == AbilityTypes.CastSpell)
                {
                    writer.Write((float)ability["MinimumRange"]);
                    writer.Write((float)ability["MaximumRange"]);
                    writer.Write((float)ability["Angle"]);
                    writer.Write((float)ability["ChantTime"]);
                    writer.Write((float)ability["Power"]);
                    writer.Write((int)Enum.Parse(typeof(CastType), (string)ability["CastType"], true));
                    JsonArray elements = ability["Elements"].AsArray();
                    writer.Write(elements.Count);
                    foreach (string e in elements)
                    {
                        writer.Write((int)Enum.Parse(typeof(Elements), e, true));
                    }
                }
                if (type == AbilityTypes.Dash)
                {
                    writer.Write((float)ability["MinimumRange"]);
                    writer.Write((float)ability["MaximumRange"]);
                    writer.Write((float)ability["Arc"]);
                    JsonArray array = ability["Velocity"].AsArray();

                    for (int i = 0; i < array.Count; i++)
                        writer.Write((float)array[i]);
                }
                if (type == AbilityTypes.ElementSteal)
                {
                    writer.Write((float)ability["Range"]);
                    writer.Write((float)ability["Angle"]);
                }
                if (type == AbilityTypes.GripCharacterFromBehind)
                {
                    writer.Write((float)ability["MaximumRange"]);
                    writer.Write((float)ability["MinimumRange"]);
                    writer.Write((float)ability["Angle"]);
                    writer.Write((float)ability["MaxWeight"]);
                }
                if (type == AbilityTypes.Jump)
                {
                    writer.Write((float)ability["MaximumRange"]);
                    writer.Write((float)ability["MinimumRange"]);
                    writer.Write((float)ability["Angle"]);
                    writer.Write((float)ability["Elevation"]);
                }
                if (type == AbilityTypes.Melee)
                {
                    writer.Write((float)ability["MinimumRange"]);
                    writer.Write((float)ability["MaximumRange"]);
                    writer.Write((float)ability["ArcAngle"]);
                    JsonArray array = ability["Weapons"].AsArray();
                    writer.Write(array.Count);

                    for (int i = 0; i < array.Count; i++)
                        writer.Write((int)array[i]);
                    writer.Write((bool)ability["Rotate"]);
                }
                if (type == AbilityTypes.PickUpCharacter || type == AbilityTypes.ZombieGrip)
                {
                    writer.Write((float)ability["MaximumRange"]);
                    writer.Write((float)ability["MinimumRange"]);
                    writer.Write((float)ability["Angle"]);
                    writer.Write((float)ability["MaxWeight"]);
                    writer.Write((string?)ability["DropAnimation"]);
                }
                if (type == AbilityTypes.Ranged)
                {
                    writer.Write((float)ability["MinimumRange"]);
                    writer.Write((float)ability["MaximumRange"]);
                    writer.Write((float)ability["Elevation"]);
                    writer.Write((float)ability["Arc"]);
                    writer.Write((float)ability["Accuracy"]);
                    JsonArray array = ability["Weapons"].AsArray();
                    writer.Write(array.Count);

                    for (int i = 0; i < array.Count; i++)
                        writer.Write((int)array[i]);
                }
                if (type == AbilityTypes.SpecialAbilityAbility)
                {
                    writer.Write((float)ability["MaximumRange"]);
                    writer.Write((float)ability["MinimumRange"]);
                    writer.Write((float)ability["Angle"]);
                    writer.Write((int)ability["WeaponSlot"]);
                }
                if (type == AbilityTypes.ThrowGrip)
                {
                    writer.Write((float)ability["MaximumRange"]);
                    writer.Write((float)ability["MinimumRange"]);
                    writer.Write((float)ability["Elevation"]);
                    JsonArray damages = ability["Damages"].AsArray();
                    writer.Write(damages.Count);
                    foreach (JsonObject damage in damages)
                    {
                        writer.Write((int)Enum.Parse(typeof(AttackProperties), (string)damage["AttackProperty"], true));
                        writer.Write((int)Enum.Parse(typeof(Elements), (string)damage["Element"], true));
                        writer.Write((float)damage["Amount"]);
                        writer.Write((float)damage["Magnitude"]);
                    }
                }
            }
        }

        private void InterpretBuff(JsonArray buffArray)
        {
            writer.Write(buffArray.Count);
            foreach (JsonObject buffObject in buffArray)
            {
                byte buff = (byte)Enum.Parse(typeof(BuffType), (string)buffObject["BuffType"], true);
                writer.Write(buff);
                writer.Write((byte)Enum.Parse(typeof(VisualCategory), (string)buffObject["BuffVisualCategory"], true));
                JsonArray buffColor = buffObject["BuffColor"].AsArray();
                for (int i = 0; i < buffColor.Count; i++)
                    writer.Write((float)buffColor[i]);

                writer.Write((float)buffObject["BuffRadius"]);
                writer.Write((string?)buffObject["BuffEffect"]);
                if (buff <= 1)
                {
                    writer.Write((int)Enum.Parse(typeof(AttackProperties), (string)buffObject["AttackProperty"], true));
                    writer.Write((int)Enum.Parse(typeof(Elements), (string)buffObject["Element"], true));
                    writer.Write((float)buffObject["Amount"]);
                    writer.Write((float)buffObject["Magnitude"]);
                }
                else if (buff == 2)
                {
                    writer.Write((int)Enum.Parse(typeof(Elements), (string)buffObject["Element"], true));
                    writer.Write((float)buffObject["Multiplier"]);
                    writer.Write((float)buffObject["Modifier"]);
                    writer.Write((bool)buffObject["StatusImmunity"]);
                }
                else if (buff == 4)
                {
                    writer.Write((float)buffObject["BoostAmount"]);
                }
                else if (buff == 5)
                {
                    writer.Write((float)buffObject["AggroReduceAmount"]);
                }
                else if (buff == 6)
                {
                    writer.Write((float)buffObject["HealthMultiplier"]);
                    writer.Write((float)buffObject["HealthModifier"]);
                }
                else if (buff == 7)
                {
                    writer.Write((float)buffObject["SpellTimeMultiplier"]);
                    writer.Write((float)buffObject["SpellTimeModifier"]);
                }
                else if (buff == 8)
                {
                    writer.Write((float)buffObject["SpellRangeMultiplier"]);
                    writer.Write((float)buffObject["SpellRangeModifier"]);
                }
            }
        }

        private void InterpretAnimations(JsonArray animationArray, BinaryWriter writer)
        {
            writer.Write(animationArray.Count);
            foreach (JsonObject animation in animationArray)
            {
                writer.Write((string?)animation["AnimationType"]);
                writer.Write((string?)animation["AnimationKey"]);
                writer.Write((float)animation["AnimationSpeed"]);
                writer.Write((float)animation["BlendTime"]);
                writer.Write((bool)animation["Loop"]);

                JsonArray actions = animation["Actions"].AsArray();
                writer.Write(actions.Count);

                foreach (JsonObject action in actions)
                {
                    ActionType type = (ActionType)Enum.Parse(typeof(ActionType), (string)action["ActionType"], true);
                    writer.Write((string)action["ActionType"]);
                    writer.Write((float)action["ActionStart"]);
                    writer.Write((float)action["ActionEnd"]);

                    if (type == ActionType.Block)
                    {
                        writer.Write((int)action["WeaponSlot"]);
                    }
                    else if (type == ActionType.BreakFree)
                    {
                        writer.Write((float)action["Magnitude"]);
                        writer.Write((int)action["WeaponSlot"]);
                    }
                    else if (type == ActionType.CameraShake)
                    {
                        writer.Write((float)action["Duuration"]);
                        writer.Write((float)action["Magnitude"]);
                    }
                    else if (type == ActionType.CastSpell)
                    {
                        bool fromStaff = (bool)action["FromStaff"];
                        writer.Write(fromStaff);
                        if (!fromStaff)
                            writer.Write((string)action["Bone"]);
                    }
                    else if (type == ActionType.Crouch)
                    {
                        writer.Write((float)action["Radius"]);
                        writer.Write((float)action["Length"]);
                    }
                    else if (type == ActionType.DamageGrip)
                    {
                        writer.Write((bool)action["DamageOwner"]);
                        JsonArray damages = action["Damages"].AsArray();
                        writer.Write(damages.Count);
                        foreach (JsonObject damage in damages)
                        {
                            writer.Write((int)Enum.Parse(typeof(AttackProperties), (string)damage["AttackProperty"], true));
                            writer.Write((int)Enum.Parse(typeof(Elements), (string)damage["Element"], true));
                            writer.Write((float)damage["Amount"]);
                            writer.Write((float)damage["Magnitude"]);
                        }
                    }
                    else if (type == ActionType.DealDamage)
                    {
                        writer.Write((int)action["WeaponSlot"]);
                        writer.Write((byte)Enum.Parse(typeof(Targets), (string)action["Target"], true));
                    }
                    else if (type == ActionType.DetachItem)
                    {
                        writer.Write((int)action["WeaponSlot"]);
                        JsonArray jsA = action["Velocity"].AsArray();
                        foreach (float direction in jsA)
                        {
                            writer.Write(direction);
                        }
                    }
                    else if (type == ActionType.Ethereal)
                    {
                        writer.Write((bool)action["Ethereal"]);
                        writer.Write((float)action["EtherealAlpha"]);
                        writer.Write((float)action["EtherealSpeed"]);
                    }
                    else if (type == ActionType.Grip)
                    {
                        writer.Write((byte)Enum.Parse(typeof(GripType), (string)action["GripType"], true));
                        writer.Write((float)action["GripRadius"]);
                        writer.Write((float)action["GripBreakFreeTolerance"]);
                        writer.Write((string?)action["GripBoneA"]);
                        writer.Write((string?)action["GripBoneB"]);
                        writer.Write((bool)action["FinishOnGrip"]);
                    }
                    else if (type == ActionType.Gunfire)
                    {
                        writer.Write((int)action["WeaponSlot"]);
                        writer.Write((float)action["Accuracy"]);
                    }
                    else if (type == ActionType.Immortal)
                    {
                        writer.Write((bool)action["Collide"]);
                    }
                    else if (type == ActionType.Invisible)
                    {
                        writer.Write((bool)action["NoEffect"]);
                    }
                    else if (type == ActionType.Jump)
                    {
                        writer.Write((float)action["Elevation"]);
                        float MinRange = (float)action["MinimumRange"];
                        if (MinRange != 0)
                        {
                            writer.Write(true);
                            writer.Write(MinRange);
                        }
                        else
                        {
                            writer.Write(false);
                        }
                        float MaxRange = (float)action["MaximumRange"];
                        if (MaxRange != 0)
                        {
                            writer.Write(true);
                            writer.Write(MinRange);
                        }
                        else { writer.Write(false); }
                    }
                    else if (type == ActionType.Move)
                    {
                        JsonArray jsA = action["Velocity"].AsArray();
                        foreach (float direction in jsA)
                        {
                            writer.Write(direction);
                        }
                    }
                    else if (type == ActionType.PlayEffect)
                    {
                        writer.Write((string?)action["Bone"]);
                        writer.Write((bool)action["Attached"]);
                        writer.Write((string?)action["Effect"]);
                        writer.Write(1.0f);
                    }
                    else if (type == ActionType.PlaySound)
                    {
                        writer.Write((string?)action["Cue"]);
                        writer.Write((int)Enum.Parse(typeof(Banks), (string?)action["Wavebank"], true));
                    }
                    else if (type == ActionType.RemoveStatus)
                    {
                        writer.Write((string?)action["Status"]);
                    }
                    else if (type == ActionType.RemoveStatus)
                    {
                        writer.Write((int)action["WeaponSlot"]);
                        writer.Write((string?)action["Bone"]);
                    }
                    else if (type == ActionType.SpawnMissile)
                    {
                        writer.Write((int)action["WeaponSlot"]);
                        JsonArray jsA = action["Velocity"].AsArray();
                        foreach (float direction in jsA)
                        {
                            writer.Write(direction);
                        }
                        writer.Write((bool)action["ItemAligned"]);
                    }
                    else if (type == ActionType.SpecialAbility)
                    {
                        int weaponValue = (int)action["WeaponSlot"];
                        writer.Write(weaponValue);
                        if (weaponValue < 0)
                        {
                            JsonObject specialAbility = action["SpecialAbility"].AsObject();
                            writer.Write((string?)specialAbility["Type"]);
                            writer.Write((string?)specialAbility["Animation"] ?? string.Empty);
                            writer.Write((string?)specialAbility["Hash"] ?? string.Empty);

                            JsonArray elements = specialAbility["Elements"].AsArray();
                            writer.Write(elements.Count);
                            foreach (string element in elements)
                            {
                                writer.Write((int)Enum.Parse(typeof(Elements), element, true));
                            }
                        }
                    }
                    else if (type == ActionType.Suicide)
                    {
                        writer.Write((bool)action["Overkill"]);
                    }
                    else if (type == ActionType.Tongue)
                    {
                        writer.Write((float)action["MaxLength"]);
                    }
                    else if (type == ActionType.WeaponVisibility)
                    {
                        writer.Write((int)action["WeaponSlot"]);
                        writer.Write((bool)action["Visible"]);
                    }

                }
            }
            for (int i = 0; i < 26; i++)
            {
                writer.Write(0);
            }
        }

    }
}
