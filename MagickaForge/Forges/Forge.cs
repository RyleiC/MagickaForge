using MagickaForge.Enums;
using MagickaForge.Forges.Character;
using MagickaForge.Forges.Item;
using System.Text.Json.Nodes;

namespace MagickaForge.Forges
{
    public abstract class Forge
    {

        protected BinaryWriter writer;
        protected JsonNode jsonRoot;

        protected const int VECTOR3_LENGTH = 3;

        protected Forge(string InstructionPath, JsonNode JsonRoot)
        {
            writer = new BinaryWriter(File.OpenWrite(Path.ChangeExtension(InstructionPath, ".xnb")));
            jsonRoot = JsonRoot;
        }

        public static void GenerateXNBs(string InstructionPath)
        {
            FileAttributes fileAttributes = File.GetAttributes(InstructionPath);
            if (fileAttributes.HasFlag(FileAttributes.Directory))
            {
                foreach (string file in Directory.GetFiles(InstructionPath, "*.json"))
                {
                    GenerateXNBs(file);
                }
                return;
            }
            else
            {

                if (!File.Exists(InstructionPath))
                {
                    throw new FileNotFoundException(InstructionPath);
                }

                JsonNode jsonRoot;

                using (var reader = new StreamReader(File.Open(InstructionPath, FileMode.Open, FileAccess.Read)))
                {
                    jsonRoot = JsonNode.Parse(reader.ReadToEnd()).AsObject();
                    reader.Close();
                }

                string XNBType = ((string)jsonRoot["XNBType"]).ToLower();

                switch (XNBType)
                {
                    case "item":
                        ItemForge itemForge = new ItemForge(InstructionPath, jsonRoot);
                        break;

                    case "character":
                        CharacterForge characterForge = new CharacterForge(InstructionPath, jsonRoot, (bool)jsonRoot["ModernMagicka"]);
                        break;

                    default:
                        throw new ArgumentException("Your XNB type does not match any existing forges, make sure it is either an item or a character!");
                }
            }
        }

        protected void InterpretAura(JsonArray auraArray)
        {
            writer.Write(auraArray.Count);
            foreach (JsonObject aura in auraArray)
            {
                AuraType auraType = (AuraType)Enum.Parse(typeof(AuraType), (string)aura["AuraType"], true);

                writer.Write((byte)Enum.Parse(typeof(AuraTarget), (string)aura["AuraTarget"], true));
                writer.Write((byte)auraType);
                writer.Write((byte)Enum.Parse(typeof(VisualCategory), (string)aura["VisualCategory"], true));

                JsonArray color = aura["Color"].AsArray();

                for (var i = 0; i < VECTOR3_LENGTH; i++)
                {
                    writer.Write((float)color[i]);
                }

                writer.Write((string?)aura["Effect"]);
                writer.Write((float)aura["Duration"]);
                writer.Write((float)aura["Radius"]);
                writer.Write((string?)aura["Types"]);
                writer.Write((int)Enum.Parse(typeof(Factions), (string)aura["Faction"], true));

                if (auraType == AuraType.Buff)
                {
                    BuffType buff = (BuffType)Enum.Parse(typeof(BuffType), (string)aura["BuffType"], true);
                    writer.Write((byte)buff);
                    writer.Write((byte)Enum.Parse(typeof(VisualCategory), (string)aura["BuffVisualCategory"], true));

                    JsonArray buffColor = aura["BuffColor"].AsArray();
                    for (var i = 0; i < VECTOR3_LENGTH; i++)
                    {
                        writer.Write((float)buffColor[i]);
                    }


                    writer.Write((float)aura["BuffRadius"]);
                    writer.Write((string?)aura["BuffEffect"]);

                    if (buff == BuffType.BoostDamage || buff == BuffType.DealDamage)
                    {
                        writer.Write((int)Enum.Parse(typeof(AttackProperties), (string)aura["AttackProperty"], true));
                        writer.Write((int)Enum.Parse(typeof(Elements), (string)aura["Element"], true));
                        writer.Write((float)aura["Amount"]);
                        writer.Write((float)aura["Magnitude"]);
                    }
                    else if (buff == BuffType.Resistance)
                    {
                        writer.Write((int)Enum.Parse(typeof(Elements), (string)aura["Element"], true));
                        writer.Write((float)aura["Multiplier"]);
                        writer.Write((float)aura["Modifier"]);
                        writer.Write((bool)aura["StatusImmunity"]);
                    }
                    else if (buff == BuffType.Boost)
                    {
                        writer.Write((float)aura["BoostAmount"]);
                    }
                    else if (buff == BuffType.ReduceAgro)
                    {
                        writer.Write((float)aura["AggroReduceAmount"]);
                    }
                    else if (buff == BuffType.ModifyHitPoints)
                    {
                        writer.Write((float)aura["HealthMultiplier"]);
                        writer.Write((float)aura["HealthModifier"]);
                    }
                    else if (buff == BuffType.ModifySpellTTL)
                    {
                        writer.Write((float)aura["SpellTimeMultiplier"]);
                        writer.Write((float)aura["SpellTimeModifier"]);
                    }
                    else if (buff == BuffType.ModifySpellRange)
                    {
                        writer.Write((float)aura["SpellRangeMultiplier"]);
                        writer.Write((float)aura["SpellRangeModifier"]);
                    }
                }
                else if (auraType == AuraType.Deflect)
                {
                    writer.Write((float)aura["DeflectStrength"]);
                }
                else if (auraType == AuraType.Boost)
                {
                    writer.Write((float)aura["BoostStrength"]);
                }
                else if (auraType == AuraType.LifeSteal)
                {
                    writer.Write((float)aura["LifestealAmount"]);
                }
                else if (auraType == AuraType.Love)
                {
                    writer.Write((float)aura["CharmRadius"]);
                    writer.Write((float)aura["CharmDuration"]);
                }
            }
        }

