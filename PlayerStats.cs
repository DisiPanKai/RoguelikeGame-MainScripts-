using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class PlayerStats
{
    public static int GreaseBonus; //урон для смазки

    public static int CurrentLevelCounter = 0; //счётчик, который является коэффициентом 100% брони

    internal static int CurrentNumberWhichIsHundredPercent = 10; //текущий уровень 100% брони

    public static List<PlayerSkills> skillData = new List<PlayerSkills>(); //все данные по скиллам Игрока
    private static readonly Text LogText = GameObject.Find("Log Field Text").GetComponent<Text>();

    public static bool MadeTurn = false;//нужно для порядка ходов. Если игрок походил, переменная становится true и в порядке ходов игрок пропускается

    public static bool DistanceAttack = false;
    public static bool WeaponSwitch = false;

    //100% статы Игрока без баффов/дебаффов.
    public static int MaxHp;
    public static int MaxHpRegen;
    public static int MaxMagDmg;
    public static int MaxPenetration;
    public static int MaxEvasion;
    public static int MaxRightWeaponMinDmg;
    public static int MaxRightWeaponMaxDmg;
    public static int MaxLeftWeaponMinDmg;
    public static int MaxLeftWeaponMaxDmg;

    //статы Игрока, которые меняются под влиянием баффов/дебаффов
    public static int Strength;
    public static int Agility;
    public static int Constitution;
    public static int Intelligence;
    public static int CurrentHp;
    public static int MagDmg;
    public static int Penetration;
    public static int PenetrationPercent;
    public static int PhysArmor;
    public static int PhysArmorPercent;
    //public static int CurrentPhysDef;
    public static int MagArmor;
    public static int ElementalResistancePercent;

    public static int Evasion;
    public static int EvasionPercent;
    public static int AccuracyPercent;
    public static int PhysCritChance;
    public static int PhysCritChancePercent;
    public static int PhysCritPowerPercent;
    public static int MagCritChance;
    public static int MagCritPower;
    public static int CloseCombatDmgPercent;
    public static int DistantCombatDmg;
    public static int MagicCombatDmg;

    //урон оружия под влиянием статов
    public static int RightWeaponMinDmg;
    public static int RightWeaponMaxDmg;
    public static int LeftWeaponMinDmg;
    public static int LeftWeaponMaxDmg;


    //бафф
    public static bool BuffIsActivated; //активировано ли усиление
    public static int BuffTurns; //сколько ходов будет висеть усиление на игроке
    public static int Buffboost; //запоминаем сколько нам даёт бафф, чтобы потом отнять это

    //дебаффы от ударов Монстра
    public static int DebuffCount = 0; //количество дебаффов
    public static int DebuffHeadAccPercent; //дебафф игрока на точность
    public static int DebuffHeadMagDmgPercent; //дебафф игрока на точность

    public static int DebuffEvaPercent; //дебафф игрока на уворот
    public static int DebuffRHandPhysDmg; //дебафф игрока на урон с правой руки. Переменная сохраняет отнятый урон дебаффом. Нужна для того, чтобы этот урон потом вернуть при лечении
    public static int DebuffRHandAccPercent; //дебафф игрока на точность с правой руки
    public static bool DebuffLDmg;  //дебафф игрока на неспособность бить левой рукой

    internal static int BonusDef; //бонусная защита от защиты щитом 

    public static int PoisonBonus = 0; //урон для яда
    public static bool Poison; //если есть яд, то эта переменная активирует яд


    //дебаффы от скиллов Монстра
    //активные
    public static bool SleepinessEffectOnHero; //эффект от скила "Сонливость"
    private static int _sleepinessDuration;

    public static bool PlagueEffectOnHero; //эффект от скила "Чума"

    public static bool WallEyeEffectOnHero; //дебафф игрока на точность со скила "Бельмо"
    private static int _wallEyeDuration;

    public static bool GallPoisonEffectOnHero; //эффект "Жёлчного яда" на игроке
    private static int _gallPoisonDmgOnHero; //урон от "Жёлчного яда" на игроке

    public static bool NakedEffectOnHero; //эффект от скила "Перелом"
    public static int NakedDuration;

    public static bool SandInTheEyesEffectOnHero; //дебафф игрока на точность со скила "Песок в глаза"
    public static int SandInTheEyesDuration;

    public static bool ParalysisEffectOnHero;//дебафф игрока на точность со скила "Паралич"
    public static int ParalysisDuration;

    public static bool StunEffectOnHero; //эффект от скила "Оглушение"
    public static int StunDuration;

    public static bool DarknessEffectOnHero; //эффект от скила "Оглушение"
    public static int DarknessDuration;
    public static int DarknessDebuffStr; //сколько отнялось статов за дебафф. Нужно для того, чтобы вернуть прежние характеристики. 
    public static int DarknessDebuffAgi; //скил снимает 30% статов. Если числа маленькие, то +30% потом не добавляет никаких статов
    public static int DarknessDebuffCon;

    public static bool TerrorEffectOnHero; //эффект от скила "Ужас"
    private static int _terrorDuration; //длительность скила

    //от пассивок
    internal static bool KickEffectOnHero; //эффект от скила "Пинок"

    public static bool PoisonEffectOnHero; //эффект "Яда" на игроке
    private static int _poisonDmgOnHero; //урон от "Яда" на игроке

    public static bool ToxicEffectOnHero; //эффект от скила "Отрава"
    //private static bool _toxicEffectOnHero; //эффект "Отрава" на игроке
    private static int _toxicDmgOnHero; //урон от "Отрава" на игроке


    //скилы игрока для оружия
    //активные
    private static bool pHitInLiver; //действие скила "Удар в печень"
    private static int pHitInLiverCd = -1; //к-ство ходов кулдауна скила "Удар в печень"
    private static bool pHitInLiverIsUsing; //после использования нужна задержка для кд скила на 1 ход

    public static bool pBeast; //действие скила "Зверь"
    private static int pBeastCd = -1; //к-ство ходов кулдауна скила "Зверь"
    private static float pBeastMultiplier; //множитель для скила "Зверь"
    private static int pBeastMultiplierForText; //множитель для скила "Зверь" для текста
    private static int pBeastDuration; //длительность скила "Зверь"
    private static int pBeastIsUsing; //инт тут для того, чтобы 2 раза отнять этот счётчик. Один отнимается в методе с кд, а второй в длительности скила

    private static bool pArmsOut; //действие скила "Оружие вон"
    private static int pArmsOutCd = -1; //к-ство ходов кулдауна скила "Оружие вон"
    private static int pArmsOutDuration; //длительность скила "Оружие вон"
    private static bool pArmsOutIsUsing; //после использования нужна задержка для кд скила на 1 ход

    internal static bool PBerserk; //действие скила "Берсерк"
    private static int pBerserkCd = -1; //к-ство ходов кулдауна скила "Берсерк"
    private static int pBerserkDuration; //длительность скила "Берсерк"
    private static bool pBerserkIsUsing; //после использования нужна задержка для кд скила на 1 ход

    private static bool pSabretooth; //действие скила "Саблезуб"
    private static int pSabretoothCd = -1; //к-ство ходов кулдауна скила "Саблезуб"
    private static bool pSabretoothIsUsing; //после использования нужна задержка для кд скила на 1 ход

    private static bool pKamikaze; //действие скила "Камикадзе"
    private static int pKamikazeCd = -1; //к-ство ходов кулдауна скила
    private static bool pKamikazeIsUsing; //после использования нужна задержка для кд скила на 1 ход

    private static bool _pArrowInTheKnee; //действие скила "Стрела в колено"
    private static int pArrowInTheKneeCd = -1; //к-ство ходов кулдауна скила
    private static bool pArrowInTheKneeIsUsing; //после использования нужна задержка для кд скила на 1 ход

    private static bool _pKnightHook; //действие скила "Рыцарский Хук"
    private static int _pKnightHookCd = -1; //к-ство ходов кулдауна скила
    private static int _pKnightHookDuration;//длительность скила 
    private static bool _pKnightHookIsUsing; //после использования нужна задержка для кд скила на 1 ход

    public static bool pMadMan; //действие скила "Безумец"
    private static int pMadManCd = -1; //к-ство ходов кулдауна скила
    private static bool pMadManIsUsing; //после использования нужна задержка для кд скила на 1 ход
    public static float pMadManMultiplier; //Множитель скила
    public static bool pMadManRelease; //Освобождение урона с множителем

    private static bool _pDischarge; //действие скила "Разряд"
    private static int pDischargeCd = -1; //к-ство ходов кулдауна скила
    private static bool pDischargeIsUsing; //после использования нужна задержка для кд скила на 1 ход

    private static bool pLunge; //действие скила "Выпад"
    private static int _pLungeCd = -1; //к-ство ходов кулдауна скила
    private static bool _pLungeIsUsing; //после использования нужна задержка для кд скила на 1 ход

    public static bool PSilence; //действие скила "Тишина"
    private static int _pSilenceCd = -1; //к-ство ходов кулдауна скила
    private static bool _pSilenceIsUsing; //после использования нужна задержка для кд скила на 1 ход

    private static bool pImperturbability;  //действие скила "Невозмутимость"
    private static int pImperturbabilityCd = -1; //к-ство ходов кулдауна скила
    private static bool pImperturbabilityIsUsing; //после использования нужна задержка для кд скила на 1 ход

    //пассивки
    public static bool pBleed; //действие скила "Кровотечение"
    public static bool pVampire; //действие скила "Вампир"
    public static bool pBoneBreaker; //действие скила "Костолом"
    public static bool PDueler; //действие скила "Дуэлянт"
    public static bool pSoulEater; //действие скила "Пожиратель душ"
    public static bool PRage; //действие скила "Ярость"

    public static bool pCrusher; //действие скила "Крушитель"


    //скилы игрока для щита
    //активные
    public static bool PSpikes; //действие скила
    private static int _pSpikesCd = -1; //к-ство ходов кулдауна скила
    private static int pSpikesDuration; //длительность скила
    private static bool pSpikesIsUsing; //после использования нужна задержка для кд скила на 1 ход

    public static bool PAntimagic; //действие скила "Антимагия"
    private static int pAntimagicCd = -1; // //к-ство ходов кулдауна скила
    private static int pAntimagicDuration; //длительность скила
    private static bool pAntimagicIsUsing; //после использования нужна задержка для кд скила на 1 ход

    private static bool pHoarder; //действие скила "Накопитель"

    private static bool pElectro; //действие скила "Электро"

    private static bool pAllLayDown; //действие скила "Всем лежать!"
    private static int pAllLayDownCd = -1; //к-ство ходов кулдауна скила
    private static int pAllLayDownDuration; //длительность скила
    private static bool pAllLayDownIsUsing; //после использования нужна задержка для кд скила на 1 ход



    //пассивные
    private static bool pRevenge; //действие скила "Месть"

    public static bool _pLuckySonOfABitch; //действие скила "Везунчик"

    internal static bool PMirror; //действие скила "Зеркало"

    public static bool _pKeeper; //действие скила "Хранитель"

    public static bool _pStrategist; //действие скила "Стратег"

    public static bool _pWoodHead; //действие скила "Опилки в голове"

    private static bool _dontFightSkillIsNotReady; //делает так, что не происходит никаких действий, если клацаем на неоткатившийся скил


    public static List<int> BloodinessEffectOnHero = new List<int>();
    public static int Initiative;


    public static void FirstLvlCalculatingStats() //рассчёт для статов первого уровня 
    {
        AccuracyPercent = 100;
        Initiative = 6;

        //урон
        RightWeaponMinDmg = PlayerEquipmentStats.RightWeaponMinDmg + BattleMechanics.RoundItUp(PlayerEquipmentStats.RightWeaponMinDmg * (CloseCombatDmgPercent / 100f));
        RightWeaponMaxDmg = PlayerEquipmentStats.RightWeaponMaxDmg + BattleMechanics.RoundItUp(PlayerEquipmentStats.RightWeaponMaxDmg * (CloseCombatDmgPercent / 100f));

        LeftWeaponMinDmg = PlayerEquipmentStats.LeftWeaponMinDmg + BattleMechanics.RoundItUp(PlayerEquipmentStats.LeftWeaponMinDmg * (CloseCombatDmgPercent / 100f));
        LeftWeaponMaxDmg = PlayerEquipmentStats.LeftWeaponMaxDmg + BattleMechanics.RoundItUp(PlayerEquipmentStats.LeftWeaponMaxDmg * (CloseCombatDmgPercent / 100f));

        //сила
        Strength = 1;
        CloseCombatDmgPercent = 2;
        Penetration = 1;
        //PenetrationPercent = 10; //формулу сократил. На самом деле тут не поделить на 10, а "/ 100 * 10"(делим на 100%, чтобы узнать 1%
        //перед использованием метода нужно скинуть CurrentNumberWhichIsHundredPercent до значения по-умолчанию
        PhysCritPowerPercent = 10;


        //ловкость
        Agility = 1;
        DistantCombatDmg = 2;
        Evasion = 1;
        //EvasionPercent = 10;
        PhysCritChance = 1;
        //PhysCritChancePercent = 10;


        //выносливость
        Constitution = 1;
        PhysArmor = 1;
        MaxHp = 40; //ХП персонажа изначально 30 + 1 очко Выносливости = 40 ХП
        CurrentHp = MaxHp;
        //PhysArmorPercent = 10;


        //интеллект
        Intelligence = 1;
        ElementalResistancePercent = 10;
    }


    public static void HpReset()
    {
        CurrentHp = MaxHp;
    }


    public static void CalculateDmgStats()
    {
        RightWeaponMinDmg = PlayerEquipmentStats.RightWeaponMinDmg + BattleMechanics.RoundItUp(PlayerEquipmentStats.RightWeaponMinDmg * (CloseCombatDmgPercent / 100f));
        RightWeaponMaxDmg = PlayerEquipmentStats.RightWeaponMaxDmg + BattleMechanics.RoundItUp(PlayerEquipmentStats.RightWeaponMaxDmg * (CloseCombatDmgPercent / 100f));

        LeftWeaponMinDmg = PlayerEquipmentStats.LeftWeaponMinDmg + BattleMechanics.RoundItUp(PlayerEquipmentStats.LeftWeaponMinDmg * (CloseCombatDmgPercent / 100f));
        LeftWeaponMaxDmg = PlayerEquipmentStats.LeftWeaponMaxDmg + BattleMechanics.RoundItUp(PlayerEquipmentStats.LeftWeaponMaxDmg * (CloseCombatDmgPercent / 100f));
    }
    //public static void CalculateStats() //подсчёт всех характеристик
    //{
    //    MaxHp = 30 + Constitution * 5 + PlayerEquipmentStats.Health;
    //    //MaxHp = 4;
    //    HpRegen = Mathf.RoundToInt(0.1f * MaxHp);
    //    MagDmg = Strength;
    //    //for (int i = 0; i < Strength; i++)
    //    //{


    //    //Debug.Log("Пробитие получилось " + Penetration);
    //    //и умножаем на +10% пробиваемости)
    //    //}

    //    PhysArmor = Constitution * 2 + PlayerEquipmentStats.HelmetPercent + PlayerEquipmentStats.BodyPercent + PlayerEquipmentStats.LeftShieldBonus;
    //    MagArmor = Constitution + PlayerEquipmentStats.MagDef;
    //    EvasionPercent = Agility * 2 + PlayerEquipmentStats.EvasionPercent;
    //    AccuracyPercent = Agility * 2 + PlayerEquipmentStats.AccuracyPercent;
    //    PhysCritChancePercent -= PlayerEquipmentStats.PhysCritChancePercent;
    //    MagCritChance -= PlayerEquipmentStats.MagCritChance;
    //}


    //public static void TransformToNewLvlStat()
    //{
    //    CurrentNumberWhichIsHundredPercent = (int)(1.5f * CurrentNumberWhichIsHundredPercent);
    //PenetrationPercent = Mathf.RoundToInt(PenetrationPercent / 1.5f);
    //    EvasionPercent = Mathf.RoundToInt(EvasionPercent / 1.5f);
    //    PhysCritChancePercent = Mathf.RoundToInt(PhysCritChancePercent / 1.5f);
    //    PhysArmorPercent = Mathf.RoundToInt(PhysArmorPercent / 1.5f);
    //    ElementalResistancePercent = Mathf.RoundToInt(ElementalResistancePercent / 1.5f);
    //}


    public static void AddNewStrStat()
    {
        Strength++;
        CloseCombatDmgPercent += 2;
        Penetration += CurrentNumberWhichIsHundredPercent / 10; //формулу сократил. На самом деле тут не поделить на 10, а "/ 100 * 10"(делим на 100%, чтобы узнать 1%)
        //PenetrationPercent += 10;
        PhysCritPowerPercent += 10;
    }


    public static void AddNewAgiStat()
    {
        Agility++;
        DistantCombatDmg += 2;
        Evasion += CurrentNumberWhichIsHundredPercent / 10;
        //EvasionPercent += 10;
        //PhysCritChancePercent += 10;
        PhysCritChance += CurrentNumberWhichIsHundredPercent / 10;
    }


    public static void AddNewConStat()
    {
        Constitution++;
        MaxHp += 10 + BattleMechanics.RoundItUp(MaxHp / 100f);
        PhysArmor += CurrentNumberWhichIsHundredPercent / 10;
        //PhysArmorPercent += 10;
    }


    public static void AddNewIntStat()
    {
        Intelligence++;
        MagicCombatDmg += 1;
        ElementalResistancePercent += 10;
    }


    //public static void MaxToCurrent()
    //{
    //    CurrentHp = MaxHp;
    //    //CurrentPhysDef = PhysArmor;
    //    //CurrentMagDef = MagArmor;
    //}


    public static void DebuffThePlayer(NpcMonster npc)
    {
        DarknessDebuffStr = Mathf.RoundToInt(Agility * 0.7f);
        Strength -= DarknessDebuffStr;
        if (Strength == 0)
        {
            Strength = 1;
            DarknessDebuffStr = 0;
        }
        DarknessDebuffAgi = Mathf.RoundToInt(Agility * 0.7f);
        Agility -= DarknessDebuffAgi;
        if (Agility == 0)
        {
            Agility = 1;
            DarknessDebuffAgi = 0;
        }
        DarknessDebuffCon = Mathf.RoundToInt(Constitution * 0.7f);
        Constitution -= DarknessDebuffCon;
        if (Constitution == 0)
        {
            Constitution = 1;
            DarknessDebuffCon = 0;
        }
        //CalculateStats();
        if (CurrentHp > MaxHp)
            CurrentHp = MaxHp;
        DarknessDuration = npc.DarknessDuration;
    }


    public static void RemoveTheDebuff()
    {
        Strength += DarknessDebuffStr;
        Agility += DarknessDebuffAgi;
        Constitution += DarknessDebuffCon;
        //CalculateStats();
    }


    public static void PlayerSkillEffects(int skillId, NpcMonster npc) //перечень эффектов от скилов игрока
    {
        switch (skillId)
        {
            //Оружейные скилы
            //активные
            case 2://Удар в печень
                if (pHitInLiverCd >= 0)
                {
                    LogText.text += "\n\r\"Удар в печень\" недоступен\n\r";
                    _dontFightSkillIsNotReady = true;
                    break;
                }
                pHitInLiverIsUsing = true;
                LogText.text += "\n\rГерой использует скил \"Удар в печень\".\n\r";
                pHitInLiver = true;
                //pHitInLiverCd = true;
                pHitInLiverCd = skillData[skillId - 1].cooldown;
                break;
            case 3://Зверь
                if (pBeastCd >= 0)
                {
                    LogText.text += "\n\r\"Зверь\" недоступен\n\r";
                    _dontFightSkillIsNotReady = true;
                    break;
                }
                pBeastIsUsing = 2;
                LogText.text += "\n\rГерой использует скил \"Зверь\".\n\r";
                pBeast = true;
                //dontFightAnotherSkillIsActive = true;
                pBeastMultiplier = 1.5f;
                pBeastMultiplierForText = 50;
                pBeastCd = skillData[skillId - 1].cooldown;
                pBeastDuration = skillData[skillId - 1].duration;
                break;
            case 4://Оружие вон
                if (pArmsOutCd >= 0)
                {
                    LogText.text += "\n\r\"Оружие вон\" недоступен\n\r";
                    _dontFightSkillIsNotReady = true;
                    break;
                }
                pArmsOutIsUsing = true;
                LogText.text += "\n\rГерой использует скил \"Оружие вон\".\n\r";
                pArmsOut = true;
                //dontFightAnotherSkillIsActive = true;
                pArmsOutCd = skillData[skillId - 1].cooldown;
                pArmsOutDuration = skillData[skillId - 1].duration;
                break;
            case 5://Берсерк
                if (pBerserkCd >= 0)
                {
                    LogText.text += "\n\r\"Берсерк\" недоступен\n\r";
                    _dontFightSkillIsNotReady = true;
                    break;
                }
                pBerserkIsUsing = true;
                LogText.text += "\n\rГерой использует скил \"Берсерк\".\n\r";
                PBerserk = true;
                //dontFightAnotherSkillIsActive = true;
                pBerserkCd = skillData[skillId - 1].cooldown;
                pBerserkDuration = skillData[skillId - 1].duration;
                break;
            case 6://Саблезуб
                if (pSabretoothCd >= 0)
                {
                    LogText.text += "\n\r\"Саблезуб\" недоступен\n\r";
                    _dontFightSkillIsNotReady = true;
                    break;
                }
                pSabretoothIsUsing = true;
                LogText.text += "\n\rГерой использует скил \"Саблезуб\".\n\r";
                pSabretooth = true;
                pSabretoothCd = skillData[skillId - 1].cooldown;
                break;
            case 7://Камикадзе
                if (pKamikazeCd >= 0)
                {
                    LogText.text += "\n\r\"Камикадзе\" недоступен\n\r";
                    _dontFightSkillIsNotReady = true;
                    break;
                }
                pKamikazeIsUsing = true;
                LogText.text += "\n\rГерой использует скил \"Камикадзе\".\n\r";
                pKamikaze = true;
                pKamikazeCd = skillData[skillId - 1].cooldown;
                break;
            case 15://Стрела в колено
                if (pArrowInTheKneeCd >= 0)
                {
                    LogText.text += "\n\r\"Стрела в колено\" недоступен\n\r";
                    _dontFightSkillIsNotReady = true;
                    break;
                }
                pArrowInTheKneeIsUsing = true;
                LogText.text += "\n\rГерой использует скил \"Стрела в колено\".\n\r";
                _pArrowInTheKnee = true;
                break;
            case 16://Рыцарский хук
                if (_pKnightHookCd >= 0)
                {
                    LogText.text += "\n\r\"Рыцарский хук\" недоступен\n\r";
                    _dontFightSkillIsNotReady = true;
                    break;
                }
                npc.KnightHookEffectOnEnemy = true;
                _pKnightHookCd = 2;
                _pKnightHookIsUsing = true;
                npc.KnightHookDuration = 2;
                LogText.text += "\n\rГерой использует скил \"Рыцарский хук\" и оглушает Монстра на 1 ход.\n\r";
                break;
            case 17://Безумец
                if (pMadManCd >= 0)
                {
                    LogText.text += "\n\r\"Безумец\" недоступен\n\r";
                    _dontFightSkillIsNotReady = true;
                    break;
                }
                pMadManRelease = true;
                pMadManIsUsing = true;
                int multiplToPerc = (int)pMadManMultiplier * 100;
                LogText.text += "\n\rГерой использует скил \"Безумец\". Множитель урона = " + multiplToPerc + "%.\n\r";
                break;
            case 18://Разряд
                if (pDischargeCd >= 0)
                {
                    LogText.text += "\n\r\"Разряд\" недоступен\n\r";
                    _dontFightSkillIsNotReady = true;
                    break;
                }
                pDischargeIsUsing = true;
                LogText.text += "\n\rГерой использует скил \"Разряд\".\n\r";
                _pDischarge = true;
                break;
            case 19://Выпад
                if (_pLungeCd >= 0)
                {
                    LogText.text += "\n\r\"Выпад\" недоступен\n\r";
                    _dontFightSkillIsNotReady = true;
                    break;
                }
                _pLungeCd = 4;
                _pLungeIsUsing = true;
                LogText.text += "\n\rГерой использует скил \"Выпад\".\n\r";
                pLunge = true;
                break;
            case 20://Тишина
                if (_pSilenceCd >= 0)
                {
                    LogText.text += "\n\r\"Тишина\" недоступен\n\r";
                    _dontFightSkillIsNotReady = true;
                    break;
                }
                _pSilenceIsUsing = true;
                _pSilenceCd = 4;
                PSilence = true;
                break;
            case 21://Невозмутимость
                if (pImperturbabilityCd >= 0)
                {
                    LogText.text += "\n\r\"Невозмутимость\" недоступен\n\r";
                    _dontFightSkillIsNotReady = true;
                    break;
                }
                pImperturbabilityIsUsing = true;
                LogText.text += "\n\rГерой использует скил \"Невозмутимость\".\n\r";
                pImperturbability = true;
                break;

            //пассивки
            case 8://Кровотечение
                LogText.text += "\n\rТеперь у Героя активирована пассивка \"Кровотечение\".\n\r";
                pBleed = true;
                break;
            case 9://Вампир
                LogText.text += "\n\rТеперь у Героя активирована пассивка \"Вампир\".\n\r";
                pVampire = true;
                break;
            case 10://Костолом
                LogText.text += "\n\rТеперь у Героя активирована пассивка \"Костолом\".\n\r";
                pBoneBreaker = true;
                break;
            case 11://Дуэлянт
                LogText.text += "\n\rТеперь у Героя активирована пассивка \"Дуэлянт\".\n\r";
                PDueler = true;
                break;
            case 22://пассивка "Безумца"
                LogText.text += "\n\rТеперь у Героя активирована пассивка \"Безумец\".\n\r";
                pMadMan = true;
                break;
            case 23://Ярость
                LogText.text += "\n\rТеперь у Героя активирована пассивка \"Ярость\".\n\r";
                PRage = true;
                break;
            case 24://Пожиратель душ
                break;
            case 25://Крушитель
                LogText.text += "\n\rТеперь у Героя активирована пассивка \"Крушитель\".\n\r";
                pCrusher = true;
                break;

            //Щитовые скилы
            //активные
            case 12://Шипы
                if (_pSpikesCd >= 0)
                {
                    LogText.text += "\n\r\"Шипы\" недоступен\n\r";
                    _dontFightSkillIsNotReady = true;
                    break;
                }
                pSpikesIsUsing = true;
                LogText.text += "\n\rГерой использует скил \"Шипы\".\n\r";
                PSpikes = true;
                _pSpikesCd = skillData[skillId - 1].cooldown;
                pSpikesDuration = skillData[skillId - 1].duration;
                break;
            case 13://Антимагия
                if (pAntimagicCd >= 0)
                {
                    LogText.text += "\n\r\"Антимагия\" недоступен\n\r";
                    _dontFightSkillIsNotReady = true;
                    break;
                }
                pAntimagicIsUsing = true;
                LogText.text += "\n\rГерой использует скил \"Антимагия\".\n\r";
                PAntimagic = true;
                pAntimagicCd = skillData[skillId - 1].cooldown;
                pAntimagicDuration = skillData[skillId - 1].duration;
                break;
            case 26://Накопитель
                break;
            case 27://Электро
                break;
            case 28://Всем лежать!
                if (pAllLayDownCd >= 0)
                {
                    LogText.text += "\n\r\"Всем лежать!\" недоступен\n\r";
                    _dontFightSkillIsNotReady = true;
                    break;
                }
                pAllLayDown = true;
                pAllLayDownCd = 2;
                pAllLayDownIsUsing = true;
                pAllLayDownDuration = 3;
                LogText.text += "\n\rГерой использует скил \"Всем лежать!\" и оглушает Монстра на 2 ход.\n\r";
                break;


            //пассивные
            case 29://Месть
                break;
            case 30://Везунчик
                break;
            case 31://Зеркало
                LogText.text += "\n\rТеперь у Героя активирована пассивка \"Зеркало\".\n\r";
                PMirror = true;
                break;
            case 32://Хранитель
                break;
            case 33://Стратег
                break;
            case 34://Дубовая голова
                break;
        }
        //максимальное число для скилов: 34
    }


    public static void PlayerBuffsDuration() //декрементация счётчиков баффов на Игроке
    {
        //Оружейные скилы
        //активные
        if (pBeastIsUsing > 0)
        {
            pBeastIsUsing--;
        }
        else
            if (pBeast)
            {
                pBeastDuration--;
                if (pBeastDuration == 0)
                {
                    pBeast = false;
                    LogText.text += "Эффект \"Зверь\" испарился в этом ходу.\n\r";
                }
                else
                    LogText.text += "\n\r\"Зверь\" ещё будет действовать " + pBeastDuration + " ходов.\n\r";
            }

        if (pArmsOut)
            LogText.text += "Если скил \"Оружие вон\" пройдёт, то он будет действовать " + pArmsOutDuration + " ходов.\n\r";


        if (PBerserk)
        {
            pBerserkDuration--;
            if (pBerserkDuration == 0)
            {
                PBerserk = false;
                LogText.text += "Эффект \"Берсерк\" испарился в этом ходу.\n\r";
            }
            else
                LogText.text += "\"Берсерк\" ещё будет действовать " + pBerserkDuration + " ходов.\n\r";
        }

        if (PSpikes)
        {
            pSpikesDuration--;
            if (pSpikesDuration == 0)
            {
                PSpikes = false;
                LogText.text += "Эффект \"Шипы\" испарился в этом ходу.\n\r";
            }
            else
                LogText.text += "\"Шипы\" ещё будут действовать " + pSpikesDuration + " ходов.\n\r";
        }

        if (PAntimagic)
        {
            pAntimagicDuration--;
            if (pAntimagicDuration == 0)
            {
                PAntimagic = false;
                LogText.text += "Эффект \"Антимагия\" испарился в этом ходу.\n\r";
            }
            else
                LogText.text += "\"Антимагия\" ещё будет действовать " + pAntimagicDuration + " ходов.\n\r";
        }
    }


    public static void PlayerSkillCds() //отнимание кд скилов игрока
    {
        //Оружейные скилы
        //активные
        if (pHitInLiverIsUsing)
        {
            pHitInLiverIsUsing = false;
        }
        else
            if (pHitInLiverCd >= 0)
            {
                LogText.text += "\n\rОткат скила \"Удар в печень\" " + pHitInLiverCd + " хода.\n\r";
                if (pHitInLiverCd == 0)
                {
                    //pHitInLiverCd = false;
                    LogText.text += "Теперь можно использовать скил.\n\r";
                }
                pHitInLiverCd--;
            }

        if (pBeastIsUsing > 0)
        {
            pBeastIsUsing--;
        }
        else
            if (pBeastCd >= 0)
            {
                LogText.text += "\n\rОткат скила \"Зверь\" " + pBeastCd + " хода.\n\r";
                if (pBeastCd == 0)
                {
                    //pBeastCd = false;
                    LogText.text += "Теперь можно использовать скил.\n\r";
                }
                pBeastCd--;
            }

        if (pArmsOutIsUsing)
        {
            pArmsOutIsUsing = false;
        }
        else
            if (pArmsOutCd >= 0)
            {
                LogText.text += "\n\rОткат скила \"Оружие вон\" " + pArmsOutCd + " хода.\n\r";
                if (pArmsOutCd == 0)
                {
                    //pArmsOutCd = false;
                    LogText.text += "Теперь можно использовать скил.\n\r";
                }
                pArmsOutCd--;
            }

        if (pBerserkIsUsing)
        {
            pBerserkIsUsing = false;
        }
        else
            if (pBerserkCd >= 0)
            {
                LogText.text += "\n\rОткат скила \"Берсерк\" " + pBerserkCd + " хода.\n\r";
                if (pBerserkCd == 0)
                {
                    //pBerserkCd = false;
                    LogText.text += "Теперь можно использовать скил.\n\r";
                }
                pBerserkCd--;
            }

        if (pSabretoothIsUsing)
        {
            pSabretoothIsUsing = false;
        }
        else
            if (pSabretoothCd >= 0)
            {
                LogText.text += "\n\rОткат скила \"Саблезуб\" " + pSabretoothCd + " хода.\n\r";
                if (pSabretoothCd == 0)
                {
                    //pSabretoothCd = false;
                    LogText.text += "Теперь можно использовать скил.\n\r";
                }
                pSabretoothCd--;
            }

        if (pKamikazeIsUsing)
        {
            pKamikazeIsUsing = false;
        }
        else
            if (pKamikazeCd >= 0)
            {
                LogText.text += "\n\rОткат скила \"Камикадзе\" " + pKamikazeCd + " хода.\n\r";
                if (pKamikazeCd == 0)
                {
                    //pKamikazeCd = false;
                    LogText.text += "Теперь можно использовать скил.\n\r";
                }
                pKamikazeCd--;
            }

        if (pArrowInTheKneeIsUsing)
        {
            pArrowInTheKneeIsUsing = false;
        }
        else
            if (pArrowInTheKneeCd >= 0)
            {
                LogText.text += "\n\rОткат скила \"Выпад\" " + pArrowInTheKneeCd + " хода.\n\r";
                if (pArrowInTheKneeCd == 0)
                {
                    LogText.text += "Теперь можно использовать скил.\n\r";
                }
                pArrowInTheKneeCd--;
            }

        if (_pKnightHookIsUsing)
        {
            _pKnightHookIsUsing = false;
        }
        else
            if (_pKnightHookCd >= 0)
            {
                LogText.text += "\n\rОткат скила \"Рыцарский хук\" " + _pKnightHookCd + " хода.\n\r";
                if (_pKnightHookCd == 0)
                {
                    LogText.text += "Теперь можно использовать скил.\n\r";
                }
                _pKnightHookCd--;
            }

        if (_pLungeIsUsing)
        {
            _pLungeIsUsing = false;
        }
        else
            if (_pLungeCd >= 0)
            {
                LogText.text += "\n\rОткат скила \"Выпад\" " + _pLungeCd + " хода.\n\r";
                if (_pLungeCd == 0)
                {
                    LogText.text += "Теперь можно использовать скил.\n\r";
                }
                _pLungeCd--;
            }

        if (pMadManIsUsing)
        {
            pMadManIsUsing = false;
        }
        else
            if (pMadManCd >= 0)
            {
                LogText.text += "\n\rОткат скила \"Выпад\" " + pMadManCd + " хода.\n\r";
                if (pMadManCd == 0)
                {
                    LogText.text += "Теперь можно использовать скил.\n\r";
                }
                pMadManCd--;
            }

        //Щитовые скиллы
        //активные
        if (pSpikesIsUsing)
        {
            pSpikesIsUsing = false;
        }
        else
            if (_pSpikesCd >= 0)
            {
                LogText.text += "\n\rОткат скила \"Шипы\" " + _pSpikesCd + " хода.\n\r";
                if (_pSpikesCd == 0)
                {
                    //_pSpikesCd = false;
                    LogText.text += "Теперь можно использовать скил.\n\r";
                }
                _pSpikesCd--;
            }

        if (pAntimagicIsUsing)
        {
            pAntimagicIsUsing = false;
        }
        else
            if (pAntimagicCd >= 0)
            {
                LogText.text += "\n\rОткат скила \"Антимагия\" " + pAntimagicCd + " хода.\n\r";
                if (pAntimagicCd == 0)
                {
                    //pAntimagicCd = false;
                    LogText.text += "Теперь можно использовать скил.\n\r";
                }
                pAntimagicCd--;
            }

        if (pAllLayDownIsUsing)
        {
            pAllLayDownIsUsing = false;
        }
        else
            if (pAllLayDownCd >= 0)
            {
                LogText.text += "\n\rОткат скила \"Всем лежать!\" " + pAllLayDownCd + " хода.\n\r";
                if (pAllLayDownCd == 0)
                {
                    LogText.text += "Теперь можно использовать скил.\n\r";
                }
                pAllLayDownCd--;
            }
    }

    public static void PlayerSkillFailure() //если не попали по врагу, то отменяем эффект скилов
    {
        //Оружейные скилы
        //активные
        if (pArmsOut)
        //а это тут, потому что нужно проверить: если игрок не пробил врага, то выключаем переключатель, который может наложить эффект от скила
        {
            pArmsOut = false;
            LogText.text += "\n\rЭффект \"Оружие вон\" не наложился.\n\r";
        }

        if (pHitInLiver)
        {
            pHitInLiver = false;
            LogText.text += "\n\rНеудачное использование скила \"Удар в печень\".\n\r";
        }

        if (pKamikaze)
        {
            pKamikaze = false;
            LogText.text += "\n\rНеудачное использование скила \"Камикадзе\".\n\r";
        }

        if (PSilence)
        {
            pKamikaze = false;
            LogText.text += "\n\rНеудачное использование скила \"Тишина\".\n\r";
        }
    }


    public static void EnemyDebuffsDurationOnHero() //декрементация счётчиков наложенных дебаффов Монстром на игроке
    {
        if (WallEyeEffectOnHero)
        {
            _wallEyeDuration--;
            if (_wallEyeDuration == 0)
            {
                WallEyeEffectOnHero = false;
                LogText.text += "Эффект \"Бельмо\" испарился.\n\r";
            }
            else
                LogText.text += "\"Бельмо\" на Герое ещё будет действовать " + _wallEyeDuration + " ходов.\n\r";
        }

        if (SandInTheEyesEffectOnHero)
        {
            SandInTheEyesDuration--;
            if (SandInTheEyesDuration == 0)
            {
                SandInTheEyesEffectOnHero = false;
                LogText.text += "Эффект \"Песок в глаза\" испарился.\n\r";
            }
            else
                LogText.text += "\"Песок в глаза\" на Герое ещё будет действовать " + SandInTheEyesDuration +
                                 " ходов.\n\r";
        }

        if (NakedEffectOnHero)
        {
            NakedDuration--;
            if (NakedDuration == 0)
            {
                NakedEffectOnHero = false;
                LogText.text += "Эффект \"Нагота\" испарился.\n\r";
            }
            else
                LogText.text += "Нагота\" на Герое ещё будет действовать " + NakedDuration + " ходов.\n\r";
        }

        if (ParalysisEffectOnHero)
        {
            ParalysisDuration--;
            if (ParalysisDuration == 0)
            {
                GameObject.Find("Attack Button").GetComponent<Button>().enabled = true;
                LogText.text += "Эффект \"Паралич\" испарился.\n\r";
                ParalysisEffectOnHero = false;
            }
            else
                LogText.text += "\"Паралич\" на Герое ещё будет действовать " + ParalysisDuration +
                                 " ходов.\n\r";
        }

        if (StunEffectOnHero)
        {
            StunDuration--;
            if (StunDuration == 0)
            {
                StunEffectOnHero = false;
                LogText.text += "Эффект \"Оглушение\" испарился.\n\r";
            }
            else
                LogText.text += "\"Оглушение\" на Герое ещё будет действовать " + StunDuration + " ходов.\n\r";
        }

        if (DarknessEffectOnHero)
        {
            DarknessDuration--;
            if (DarknessDuration == 0)
            {
                DarknessEffectOnHero = false;
                RemoveTheDebuff();
                LogText.text += "Эффект \"Тьма\" испарился. Сила теперь " + Strength +
                                 ", Ловкость " + Agility + ", Телосложение " + Constitution +
                                 ".\n\r";
            }
            else
                LogText.text += "\"Тьма\" на Герое ещё будет действовать " + DarknessDuration + " ходов.\n\r";
        }
    }


    public static void PlayerBuffEffect()//метод с проверкой усиления характеристик
    {
        if (BuffIsActivated)
        {
            BuffTurns--;
            if (BuffTurns == 0)
            {
                BuffIsActivated = false;
                Strength -= Buffboost;
                Agility -= Buffboost;
                Constitution -= Buffboost;
                //CalculateStats();
                if (CurrentHp > MaxHp)
                    CurrentHp = MaxHp;
                LogText.text += "\n\rУсиление характеристик закончилось. Теперь ХП Героя = " + CurrentHp +
                                "; Физ. защита = " + PhysArmor + "; Маг. защита = " + MagArmor + ".\n\r";
                return;
            }
            LogText.text += "\n\rДо конца усиления характеристик осталось " + BuffTurns + " ходов.\n\r";
        }
    }

    public static void PlayerPhysAttack(NpcMonster npc, int handOption) //физичеческая атака игрока
    {
        //int playerAccuracyChance = AccuracyPercent - npc.EvasionPercent;
        //int enemyEvasion = 0;
        bool hit; //попали в монстра или нет
        //LogText.text += "\n\rГерой бьёт физической атакой.\n\r";
        if (npc.EnemyIsSleeping)
        {
            LogText.text += "2. " + npc.Name + " спит. Мы 100% попадаем по спящему Монстру.\n\r";
            hit = true;
        }
        else
        {
            int playerAccuracyRes = AccuracyPercent - npc.EvasionPercent;
            if (playerAccuracyRes < 0)
                playerAccuracyRes = 0;
            LogText.text += "2. Точность Героя: " + AccuracyPercent + "%. Уворот Монстра: " + npc.EvasionPercent + "%. Шанс попадания по Монстру: " + playerAccuracyRes + "%. ";
            hit = BattleMechanics.ChanceCalculation(playerAccuracyRes);
            ////playerAccuracyChance = Random.Range(1, 11);//кидаем кубик от 1 до 10
            //LogText.text += "2. Шанс попадания героя = " + AccuracyPercent + "-" + npc.EvasionPercent + " = " +
            //                playerAccuracyChance + ".";
            ////"\n\r а)Кидаем кубик от 1 до 10 (выпадает " + playerAccuracyChance + ") ";
            ////playerAccuracyChance += AccuracyPercent; //точность с рандом + точность со статов + точность со шмоток
            ////LogText.text += "+ бонус от статов и эквипа(" + AccuracyPercent + ") = " + playerAccuracyChance;
            //if (DebuffHeadAccPercent > 0)
            //{
            //    int debuffValue = Mathf.RoundToInt(playerAccuracyChance * DebuffHeadAccPercent);
            //    playerAccuracyChance -= debuffValue;
            //    LogText.text += " - " + debuffValue + "(30% от сотрясения головы) = " + playerAccuracyChance;
            //}
            //if (WallEyeEffectOnHero)
            //{
            //    int additionFromWallEye = Mathf.RoundToInt(playerAccuracyChance * 0.5f);
            //    playerAccuracyChance -= additionFromWallEye;
            //    LogText.text += " - " + additionFromWallEye + "(50% от скила \"Бельмо\") = " + playerAccuracyChance;
            //}
            //if (SandInTheEyesEffectOnHero)
            //{
            //    int additionFromSandInTheEyes = Mathf.RoundToInt(playerAccuracyChance * 0.5f);
            //    playerAccuracyChance -= additionFromSandInTheEyes;
            //    LogText.text += " - " + additionFromSandInTheEyes + "(50% от скила \"Песок в глаза\") = " + playerAccuracyChance;
            //}

            //LogText.text += ".\n\r";

            //enemyEvasion = Random.Range(1, 6);
            //LogText.text += " б)Уворот монстра = кубик от 1 до 5 (выпадает " + enemyEvasion;
            //enemyEvasion += npc.EvasionPercent;
            //LogText.text += ") + бонус уворота(" + npc.EvasionPercent + ") = " + enemyEvasion;
            //if (npc.DebuffEvaPercent != 0f)
            //{
            //    int debuffValue = Mathf.RoundToInt(enemyEvasion * npc.DebuffEvaPercent);
            //    enemyEvasion -= debuffValue;
            //    if (npc.Scrapper)
            //        LogText.text += " + " + debuffValue + "(30% уворота от \"Драчуна\") = " + enemyEvasion;
            //    else
            //        LogText.text += " - " + debuffValue + "(30% уворота от ран на ногах) = " + enemyEvasion;
            //}

            //if (npc.Agility)
            //{
            //    //enemyEvasion = Mathf.RoundToInt(enemyEvasion * 1.5f);
            //    int additionFromAgility = Mathf.RoundToInt(enemyEvasion * 0.5f);
            //    enemyEvasion += additionFromAgility;
            //    LogText.text += " + " + additionFromAgility + "(50% от скила \"Ловкость\") = " + enemyEvasion;
            //}
            //LogText.text += ".\n\r";
        }

        if (hit)//если попадаем
        {
            LogText.text += " Попадание.\n\r";
            if (npc.EnemyIsSleeping)
            {
                npc.EnemyIsSleeping = false;
                LogText.text += "Монстр просыпается от Вашей атаки.\n\r";
            }
            //if (npc.Parry)
            //{
            //    int parryChance = Random.Range(1, 6);
            //    LogText.text += "У Монстра активна пассивка \"Парирование\", шанс парирования 1d4. На кубике выпало " + parryChance + ". ";
            //    if (parryChance == 5)
            //    {
            //        LogText.text += "Монстр парирует удар и возвращает Герою половину урона.\n\r";
            //        int parryDmg = PlayerDamageCalculation(npc);
            //        LogText.text += "Урон, который нанёс бы Герой " + parryDmg + ", делим это на 2 = ";
            //        parryDmg /= 2;
            //        LogText.text += parryDmg + ". ХП Героя было " + CurrentHp + " - " + parryDmg + " = ";
            //        CurrentHp -= parryDmg;
            //        if (CurrentHp < 0)
            //            CurrentHp = 0;
            //        LogText.text += CurrentHp + ".\n\r";

            //        return;
            //    }
            //    else
            //    {
            //        LogText.text += "\"Парирование\" не прошло.\n\r";
            //    }
            //}
            //LogText.text += "Точность Героя(" + playerAccuracyChance + ") >= уворота Монстра(" + enemyEvasion + "). Герой попадает! Точите своё оружие, сейчас устроим взбучку!\n\r";

            //if (npc.PoisonImmunity)
            //    LogText.text += "У Монстра иммунитет к яду.\n\r";
            //else
            //    if (!npc.PoisonEffectOnEnemy && PoisonBonus > 0)
            //    {
            //        npc.PoisonEffectOnEnemy = true;
            //        LogText.text += "Яд сработал! Ищи противоядие, глупец! Каждый ход Монстру будет наносится " + PoisonBonus + " урона.\n\r";
            //        npc.DebuffCount++;
            //        LogText.text += "Теперь у Монстра " + npc.DebuffCount + "дебаффа.\n\r";
            //    }

            int playerDmg = PlayerDamageCalculation(npc, handOption);

            ////активные скилы
            //if (pHitInLiver)
            //{
            //    playerDmg *= 2;
            //    pHitInLiver = false;
            //    LogText.text += "Скил \"Удар в печень\" даёт +100% урона. Теперь урон = " + playerDmg + ".\n\r";
            //}

            //if (pBeast)
            //{
            //    playerDmg = Mathf.RoundToInt(playerDmg * pBeastMultiplier);
            //    LogText.text += "Скил \"Зверь\" каждый удар даёт +50% урона. Текущая прибавка к урону = " + pBeastMultiplierForText + "%. Теперь урон = " + playerDmg + ".\n\r";
            //    pBeastMultiplier += 0.5f;
            //    pBeastMultiplierForText += 50;
            //}

            //if (pArmsOut)
            //{
            //    if (npc.DebuffCount >= 3)
            //    {
            //        LogText.text += "Скил \"Оружие вон\" не прошёл. Количетсво дебаффов не может быть больше 3.\n\r";
            //    }
            //    else
            //    {
            //        LogText.text += "Скил \"Оружие вон\" понижает урон Монстра на 50% на 3 хода. ";
            //        npc.DebuffCount++;
            //        LogText.text += "Теперь у Монстра " + npc.DebuffCount + " дебаффa.\n\r";
            //        npc.ArmsOutEffectOnEnemy = true;
            //        pArmsOut = false;
            //    }
            //}

            //if (pKamikaze)
            //{
            //    int kamikazeDmg = Mathf.RoundToInt((MaxHp - CurrentHp) * 0.5f);
            //    playerDmg += kamikazeDmg;
            //    LogText.text += "Скил \"Камикадзе\". Прибавка к урону " + kamikazeDmg + ". Теперь урон = " + playerDmg + ".\n\r";
            //}

            //if (_pArrowInTheKnee)
            //{
            //    BleedMechanic(npc);
            //    _pArrowInTheKnee = false;
            //}

            //if (pLunge)
            //{
            //    pLunge = false;

            //    int bonusDmg = Mathf.RoundToInt(playerDmg * 0.3f);
            //    pHitInLiver = false;
            //    LogText.text += "Скил \"Выпад\" даёт +30% урона. Урон = " + playerDmg + " + " + bonusDmg + " = ";
            //    playerDmg += bonusDmg;
            //    LogText.text += playerDmg + ".\n\r";
            //}

            //if (_pDischarge)
            //{
            //    if (npc.DebuffCount >= 3)
            //    {
            //        LogText.text += "Скил \"Разряд\" не прошёл. Количетсво дебаффов не может быть больше 3.\n\r";
            //    }
            //    else
            //    {
            //        npc.DischargeEffectOnEnemy = true;
            //        npc.DischargeDuration = 3;
            //        LogText.text += "Скил \"Разряд\" накладывает дебафф, который понижает урон Монстра на 25% на 3 хода.\n\r";
            //        npc.DebuffCount++;
            //        LogText.text += "Теперь у Монстра " + npc.DebuffCount + " дебаффa.\n\r";
            //    }
            //}

            //if (pMadMan)
            //{
            //    if (pMadManRelease)
            //    {
            //        pMadManRelease = false;
            //        int multiplToPerc = (int)pMadManMultiplier * 100;
            //        int bonusDmg = Mathf.RoundToInt(playerDmg * pMadManMultiplier);
            //        LogText.text += "Скил \"Безумец\" высвобождает весь накопленный бонус(" + multiplToPerc +
            //            "%). Урон = " + playerDmg + " + " + bonusDmg + " = ";
            //        playerDmg += bonusDmg;
            //        LogText.text += playerDmg + ".\n\r";
            //    }
            //    else
            //    {
            //        pMadManMultiplier += 0.5f;
            //        int multiplToPerc = (int)(pMadManMultiplier * 100);
            //        LogText.text += "Скил \"Безумец\" накапливает урон к скилу. Текущий модификатор " + multiplToPerc + "%.\n\r";
            //    }
            //}
            //if (PSilence)
            //{
            //    if (npc.DebuffCount >= 3)
            //    {
            //        LogText.text += "Скил \"Тишина\" не прошёл. Количетсво дебаффов не может быть больше 3.\n\r";
            //    }
            //    else
            //    {
            //        LogText.text += "Скил \"Тишина\" лишает Монстра использовать любые способности на 3 хода.\n\r";
            //        npc.DebuffCount++;
            //        LogText.text += "Теперь у Монстра " + npc.DebuffCount + " дебаффa.\n\r";
            //        npc.SilenceEffectOnEnemy = true;
            //        npc.SilenceDuration = 3;
            //    }
            //}
            //конец перечня активных скилов

            //пассивные скилы
            //if (PRage)
            //{
            //    playerDmg *= 2;
            //    LogText.text += "Пассивка \"Ярость\" даёт 100% прибавку к урону. Урон = " + playerDmg + ".\n\r";
            //}

            //if (pSabretooth)
            //{
            //    LogText.text += "Саблезуб делает условно броню Монстра 0.\n\r";
            //    npc.PhysArmorPercent = 0;
            //    pSabretooth = false;
            //}

            //int maxWpnLvl = (PlayerEquipmentStats.RightWeaponLvl >= PlayerEquipmentStats.LeftWeaponLvl) ? PlayerEquipmentStats.RightWeaponLvl : PlayerEquipmentStats.LeftWeaponLvl;
            //int playerPenetration = npc.Weakness ? Random.Range(1, 3) : Random.Range(1, 9); //пробитие игрока. Если наложен эффект "Бессилие", то пробитие 1d2. В остальном же случае пробитие стандартное: 1d8

            //if (npc.Weakness)
            //    LogText.text += "Действует эффект \"Бессилие\".\n\r";
            //LogText.text += "6. Вычисление пробития. Кидаем кубик от 1 до ";
            //if (!npc.Weakness)
            //    LogText.text += "8";
            //else
            //    LogText.text += "2";
            //LogText.text += "(выпадает " + playerPenetration + "), к результату прибавляем бонусы от силы(" + Strength + ") + бонус от эквипа(" + PlayerEquipmentStats.Penetration +
            //") + максимальный лвл оружия(" + maxWpnLvl + ") = ";
            //playerPenetration = playerPenetration + Strength + PlayerEquipmentStats.Penetration + maxWpnLvl;
            //if (PRage)
            //{
            //    playerPenetration *= 2;
            //    LogText.text += "Пассивка \"Ярость\" даёт 100% прибавку к пробитию. Пробитие = " + playerPenetration + ".\n\r";
            //}
            //else
            //{
            //    LogText.text += playerPenetration + ".\n\r";
            //}

            int enemyArmorResultPercent = 0; //остаток брони Монстра в процентах
            if (PenetrationPercent >= npc.PhysArmorPercent) //если пробитие Игрока больше защиты Монстра в процентах, то enemyArmorResultPercent остаётся 0. 
            //С этого следует, что урон будет чистый
            {
                LogText.text += "6. Пробитие Героя(" + PenetrationPercent + "%) больше защиты Монстра(" +
                                 npc.PhysArmorPercent + "%). По Монстру проходит чистый урон.\n\r";
            }
            else
            {
                enemyArmorResultPercent = npc.PhysArmorPercent - PenetrationPercent;
                LogText.text += "6. Защита Монстра(" + npc.PhysArmorPercent + "%) - пробитие Героя(" + PenetrationPercent + "%) = " + enemyArmorResultPercent +
                    "%.\n\r";
            }

            //enemyArmorResult = 2;

            if (enemyArmorResultPercent != 0)//обрезка урона за счёт оставшейся брони в процентах
            {
                //int currentHundredPercentArmor = CurrentNumberWhichIsHundredPercent + (int)(2.5f*CurrentLevelCounter);
                //float armorToPercent = (float)enemyArmorResultPercent / CurrentNumberWhichIsHundredPercent;
                //LogText.text += "100% брони сейчас " + CurrentNumberWhichIsHundredPercent + ", остаток брони " + enemyArmorResultPercent +
                //" = " + armorToPercent * 100 + "% от 100% брони. Урон Героя(" + playerDmg + ") - " + armorToPercent * 100 + "% = ";
                //playerDmg -= Mathf.RoundToInt(playerDmg * armorToPercent);
                float dmgFactor = (100 - enemyArmorResultPercent) / 100f;
                playerDmg = BattleMechanics.RoundItUp(playerDmg * dmgFactor);
                LogText.text += "Неполное пробитие Героя. Урон теперь = " + playerDmg + ".\n\r";
            }

            LogText.text += "ХП Монстра(" + npc.CurrentHp + ") - урон Героя(" + playerDmg + ") = ";
            npc.CurrentHp -= playerDmg;
            if (npc.CurrentHp < 0)
                npc.CurrentHp = 0;
            LogText.text += npc.CurrentHp + ".\n\r";

            ////пассивные скилы
            //if (pVampire)
            //{
            //    int vampirism = playerDmg / 2;
            //    CurrentHp += vampirism;
            //    if (CurrentHp > MaxHp)
            //        CurrentHp = MaxHp;
            //    LogText.text += "Вампиризм: " + vampirism + ". ХП Героя: " + CurrentHp + ".\n\r";
            //}
            //if (pBleed && npc.CurrentHp != 0)
            //{
            //    BleedMechanic(npc);
            //}
            //конец перечня пассивных скилов
        }
        else
        {
            LogText.text += " Промах, мать твою!\n\r";
            //if (npc.Motivation) //восстанавливает здоровье Монстра, если игрок промахнулся
            //{
            //    LogText.text +=
            //        "Способность Монстра \"Мотивация\" восстанавливает здоровье Монстру при промахе Героя. Вычисляем урон Героя.\n\r";
            //    npc.MotivationEffect = true; //ставим true, чтобы не учитывался Шанс ранения
            //    int npcHeal = PlayerDamageCalculation(npc);
            //    npc.MotivationEffect = false; //возвращаем базовое значение
            //    npc.CurrentHp += npcHeal;
            //    if (npc.CurrentHp > npc.MaxHp)
            //        npc.CurrentHp = npc.MaxHp;
            //    LogText.text += "Герой бы нанёс " + npcHeal + " урона. Теперь у Монстра " + npc.CurrentHp +
            //        " ХП.\n\r";
            //}
        }

    }


    //handOption:
    //1 - правая рука
    //2 - левая рука
    private static int PlayerDamageCalculation(NpcMonster npc, int handOption) //отдельный метод для вычисления урона игрока. Нужен отдельно для вражеского скила "Мотивация", 
    //который при промахе игрока восполняет здоровье врагу от количества урона, который нанёс бы игрок
    {
        int playerDmg = 0;
        LogText.text += "3. Вычисляем урон игрока: ";
        //if (PlayerEquipmentStats.RightWeaponMinDmg == 0)
        //    LogText.text += "  0\n\r";
        if (!KickEffectOnHero)
        {
            //for (byte i = 0; i < PlayerEquipmentStats.RightWeaponMinDmg; i++)
            //{
            if (handOption == 1)
            {
                int rightDmg = Random.Range(RightWeaponMinDmg, RightWeaponMaxDmg + 1);
                playerDmg += rightDmg;
                LogText.text += "разброс урона = " + RightWeaponMinDmg + "-" + RightWeaponMaxDmg + ". Получилось " + rightDmg;
                int closeCombatAddDmg = BattleMechanics.RoundItUp(playerDmg * (CloseCombatDmgPercent / 100f));
                playerDmg += closeCombatAddDmg;
                LogText.text += " + " + closeCombatAddDmg + "(" + CloseCombatDmgPercent + "% от бонуса ББ).\n\r";
                //if (PlagueEffectOnHero)
                //    LogText.text += "Активен скил \"Чума\". Бонусы от заточек не действуют.";
                //else
                //    if (PlayerEquipmentStats.RightWeaponAddDmgPercent > 0)
                //    {
                //        playerDmg += PlayerEquipmentStats.RightWeaponAddDmgPercent;
                //        LogText.text +=
                //            "На оружии в правой руке имеется заточка! Вот это да! К суммарному урону с руки прибавляем " +
                //            PlayerEquipmentStats.RightWeaponAddDmgPercent + " = " + playerDmg + ".\n\r";
                //    }
                if (DebuffRHandPhysDmg > 0)
                {
                    int debufValue = Mathf.RoundToInt(playerDmg * DebuffRHandPhysDmg);
                    playerDmg -= debufValue;
                    LogText.text += "Правая рука повреждена. Штраф: - " + debufValue + "(30% от урона правой руки) = " +
                                    playerDmg +
                                    ". ";
                }
            }
            else
            {
                int leftDmg = Random.Range(LeftWeaponMinDmg, LeftWeaponMaxDmg + 1);
                playerDmg += leftDmg;
                LogText.text += "разброс урона = " + LeftWeaponMinDmg + "-" + LeftWeaponMaxDmg + ". Получилось " + leftDmg + ". ";
            }
        }
        else
        {
            LogText.text += "Способность \"Пинок\" выбила правое оружие из руки Героя.";
            KickEffectOnHero = false;
        }

        //if (DebuffLDmg)
        //{
        //    LogText.text += "\n\r б)Левая рука повреждена, ею нельзя бить. 0 урона.\n\r";
        //}
        //else
        //{
        //    LogText.text += "\n\r б)Урон с левой руки руки: ";
        //    if (LeftWeaponMaxDmg > 0)
        //    {
        //        
        //        int leftDmg = Random.Range(LeftWeaponMinDmg, LeftWeaponMaxDmg + 1);
        //        //playerDmg += leftDmg;
        //        LogText.text += "разброс урона = " + LeftWeaponMinDmg + "-" + LeftWeaponMaxDmg + ". Получилось " + leftDmg + ".";
        //        //LogText.text += "   На кубике №" + (i + 1) + " выпало " + leftDmg + ". Суммарный урон = " +
        //        //                playerDmg + ".\n\r";
        //        //}
        //        //if (!npc.Plague)
        //        //    if (PlayerEquipmentStats.LeftWeaponAddDmgPercent > 0)
        //        //    {
        //        //        playerDmg += PlayerEquipmentStats.LeftWeaponAddDmgPercent;
        //        //        LogText.text +=
        //        //            "На оружии в левой руке имеется заточка! Вот это да! К суммарному урону с руки прибавляем " +
        //        //            PlayerEquipmentStats.LeftWeaponAddDmgPercent + " = " + playerDmg + ".\n\r";
        //        //        PlagueEffectOnHero = true;
        //        //    }
        //    }
        //    else
        //        LogText.text += "0-0.\n\r";
        //}


        LogText.text += "Полученный результат = " + playerDmg;
        if (GreaseBonus > 0)
        {
            if (npc.Plague)
                LogText.text += ".\n\rБонус от смазки не действует. \"Чума\" же";
            else
            {
                LogText.text += " + БОНУС ОТ СМАЗКИ(" + GreaseBonus + ") = ";
                playerDmg += GreaseBonus;
                LogText.text += "" + playerDmg;
            }
        }
        LogText.text += ".\n\r";

        //if (PlayerEquipmentStats.RightWeaponMaxDmg > 0 && PlayerEquipmentStats.LeftWeaponMaxDmg > 0) //если оружие в обеих руках, то уменьшаем урон
        //{
        //    playerDmg = Mathf.RoundToInt(playerDmg * 0.6f);
        //    LogText.text += "Оружие в двух руках! Результат режем на 40%. Получилось " + playerDmg + ".\n\r";
        //}

        //int critChance = BattleMechanics.RoundItUp((float)PhysCritChance / CurrentNumberWhichIsHundredPercent * 100); //вычисление крита
        LogText.text += "4. Шанс критического удара: " + PhysCritChance + "(" + PhysCritChancePercent + "%). ";
        bool criticalHit = BattleMechanics.ChanceCalculation(PhysCritChancePercent);
        //criticalHit = true;
        //CritChance = PhysCritChancePercent; //для теста скилов
        //LogText.text += "4. Шанс крита 1d" + PhysCritChancePercent + ". На кубике выпало " + critChance + ", что ";
        if (criticalHit)
        //{
        //if (critChance == PhysCritChancePercent) //если крит прошёл, то умножаем урон на 2
        {
            LogText.text += "Критический урон прошёл. Урон Героя(" + playerDmg + ") + 100% урона от крита(" + playerDmg + ")";
            int physCritRes = BattleMechanics.RoundItUp(playerDmg * (PhysCritPowerPercent / 100f));
            playerDmg = playerDmg * 2 + physCritRes;
            LogText.text += " + сила крита " + PhysCritPowerPercent + "%(" + physCritRes + " от урона) = " + playerDmg + ". ";
            //LogText.text += "является максимальным значением. Урон увеличен вдвое\n\r";
            if (npc.NervousBreakdown)
            {
                int bonusDamage = 5;
                npc.NervousBreakdownBonus += bonusDamage;
                LogText.text += "Пассивка \"Нервный срыв\" даёт бонус к урону Монстру в количестве " + bonusDamage +
                                " за каждый крит Героя. Текущий бонус: " + npc.NervousBreakdownBonus + ".\n\r";
            }
            if (!npc.MotivationEffect)
                //если у монстра есть "Мотивация" и она не сработала, то шанс ранения учитываем. Если прошла, то не затрагивает этот блок
                if (npc.CurrentHp - playerDmg > 0)
                {
                    LogText.text += "Прошёл крит. урон!  Шанс ранить Монстра удваивается: 20%";
                    BattleMechanics.WoundChance(npc, 1, 20);
                }
        }
        else
        {
            LogText.text += "Критический удар не прошёл.\n\r";
            if (!npc.MotivationEffect)
                if (npc.CurrentHp - playerDmg > 0) //если Монстр ещё жив, то можем ранить его
                {
                    LogText.text += "Шанс ранить Монстра: 10%";
                    BattleMechanics.WoundChance(npc, 1, 10);
                }
        }
        return playerDmg;
    }


    private static void BleedMechanic(NpcMonster npc)
    {
        int bleedRndm = Random.Range(1, 5);
        LogText.text += "\n\rУ Героя есть шанс оставить кровотечение на Монстре, шанс 1d4. На кубике выпало " + bleedRndm + ". ";
        if (bleedRndm == 4)
        {
            npc.BleedEffectOnEnemy.Add(skillData[7].duration);
            LogText.text += "\n\rНаложен эффект кровотечения. -2 хп 5 ходов.\n\r";
            npc.DebuffCount++;
            LogText.text += "Теперь у Монстра " + npc.DebuffCount + " дебаффa.\n\r";
        }
        else
            LogText.text += "Кровотечение не прошло.\n\r";
    }

    public static void PlayerMagicAttack(NpcMonster npc, int scrollMutl, int scrollDamage) //магическая атака игрока со свитка
    {
        if (npc.SpellImunity)
        {
            LogText.text += "\n\rГерой, да прочти ты уже руны на монстре! У него же иммунитет к магии. Магическая атака в молоко!\n\r";
            return;
        }
        int playerDamage = 0;

        LogText.text += "\n\r\n\rУ Героя из сумки вылетает светящийся свиток!\n\r";

        if (npc.EnemyIsSleeping)
        {
            npc.EnemyIsSleeping = false;
            LogText.text += "Монстр просыпается от Вашей атаки.\n\r";
        }

        LogText.text += "2. Высчитываем урон свитка:\n\r";

        for (int i = 0; i < scrollMutl; i++)
        {
            int rndmDmg = Random.Range(1, scrollDamage + 1);
            playerDamage += rndmDmg;
            LogText.text += "  На кубике №" + (i + 1) + " выпало " + rndmDmg + ". Суммарный урон = " + playerDamage + ".\n\r";
        }

        playerDamage += Strength;
        LogText.text += "К полученому результату добавляем урон от статов(" + Strength + ") = " + playerDamage + "\n\r";

        int critChance = Random.Range(1, MagCritChance + 1); //вычисление крита
        critChance = MagCritChance;
        LogText.text += "3. Шанс крита 1d" + MagCritChance + ". На кубике выпало " + critChance + ", что ";
        if (critChance == MagCritChance) //если крит прошёл, то умножаем урон на 2
        {
            playerDamage *= 2;
            LogText.text += "является максимальным значением. Урон увеличен вдвое.\n\r";
            if (npc.NervousBreakdown)
            {
                int bonusDamage = 5;
                npc.NervousBreakdownBonus += bonusDamage;
                LogText.text += "Пассивка \"Нервный срыв\" даёт бонус к урону Монстру в количестве " + bonusDamage +
                    " за каждый крит Героя. Текущий бонус: " + npc.NervousBreakdownBonus + ".\n\r";
            }
        }
        else
        {
            LogText.text += "не является максимальным значением.\n\r";
        }

        if (playerDamage > npc.CurrentMagDef) //если урона хватает, чтобы пробить броню
        {
            LogText.text += "МАГИЧЕСКАЯ АТАКА ПРОХОДИТ!\n\r 4. Брони у Монстра было " + npc.MaxMagDef + ", а через броню прошло ";
            playerDamage = playerDamage - npc.MaxMagDef;
            LogText.text += playerDamage + " урона.\n\r";

            npc.CurrentMagDef = npc.MaxMagDef;

            LogText.text += "5. Полученный урон вычитаем из здоровья Монстра: " + npc.CurrentHp + " - " + playerDamage + " = ";
            npc.CurrentHp -= playerDamage;
            if (npc.CurrentHp < 0)
                npc.CurrentHp = 0;
            LogText.text += npc.CurrentHp + ".\n\r";
            if (npc.CurrentHp == 0)
            {
                if (npc.Reincarnation)
                {
                    npc.EnemyReincarnation();
                    //return;
                }
            }
            else
            {
                //if (NpcMonster.CurrentHp != 0) //условия для того, чтобы не ранить монстра, когда он и так мёртв
                //{
                if (critChance == MagCritChance) //если был крит, то больше шанс ранить
                {
                    LogText.text += "Был крит. урон! Шанс ранить Монстра удваивается: 20%";
                    BattleMechanics.WoundChance(npc, 1, 20);
                }
                else
                {
                    LogText.text += "Шанс ранить Монстра: 10%";
                    BattleMechanics.WoundChance(npc, 1, 10);
                }
            }
        }
        else
        {
            if (npc.CurrentMagDef - playerDamage == 0)
            {
                playerDamage = Random.Range(1, 3);
                npc.CurrentHp -= playerDamage;
                if (npc.CurrentHp < 0)
                {
                    if (npc.Reincarnation)
                    {
                        npc.EnemyReincarnation();
                        return;
                    }
                    //if (eZombie)
                    //{
                    //    EnemySkillZombieDied();
                    //    LogText.text += "Зомби умер. Вместо него теперь хозяин.\n\rХодит Герой.\n\r";
                    //    return;
                    //}
                    npc.CurrentHp = 0;

                }
                LogText.text += "3. Броня разбита магией! Расплавленные осколки брони шрапнелью выстреливают в Монстра.\n\r4. Урон от малого пробития = " +
                    playerDamage + ".\n\r";
                LogText.text += "У Монстра осталось " + npc.CurrentHp + ".\n\r";
                if (npc.CurrentHp != 0) //условия для того, чтобы не ранить монстра, когда он и так мёртв
                    if (critChance == MagCritChance)
                    {
                        LogText.text += "При малом пробитии 5% шанс ранить Героя";
                        BattleMechanics.WoundChance(npc, 1, 5);
                    }
                    else
                    {
                        LogText.text += "Был крит. урон! При малом пробитии с крит. уроном шанс удваивается: 10% ранить Героя";
                        BattleMechanics.WoundChance(npc, 1, 10);
                    }
                return;
            }
            LogText.text += "         Не пробили! Давайте больше даров своим Богам!\n\r";
        }
    }


    /* 3 варианта для метода:
     * 1 - Игрок атакует
     * 2 - Игрок защищается
     * 3 - Вариант для того, чтобы действовал яд
     */
    public static void PlayerBattlePhase(byte option, NpcMonster npc) //метод для боя с фазами физ. атаки и физ. защиты игрока
    {
        if (_dontFightSkillIsNotReady)
        {
            _dontFightSkillIsNotReady = false;
            return;
        }

        if (PoisonEffectOnHero) //если яд на игроке
        {
            LogText.text += "\n\rЯд медленно разъедает всё внутри. ХП Героя было(" + CurrentHp + ") - урон от яда(" +
                            _poisonDmgOnHero + ") = ";
            CurrentHp -= _poisonDmgOnHero;
            if (CurrentHp < 0)
                CurrentHp = 0;
            LogText.text += CurrentHp + ".\n\r";
            if (CurrentHp == 0)
            {
                LogText.text += "Герой не выдержал тяжёлой участи и умер от яда.\n\r";
                return;
            }
        }

        if (GallPoisonEffectOnHero)
        {
            LogText.text += "\n\rЖёлчный яд медленно разъедает всё внутри. ХП Героя было(" + CurrentHp + ") - урон от яда(" +
                            _gallPoisonDmgOnHero + ") = ";
            CurrentHp -= _gallPoisonDmgOnHero;
            if (CurrentHp < 0)
                CurrentHp = 0;
            LogText.text += CurrentHp + ".\n\r";
            if (CurrentHp == 0)
            {
                LogText.text += "Герой не выдержал тяжёлой участи и умер от жёлчного яда.\n\r";
                return;
            }
        }

        if (ToxicEffectOnHero)
        {
            LogText.text += "\n\rОтрава медленно разъедает всё внутри. ХП Героя было(" + CurrentHp + ") - урон от отравы(" +
                            _toxicDmgOnHero + ") = ";
            CurrentHp -= _toxicDmgOnHero;
            if (CurrentHp < 0)
                CurrentHp = 0;
            LogText.text += CurrentHp + ".\n\r";
            if (CurrentHp == 0)
            {
                LogText.text += "Герой не выдержал тяжёлой участи и умер от отравы.\n\r";
                return;
            }
        }

        for (int i = 0; i < BloodinessEffectOnHero.Count; i++)
        {
            BloodinessEffectOnHero.Remove(0);
        }

        for (int i = 0; i < BloodinessEffectOnHero.Count; i++)
        {

            CurrentHp -= 2;
            if (CurrentHp < 0)
                CurrentHp = 0;
            BloodinessEffectOnHero[i]--;

            LogText.text += "Стак №" + (i + 1) + ". -2 хп от кровотечения. Теперь у Героя " + CurrentHp +
                " ХП. До конца стака осталось " + BloodinessEffectOnHero[i] + " ходов";
            if (BloodinessEffectOnHero[i] == 0)
            {
                LogText.text += ". Стак уже не будет работать в следующем ходу";
            }
            LogText.text += ".\n\r";
            if (CurrentHp == 0)
            {
                LogText.text += "Герой не выдержал тяжёлой участи и умер от кровотечения.\n\r";
                return;
            }
        }

        switch (option)
        {
            case 1:
                BonusDef = 0;
                if (BattleMechanics.GameOver)
                    return;
                if (RightWeaponMaxDmg > 0)
                {
                    if (DistanceAttack)
                    {
                        LogText.text += "Выстрел из оружия дальнего боя:\n\r";
                    }
                    else
                    {
                        LogText.text += "Удар ближнего боя с правой руки руки:\n\r";
                    }
                    PlayerPhysAttack(npc, 1);
                }

                if (npc.CurrentHp != 0)
                {
                    if (DebuffLDmg && LeftWeaponMaxDmg > 0)
                    {
                        LogText.text += "\n\rЛевая рука повреждена, ею нельзя бить.\n\r";
                    }
                    else
                    {
                        if (LeftWeaponMaxDmg > 0 && !DistanceAttack)
                        {
                            LogText.text += "\n\rУдар с левой руки руки:\n\r";
                            PlayerPhysAttack(npc, 2);
                        }
                    }
                }
                break;
            case 2:
                if (PlayerEquipmentStats.LeftShield && !DebuffLDmg)
                {
                    BonusDef = BattleMechanics.RoundItUp(PhysArmor * 0.3f);
                    if (BonusDef < 1)
                        BonusDef = 1;
                    LogText.text += "\n\rГерой защищается щитом. Бонус защиты(30% от брони Героя) = " + BonusDef +
                                    ".\n\r";
                }
                else
                {
                    BonusDef = BattleMechanics.RoundItUp(PhysArmor * 0.1f);
                    if (BonusDef < 1)
                        BonusDef = 1;
                    if ((DebuffLDmg || NakedEffectOnHero) && PlayerEquipmentStats.LeftShield)
                    {
                        LogText.text +=
                            "\n\rУ Героя щит есть, а использовать он его не может! ";
                        if (DebuffLDmg)
                            LogText.text += "Левая рука повреждена! ";
                        if (NakedEffectOnHero)
                            LogText.text += "Дебафф \"Нагота\" наложен! ";
                        LogText.text += "Придётся защищаться оружием.";
                    }
                    LogText.text += "\n\rГерой защищается оружием. Бонус защиты(10% от брони Героя) = " + BonusDef + ".\n\r";
                }
                break;
            case 3:
                break;
        }
        if (npc.CurrentHp == 0)
        {
            //if (eZombie)
            //{
            //    EnemySkillZombieDied();
            //    LogText.text += "Зомби умер. Вместо него теперь хозяин.\n\rХодит Герой.\n\r";
            //}
            if (npc.Reincarnation)
            {
                npc.EnemyReincarnation();
            }
        }
    }


    public static void PlayerNumbersToPercent()
    {
        PenetrationPercent = BattleMechanics.RoundItUp((float)Penetration / CurrentNumberWhichIsHundredPercent * 100f);
        PhysArmorPercent = BattleMechanics.RoundItUp((float)PhysArmor / CurrentNumberWhichIsHundredPercent * 100f);
        PhysCritChancePercent = BattleMechanics.RoundItUp((float)PhysCritChance / CurrentNumberWhichIsHundredPercent * 100); //вычисление крита
        EvasionPercent = BattleMechanics.RoundItUp((float)Evasion / CurrentNumberWhichIsHundredPercent * 100); //вычисление крита
    }
}