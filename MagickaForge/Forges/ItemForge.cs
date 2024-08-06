using MagickaForge.Enums;
using System.Data;
using System.Text.Json.Nodes;

namespace MagickaForge.Forges
{
#pragma warning disable CS8602
#pragma warning disable CS8604
    public class ItemForge
    {
        private readonly byte[] XNB_HEADER =
        {
            0x58, 0x4E, 0x42, 0x77, 0x04, 0x00, 0x01, 0x00, 0x00, 0x00, 0x01, 0x4C,
            0x4D, 0x61, 0x67, 0x69, 0x63, 0x6B, 0x61, 0x2E, 0x43, 0x6F, 0x6E, 0x74,
            0x65, 0x6E, 0x74, 0x52, 0x65, 0x61, 0x64, 0x65, 0x72, 0x73, 0x2E, 0x49,
            0x74, 0x65, 0x6D, 0x52, 0x65, 0x61, 0x64, 0x65, 0x72, 0x2C, 0x20, 0x4D,
            0x61, 0x67, 0x69, 0x63, 0x6B, 0x61, 0x2C, 0x20, 0x56, 0x65, 0x72, 0x73,
            0x69, 0x6F, 0x6E, 0x3D, 0x31, 0x2E, 0x30, 0x2E, 0x30, 0x2E, 0x30, 0x2C,
            0x20, 0x43, 0x75, 0x6C, 0x74, 0x75, 0x72, 0x65, 0x3D, 0x6E, 0x65, 0x75,
            0x74, 0x72, 0x61, 0x6C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01
        };

        public void InstructionsToXNB(string InstructionPath)
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
            writer.Write((string?)instructionNode["LocalizedDescription"]);

            JsonArray arraySounds = instructionNode["Sounds"].AsArray();
            writer.Write(arraySounds.Count);

            foreach (JsonObject sounds in arraySounds)
            {
                writer.Write((string?)sounds["Cue"]);
                writer.Write((int)Enum.Parse(typeof(Banks), (string?)sounds["Wavebank"], true));
            }

            writer.Write((bool)instructionNode["Grabbable"]);
            writer.Write((bool)instructionNode["Bound"]);
            writer.Write((int)instructionNode["BlockStrength"]);
            writer.Write((byte)Enum.Parse(typeof(WeaponClass), (string?)instructionNode["WeaponClass"], true));
            writer.Write((float)instructionNode["CooldownTime"]);

            writer.Write((bool)instructionNode["HideModel"]);
            writer.Write((bool)instructionNode["HideEffects"]);
            writer.Write((bool)instructionNode["PauseSounds"]);

            JsonArray arrayResistances = (JsonArray)instructionNode["Resistances"];
            writer.Write(arrayResistances.Count);
            foreach (JsonObject resistance in arrayResistances)
            {
                writer.Write((int)Enum.Parse(typeof(Elements), (string?)resistance["Element"], true));
                writer.Write((float)resistance["Multiplier"]);
                writer.Write((float)resistance["Modifier"]);
                writer.Write((bool)resistance["StatusImmunity"]);
            }

            writer.Write((byte)Enum.Parse(typeof(PassiveAbilities), (string?)instructionNode["PassiveAbilityType"], true));
            writer.Write((float)instructionNode["PassiveAbilityStrength"]);

            JsonArray arrayEffects = instructionNode["Effects"].AsArray();
            writer.Write(arrayEffects.Count);
            foreach (string effect in arrayEffects)
            {
                writer.Write(effect);
            }

            JsonArray arrayLights = (JsonArray)instructionNode["Lights"];
            writer.Write(arrayLights.Count);
            foreach (JsonObject light in arrayLights)
            {
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

            bool hasSpecialAbility = (bool)instructionNode["HasSpecialAbility"];
            writer.Write(hasSpecialAbility);
            if (hasSpecialAbility)
            {
                writer.Write((float)instructionNode["SpecialAbilityCooldown"]);
                JsonObject specialAbility = instructionNode["SpecialAbility"].AsObject();
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
            writer.Write((float)instructionNode["MeleeRange"]);
            writer.Write((bool)instructionNode["MeleeMultihit"]);

            InterpretCondition(instructionNode["MeleeConditions"].AsArray(), writer);

            writer.Write((float)instructionNode["RangedRange"]);
            writer.Write((bool)instructionNode["Facing"]);
            writer.Write((float)instructionNode["Homing"]);
            writer.Write((float)instructionNode["RangedElevation"]);
            writer.Write((float)instructionNode["RangedDanger"]);
            writer.Write((float)instructionNode["GunRange"]);
            writer.Write((int)instructionNode["GunClip"]);
            writer.Write((int)instructionNode["GunRate"]);
            writer.Write((float)instructionNode["GunAccuracy"]);

            writer.Write((string?)instructionNode["GunSound"]);
            writer.Write((string?)instructionNode["GunMuzzleEffect"]);
            writer.Write((string?)instructionNode["GunShellEffect"]);
            writer.Write((float)instructionNode["GunTracerVelocity"]);
            writer.Write((string?)instructionNode["GunTracer"]);
            writer.Write((string?)instructionNode["GunNonTracer"]);

            InterpretCondition(instructionNode["GunConditions"].AsArray(), writer);
            writer.Write((string?)instructionNode["ProjectileModel"]);
            InterpretCondition(instructionNode["RangedConditions"].AsArray(), writer);
            writer.Write((float)instructionNode["Scale"]);
            writer.Write((string?)instructionNode["Model"]);
            InterpretAura(instructionNode["Auras"].AsArray(), writer);

            writer.Close(); //END
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
    }
}
