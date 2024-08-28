﻿namespace MagickaForge.Utils
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
        D = 1,
        Water = 2,
        Q = 2,
        Cold = 4,
        R = 4,
        Fire = 8,
        F = 8,
        Lightning = 16,
        A = 16,
        Arcane = 32,
        S = 32,
        Life = 64,
        W = 64,
        Shield = 128,
        E = 128,
        Ice = 256,
        QR = 256,
        RQ = 256,
        Steam = 512,
        QF = 512,
        FQ = 512,
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
        None = 0,
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

    public enum BloodType
    {
        regular,
        green,
        black,
        wood,
        insect,
        none
    }

    public enum Animations
    {
        None,
        idle,
        idlelong,
        idlelong0,
        idlelong1,
        idlelong2,
        idle_agg,
        idlelong_agg,
        idlelong_agg0,
        idlelong_agg1,
        idlelong_agg2,
        idle_wnd,
        idlelong_wnd,
        idlelong_wnd0,
        idlelong_wnd1,
        idlelong_wnd2,
        idle_grip,
        die0,
        die1,
        die_drown,
        die_drown0,
        die_drown1,
        die_drown2,
        hit,
        hit_fly,
        hit_stun_begin,
        hit_stun_end,
        hit_slide,
        attack_hit,
        attack_melee0,
        attack_melee1,
        attack_melee2,
        attack_melee3,
        attack_melee4,
        attack_ranged0,
        attack_ranged1,
        attack_ranged2,
        attack_ranged3,
        attack_recoil,
        block,
        boost,
        charge_area,
        charge_area_loop,
        charge_force,
        charge_force_loop,
        cast_area_blast,
        cast_area_fireworks,
        cast_area_ground,
        cast_area_lightning,
        cast_area_push,
        cast_charge,
        cast_force_lightning,
        cast_force_projectile,
        cast_force_push,
        cast_force_railgun,
        cast_force_shield,
        cast_force_spray,
        cast_force_zap,
        cast_magick_global,
        cast_magick_direct,
        cast_magick_self,
        cast_magick_sweep,
        cast_self,
        cast_sword,
        cast_sword_lightning,
        cast_sword_projectile,
        cast_sword_railgun,
        cast_sword_shield,
        cast_sword_spray,
        cast_teleport,
        cast_spell0,
        cast_spell1,
        cast_spell2,
        cast_spell3,
        cast_spell4,
        cast_spell5,
        cast_spell6,
        cast_spell7,
        cast_spell8,
        cast_spell9,
        chant,
        cutscene_fall,
        cutscene_eat,
        cutscene_eaten,
        cutscene_sit,
        cutscene_sit_talk0,
        cutscene_sit_talk1,
        cutscene_rise,
        cutscene_hide,
        cutscene_unhide,
        cutscene_cheer,
        cutscene_complete0,
        cutscene_complete1,
        cutscene_complete2,
        cutscene_complete3,
        cutscene_fail,
        cutscene_teleport_in,
        cutscene_teleport_out,
        cutscene_introduction,
        cutscene_idle,
        cutscene_talkfriendly0,
        cutscene_talkfriendly1,
        cutscene_talkidle,
        cutscene_talkagg0,
        cutscene_talkagg1,
        cutscene_talkagg2,
        cutscene_walklaugh,
        cutscene_slashvlad,
        cutscene_slashvladidle,
        cutscene_lookingforvlad,
        cutscene_shame,
        cutscene_woundedkneeling,
        cutscene_lastbreath,
        cutscene_stabbed,
        cutscene_stabbedidle,
        cutscene_woundedstanding,
        cutscene_woundedtalk,
        cutscene_losinghandbegin,
        cutscene_losinghandidle,
        cutscene_losinghandend,
        cutscene_diepose0,
        cutscene_diepose1,
        cutscene_flipcoin_begin,
        cutscene_flipcoin_idle,
        cutscene_flipcoin_end,
        cutscene_talkshame_begin,
        cutscene_talkshame_idle,
        cutscene_talkshame_end,
        emote_talk0,
        emote_point0,
        emote_terrified0,
        emote_horrified0,
        emote_sigh0,
        emote_rejoice0,
        emote_rejoice1,
        emote_confused0,
        emote_confused1,
        emote_confused2,
        emote_confused3,
        move_walk,
        move_wnd,
        move_run,
        move_sprint,
        move_panic,
        move_stumble,
        move_fall,
        move_jump_begin,
        move_jump_mid,
        move_jump_up,
        move_jump_down,
        move_jump_end,
        pickup_weapon,
        pickup_staff,
        pickup_magick,
        revive,
        talk_casual0,
        talk_casual1,
        talk_casual2,
        talk_casual3,
        talk_casual4,
        talk_alarmed0,
        talk_alarmed1,
        talk_alarmed2,
        talk_alarmed3,
        talk_greeting0,
        talk_greeting1,
        talk_terrified0,
        talk_terrified1,
        talk_sad0,
        talk_sad1,
        talk_intro0,
        talk_intro1,
        talk_intro2,
        use,
        spawn,
        spawn_woundedkneeling,
        despawn,
        removestatus0,
        removestatus1,
        removestatus2,
        spec_alert0,
        spec_alert1,
        spec_alert2,
        spec_alert3,
        spec_action0,
        spec_action1,
        spec_action2,
        spec_action3,
        spec_action4,
        spec_arm,
        spec_disarm,
        spec_materialize,
        spec_dormant,
        spec_etherealize,
        spec_grapple,
        spec_entangled,
        spec_entangled_attack,
        spec_sit_sleep,
        spec_sit,
        spec_throwitem,
        spec_musician_microphone,
        spec_musician_drum,
        spec_musician_saxophone,
        spec_ghost_drink,
        spec_ghost_poisoned,
        spec_wounded_idle,
        spec_wounded_talk0,
        spec_wounded_talk1,
        spec_wounded_talk2,
        spec_cart_front_enter,
        spec_cart_front_depart,
        spec_cart_front_arrive,
        spec_cart_front_exit,
        spec_cart_back_enter,
        spec_cart_back_sit,
        spec_cart_back_exit,
        spec_spawn_climbup0,
        spec_spawn_climbup1,
        special0,
        special1,
        special2,
        special3,
        special4,
        special5,
        special6,
        special7,
        special8,
        special_getup0,
        special_getup1,
        special_delorean,
        special_lookup,
        stunned,
        taunt0,
        taunt1,
        taunt2,
    }
    public enum ActionType
    {
        Block,
        BreakFree,
        CameraShake,
        CastSpell,
        Crouch,
        DamageGrip,
        DealDamage,
        DetachItem,
        Ethereal,
        Footstep,
        Grip,
        Gunfire,
        Immortal,
        Invisible,
        Jump,
        Move,
        OverkillGrip,
        PlayEffect,
        PlaySound,
        ReleaseGrip,
        RemoveStatus,
        SetItemAttach,
        SpawnMissile,
        SpecialAbility,
        Suicide,
        ThrowGrip,
        Tongue,
        WeaponVisibility
    }

    [Flags]
    public enum Targets : byte
    {
        None = 0,
        Target = 0,
        Friendly = 1,
        Enemy = 2,
        NonCharacters = 4,
        All = 255
    }

    public enum GripType : byte
    {
        Pickup,
        Ride,
        Hold
    }

    public enum AbilityTarget : byte
    {
        Self = 1,
        S = 1,
        Enemy,
        E = 2,
        Friendly,
        F = 3
    }

    public enum AbilityTypes
    {
        Block,
        CastSpell,
        ConfuseGrip,
        DamageGrip,
        Dash,
        ElementSteal,
        GripCharacterFromBehind,
        Jump,
        Melee,
        PickUpCharacter,
        Ranged,
        SpecialAbilityAbility,
        ThrowGrip,
        ZombieGrip
    }
    public enum CastType
    {
        None,
        Blast = 0,
        Force,
        Area,
        Self,
        Weapon,
        Magick
    }

    [Flags]
    public enum MovementProperties
    {
        Default = 0,
        Water = 1,
        Jump = 2,
        Fly = 4,
        Dynamic = 128,
        All = 255
    }
}

