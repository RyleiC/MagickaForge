namespace MagickaForge.Enums
{
    public enum WeaponClass : byte
    {
        Default,
        Thrust_Fast,
        Thrust_Medium,
        Thrust_Slow,
        Crush_Fast,
        Crush_Medium,
        Crush_Slow,
        Slash_Fast,
        Slash_Medium,
        Slash_Slow,
        Throw_Fast,
        Throw_Medium,
        Throw_Slow,
        Unarmed,
        Handgun,
        Rifle,
        Machinegun,
        Heavy,
        Staff,
        NewAnimationSet0,
        NewAnimationSet1,
        NewAnimationSet2,
        NewAnimationSet3,
        NewAnimationSet4,
        NewAnimationSet5,
        NewAnimationSet6,
        NewAnimationSet7
    }

    [Flags]
    public enum Elements
    {
        None = 0,
        Earth = 1,
        Physical = 1,
        Water = 2,
        Cold = 4,
        Fire = 8,
        Lightning = 16,
        Arcane = 32,
        Life = 64,
        Shield = 128,
        Ice = 256,
        Steam = 512,
        Poison = 1024,
        Offensive = 65343,
        Defensive = 176,
        All = 65535,
        Magick = 65535,
        Basic = 255,
        Instant = 881,
        InstantPhysical = 369,
        InstantNonPhysical = 624,
        StatusEffects = 1614,
        ShieldElements = 224,
        PhysicalElements = 257,
        Beams = 96
    }

    public enum LightVariationType : byte
    {
        None,
        Sine,
        Flicker,
        Candle,
        Strobe
    }

    [Flags]
    public enum Banks : int
    {
        WaveBank = 1,
        Music = 2,
        Ambience = 4,
        UI = 8,
        Spells = 16,
        Characters = 32,
        Footsteps = 64,
        Weapons = 128,
        Misc = 256,
        Additional = 512,
        AdditionalMusic = 1024
    }

    public enum EventType : byte
    {
        Damage,
        Splash,
        Sound,
        Effect,
        Remove,
        CameraShake,
        Decal,
        Blast,
        Spawn,
        Overkill,
        SpawnGibs,
        SpawnItem,
        SpawnMagick,
        SpawnMissile,
        Light,
        CastMagick,
        DamageOwner,
        Callback
    }

    [Flags]
    public enum EventConditionType : byte
    {
        Default = 1,
        Hit = 2,
        Collision = 4,
        Damaged = 8,
        Timer = 16,
        Death = 32,
        OverKill = 64
    }

    [Flags]
    public enum AttackProperties : int
    {
        Damage = 1,
        Knockdown = 2,
        Pushed = 4,
        Knockback = 6,
        Piercing = 8,
        ArmourPiercing = 16,
        Status = 32,
        Entanglement = 64,
        Stun = 128,
        Bleed = 256
    }
    public enum Order : byte
    {
        None,
        Idle,
        Attack,
        Defend,
        Flee,
        Wander,
        Panic
    }

    [Flags]
    public enum ReactTo : byte
    {
        None = 0,
        Attack = 1,
        Proximity = 2
    }
    public enum PassiveAbilities : byte
    {
        None,
        ShieldBoost,
        AreaLifeDrain,
        ZombieDeterrent,
        ReduceAggro,
        EnhanceAllyMelee,
        AreaRegeneration,
        InverseArcaneLife,
        Zap,
        BirchSteam,
        WetLightning,
        MoveSpeed,
        Glow,
        Mjolnr,
        Gungner,
        MasterSword,
        DragonSlayer
    }

    public enum MagickType
    {
        None,
        Revive,
        Grease,
        Haste,
        Invisibility,
        Teleport,
        Fear,
        Charm,
        ThunderB,
        Rain,
        Tornado,
        Blizzard,
        MeteorS,
        Conflagration,
        ThunderS,
        TimeWarp,
        Vortex,
        SUndead,
        SElemental,
        SDeath,
        SPhoenix,
        Nullify,
        Corporealize,
        CTD,
        Napalm,
        Portal,
        TractorPull,
        ProppMagick,
        Levitate,
        ChainLightning,
        Confuse,
        Wave,
        PerformanceEnchantment,
        JudgementSpray,
        Amalgameddon
    }
    public enum AuraTarget : byte
    {
        Friendly,
        FriendlyButSelf,
        Enemy,
        All,
        AllButSelf,
        Self,
        Type,
        TypeButSelf,
        Faction,
        FactionButSelf
    }
    public enum AuraType : byte
    {
        Buff,
        Deflect,
        Boost,
        LifeSteal,
        Love
    }
    public enum VisualCategory : byte
    {
        None,
        Offensive,
        Defensive,
        Special
    }

    [Flags]
    public enum Factions
    {
        None = 0,
        Evil = 1,
        Wild = 2,
        Friendly = 4,
        Demon = 8,
        Undead = 16,
        Human = 32,
        Wizard = 64,
        Neutral = 255,
        Player0 = 256,
        Player1 = 512,
        Player2 = 1024,
        Player3 = 2048,
        Team_Red = 4096,
        Team_Blue = 8192,
        Players = 16128,
    }

    public enum BuffType : byte
    {
        BoostDamage,
        DealDamage,
        Resistance,
        Undying,
        Boost,
        ReduceAgro,
        ModifyHitPoints,
        ModifySpellTTL,
        ModifySpellRange
    }
}