        protected void InterpretCondition(JsonArray conditionHolder)
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

                    JsonObject conditionEvent = action["Event"].AsObject();

                    if (eventType == EventType.Damage)
                    {
                        writer.Write((int)Enum.Parse(typeof(AttackProperties), (string)conditionEvent["AttackProperty"], true));
                        writer.Write((int)Enum.Parse(typeof(Elements), (string)conditionEvent["Element"], true));
                        writer.Write((float)conditionEvent["Amount"]);
                        writer.Write((float)conditionEvent["Magnitude"]);
                        writer.Write((bool)conditionEvent["VelocityBased"]);
                    }

                    else if (eventType == EventType.Splash)
                    {
                        writer.Write((int)Enum.Parse(typeof(AttackProperties), (string)conditionEvent["AttackProperty"], true));
                        writer.Write((int)Enum.Parse(typeof(Elements), (string)conditionEvent["Element"], true));
                        writer.Write((int)conditionEvent["Amount"]);
                        writer.Write((float)conditionEvent["Magnitude"]);
                        writer.Write((float)conditionEvent["Radius"]);
                    }

                    else if (eventType == EventType.Sound)
                    {
                        writer.Write((int)Enum.Parse(typeof(Banks), (string)conditionEvent["Bank"], true));
                        writer.Write((string)conditionEvent["Cue"]);
                        writer.Write((float)conditionEvent["Magnitude"]);
                        writer.Write((bool)conditionEvent["StopOnRemove"]);
                    }

                    else if (eventType == EventType.Effect)
                    {
                        writer.Write((bool)conditionEvent["Follow"]);
                        writer.Write((bool)conditionEvent["WorldAligned"]);
                        writer.Write((string)conditionEvent["Effect"]);
                    }

                    if (eventType == EventType.Remove)
                    {
                        writer.Write((int)conditionEvent["Bounces"]);
                    }

                    if (eventType == EventType.CameraShake)
                    {
                        writer.Write((float)conditionEvent["Time"]);
                        writer.Write((float)conditionEvent["Magnitude"]);
                        writer.Write((bool)conditionEvent["AtPosition"]);
                    }

