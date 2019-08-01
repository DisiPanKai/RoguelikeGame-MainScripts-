using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public static class BattleMechanics
{
    public static int RemainingPoints = 2; //общее к-ство очков за все уровни. на первом уровне всегда 2 очка для характеристик
    public static int PointsPerLvl = 2; //к-ство очков за 1 уровень. переменная постоянно меняется, когда добавляем очки
    public static Transform[] MainStatsGameObjects = { 
        GameObject.Find("Strength").transform.Find("Button"), 
        GameObject.Find("Agility").transform.Find("Button"),
        GameObject.Find("Constitution").transform.Find("Button"),
        GameObject.Find("Intelligence").transform.Find("Button")
    };

    public static int CurrentLvl = 1; //текущий уровень игрока

    public static readonly Text LogText = GameObject.Find("Log Field Text").GetComponent<Text>();
    public static Dictionary<string, NpcMonster> NpcData = new Dictionary<string, NpcMonster>(); //все данные о Монстрах
    public static List<NpcMonster> NpcList = new List<NpcMonster>(); //данные о Монстрах на поле боя
    public static int SelectedNpc = 0; //номер выбранного Монстра

    //private static bool _enemyHitFirst; //переменная для инициативы. Если Монстр бьёт первый, то не учитываем выбранного Монстра в переменной SelectedNpc

    //static int playerInitiative; //инициатива игрока
    //static int enemyInitiative; //инициатива монстра

    public static bool GameOver; //когда все нипы умирают, или умирает игрок, эта переменная выводит всех из боя
    //public static bool PlayerMadeHisTurn; //нужно для порядка ходов. Если игрок походил, переменная становится true и в порядке ходов игрок пропускается
    public static bool GameStarted; //нужно для смены оружия дальнего и ближнего боя. Вне боя, мы просто меняем инициативу. Тогда бой ещё не начался. 
    //А когда начался, то Герой должен пропустить ход, чтобы сменить оружие+перерассчёт инициативы.


    public static List<int> InitiativeOrder = new List<int>(); //инициативный порядок ходов всех в бою
    private static int _playerOrderValue; //текущий номер игрока. Число всегда будет равно +1 от id последнего монстра в массиве монстров
    //private static int orderNumber = 0; //нужна для 
    //static void SaveEnenmyState() //сохраняем текущее состояние монстра перед заменой на Зомби
    //{
    //    _savedNpcState.savedBleedEffect = BleedEffectOnEnemy;
    //    _savedNpcState.savedDebuffCount = eDebuffCount;
    //    _savedNpcState.savedDebuffAcc = eDebuffAcc;
    //    _savedNpcState.savedDebuffDmg = eDebuffDmg;
    //    _savedNpcState.savedDebuffEva = eDebuffEva;
    //    _savedNpcState.savedDef = NpcMonster.CurrentPhysDef;
    //    _savedNpcState.savedHp = NpcMonster.CurrentHp;
    //    _savedNpcState.savedEnemySkills = enemySkills;
    //    _savedNpcState.savedPoison = Poison;
    //    _savedNpcState.savedArmsOut = pArmsOut;
    //}


    //static void ReturnPreviousEnemyState(NpcMonster npc) //после исчезновения Зомби возвращаем предудущее состояние монстра
    //{
    //    BleedEffectOnEnemy = _savedNpcState.savedBleedEffect;
    //    npc.DebuffCount = _savedNpcState.savedDebuffCount;
    //    npc.DebuffHeadAccPercent = _savedNpcState.savedDebuffAcc;
    //    npc.DebuffDmg = _savedNpcState.savedDebuffDmg;
    //    npc.DebuffEvaPercent = _savedNpcState.savedDebuffEva;
    //    enemySkills = _savedNpcState.savedEnemySkills;
    //    Poison = _savedNpcState.savedPoison;
    //    pArmsOut = _savedNpcState.savedArmsOut;
    //    npc.CurrentHp = _savedNpcState.savedHp;
    //    npc.PhysArmor = _savedNpcState.savedDef;
    //}


    public static void InitiativeCount() //подсчёт инициативы и последовательности ходов всех участвующих в бою
    {
        //EnemySkillCountersCheck();
        InitiativeOrder.Clear();
        if (PlayerStats.WeaponSwitch)
        {
            LogText.text += "\n\r1. Перерасчёт инициативы.";
            PlayerStats.WeaponSwitch = false;
        }
        else
        {
            LogText.text += "\n\r1. Инициатива.";
        }

        LogText.text += "\n\r а)Инициатива Героя: " +
            PlayerStats.Initiative + ".\n\r" + " б)Инициатива Монстров:\n\r";
        for (int i = 0; i < NpcList.Count; i++)
        {
            LogText.text += "   " + (i + 1) + ") " + NpcList[i].Name + " - " + NpcList[i].Initiative;
            if (i == NpcList.Count - 1)
            {
                LogText.text += ".\n\r"; //если последний Монстр в списке, то ставим точку
            }
            else
            {
                LogText.text += ";\n\r";
            }
        }
        _playerOrderValue = NpcList.Count;
        if (PlayerStats.Initiative == 11)
        {
            //LogText.text += " Герой ходит первый.\n\r";
            InitiativeOrder.Add(_playerOrderValue);
        }

        int maxDistanceNpc = 0; //
        int maxCloseNpc = 0;
        foreach (var npc in NpcList)
        {
            if (npc.Initiative == 10)
                maxDistanceNpc++; //подсчёт общего к-ства монстров в бою

            if (npc.Initiative == 5)
                maxCloseNpc++;
        }

        //замысел в целом: когда больше 1 моба, то нужно рандомом определить кто из них будет ходить первый

        while (maxDistanceNpc != 0)
        {
            int distanceInitiativeChance = 100 / maxDistanceNpc; //шанс того, что походит монстр. больше нужно для большого к-ства монстров. 
            for (int i = 1; i <= maxDistanceNpc; i++)
            {
                LogText.text += "Рассчёт шанса инициативы Монстра дистанционного боя № " + i + ". Шанс: " + distanceInitiativeChance + "%. ";
                bool npcTurnChance = ChanceCalculation(distanceInitiativeChance); //переменная для хода Монстра. Метод возвращает bool
                if (npcTurnChance || i == maxDistanceNpc) //итак
                //если прошёл шанс, допустим, на второго нпс дальнего боя, то нам нужно найти, какой номер у нашего нипа в общем списке, а не только дистанционных
                //затем, добавляем нипа в список
                {
                    int distanceNpcCounter = 0;
                    int npcOrderNumber = 0;
                    foreach (var npc in NpcList)
                    {
                        if (npc.Initiative == 10 && !npc.NpcInInitiativeList)
                        {
                            distanceNpcCounter++; //подсчёт общего к-ства монстров в бою
                        }
                        if (distanceNpcCounter == i)
                        {
                            npc.NpcInInitiativeList = true;
                            InitiativeOrder.Add(npcOrderNumber);
                            break;
                        }
                        npcOrderNumber++;
                    }
                    maxDistanceNpc--;
                }
                LogText.text += "\n\r";
            }
        }

        if (PlayerStats.Initiative == 6)
        {
            InitiativeOrder.Add(_playerOrderValue);
        }

        while (maxCloseNpc != 0)
        {
            int closeInitiativeChance = 100 / maxCloseNpc; //шанс того, что походит монстр. больше нужно для большого к-ства монстров. 
            for (int i = 1; i <= maxCloseNpc; i++)
            {
                LogText.text += "Рассчёт шанса инициативы Монстра ближнего боя № " + i + ". Шанс: " + closeInitiativeChance + "%. ";
                bool npcTurnChance = ChanceCalculation(closeInitiativeChance); //переменная для хода Монстра. Метод возвращает bool
                if (npcTurnChance || i == maxCloseNpc) //итак
                //если прошёл шанс, допустим, на второго нпс дальнего боя, то нам нужно найти, какой номер у нашего нипа в общем списке, а не только дистанционных
                //затем, добавляем нипа в список
                {
                    int closeNpcCounter = 0;
                    int npcOrderNumber = 0;
                    foreach (var npc in NpcList)
                    {
                        if (npc.Initiative == 5 && !npc.NpcInInitiativeList)
                        {
                            closeNpcCounter++; //подсчёт общего к-ства монстров в бою
                        }
                        if (closeNpcCounter == i)
                        {
                            npc.NpcInInitiativeList = true;
                            InitiativeOrder.Add(npcOrderNumber);
                            break;
                        }
                        npcOrderNumber++;
                    }
                    maxCloseNpc--;
                }
                LogText.text += "\n\r";
            }
        }
        LogText.text += "Порядок ходов на поле боя: ";
        foreach (var order in InitiativeOrder)
        {
            if (order == _playerOrderValue)
            {
                LogText.text += "Герой";
            }
            else
            {
                LogText.text += NpcList[order].Name;
            }
            if (order == InitiativeOrder[InitiativeOrder.Count - 1])
                LogText.text += ".\n\r";
            else
                LogText.text += ", ";
        }
        foreach (var npc in NpcList)
        {
            npc.NpcInInitiativeList = false;
        }
        TurnPhase();
    }


    public static void TurnPhase() //ход всех участвующих по выстроенному порядку
    {
        int turnPosition = 1; //счётчик позиции
        foreach (int orderNumber in InitiativeOrder)
        {
            if (orderNumber == _playerOrderValue && !PlayerStats.MadeTurn) //если Герой, то он ходит и мы выходим из цикла
            {
                LogText.text += "\n\r" + turnPosition + ") Ход Героя.\n\r";
                PlayerStats.MadeTurn = true;
                return;
            }
            if (orderNumber == _playerOrderValue)
            {
                turnPosition++;
                continue;
            }
            //if (NpcList[orderNumber].CurrentHp == 0)
            //{
            //    if (GameOver)
            //        return;
            //    else
            //        continue;
            //}
            if (!NpcList[orderNumber].NpcMadeHisTurn)
            {
                LogText.text += "\n\r" + turnPosition + ") Ходит " + NpcList[orderNumber].Name + ". ";
                NpcList[orderNumber].MonsterBattlePhase();
                if (PlayerStats.CurrentHp == 0)
                {
                    if (GameOver)
                        return;
                    else
                        WhoIsLoosing(NpcList[orderNumber]);
                }
                if (NpcList[orderNumber].CurrentHp == 0)
                    if (GameOver)
                        return;
            }
            turnPosition++;
        }
        int madeTurnCounter = NpcList.Count(npc => (npc.NpcMadeHisTurn || npc.CurrentHp == 0) && PlayerStats.MadeTurn);
        if (madeTurnCounter == NpcList.Count)
        {
            foreach (NpcMonster npc in NpcList)
            {
                npc.NpcMadeHisTurn = false;
            }
            PlayerStats.MadeTurn = false;
            if (!PlayerStats.WeaponSwitch)
            {
                TurnPhase();
            }
        }
    }


    public static void EnemySkillCountersCheck() //проверка и декрементация счётчиков кд, длительности скилов врага
    {
        //NpcSkillFailure();
        foreach (NpcMonster npc in NpcList)
        {
            npc.EnemySkillsCds();
            npc.EnemyBuffsDuration();
            npc.PlayerDebuffsDurationOnEnemy(); //дебаффы игрока на враге
        }

    }


    public static void PlayerSkillAndBuffCountersCheck() //проверка и декрементация счётчиков кд, длительности скилов, длительности баффов игрока и отключение скиллов, если они не прошли
    {
        PlayerSkillCounters();
        PlayerStats.PlayerBuffEffect();
        PlayerStats.PlayerSkillFailure();
    }

    static void PlayerSkillCounters() //проверка и декрементация счётчиков кд, длительности скилов
    {
        //PlayerDelayedSkillDuration();
        PlayerStats.PlayerSkillCds();
        PlayerStats.PlayerBuffsDuration();
        PlayerStats.EnemyDebuffsDurationOnHero(); //дебаффы врага на игроке
    }


    public static void AfterPlayerAction()
    //набор методов после атаки Героя. Вмещает в себя и декрементацию кд, длительности скилов врага, и активацию скилов врага;
    //затем идёт атака; проверка на поражение; декрементацию кд, длительности скилов игрока
    {
        //if (dontFightSkillIsNotReady)
        //{
        //    dontFightSkillIsNotReady = false;
        //    return;
        //}
        //if (!NpcList[SelectedNpc].NpcMadeHisTurn)
        ////если выбранный моб не атаковал, то он бьёт первым, а затем все остальные
        //{
        //    NpcList[SelectedNpc].MonsterBattlePhase();
        //}
        foreach (NpcMonster npc in NpcList) //атака оставшихся мобов
        {
            if (npc.CurrentHp == 0)
            {
                if (GameOver)
                    return;
                else
                    continue;
            }
            if (!npc.NpcMadeHisTurn) //если моб ещё не атаковал Героя, то он бьёт в этом ходу
                npc.MonsterBattlePhase();
            if (PlayerStats.CurrentHp == 0)
            {
                if (GameOver)
                    return;
                else
                    WhoIsLoosing(npc);
            }
            if (npc.CurrentHp == 0)
                if (GameOver)
                    return;
        }
        foreach (NpcMonster npc in NpcList)
        {
            npc.NpcMadeHisTurn = false;
            npc.EnemySkillPreparation();
        }

        PlayerSkillAndBuffCountersCheck();
    }


    //public static void NpcSkillFailure() //если не пробили броню игрока, то отменяем эффект скилов
    //{
    //if (eKickEffect)
    //{
    //    eKickEffect = false;
    //    LogText.text += "\n\rЭффект Монстра \"Пинок\" испарился, потому что броня не была пробита.\n\r";
    //}
    //}


    /*option:
     * 1 - бьёт игрок и вешает дебафф на монстра
     * 2 - бьёт монстр и вешает дебафф на игрока
     * 
     * chance(шанс ранения)
     */
    public static void WoundChance(NpcMonster npc, int option, int chance)//генератор шанса ранения
    {
        if ((option == 1 && npc.DebuffCount == 10) || (option == 2 && PlayerStats.DebuffCount == 10))
        {
            LogText.text += "\n\r5. Наложено максимальное количество дебаффов ";
            if (option == 1)
                LogText.text += "на Монстре";
            if (option == 2)
                LogText.text += "на Герое";
            LogText.text += ".\n\r";
            return;
        }
        if (PlayerStats.pBoneBreaker && option == 1)
        {
            chance += 10;
            LogText.text += " + 10% от пассивки = " + chance + "%";
        }
        if (!npc.Chiropractor)
            LogText.text += ".\n\r";

        //int woundRndmChance = Random.Range(1, 101);
        //if (npc.Chiropractor && option == 2)
        //{
        //    woundRndmChance = 51;
        //    npc.Chiropractor = false;
        //}
        //woundRndmChance = 55; //для теста скила
        //int additionalChance = 0;
        //switch (chance)
        //{
        //    case 5: //при малом пробитии
        //        //LogText.text += "5%";
        //        additionalChance = -5;
        //        break;
        //    case 10: //при обычном ударе или крите с малого пробития
        //        //LogText.text += "10%";
        //        break;
        //    case 15: //при малом пробитии + пассивка
        //        additionalChance = 5;
        //        break;
        //    case 20: //при крите или обычном ударе + пассивка
        //        //LogText.text += "20%";
        //        additionalChance = 10;
        //        break;
        //    //case 25: //если шанс +5%, то при крите с пассивкой
        //    //    additionalChance = 15;
        //    //    break;
        //    case 30: //крит + пассивка
        //        additionalChance = 20;
        //        break;
        //}

        LogText.text += "5. Повреждение частей тела. ";
        bool woundSuccess = ChanceCalculation(chance);
        if (woundSuccess)
        {
            LogText.text += "Ранение прошло. Далее вычисляем, что будем ранить. ";
            if (option == 1)
            {
                if (npc.Scrapper)
                    LogText.text += "У Монстра активна пассивка \"Драчун\". Штрафы превращаются в бонусы.\n\r";



                int randomPart = Random.Range(1, 76);
                while ((npc.DebuffDmg > 0 && randomPart >= 1 && randomPart <= 50) ||
                       (npc.DebuffAcc > 0 && randomPart >= 51 && randomPart <= 60) ||
                       (npc.DebuffEva > 0 && randomPart >= 61 && randomPart <= 75))
                {
                    randomPart = Random.Range(1, 76);
                }
                if (randomPart >= 1 && randomPart <= 50) //руки, лапы и т. д.
                {

                    if (npc.Scrapper)
                    {
                        npc.DebuffDmg = -0.3f;
                        LogText.text += "Герой хотел повредить руки, лапы или что у них там. Не судьба. +30% урона.\n\r";
                    }
                    else
                    {
                        npc.DebuffDmg = 0.3f;
                        LogText.text += "Герой повредил Монстру руки, лапы, или что у них там. -30% урона.\n\r";
                    }
                }

                if (randomPart >= 51 && randomPart <= 60) //голова
                {
                    if (npc.Scrapper)
                    {
                        npc.DebuffDmg = -0.3f;
                        LogText.text += "Герой хотел повредить голову. Не судьба. +30% точности физических атак и +30% урона от магии.\n\r";
                    }
                    else
                    {
                        npc.DebuffAcc = 0.3f;
                        LogText.text += "Герой повредил Монстру голову. -30% точности физических атак и -30% урона от магии.\n\r";
                    }
                }

                if (randomPart >= 61 && randomPart <= 75) //ноги
                {
                    if (npc.Scrapper)
                    {
                        npc.DebuffDmg = -0.3f;
                        LogText.text +=
                            "Герой хотел повредить ногу. Не судьба. +30% уворота.\n\r";
                    }
                    else
                    {
                        npc.DebuffEva = 0.3f;
                        LogText.text += "Герой повредил Монстру ноги. -30% уворота.\n\r";
                    }
                }
                if (!npc.Scrapper)
                {
                    npc.DebuffCount++;
                    LogText.text += "Теперь у Монстра " + npc.DebuffCount + " дебаффa.\n\r";
                }
            }
            else
            {
                int randomPart = Random.Range(1, 76);
                while ((PlayerStats.DebuffRHandPhysDmg > 0 && randomPart >= 1 && randomPart <= 15) ||
                       (PlayerStats.DebuffLDmg && randomPart >= 16 && randomPart <= 30) ||
                       (PlayerStats.DebuffHeadAccPercent > 0 && randomPart >= 31 && randomPart <= 35) ||
                       (PlayerStats.DebuffEvaPercent > 0 && randomPart >= 61 && randomPart <= 75))
                {
                    randomPart = Random.Range(1, 76);
                }
                if (randomPart >= 1 && randomPart <= 10) //правая рука (10%)
                {
                    //float accDebuffPercent = 0.15f; //процент дебаффа
                    PlayerStats.DebuffRHandAccPercent = 15; //дебафф в цифрах
                    PlayerStats.AccuracyPercent -= PlayerStats.DebuffRHandPhysDmg;
                    if (PlayerStats.AccuracyPercent < 0)
                    {
                        PlayerStats.DebuffRHandAccPercent += PlayerStats.AccuracyPercent; //
                        PlayerStats.AccuracyPercent = 0;
                    }
                    float dmgDebuffPercent = 0.15f;
                    int rightWeaponMinDmgDebuff = Mathf.RoundToInt(PlayerStats.MaxRightWeaponMinDmg * dmgDebuffPercent);
                    int rightWeaponMaxDmgDebuff = Mathf.RoundToInt(PlayerStats.MaxRightWeaponMaxDmg * dmgDebuffPercent); ;
                    PlayerStats.RightWeaponMinDmg -= rightWeaponMinDmgDebuff;
                    PlayerStats.RightWeaponMaxDmg -= rightWeaponMaxDmgDebuff;
                    //PlayerStats.DebuffRHandPhysDmg = Mathf.RoundToInt(PlayerStats.AccuracyPercent * dmgDebuffPercent);
                    LogText.text += "Герою повредили правую руку. -15% урона и 15% точности от правой руки.\n\r";
                }

                if (randomPart >= 11 && randomPart <= 20) //левая рука (10%)
                {
                    //PlayerEquipmentStats.LeftWeaponMinDmg = 0;
                    //PlayerEquipmentStats.LeftShieldBonus = 0;
                    PlayerStats.DebuffLDmg = true;
                    LogText.text +=
                        "Герою повредили левую руку. Если в левой руке было оружие или щит, оно становится недееспособным.\n\r";
                }

                if (randomPart >= 21 && randomPart <= 25) //голова (5%)
                {
                    PlayerStats.DebuffHeadAccPercent = 20;
                    PlayerStats.AccuracyPercent -= PlayerStats.DebuffHeadAccPercent;
                    if (PlayerStats.AccuracyPercent < 0)
                    {
                        PlayerStats.DebuffHeadAccPercent += PlayerStats.AccuracyPercent; //
                        PlayerStats.AccuracyPercent = 0;
                    }
                    //место для магии
                    LogText.text += "Герою повредили голову. -20% точности оружия и -20% урона от магии.\n\r";
                }

                if (randomPart >= 26 && randomPart <= 40) //корпус (15%)
                {
                    //PlayerStats.DebuffEvaPercent = 0.3f;
                    float hpDebuffPercent = 0.15f;
                    int hpDebuffAmount = Mathf.RoundToInt(PlayerStats.MaxHp * hpDebuffPercent);
                    PlayerStats.MaxHp -= hpDebuffAmount;
                    LogText.text += "Герою повредили корпус. -15% здоровья.\n\r";
                }

                if (randomPart >= 41 && randomPart <= 55) //ноги (15%)
                {
                    PlayerStats.DebuffEvaPercent = 20;
                    PlayerStats.EvasionPercent -= PlayerStats.DebuffEvaPercent;
                    if (PlayerStats.EvasionPercent < 0)
                    {
                        PlayerStats.DebuffEvaPercent += PlayerStats.EvasionPercent; //
                        PlayerStats.EvasionPercent = 0;
                    }
                    LogText.text += "Герою повредили ноги. -20% уворота.\n\r";
                }
                PlayerStats.DebuffCount++;
                LogText.text += "Теперь у Героя " + PlayerStats.DebuffCount + " дебаффа.\n\r";
            }
        }
        else
        {
            LogText.text += "Ранение не прошло.\n\r";
        }
    }


    public static int RoundItUp(float f)
    {
        int roundedValue = (int)Mathf.Floor(f + 0.5f);
        return roundedValue;
    }

    //private static void GeaseEffect() //метод с проверкой смазки
    //{
    //    if (greaseIsActivated)
    //    {
    //        greaseTurns--;
    //        if (greaseTurns == 0)
    //        {
    //            greaseIsActivated = false;
    //            GreaseBonus = 0;
    //            LogText.text += "\n\rСмазка размазалась по Монстру. Теперь ты сам по себе, Герой.\n\r";
    //            return;
    //        }
    //        LogText.text += "\n\rДо конца усиления урона осталось " + greaseTurns + " ходов.\n\r";
    //    }
    //}

    //public static void CheckingGains() //проверка всех усилений игрока
    //{
    //    PlayerBuffEffect();
    //    GeaseEffect();
    //}

    public static void WhoIsLoosing(NpcMonster npc) //проверка и вывод сообщения кто победил
    {
        if (PlayerStats.CurrentHp == 0)
        {
            LogText.text += "\n\rПобеда досталась Монстрам. Герой пал. Мы все будем скорбить по нему...";
            GameOverMessage();
            return;
        }

        if (npc.CurrentHp == 0)
        {
            InitiativeOrder.Remove(npc.Id);
            LogText.text += npc.Name + " был убит Героем.\n\r";
        }

        int counterDeadNpc = NpcList.Count(monster => monster.CurrentHp == 0);
        if (counterDeadNpc == NpcList.Count)
        {
            LogText.text += "\n\rПобеда досталась Герою! Монстры были побеждены, а деревня спасена! " +
                             "Наш Герой будет сыт, пьян и вознаграждён девицами из деревни за свой подвиг! Надеюсь, они красивые!";
            GameOverMessage();
            GameOver = true;
        }
    }


    private static void GameOverMessage()
    {
        LogText.text += "\n\rНу вот, схватка и закончилась. Чего тут глазеете? А ну разбегаемся все по домам! Кыш отсюда!\n\r";
        //GameOver = true;
        PlayerStats.Poison = false;
        PlayerStats.GreaseBonus = 0;
        PlayerStats.BuffIsActivated = false;
        PlayerStats.BuffTurns = 0;
        GameObject.Find("Attack Button").GetComponent<Button>().enabled = false;
        GameObject.Find("Defence Button").GetComponent<Button>().enabled = false;
        GameObject.Find("Attack Scroll").transform.Find("Button").GetComponent<Button>().enabled = false;
        GameObject.Find("Heal Scroll").transform.Find("Button").GetComponent<Button>().enabled = false;
        GameObject.Find("Buff Scroll").transform.Find("Button").GetComponent<Button>().enabled = false;
        GameObject.Find("Sleep Scroll").transform.Find("Button").GetComponent<Button>().enabled = false;
    }


    public static bool ChanceCalculation(int chance)
    {
        int rndmChance = Random.Range(1, 101); //рандомизатор 100% 
        LogText.text += "Рандомизатор шанса. Если число выпадет до " + chance + ", значит шанс прошёл. Выпало " + rndmChance + ". ";
        bool success = (rndmChance >= 1) && (rndmChance <= chance);
        //bool success = false;
        //if ((rndmChance >= 1) && (chance <= rndmChance))
        //    success = true;
        if (success)
            LogText.text += "Шанс прошёл.";
        else
            LogText.text += "Шанс не прошёл.";
        return success;
    }
}
