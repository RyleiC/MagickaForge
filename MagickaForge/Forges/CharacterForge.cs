using MagickaForge.Enums;
using System.Data;
using System.Text.Json.Nodes;

namespace MagickaForge.Forges
{
#pragma warning disable CS8602
#pragma warning disable CS8604
    public class CharacterForge
    {
        bool modernMagicka = false;
        public static readonly byte[] XNB_HEADER =
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
        public void InstructionsToXNB(string InstructionPath, bool ModernMagicka)
        {

            if (!File.Exists(InstructionPath))
            {
                throw new System.IO.FileNotFoundException(InstructionPath);
            }

            StreamReader reader = new StreamReader(File.OpenRead(InstructionPath));
            BinaryWriter writer = new BinaryWriter(File.OpenWrite(Path.ChangeExtension(InstructionPath, ".xnb")));

            JsonObject instructionNode = JsonNode.Parse(reader.ReadToEnd()).AsObject();
            reader.Close();

            if (instructionNode == null)
            {
                throw new NoNullAllowedException();
            }

            writer.Write(XNB_HEADER); //START

            writer.Write((string?)instructionNode["Name"]);
            writer.Write((string?)instructionNode["LocalizedName"]);
            writer.Write((int)Enum.Parse(typeof(Factions), (string?)instructionNode["Faction"], true));
            writer.Write((int)Enum.Parse(typeof(BloodType), (string?)instructionNode["BloodType"], true));
            writer.Write((bool)instructionNode["IsEthereal"]);
            writer.Write((bool)instructionNode["LooksEthereal"]);
            writer.Write((bool)instructionNode["Fearless"]);
            writer.Write((bool)instructionNode["Uncharmable"]);
            writer.Write((bool)instructionNode["Nonslippery"]);
            writer.Write((bool)instructionNode["HasFairy"]);
            writer.Write((bool)instructionNode["CanSeeInvisible"]);

            JsonArray arraySounds = instructionNode["Sounds"].AsArray();
            writer.Write(arraySounds.Count);

            foreach (JsonObject sounds in arraySounds)
            {
                writer.Write((string?)sounds["Cue"]);
                writer.Write((int)Enum.Parse(typeof(Banks), (string?)sounds["Wavebank"], true));
            }

            JsonArray arrayGibs = instructionNode["Gibs"].AsArray();
            writer.Write(arrayGibs.Count);
            foreach (JsonObject gib in arrayGibs)
            {
                writer.Write((string?)gib["Model"]);
                writer.Write((float)gib["Mass"]);
                writer.Write((float)gib["Scale"]);
            }

            JsonArray arrayLights = (JsonArray)instructionNode["Lights"];
            writer.Write(arrayLights.Count);
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

            writer.Write((float)instructionNode["MaxHitpoints"]);
            writer.Write((int)instructionNode["NumberOfHealthbars"]);
            writer.Write((bool)instructionNode["Undying"]);
            writer.Write((float)instructionNode["UndieTime"]);
            writer.Write((float)instructionNode["UndieHitpoints"]);
            writer.Write((int)instructionNode["PainTolerance"]);
            writer.Write((float)instructionNode["KnockdownTolerance"]);
            writer.Write((int)instructionNode["ScoreValue"]);
            if (modernMagicka)
            {
                writer.Write((int)instructionNode["XPValue"]);
                writer.Write((bool)instructionNode["RewardOnKill"]);
                writer.Write((bool)instructionNode["RewardOnOverkill"]);
            }
            writer.Write((int)instructionNode["Regeneration"]);
            writer.Write((float)instructionNode["MaxPanic"]);
            writer.Write((float)instructionNode["ZapModifier"]);
            writer.Write((float)instructionNode["Length"]);
            writer.Write((float)instructionNode["Radius"]);
            writer.Write((float)instructionNode["Mass"]);
            writer.Write((float)instructionNode["Speed"]);
            writer.Write((float)instructionNode["TurnSpeed"]);
            writer.Write((float)instructionNode["BleedRate"]);
            writer.Write((float)instructionNode["StunTime"]);
            writer.Write((int)Enum.Parse(typeof(Banks), (string?)instructionNode["SummonElementBank"], true));
            writer.Write((string?)instructionNode["SummonElementCue"]);

            JsonArray arrayResistances = (JsonArray)instructionNode["Resistances"];
            writer.Write(arrayResistances.Count);
            foreach (JsonObject resistance in arrayResistances)
            {
                writer.Write((int)Enum.Parse(typeof(Elements), (string?)resistance["Element"], true));
                writer.Write((float)resistance["Multiplier"]);
                writer.Write((float)resistance["Modifier"]);
                writer.Write((bool)resistance["StatusImmunity"]);
            }

            JsonArray arrayModels = (JsonArray)instructionNode["Models"];
            writer.Write(arrayModels.Count);
            foreach (JsonObject model in arrayModels)
            {
                writer.Write((string?)model["Model"]);
                writer.Write((float)model["Scale"]);
                JsonArray array = model["Tint"].AsArray();

                for (int i = 0; i < array.Count; i++)
                    writer.Write((float)array[i]);
            }
            writer.Write((string?)instructionNode["Animation"]);

            JsonArray arrayEffects = instructionNode["Effects"].AsArray();
            writer.Write(arrayEffects.Count);
            foreach (JsonObject effect in arrayEffects)
            {
                writer.Write((string?)effect["Bone"]);
                writer.Write((string?)effect["Effect"]);
            }

            JsonArray arrayAnimations = instructionNode["AnimationClips"].AsArray();
            InterpretAnimations(arrayAnimations, writer);
            JsonArray arrayEquipment = instructionNode["Equipment"].AsArray();
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
            JsonArray characterConditions = instructionNode["EventConditions"].AsArray();
            InterpretCondition(characterConditions, writer);

            writer.Write((float)instructionNode["AlertRadius"]);
            writer.Write((float)instructionNode["GroupChase"]);
            writer.Write((float)instructionNode["GroupSeperation"]);
            writer.Write((float)instructionNode["GroupCohesion"]);
            writer.Write((float)instructionNode["GroupAlignment"]);
            writer.Write((float)instructionNode["GroupWander"]);
            writer.Write((float)instructionNode["FriendlyAvoidance"]);
            writer.Write((float)instructionNode["EnemyAvoidance"]);
            writer.Write((float)instructionNode["SightAvoidance"]);
            writer.Write((float)instructionNode["DangerAvoidance"]);
            writer.Write((float)instructionNode["AngerWeight"]);
            writer.Write((float)instructionNode["DistanceWeight"]);
            writer.Write((float)instructionNode["HealthWeight"]);
            writer.Write((bool)instructionNode["Flocking"]);
            writer.Write((float)instructionNode["BreakFreeStrength"]);
            InterpretAbilities(instructionNode["Abilities"].AsArray(), writer);
            JsonArray moveAbilities = instructionNode["MoveAbilities"].AsArray();
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
            InterpretAura(instructionNode["Buffs"].AsArray(), writer);
            InterpretAura(instructionNode["Auras"].AsArray(), writer);

            writer.Close(); //END
        }
        private void InterpretAbilities(JsonArray abilityArray, BinaryWriter writer)
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