                    else if (eventType == EventType.Blast)
                    {
                        throw new NotImplementedException("Blast is not a valid event option! Please use a different event type!");
                    }

                    else if (eventType == EventType.Spawn)
                    {
                        writer.Write((string)conditionEvent["Type"]!);
                        writer.Write((string)conditionEvent["IdleAnimation"]! ?? string.Empty);
                        writer.Write((string)conditionEvent["SpawnAnimation"] ?? string.Empty);
                        writer.Write((float)conditionEvent["Health"]);
                        writer.Write((int)Enum.Parse(typeof(Order), (string)conditionEvent["Order"], true));
                        writer.Write((int)Enum.Parse(typeof(ReactTo), (string)conditionEvent["ReactTo"], true));
                        writer.Write((int)Enum.Parse(typeof(Order), (string)conditionEvent["Reaction"], true));
                        writer.Write((float)conditionEvent["Rotation"]);

                        JsonArray offset = conditionEvent["Offset"].AsArray();

                        for (var i = 0; i < VECTOR3_LENGTH; i++)
                        {
                            writer.Write((float)offset[i]);
                        }
                    }

                    else if (eventType == EventType.SpawnGibs)
                    {
                        writer.Write((int)conditionEvent["StartIndex"]);
                        writer.Write((int)conditionEvent["EndIndex"]);
                    }

                    else if (eventType == EventType.SpawnItem)
                    {
                        writer.Write((string)conditionEvent["Item"]);
                    }

                    else if (eventType == EventType.SpawnMagick)
                    {
                        writer.Write((string)conditionEvent["SetToRandom"]);
                        writer.Write((string)conditionEvent["MagickType"]);
                    }

                    else if (eventType == EventType.SpawnMissile)
                    {
                        writer.Write((string)conditionEvent["Type"]);
                        JsonArray velocity = conditionEvent["Velocity"].AsArray();

                        for (var i = 0; i < VECTOR3_LENGTH; i++)
                        {
                            writer.Write((float)velocity[i]);
                        }

                        writer.Write((bool)conditionEvent["Facing"]);
                    }


                    else if (eventType == EventType.Light)
                    {
                        writer.Write((float)conditionEvent["Radius"]);
                        JsonArray diffuseColor = conditionEvent["DiffuseColor"].AsArray();

                        for (var i = 0; i < VECTOR3_LENGTH; i++)
                        {
                            writer.Write((float)diffuseColor[i]);
                        }

                        JsonArray ambientColor = conditionEvent["AmbientColor"].AsArray();

                        for (var i = 0; i < VECTOR3_LENGTH; i++)
                        {
                            writer.Write((float)ambientColor[i]);
                        }

                        writer.Write((float)conditionEvent["SpecularAmount"]);
                        writer.Write((byte)Enum.Parse(typeof(LightVariationType), (string?)conditionEvent["LightVariationType"], true));
                        writer.Write((float)conditionEvent["VariationAmount"]);
                        writer.Write((float)conditionEvent["VariationSpeed"]);
                    }

                    else if (eventType == EventType.CastMagick)
                    {
                        writer.Write((string)conditionEvent["MagickType"]);
                        JsonArray elements = conditionEvent["Elements"].AsArray();
                        writer.Write(elements.Count);
                        foreach (string element in elements)
                        {
                            writer.Write((int)Enum.Parse(typeof(Elements), element, true));
                        }
                    }

                    else if (eventType == EventType.DamageOwner)
                    {
                        writer.Write((int)Enum.Parse(typeof(AttackProperties), (string)conditionEvent["AttackProperty"], true));
                        writer.Write((int)Enum.Parse(typeof(Elements), (string)conditionEvent["Element"], true));
                        writer.Write((float)conditionEvent["Amount"]);
                        writer.Write((float)conditionEvent["Magnitude"]);
                        writer.Write((bool)conditionEvent["VelocityBased"]);
                    }

                    if (eventType == EventType.Callback)
                    {
                        throw new NotSupportedException("Callback is not a valid event option! Please use a different event type!");
                    }

                }
            }
        }
    }
}
