using MagickaForge.Utils;
using System.Text.Json.Nodes;

namespace MagickaForge.Forges.Item
{
    public class ItemForge : Forge
    {
        //This is contains the initial header for a item XNB before information is written down and is uniform across all files
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

        public ItemForge(string InstructionPath, JsonNode JsonRoot) : base(InstructionPath, JsonRoot)
        {
            InstructionsToXNB();
        }

        protected void InstructionsToXNB()
        {

            writer.Write(XNB_HEADER); //START

            writer.Write((string?)jsonRoot["Name"]);
            writer.Write((string?)jsonRoot["LocalizedName"]);
            writer.Write((string?)jsonRoot["LocalizedDescription"]);

            JsonArray arraySounds = jsonRoot["Sounds"].AsArray();
            writer.Write(arraySounds.Count);

            foreach (JsonObject sounds in arraySounds)
            {
                writer.Write((string?)sounds["Cue"]);
                writer.Write((int)Enum.Parse(typeof(Banks), (string?)sounds["Wavebank"], true));
            }

            writer.Write((bool)jsonRoot["Grabbable"]);
            writer.Write((bool)jsonRoot["Bound"]);
            writer.Write((int)jsonRoot["BlockStrength"]);
            writer.Write((byte)Enum.Parse(typeof(WeaponClass), (string?)jsonRoot["WeaponClass"], true));
            writer.Write((float)jsonRoot["CooldownTime"]);

            writer.Write((bool)jsonRoot["HideModel"]);
            writer.Write((bool)jsonRoot["HideEffects"]);
            writer.Write((bool)jsonRoot["PauseSounds"]);

            JsonArray arrayResistances = jsonRoot["Resistances"].AsArray();
            writer.Write(arrayResistances.Count);
            foreach (JsonObject resistance in arrayResistances)
            {
                writer.Write((int)Enum.Parse(typeof(Elements), (string?)resistance["Element"], true));
                writer.Write((float)resistance["Multiplier"]);
                writer.Write((float)resistance["Modifier"]);
                writer.Write((bool)resistance["StatusImmunity"]);
            }

            writer.Write((byte)Enum.Parse(typeof(PassiveAbilities), (string?)jsonRoot["PassiveAbilityType"], true));
            writer.Write((float)jsonRoot["PassiveAbilityStrength"]);

            JsonArray arrayEffects = jsonRoot["Effects"].AsArray();
            writer.Write(arrayEffects.Count);
            foreach (string effect in arrayEffects)
            {
                writer.Write(effect);
            }

            JsonArray arrayLights = jsonRoot["Lights"].AsArray();
            writer.Write(arrayLights.Count);
            if (arrayLights.Count > 1)
            {
                throw new Exception("Items may only have 1 light!");
            }
            foreach (JsonObject light in arrayLights)
            {
                writer.Write((float)light["Radius"]);
                JsonArray diffuseColor = light["DiffuseColor"].AsArray();

                for (var i = 0; i < VECTOR3_LENGTH; i++)
                {
                    writer.Write((float)diffuseColor[i]);
                }

                JsonArray ambientColor = light["AmbientColor"].AsArray();

                for (var i = 0; i < VECTOR3_LENGTH; i++)
                {
                    writer.Write((float)ambientColor[i]);
                }

                writer.Write((float)light["SpecularAmount"]);
                writer.Write((byte)Enum.Parse(typeof(LightVariationType), (string?)light["LightVariationType"], true));
                writer.Write((float)light["VariationAmount"]);
                writer.Write((float)light["VariationSpeed"]);
            }

            bool hasSpecialAbility = (bool)jsonRoot["HasSpecialAbility"];
            writer.Write(hasSpecialAbility);
            if (hasSpecialAbility)
            {
                writer.Write((float)jsonRoot["SpecialAbilityCooldown"]);
                JsonObject specialAbility = jsonRoot["SpecialAbility"].AsObject();
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
            writer.Write((float)jsonRoot["MeleeRange"]);
            writer.Write((bool)jsonRoot["MeleeMultihit"]);

            InterpretCondition(jsonRoot["MeleeConditions"].AsArray());

            writer.Write((float)jsonRoot["RangedRange"]);
            writer.Write((bool)jsonRoot["Facing"]);
            writer.Write((float)jsonRoot["Homing"]);
            writer.Write((float)jsonRoot["RangedElevation"]);
            writer.Write((float)jsonRoot["RangedDanger"]);
            writer.Write((float)jsonRoot["GunRange"]);
            writer.Write((int)jsonRoot["GunClip"]);
            writer.Write((int)jsonRoot["GunRate"]);
            writer.Write((float)jsonRoot["GunAccuracy"]);

            writer.Write((string?)jsonRoot["GunSound"]);
            writer.Write((string?)jsonRoot["GunMuzzleEffect"]);
            writer.Write((string?)jsonRoot["GunShellEffect"]);
            writer.Write((float)jsonRoot["GunTracerVelocity"]);
            writer.Write((string?)jsonRoot["GunTracer"]);
            writer.Write((string?)jsonRoot["GunNonTracer"]);

            InterpretCondition(jsonRoot["GunConditions"].AsArray());
            writer.Write((string?)jsonRoot["ProjectileModel"]);
            InterpretCondition(jsonRoot["RangedConditions"].AsArray());
            writer.Write((float)jsonRoot["Scale"]);
            writer.Write((string?)jsonRoot["Model"]);
            InterpretAura(jsonRoot["Auras"].AsArray());

            writer.Close(); //END
        }
    }
}