        private void InterpretCondition(JsonArray conditionHolder, BinaryWriter writer)
        {
            writer.Write(conditionHolder.Count);
            foreach (JsonObject condition in conditionHolder)
            {
                writer.Write((byte)Enum.Parse(typeof(EventConditionType), (string)condition["ConditionType"], true));
                writer.Write((int)condition["Hitpoints"]);
                writer.Write((int)Enum.Parse(typeof(Elements), (string)condition["Element"], true));
                writer.Write((float)condition["Threshold"]);
                writer.Write((float)condition["Time"]);
                writer.Write((bool)condition["Repeat"]);
                JsonArray events = (JsonArray)condition["Events"];
                writer.Write(events.Count);

                foreach (JsonObject action in events)
                {
                    EventType eventType = (EventType)Enum.Parse(typeof(EventType), (string)action["EventType"], true);
                    writer.Write((byte)eventType);

                    JsonObject itemEvent = action["Event"].AsObject();

                    if (eventType == EventType.Damage)
                    {
                        writer.Write((int)Enum.Parse(typeof(AttackProperties), (string)itemEvent["AttackProperty"], true));
                        writer.Write((int)Enum.Parse(typeof(Elements), (string)itemEvent["Element"], true));
                        writer.Write((float)itemEvent["Amount"]);
                        writer.Write((float)itemEvent["Magnitude"]);
                        writer.Write((bool)itemEvent["VelocityBased"]);
                    }

                    else if (eventType == EventType.Splash)
                    {
                        writer.Write((int)Enum.Parse(typeof(AttackProperties), (string)itemEvent["AttackProperty"], true));
                        writer.Write((int)Enum.Parse(typeof(Elements), (string)itemEvent["Element"], true));
                        writer.Write((int)itemEvent["Amount"]);
                        writer.Write((float)itemEvent["Magnitude"]);
                        writer.Write((float)itemEvent["Radius"]);
                    }

                    else if (eventType == EventType.Sound)
                    {
                        writer.Write((int)Enum.Parse(typeof(Banks), (string)itemEvent["Bank"], true));
                        writer.Write((string)itemEvent["Cue"]);
                        writer.Write((float)itemEvent["Magnitude"]);
                        writer.Write((bool)itemEvent["StopOnRemove"]);
                    }

                    else if (eventType == EventType.Effect)
                    {
                        writer.Write((bool)itemEvent["Follow"]);
                        writer.Write((bool)itemEvent["WorldAligned"]);
                        writer.Write((string)itemEvent["Effect"]);
                    }

                    if (eventType == EventType.Remove)
                    {
                        writer.Write((int)itemEvent["Bounces"]);
                    }

                    if (eventType == EventType.CameraShake)
                    {
                        writer.Write((float)itemEvent["Time"]);
                        writer.Write((float)itemEvent["Magnitude"]);
                        writer.Write((bool)itemEvent["AtPosition"]);
                    }

                    //TODO DECAL

                    else if (eventType == EventType.Blast)
                    {
                        throw new NotImplementedException();
                    }

                    else if (eventType == EventType.Spawn)
                    {
                        writer.Write((string)itemEvent["Type"]);
                        writer.Write((string?)itemEvent["IdleAnimation"] ?? string.Empty);
                        writer.Write((string?)itemEvent["SpawnAnimation"] ?? string.Empty);
                        writer.Write((float)itemEvent["Health"]);
                        writer.Write((int)Enum.Parse(typeof(Order), (string)itemEvent["Order"], true));
                        writer.Write((int)Enum.Parse(typeof(ReactTo), (string)itemEvent["ReactTo"], true));
                        writer.Write((int)Enum.Parse(typeof(Order), (string)itemEvent["Reaction"], true));
                        writer.Write((float)itemEvent["Rotation"]);

                        JsonArray array = itemEvent["Offset"].AsArray();

                        for (int i = 0; i < array.Count; i++)
                            writer.Write((float)array[i]);
                    }

                    else if (eventType == EventType.SpawnGibs)
                    {
                        writer.Write((int)itemEvent["StartIndex"]);
                        writer.Write((int)itemEvent["EndIndex"]);
                    }

                    else if (eventType == EventType.SpawnItem)
                    {
                        writer.Write((string)itemEvent["Item"]);
                    }

                    else if (eventType == EventType.SpawnMagick)
                    {
                        writer.Write((string)itemEvent["SetToRandom"]);
                        writer.Write((string)itemEvent["MagickType"]);
                    }

                    else if (eventType == EventType.SpawnMissile)
                    {
                        writer.Write((string)itemEvent["Type"]);
                        JsonArray array = itemEvent["Velocity"].AsArray();

                        for (int i = 0; i < array.Count; i++)
                            writer.Write((float)array[i]);
                        writer.Write((bool)itemEvent["Facing"]);
                    }


                    else if (eventType == EventType.Light)
                    {
                        writer.Write((float)itemEvent["Radius"]);
                        JsonArray array = itemEvent["DiffuseColor"].AsArray();

                        for (int i = 0; i < array.Count; i++)
                            writer.Write((float)array[i]);

                        array = itemEvent["AmbientColor"].AsArray();

                        for (int i = 0; i < array.Count; i++)
                            writer.Write((float)array[i]);

                        writer.Write((float)itemEvent["SpecularAmount"]);
                        writer.Write((byte)Enum.Parse(typeof(LightVariationType), (string?)itemEvent["LightVariationType"], true));
                        writer.Write((float)itemEvent["VariationAmount"]);
                        writer.Write((float)itemEvent["VariationSpeed"]);
                    }

                    else if (eventType == EventType.CastMagick)
                    {
                        writer.Write((string)itemEvent["MagickType"]);
                        JsonArray elements = itemEvent["Elements"].AsArray();
                        writer.Write(elements.Count);
                        foreach (string element in elements)
                        {
                            writer.Write((int)Enum.Parse(typeof(Elements), element, true));
                        }
                    }

                    else if (eventType == EventType.DamageOwner)
                    {
                        writer.Write((int)Enum.Parse(typeof(AttackProperties), (string)itemEvent["AttackProperty"], true));
                        writer.Write((int)Enum.Parse(typeof(Elements), (string)itemEvent["Element"], true));
                        writer.Write((float)itemEvent["Amount"]);
                        writer.Write((float)itemEvent["Magnitude"]);
                        writer.Write((bool)itemEvent["VelocityBased"]);
                    }

                    if (eventType == EventType.Callback)
                    {
                        throw new NotSupportedException();
                    }

                }
            }
        }
        private void InterpretBuff(JsonArray buffArray, BinaryWriter writer)
        {
            byte buff = (byte)Enum.Parse(typeof(BuffType), (string)buffArray["BuffType"], true);
            writer.Write(buff);
            writer.Write((byte)Enum.Parse(typeof(VisualCategory), (string)buffArray["BuffVisualCategory"], true));
            JsonArray buffColor = buffArray["BuffColor"].AsArray();
            for (int i = 0; i < buffColor.Count; i++)
                writer.Write((float)buffColor[i]);

            writer.Write((float)buffArray["BuffRadius"]);
            writer.Write((string?)buffArray["BuffEffect"]);
            if (buff <= 1)
            {
                writer.Write((int)Enum.Parse(typeof(AttackProperties), (string)buffArray["AttackProperty"], true));
                writer.Write((int)Enum.Parse(typeof(Elements), (string)buffArray["Element"], true));
                writer.Write((float)buffArray["Amount"]);
                writer.Write((float)buffArray["Magnitude"]);
            }
            else if (buff == 2)
            {
                writer.Write((int)Enum.Parse(typeof(Elements), (string)buffArray["Element"], true));
                writer.Write((float)buffArray["Multiplier"]);
                writer.Write((float)buffArray["Modifier"]);
                writer.Write((bool)buffArray["StatusImmunity"]);
            }
            else if (buff == 4)
            {
                writer.Write((float)buffArray["BoostAmount"]);
            }
            else if (buff == 5)
            {
                writer.Write((float)buffArray["AggroReduceAmount"]);
            }
            else if (buff == 6)
            {
                writer.Write((float)buffArray["HealthMultiplier"]);
                writer.Write((float)buffArray["HealthModifier"]);
            }
            else if (buff == 7)
            {
                writer.Write((float)buffArray["SpellTimeMultiplier"]);
                writer.Write((float)buffArray["SpellTimeModifier"]);
            }
            else if (buff == 8)
            {
                writer.Write((float)buffArray["SpellRangeMultiplier"]);
                writer.Write((float)buffArray["SpellRangeModifier"]);
            }
        }
        private void InterpretAura(JsonArray auraArray, BinaryWriter writer)
        {
            writer.Write(auraArray.Count);
            foreach (JsonObject aura in auraArray)
            {
                byte target = (byte)Enum.Parse(typeof(AuraTarget), (string)aura["AuraTarget"], true);
                byte auraType = (byte)Enum.Parse(typeof(AuraType), (string)aura["AuraType"], true);
                byte visualCategory = (byte)Enum.Parse(typeof(VisualCategory), (string)aura["VisualCategory"], true);
                float[] color = new float[3];
                JsonArray array = aura["Color"].AsArray();
                string? effect = (string?)aura["Effect"];
                float ttl = (float)aura["Duration"];
                float radius = (float)aura["Radius"];
                string? types = (string?)aura["Types"];
                int faction = (int)Enum.Parse(typeof(Factions), (string)aura["Faction"], true);

                writer.Write(target);
                writer.Write(auraType);
                writer.Write(visualCategory);
                for (int i = 0; i < array.Count; i++)
                {
                    color[i] = (float)array[i];
                    writer.Write((float)array[i]);
                }
                writer.Write(effect);
                writer.Write(ttl);
                writer.Write(radius);
                writer.Write(types);
                writer.Write(faction);

                if ((AuraType)auraType == AuraType.Buff)
                {
                    byte buff = (byte)Enum.Parse(typeof(BuffType), (string)aura["BuffType"], true);
                    writer.Write(buff);
                    writer.Write((byte)Enum.Parse(typeof(VisualCategory), (string)aura["BuffVisualCategory"], true));
                    JsonArray buffColor = aura["BuffColor"].AsArray();
                    for (int i = 0; i < buffColor.Count; i++)
                        writer.Write((float)buffColor[i]);

                    writer.Write((float)aura["BuffRadius"]);
                    writer.Write((string?)aura["BuffEffect"]);
                    if (buff <= 1)
                    {
                        writer.Write((int)Enum.Parse(typeof(AttackProperties), (string)aura["AttackProperty"], true));
                        writer.Write((int)Enum.Parse(typeof(Elements), (string)aura["Element"], true));
                        writer.Write((float)aura["Amount"]);
                        writer.Write((float)aura["Magnitude"]);
                    }
                    else if (buff == 2)
                    {
                        writer.Write((int)Enum.Parse(typeof(Elements), (string)aura["Element"], true));
                        writer.Write((float)aura["Multiplier"]);
                        writer.Write((float)aura["Modifier"]);
                        writer.Write((bool)aura["StatusImmunity"]);
                    }
                    else if (buff == 4)
                    {
                        writer.Write((float)aura["BoostAmount"]);
                    }
                    else if (buff == 5)
                    {
                        writer.Write((float)aura["AggroReduceAmount"]);
                    }
                    else if (buff == 6)
                    {
                        writer.Write((float)aura["HealthMultiplier"]);
                        writer.Write((float)aura["HealthModifier"]);
                    }
                    else if (buff == 7)
                    {
                        writer.Write((float)aura["SpellTimeMultiplier"]);
                        writer.Write((float)aura["SpellTimeModifier"]);
                    }
                    else if (buff == 8)
                    {
                        writer.Write((float)aura["SpellRangeMultiplier"]);
                        writer.Write((float)aura["SpellRangeModifier"]);
                    }
                }
                else if ((AuraType)auraType == AuraType.Deflect)
                {
                    writer.Write((float)aura["DeflectStrength"]);
                }
                else if ((AuraType)auraType == AuraType.Boost)
                {
                    writer.Write((float)aura["BoostStrength"]);
                }
                else if ((AuraType)auraType == AuraType.LifeSteal)
                {
                    writer.Write((float)aura["LifestealAmount"]);
                }
                else if ((AuraType)auraType == AuraType.Love)
                {
                    writer.Write((float)aura["CharmRadius"]);
                    writer.Write((float)aura["CharmDuration"]);
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
                            writer.Write((float)action["Bone"]);
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
                        writer.Write((Single)action["MaxLength"]);
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
