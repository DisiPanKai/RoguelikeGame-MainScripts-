using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NpcMonster : ICloneable
{
    Text _logText = GameObject.Find("Log Field Text").GetComponent<Text>();

    public bool NpcMadeHisTurn;//когда за раунд нип сделал ход, он не может ходить в этом раунде
    public int Id;
    public List<NpcSkillData> SkillsData = new List<NpcSkillData>();
    public string Name;
    public int Lvl;
    public int PhysMinDmg;
    public int PhysMaxDmg;
    public int MagMinDmg;
    public int MagMaxDmg;
    public int PhysArmorPercent;
    public int CurrentMagDef;
    public int MaxMagDef;
    public int MaxHp;
    public int CurrentHp;
    public int AccuracyPercent;
    public int EvasionPercent;
    public int Initiative;
    public int CritChancePercent;
    public int BonusPhysAttack;
    public int BonusMagAttack;
    public int PenetrationPercent;
    public bool DistanceAttack = false;
    public bool NpcInInitiativeList = false; //если нип в списке, то он не засчитывается в списке порядка ходов
    public bool ForDublicate = false;

    public bool EnemyHit = true;//переменная для скилов, после которых либо идёт мгновенный удар, либо использование скила и пропуск удара

    public int DebuffCount;//общее количество дебаффов

    //дебаффы от ударов игрока
    public float DebuffAcc;//дебафф монстра на точность
    public float DebuffEva;//дебафф монстра на уворот
    public float DebuffDmg;//дебафф монстра на урон

    //дебаффы от скилов игрока
    public bool ArmsOutEffectOnEnemy; //наложен ли эффект от дебаффа "Оружие вон"
    public int ArmsOutDuration;
    public bool KnightHookEffectOnEnemy; //наложен ли эффект от дебаффа "Рыцарский хук"
    public int KnightHookDuration;
    public bool AllLayDownEffectOnEnemy; //наложен ли эффект от дебаффа "Всем лежать!"
    public int AllLayDownDuration;
    public bool DischargeEffectOnEnemy; //наложен ли эффект от дебаффа "Разряд"
    public int DischargeDuration;
    public bool SilenceEffectOnEnemy; //наложен ли эффект от дебаффа "Тишина"
    public int SilenceDuration;
    public List<int> BleedEffectOnEnemy = new List<int>(); //все эффекты от скила игрока "Кровотечение". Содержит к-ство ходов до окончания эффекта
    public bool PoisonEffectOnEnemy; //наложен ли эффект от дебаффа "Яд"


    //дебаффы от свитков игрока
    public bool EnemyIsSleeping; //спит ли монстр от свитка
    public int SleepTurns; //сколько ходов ему спать монстр

    public List<int> enemySkills = new List<int>(); //лист для скилов Монстра

    //активные скилы монстров
    public bool Agility; //действие скила "Ловкость"
    public int AgilityCd = -1; //счётчик кд
    public int AgilityDuration; //длительность скила
    //public   bool eAgilityIsUsing; //после использования нужна задержка для кд скила на 1 ход

    public bool Sleepiness; //действие скила "Сонливость"
    // int eSleepinessDuration; //сколько спит игрок

    public bool Plague; //действие скила "Чума"

    public bool WallEye; //действие скила "Бельмо"
    public int WallEyeCd = -1; //счётчик кд
    public int WallEyeDuration; //длительность скила

    public bool DarkShot; //действие скила "Тёмный укол"

    public bool Weakness; //действие скила "Слабость"

    public bool Stun; //действие скила "Ошеломление"
    public int StunCd = -1; //счётчик кд
    public int StunDuration; //длительность скила

    public bool PowerfulHit; //действие скила "Мощный удар"

    public bool AccurateHit; //действие скила "Точный удар"

    public bool GallPoison; //действие скила "Жёлчный яд"

    //public bool Naked; //действие скила "Нагота"(был "Голый")


    public bool SandInTheEyes; //действие скила "Песок в глаза"
    public int SandInTheEyesDuration; //длительность скила

    public bool BloodyMassacre; //действие скила "Кровавая расправа"
    public int MassacreBonus = 20; //бонус скила "Кровавая расправа"

    public bool Paralysis; //действие скила "Паралич"
    public int ParalysisDuration; //длительность скила

    public bool Cannibalism; //действие скила "Людоедство"

    public bool Zombie; //действие скила "Зомби"
    public int ZombieDuration; //длительность скила

    public bool Darkness; //действие скила "Тьма
    public int DarknessDuration; //длительность скила

    public bool HeavensPunishment; //действие скила "Кара небесная"

    public bool Recovery; //действие скила "Оздоровление"

    public bool Terror; //действие скила "Ужас"
    public int TerrorDuration; //длительность скила

    public bool Chiropractor; //действие скила "Костоправ"

    public bool OnyxShell; //действие скила "Ониксовый панцирь"
    public int OnyxShellCd = -1; //действие скила "Ониксовый панцирь"

    public bool DaggerInTeeth; //действие скила "Кинжал в зубы"
    public int DaggerInTeethDuration; //длительность скила
    public int DaggerInTeethCd = -1; //счётчик кд

    //пассивки монстров
    public bool Kick; //действие скила "Пинок"

    public bool Motivation; //действие скила "Мотивация"
    public bool MotivationEffect; //прок скила "Мотивация". Отдельная переменная для того, чтобы игнорировать Шанс ранения при расчёте урона для "Мотивации"

    //public bool Poison; //действие скила "Яд"
    public int PoisonBonus = 0; //урон для яда

    public bool PoisonImmunity; //действие скила "Иммунитет к яду"

    public bool TheFlowOfLife; //действие скила "Поток жизни"
    //public   bool eStunCd; //пошёл ли кд скила
    public int TheFlowOfLifeCd = -1; //счётчик кд
    public int TheFlowOfLifeCounter; //счётчик для срабатывания скила. Будет взят из строки "duration" в БД

    public bool Trick; //действие скила "Обман"

    public bool Toxic; //действие скила "Отрава"

    public bool Bloodiness; //действие скила "Кровожадность"


    public bool SpellImunity; //действие скила "Иммунитет к заклинаниям"

    public bool Devourer; //действие скила "Пожиратель"
    public int DevourerBonus; //бонус к атаке от скила

    public bool Scrapper; //действие скила "Драчун"

    public bool Regeneration; //действие скила "Регенерация"

    public bool NervousBreakdown; //действие скила "Нервный срыв"
    public int NervousBreakdownBonus; //бонус урона от скила

    public bool Parry; //действие скила "Парирование"
    public bool ParryEffect; //прок скила "Парирование". Отдельная переменная для того, чтобы игнорировать Шанс ранения при расчёте урона для "Парирования"

    public bool SoulEater;//действие скила "Пожиратель душ"

    public bool SkinProtection;//действие скила "Кожная защита"

    public bool Reincarnation; //действие скила "Реинкарнация"

    public bool Vampirism; //действие скила "Вампиризм"


    public NpcMonster Clone()
    {
        return (NpcMonster)MemberwiseClone();
    }
    object ICloneable.Clone()
    {
        return Clone();
    }


    public void EnemySkillEffects(int skillNumber)
    {
        EnemyHit = false;
        switch (skillNumber)
        {
            //активные
            case 1://"Ловкость":
                _logText.text += Name + " активирует скил \"Ловкость\". Уворот Монстра увелчивается на 3 хода.\n\r";
                enemySkills.Remove(skillNumber);
                Agility = true;
                //eAgilityIsUsing = true;
                AgilityCd = 3;
                AgilityDuration = 3;
                break;
            case 2://"Сонливость":
                _logText.text += Name + " активирует скил \"Сонливость\". ";
                if (!PlayerStats.PoisonEffectOnHero)
                {
                    _logText.text += "Герой уснул на 1 ход.\n\r";
                    GameObject.Find("Buttons").GetComponent<ButtonsScript>().WaitButtonAppears();
                    PlayerStats.SleepinessEffectOnHero = true;
                }
                else
                {
                    _logText.text += "На Герое есть яд. Герой проснулся.\n\r\n\r";

                    _logText.text += "Яд медленно разъедает всё внутри. ХП Героя было(" + PlayerStats.CurrentHp + ") - урон от яда(" +
                                    PoisonBonus + ") = ";
                    PlayerStats.CurrentHp -= PoisonBonus;
                    if (PlayerStats.CurrentHp < 0)
                        PlayerStats.CurrentHp = 0;
                    _logText.text += PlayerStats.CurrentHp + ".\n\r";
                    if (PlayerStats.CurrentHp == 0)
                    {
                        _logText.text += "Герой не выдержал тяжёлой участи и умер от яда.\n\r";
                    }
                }
                break;
            case 3://"Чума":
                _logText.text += Name + " активирует скил \"Чума\".\n\r";
                Plague = true;
                break;
            case 4: //"Бельмо":
                enemySkills.Remove(skillNumber);
                _logText.text += Name + " активирует скил \"Бельмо\". Снижает точность Героя на 3 хода.\n\r";
                WallEye = true;
                WallEyeCd = 3;
                WallEyeDuration = 3;
                break;
            case 5://"Тёмный укол":
                _logText.text += Name + " активирует скил \"Тёмный укол\".\n\r";
                DarkShot = true;
                break;
            case 6: //"Бессилие":
                _logText.text += Name + " активирует скил \"Бессилие\".\n\r";
                Weakness = true;
                break;
            case 7: //"Оглушение":
                enemySkills.Remove(skillNumber);
                _logText.text += Name + " активирует скил \"Оглушение\". Игрок оглушён на 2 хода. \n\r";
                GameObject.Find("Buttons").GetComponent<ButtonsScript>().WaitButtonAppears();
                PlayerStats.StunEffectOnHero = true;
                StunCd = 2;
                PlayerStats.StunDuration = 2;
                break;
            case 8: //"Мощный удар":
                _logText.text += Name + " активирует скил \"Мощный удар\".\n\r";
                PowerfulHit = true;
                break;
            case 9: //"Желчный яд":
                _logText.text += Name + " активирует скил \"Желчный яд\".\n\r";
                GallPoison = true;
                break;
            case 10: //"Нагота":
                _logText.text += Name + " активирует скил \"Нагота\".\n\r";
                if (PlayerEquipmentStats.LeftShield)
                {
                    PlayerStats.NakedEffectOnHero = true;
                    PlayerEquipmentStats.LeftShieldBonus = 0;
                    PlayerStats.NakedDuration = 3;
                    _logText.text += "На " + PlayerStats.NakedDuration + " хода Герой не может использовать щит.\n\r";
                }
                else
                    _logText.text += "На Герое нет щита. Эффект бессмысленный и рассеивается.\n\r";
                break;
            case 11: //"Песок в глаза":
                _logText.text += Name + " активирует скил \"Песок в глаза\". Снижает точность Героя на 3 хода.\n\r";
                SandInTheEyes = true;
                SandInTheEyesDuration = 3;
                break;
            case 12: //"Кровавая расправа":
                _logText.text += Name + " активирует скил \"Кровавая расправа\".\n\r";
                BloodyMassacre = true;
                enemySkills.Remove(skillNumber);
                break;
            case 13: //"Паралич":
                _logText.text += Name + " активирует скил \"Паралич\" на 2 хода.\n\r";
                Paralysis = true;
                GameObject.Find("Attack Button").GetComponent<Button>().enabled = false;
                ParalysisDuration = 2;
                break;
            case 14://"Людоедство":
                _logText.text += Name + " активирует скил \"Людоедство\".\n\r";
                Cannibalism = true;
                break;
            case 15: //"Зомби":
                _logText.text += Name + " активирует скил \"Зомби\".\n\r";
                break;
            case 16: //"Тьма":
                _logText.text += Name + " активирует скил \"Тьма\". ХП было " + PlayerStats.CurrentHp + ", защиты было " + PlayerStats.PhysArmor + ". ";
                Darkness = true;
                PlayerStats.DebuffThePlayer(this);
                _logText.text += "ХП теперь " + PlayerStats.CurrentHp + ", защиты теперь " + PlayerStats.PhysArmor + ". Сила теперь " +
                    PlayerStats.Strength + ", Ловкость " + PlayerStats.Agility + ", Телосложение " + PlayerStats.Constitution + ".\n\r";
                DarknessDuration = 3;
                break;
            case 17://"Кара небесная":
                _logText.text += Name + " активирует скил \"Кара небесная\".\n\r";
                HeavensPunishment = true;
                break;
            case 18://"Оздоровление":
                CurrentHp = MaxHp;
                _logText.text += "\n\r" + Name + " восполнил себе полностью ХП \"Оздоровлением\". Теперь у него " + CurrentHp + " хп.\n\r";
                break;
            case 27://"Точный удар":
                _logText.text += Name + " активирует скил \"Точный удар\".\n\r";
                AccurateHit = true;
                break;
            case 34://"Ужас":
                //не сделан уход из боя
                PlayerStats.TerrorEffectOnHero = true;
                TerrorDuration = 2;
                _logText.text += Name + " активирует скил \"Ужас\". Игрок испуган на 2 хода. \n\r";
                GameObject.Find("Buttons").GetComponent<ButtonsScript>().WaitButtonAppears();
                break;
            case 35://"Костоправ":
                _logText.text += Name + " активирует скил \"Костоправ\". На Героя вешается кровотечение и травма.\n\r";
                Chiropractor = true;
                PlayerStats.BloodinessEffectOnHero.Add(3);
                BattleMechanics.WoundChance(this, 2, 10);
                break;
            case 36://"Ониксовый панцырь":
                _logText.text += Name + " активирует скил \"Ониксовый панцырь\" и прибавляет +200 к текущей броне.\n\r";
                PhysArmorPercent += 200;
                OnyxShellCd = 6;
                enemySkills.Remove(skillNumber);
                break;
            case 37://"Кинжал в зубы":
                _logText.text += Name + " активирует скил \"Кинжал в зубы\".\n\r";
                DaggerInTeeth = true;
                DaggerInTeethDuration = 3;
                DaggerInTeethCd = 7;
                break;


            //пассивки
            case 19://"Пинок":
                //_logText.text += "У Монстра теперь активна пассивка \"Пинок\".\n\r";
                Kick = true;
                break;
            case 20://"Мотивация":
                //_logText.text += "У Монстра теперь активна пассивка \"Мотивация\".\n\r";
                Motivation = true;
                break;
            case 21://"Яд":
                //_logText.text += "У Монстра теперь активна пассивка \"Яд\".\n\r";
                PoisonBonus = 2;
                break;
            case 22://"Иммунитет к яду":
                //_logText.text += "У Монстра теперь активна пассивка \"Иммунитет к яду\".\n\r";
                PoisonImmunity = true;
                break;
            case 23://"Поток жизни":
                //if (eTheFlowOfLifeCd > 0)
                //{
                //    _logText.text += "скил \"Поток жизни\" ещё в КД.\n\r";
                //    return;
                //}
                //_logText.text += "У Монстра теперь активна пассивка \"Поток жизни\".\n\r";
                TheFlowOfLife = true;
                TheFlowOfLifeCounter = 3;
                break;
            case 24://"Обман":
                //_logText.text += "У Монстра теперь активна пассивка \"Обман\".\n\r";
                Trick = true;
                break;
            case 25://"Отрава":
                //_logText.text += "У Монстра теперь активна пассивка \"Отрава\".\n\r";
                Toxic = true;
                break;
            case 26://"Кровожадность":
                //_logText.text += "У Монстра теперь активна пассивка \"Кровожадность\".\n\r";
                Bloodiness = true;
                break;
            case 28://"Иммунитет к заклинаниям":
                //_logText.text += "У Монстра теперь активна пассивка \"Иммунитет к заклинаниям\".\n\r";
                SpellImunity = true;
                break;
            case 29://"Пожиратель":
                //_logText.text += "У Монстра теперь активна пассивка \"Пожиратель\".\n\r";
                Devourer = true;
                break;
            case 30://"Драчун":
                //_logText.text += "У Монстра теперь активна пассивка \"Драчун\".\n\r";
                Scrapper = true;
                break;
            case 31://"Регенерация":
                //_logText.text += "У Монстра теперь активна пассивка \"Регенерация\".\n\r";
                Regeneration = true;
                break;
            case 32://"Нервный срыв":
                //_logText.text += "У Монстра теперь активна пассивка \"Нервный срыв\".\n\r";
                NervousBreakdown = true;
                break;
            case 33://"Парирование":
                //_logText.text += "У Монстра теперь активна пассивка \"Парирование\".\n\r";
                Parry = true;
                break;
            case 38://"Пожиратель душ":
                //_logText.text += "У Монстра теперь активна пассивка \"Пожиратель душ\".\n\r";
                SoulEater = true;
                break;
            case 39://"Кожная защита":
                //_logText.text += "У Монстра теперь активна пассивка \"Кожная защита\".\n\r";
                SkinProtection = true;
                break;
            case 40://"Реинкарнация":
                //_logText.text += "У Монстра теперь активна пассивка \"Реинкарнация\".\n\r";
                Reincarnation = true;
                break;
            case 41://"Вампиризм":
                //_logText.text += "У Монстра теперь активна пассивка \"Вампиризм\".\n\r";
                Vampirism = true;
                break;
            //максимальное число для скилов: 41
        }
        switch (skillNumber)
        {
            case 5:
            case 8:
            case 13:
            case 14:
            case 17:
                EnemyHit = true;
                break;
        }
        if (skillNumber >= 19 && skillNumber <= 34 || skillNumber >= 38 && skillNumber <= 41)
            EnemyHit = true;
    }

    public void AddActiveOrPassiveSkill(int skillId)
    {
        if (skillId >= 19 && skillId <= 33 || skillId >= 38 && skillId <= 41)
        {
            NpcSkillData newSkillData = new NpcSkillData { ability_id = skillId };
            SkillsData.Add(newSkillData);
            EnemySkillEffects(skillId); //активируем пассивку
        }
        else
        {
            enemySkills.Add(skillId);
        }
    }

    public void CheckForEnemySkills() //рандомизатор для активирования скила врага
    {
        if (SilenceEffectOnEnemy)
        {
            _logText.text += "\n\rСкил Героя \"Тишина\" не даёт Монстру использовать способности Монстру.\n\r";
            PlayerStats.PSilence = false; //выключает триггер (если не выключить, то игрок будет кидать Тишину с каждого удара
        }
        else
        {
            if (enemySkills.Count > 0)
            {
                int rndmUseSkillChance = Random.Range(1, 6); //будет ли использован скил
                rndmUseSkillChance = 5;
                _logText.text += "\n\rУ монстра есть скилы. Шанс 1d5, что он их использует. Выпадает " +
                                 rndmUseSkillChance + ". ";
                if (rndmUseSkillChance == 5)
                {
                    if (PlayerStats.PRage)
                    {
                        _logText.text += "У Героя имеется пассивка \"Ярость\". Так, как у Монстра сработала активная способность, Герой атакует " +
                             "с бонусом к урону и пробития перед применением способности Монстра.\n\r";
                        //PlayerStats.PlayerPhysAttack(this);
                    }
                    int rndmSkill = Random.Range(1, enemySkills.Count + 1);
                    _logText.text += "Активируется рандомный активный скил Монстра. Всего скилов " + enemySkills.Count +
                                     ", а использоваться будет скил №" + rndmSkill +
                                     " из списка скилов, с идентификатором " + enemySkills[rndmSkill - 1] + ".\n\r";
                    EnemySkillEffects(enemySkills[rndmSkill - 1]);
                }
                else
                    _logText.text += "Монстру не повезло.\n\r";
            }
        }
        //enemySkills.Clear();
    }


    public void EnemyReincarnation()
    {
        //вставить метод, который будет брать данные из БД монстров 

        //NpcMonster.CurrentHp = NpcMonster.MaxHp;
        SetDefaultEnemyState();
        Reincarnation = false;
        _logText.text += "У Монстра активна пассивка \"Реинкарнация\". Монстр возраждается, ХП Монстра снова полное и составляет " +
                        CurrentHp + ".\n\r";
    }


    public void SetDefaultEnemyState() //ставим дефолтное состояние для Монстра после скилла "Реинкарнация"
    {
        BleedEffectOnEnemy.Clear();
        DebuffCount = 0;
        DebuffAcc = 0f;
        DebuffEva = 0f;
        DebuffDmg = 0f;
        if (!Reincarnation) //не помню, для чего это
            enemySkills.Clear();
        PlayerStats.Poison = false;
        ArmsOutEffectOnEnemy = false;
    }


    public void EnemySkillsCds()
    {
        if (AgilityCd >= 0)
        {
            _logText.text += "\n\r" + Name + ": Откат скила \"Ловкость\" " + AgilityCd + " хода.\n\r";
            if (AgilityCd == 0)
            {
                _logText.text += Name + ": Теперь Монстр может использовать скил.\n\r";
                int skillNumber = 1;
                enemySkills.Add(skillNumber);
            }
            AgilityCd--;
        }

        if (WallEyeCd >= 0)
        {
            _logText.text += "\n\r" + Name + ": Откат скила \"Бельмо\" " + WallEyeCd + " хода.\n\r";
            if (WallEyeCd == 0)
            {
                _logText.text += Name + ": Теперь Монстр может использовать скил.\n\r";
                int skillNumber = 4;
                enemySkills.Add(skillNumber);
            }
            WallEyeCd--;
        }

        if (StunCd >= 0)
        {
            _logText.text += "\n\r" + Name + ": Откат скила \"Оглушение\" " + StunCd + " хода.\n\r";
            if (StunCd == 0)
            {
                _logText.text += Name + ": Теперь Монстр может использовать скил.\n\r";
                int skillNumber = 7;
                enemySkills.Add(skillNumber);
            }
            StunCd--;
        }

        if (TheFlowOfLifeCd >= 0)
        {
            _logText.text += "\n\r" + Name + ": Откат скила \"Поток жизни\" " + TheFlowOfLifeCd + " хода.\n\r";
            if (TheFlowOfLifeCd == 0)
            {
                _logText.text += Name + ": Монстр активирует пассивный скил повторно.\n\r";
                TheFlowOfLifeCounter = 3;
            }
            TheFlowOfLifeCd--;
        }

        if (OnyxShellCd >= 0)
        {
            _logText.text += "\n\r" + Name + ": Откат скила \"Ониксовый панцырь\" " + OnyxShellCd + " хода.\n\r";
            if (OnyxShellCd == 0)
            {
                _logText.text += Name + ": Теперь Монстр может использовать скил.\n\r";
                int skillNumber = 36;
                enemySkills.Add(skillNumber);
            }
            OnyxShellCd--;
        }

        if (DaggerInTeethCd >= 0)
        {
            _logText.text += "\n\r" + Name + ": Откат скила \"Кинжал в зубы\" " + DaggerInTeethCd + " хода.\n\r";
            if (DaggerInTeethCd == 0)
            {
                _logText.text += Name + ": Теперь Монстр может использовать скил.\n\r";
                int skillNumber = 37;
                enemySkills.Add(skillNumber);
            }
            DaggerInTeethCd--;
        }
    }


    public void EnemyBuffsDuration() //декрементация счётчиков баффов на Монстре
    {
        if (Agility)
        {
            AgilityDuration--;
            if (AgilityDuration == 0)
            {
                Agility = false;
                _logText.text += Name + ": Эффект \"Ловкость\" у Монстра испарился.\n\r";
            }
            else
                _logText.text += Name + ": \"Ловкость\" у Монстра ещё будет действовать " + AgilityDuration +
                                 " ходов.\n\r";
        }

        if (Zombie)
        {
            ZombieDuration--;
            if (ZombieDuration == 0)
            {
                //EnemySkillZombieDied();
                _logText.text += Name + ": Эффект \"Зомби\" испарился.\n\r";
            }
            else
                _logText.text += Name + ": \"Зомби\" ещё будет действовать " + ZombieDuration + " ходов.\n\r";
        }

        if (DaggerInTeeth)
        {
            DaggerInTeethDuration--;
            if (DaggerInTeethDuration == 0)
            {
                DaggerInTeeth = false;
                _logText.text += Name + ": Эффект \"Кинжал в зубы\" испарился.\n\r";
            }
            else
                _logText.text += Name + ": \"Кинжал в зубы\" у Монстра ещё будет действовать " + DaggerInTeethDuration +
                                 " ходов.\n\r";
        }
    }



    public void EnemySkillPreparation() //подсчёт ходов сколько скилу осталось до использования
    {
        if (TheFlowOfLife && TheFlowOfLifeCd == -1)
        {
            TheFlowOfLifeCounter--;
            _logText.text += "\n\r" + Name + ": У Монстра активна пассивка \"Поток жизни\", через " + TheFlowOfLifeCounter +
                             " ходов ХП Монстра увеличится вдвое.\n\r";
            if (TheFlowOfLifeCounter == 0)
            {
                _logText.text += "ХП Монстра было " + CurrentHp + ", максимальное ХП " + MaxHp +
                                 ". Теперь ХП Монстра ";
                CurrentHp *= 2;
                MaxHp *= 2;
                _logText.text += CurrentHp + ", максимальное ХП стало " + MaxHp + ".\n\r";
                TheFlowOfLifeCd = 7;
            }
        }

        if (Terror)
        //вместо отдельного метода для этого скила, он тут, потому что все КД Монстра до юза скила Монстра, а этому скилу нужно после использования
        {
            TerrorDuration--;
            if (TerrorDuration == 0)
            {
                Terror = false;
                _logText.text += Name + ": Эффект \"Ужас\" испарился.\n\r";
            }
            else
                _logText.text += "\"Ужас\" на Герое ещё будет действовать " + TerrorDuration + " ходов.\n\r";
        }
    }


    void PhysOrMageMonsterAttack()//проверка на вид атаки монстра
    {
        if (PhysMinDmg > 0)
            MonsterPhysAttack();
        else
            MonsterMagAttack();
        if (PlayerStats.CurrentHp == 0 || CurrentHp == 0)
        {
            BattleMechanics.WhoIsLoosing(this);
        }
    }


    public void MonsterDmgPhase() //фаза атаки монстра
    {
        if (SoulEater) //"Пожиратель душ"
        {
            float currentHpToPercent = (float)CurrentHp / MaxHp; //конвертация текущего хп в проценты
            float entryTreshold = 0.3f; //порог срабатывания скила
            if (currentHpToPercent <= entryTreshold)
            {
                _logText.text +=
                    "У Монстра имеется пассивка \"Пожиратель душ\". Сейчас у Монстра меньше 30% ХП и есть шанс 1d4, что он поменяется ХП с Героем. ";
                int soulEaterRnmd = Random.Range(1, 5);
                if (soulEaterRnmd == 4)
                {
                    //Если что, выключить скил тут
                    _logText.text += "На кубике выпало 4! ХП Монстра(" + CurrentHp +
                                    ") меняется с ХП Героя(" + PlayerStats.CurrentHp + ").\n\r";
                    int tempNpcHp = CurrentHp;
                    CurrentHp = PlayerStats.CurrentHp;
                    PlayerStats.CurrentHp = tempNpcHp;
                    SoulEater = false;
                }
                else
                {
                    _logText.text += "На кубике выпало " + soulEaterRnmd +
                                    ". Это не то, что нужно сейчас Монстру.\n\r";
                }
            }
        }
        if (PlayerStats.PMirror)
        {
            int mirrorChance = Random.Range(1, 6);
            _logText.text += "\n\rУ Героя активна пассивка \"Зеркало\", шанс срабатывание 1d5. На кубике выпало " + mirrorChance + ". ";
            if (mirrorChance == 5)
            {
                _logText.text += "\"Зеркало\" сработал! Герой отражает удар Монстра с 50% уроном.\n\r\n\r";
                //высчитывание урона Монстра
                int fakeNpcDmg = CalculateMonsterPhysDamage();
                fakeNpcDmg /= 2;
                _logText.text += "ХП Монстра(" + CurrentHp + ") - 50% урона от Монстра(" + fakeNpcDmg + ") = ";
                CurrentHp -= fakeNpcDmg;
                if (CurrentHp < 0)
                    CurrentHp = 0;
                _logText.text += CurrentHp + ".\n\r";
                if (CurrentHp == 0)
                {
                    BattleMechanics.WhoIsLoosing(this);
                    return;
                }
            }
            else
            {
                _logText.text += "Эффект \"Зеркало\" не прошёл.\n\r";
            }
        }
        else
        {
            PhysOrMageMonsterAttack();
        }
        if (BattleMechanics.GameOver)
            return;

        if (Trick)
        {
            int trickChance = Random.Range(1, 6);
            _logText.text += "\n\rУ Монстра активна пассивка \"Обман\", шанс срабатывание 1d5. На кубике выпало " + trickChance + ". ";
            if (trickChance == 5)
            {
                _logText.text += "\"Обман\" сработал! Монстр бьёт дважды.\n\r\n\rАтака №1\n\r";
                PhysOrMageMonsterAttack();
                if (PlayerStats.CurrentHp == 0)
                {
                    BattleMechanics.WhoIsLoosing(this);
                    return;
                }
                _logText.text += "\n\rАтака №2\n\r";
            }
            else
            {
                _logText.text += "Эффект \"Обман\" не прошёл.\n\r";
            }
        }
    }


    public void PlayerDebuffsDurationOnEnemy() //декрементация счётчиков дебаффов на Монстре
    {
        if (PoisonEffectOnEnemy) //если яд на монстре
        {
            if (EnemyIsSleeping) //если спит, будим
            {
                EnemyIsSleeping = false;
                _logText.text += Name + " проснулся от яда. Кто-то тут просчитался!\n\r\n\r";
            }
            _logText.text += "Яд медленно разъедает всё внутри. ХП Монстра было(" + CurrentHp + ") - урон от яда(" +
                            PlayerStats.PoisonBonus + ") = ";
            CurrentHp -= PlayerStats.PoisonBonus;
            if (CurrentHp < 0)
            {
                CurrentHp = 0;
            }
            if (CurrentHp == 0 && Reincarnation)
            {
                if (Reincarnation)
                {
                    EnemyReincarnation();
                }
                return;
            }
            _logText.text += CurrentHp + ".\n\r";
            if (CurrentHp == 0)
            {
                _logText.text += Name + " не выдержал тяжёлой участи и умер от яда.\n\r";
                return;
            }
        }

        for (int i = 0; i < BleedEffectOnEnemy.Count; i++) //отнимание ходов на стаках кровотечения
        {
            BleedEffectOnEnemy.Remove(0);
        }

        for (int i = 0; i < BleedEffectOnEnemy.Count; i++) //урон от стаков кровотечения
        {
            CurrentHp -= 2;
            if (CurrentHp < 0)
                CurrentHp = 0;
            if (CurrentHp == 0 && Reincarnation)
            {

                if (Reincarnation)
                {
                    EnemyReincarnation();
                    return;
                }
            }
            if (CurrentHp == 0)
            {
                _logText.text += Name + " не выдержал тяжёлой участи и умер от кровотечения.\n\r";
                return;
            }
            BleedEffectOnEnemy[i]--;

            _logText.text += "Стак №" + (i + 1) + ". -2 хп от кровотечения. Теперь у Монстра " + CurrentHp +
                " ХП. До конца стака осталось " + BleedEffectOnEnemy[i] + " ходов";
            if (BleedEffectOnEnemy[i] == 0)
            {
                _logText.text += ". Стак уже не будет работать в следующем ходу";
            }
            _logText.text += ".\n\r";
        }

        if (ArmsOutEffectOnEnemy)
        {
            ArmsOutDuration--;
            if (ArmsOutDuration == 0)
            {
                ArmsOutEffectOnEnemy = false;
                _logText.text += "Эффект \"Оружие вон\" испарился в этом ходу.\n\r";
            }
            else
                _logText.text += "\"Оружие вон\" ещё будет действовать " + ArmsOutDuration + " ходов.\n\r";
        }

        if (KnightHookEffectOnEnemy)
        {
            KnightHookDuration--;
            if (KnightHookDuration == 0)
            {
                KnightHookEffectOnEnemy = false;
                _logText.text += "Эффект \"Рыцарский хук\" испарился в этом ходу.\n\r";
            }
            else
                _logText.text += "\"Рыцарский хук\" ещё будет действовать " + KnightHookDuration + " ходов.\n\r";
        }

        if (AllLayDownEffectOnEnemy)
        {
            AllLayDownDuration--;
            if (AllLayDownDuration == 0)
            {
                AllLayDownEffectOnEnemy = false;
                _logText.text += "Эффект \"Всем лежать!\" испарился в этом ходу.\n\r";
            }
            else
                _logText.text += "\"Всем лежать!\" ещё будет действовать " + AllLayDownDuration + " ходов.\n\r";
        }

        if (DischargeEffectOnEnemy)
        {
            DischargeDuration--;
            if (DischargeDuration == 0)
            {
                DischargeEffectOnEnemy = false;
                _logText.text += "Эффект \"Разряд\" испарился в этом ходу.\n\r";
            }
            else
                _logText.text += "\"Разряд\" ещё будет действовать " + DischargeDuration + " ходов.\n\r";
        }

        if (SilenceEffectOnEnemy)
        {
            SilenceDuration--;
            if (SilenceDuration == 0)
            {
                SilenceEffectOnEnemy = false;
                _logText.text += "Эффект \"Тишина\" испарился в этом ходу.\n\r";
            }
            else
                _logText.text += "\"Тишина\" ещё будет действовать " + SilenceDuration + " ходов.\n\r";
        }


    }


    public void MonsterPhysAttack() //физическая атака монстра
    {
        //int enemyAccuracy = Random.Range(1, 11); //вычисляем точность, кидаем кубик от 1 до 10
        _logText.text += "\n\r" + Name + " атакует физической атакой!\n\r";

        int npcAccuracyRes = AccuracyPercent - PlayerStats.EvasionPercent;
        if (npcAccuracyRes < 0)
            npcAccuracyRes = 0;
        _logText.text += "2. Точность Монстра: " + AccuracyPercent + "%. Уворот Героя: " + PlayerStats.EvasionPercent + "%. Шанс попадания по Герою: " + npcAccuracyRes + "%. ";
        //                 "2. Шанс попадания монстра.\n\r а)Кидаем кубик от 1 до 10 (выпадает " + enemyAccuracy + ") ";
        //enemyAccuracy += AccuracyPercent;
        //_logText.text += "+ бонус от статов(" + AccuracyPercent + ") = " + enemyAccuracy;
        //if (DebuffAcc > 0)
        //{
        //    int debuffValue = Mathf.RoundToInt(enemyAccuracy * DebuffAcc);
        //    enemyAccuracy -= debuffValue;
        //    if (Scrapper)
        //        _logText.text += " + " + debuffValue + "(30% точности от \"Драчуна\") = " + enemyAccuracy;
        //    else
        //        _logText.text += " - " + debuffValue + "(30% точности от сотрясения головы) = " + enemyAccuracy;
        //}
        //_logText.text += ".\n\r";

        //int playerEvasion = Random.Range(1, 6);
        //_logText.text += " б)Уворот героя = кубик от 1 до 5 (выпадает " + playerEvasion;
        //playerEvasion += PlayerStats.EvasionPercent;
        //_logText.text += ") + бонус уворота(" + PlayerStats.EvasionPercent + ") = " + playerEvasion;
        //if (PlayerStats.DebuffEvaPercent > 0)
        //{
        //    int debuffValue = Mathf.RoundToInt(playerEvasion * PlayerStats.DebuffEvaPercent);
        //    playerEvasion -= debuffValue;
        //    _logText.text += " - " + debuffValue + "(30% уворота от ран в ногах) = " + playerEvasion;
        //}
        //_logText.text += ".\n\r";
        bool hit = BattleMechanics.ChanceCalculation(npcAccuracyRes); ; //попали в монстра или нет

        if (hit) //монстр попадает
        {
            if (PlayerStats.PDueler)
            {
                int parryChance = Random.Range(1, 6);
                _logText.text += "Активна пассивка \"Дуэлянт\". Шанс парирования атаки 1 из 5. На кубике выпало " + parryChance + ". ";
                if (parryChance == 5)
                {
                    _logText.text += "Герой парирует удар и контратакует.\n\r";
                    //PlayerStats.PlayerPhysAttack(this);
                    return;
                }
                else
                    _logText.text += "Парирование не прошло.\n\r";
            }



            if (Kick)
            {
                if (PlayerStats.DebuffCount >= 3)
                {
                    _logText.text += "Скил \"Пинок\" не прошёл. Количетсво дебаффов не может быть больше 3.\n\r";
                }
                else
                {
                    int rndm = Random.Range(1, 5);
                    _logText.text +=
                        "У Монстра активна пассивка \"Пинок\", который может выбить оружие из правой руки. Шанс срабатывания 1d4. На кубике выпало " +
                        rndm + ". ";
                    if (rndm == 4)
                    {
                        PlayerStats.KickEffectOnHero = true;
                        _logText.text += "Эффект \"Пинка\" активен. ";
                        PlayerStats.DebuffCount++;
                        _logText.text += "Теперь у Героя " + PlayerStats.DebuffCount + "дебаффа.\n\r";
                    }
                    else
                        _logText.text += "Эффект \"Пинка\" не прошёл.\n\r";
                }
            }
            //_logText.text += "Точность Монстра(" + enemyAccuracy + ") >= уворота Героя(" + playerEvasion + "). Монстр попадает! Чтоб он провалился!\n\r";
            _logText.text += " Попадание.\n\r";
            int npcDmg = CalculateMonsterPhysDamage();

            if (NervousBreakdown && NervousBreakdownBonus > 0)
            {
                npcDmg += NervousBreakdownBonus;
                _logText.text += "Пассивка \"Нервный срыв\" даёт бонус к урону Монстру " + NervousBreakdownBonus +
                                " = " + npcDmg + ".\n\r";
            }

            if (ArmsOutEffectOnEnemy)
            {
                npcDmg = Mathf.RoundToInt(npcDmg * 0.5f);
                _logText.text += "На Монстре есть дебафф от \"Оружие вон\". -75% к урону = " + npcDmg + ".\n\r";
            }

            if (DebuffDmg > 0)
            {
                int debuffValue = Mathf.RoundToInt(npcDmg * DebuffDmg);
                npcDmg -= debuffValue;
                if (Scrapper)
                    _logText.text += "Пассивка \"Драчун\". Бонус: + " + debuffValue + "(30% от урона) = " + npcDmg + ".\n\r";
                else
                    _logText.text += "Лапы или руки повреждены. Штраф: - " + debuffValue + "(30% от урона) = " + npcDmg + ".\n\r";
            }

            if (DarkShot)
            {
                npcDmg = Mathf.RoundToInt(npcDmg * 1.5f);
                _logText.text += "скил \"Тёмный укол\" даёт бонус 50% урона. Урон теперь = " + npcDmg + ".\n\r";
                DarkShot = false;
            }

            if (PowerfulHit)
            {
                npcDmg = Mathf.RoundToInt(npcDmg * 1.5f);
                _logText.text += "скил \"Мощный удар\" даёт бонус 50% урона. Урон теперь = " + npcDmg + ".\n\r";
                PowerfulHit = false;
            }

            if (HeavensPunishment)
            {
                npcDmg *= 2;
                _logText.text += "скил \"Мощный удар\" даёт бонус 100% урона. Урон теперь = " + npcDmg + ".\n\r";
                HeavensPunishment = false;
            }

            if (BloodyMassacre)
            {
                int dmgBonus = Mathf.RoundToInt(npcDmg * (MassacreBonus / 100f + 1f));
                npcDmg += dmgBonus;
                _logText.text += "скил \"Кровавая расправа\" даёт прибавку к урону Монстра, которая каждый ход увеличивается на 20%. Сейчас прибавка = " +
                                MassacreBonus + "%, что составляет " + dmgBonus + ". Урон Монстра теперь " + npcDmg + ".\n\r";
                MassacreBonus += 20;
            }

            if (Devourer)
            {
                npcDmg += DevourerBonus;
                _logText.text += "Пассивка \"Пожиратель\" даёт бонус урона от пробитой в текущем ходу брони. Урон теперь = " + npcDmg + ".\n\r";
            }

            if (DischargeEffectOnEnemy)
            {
                npcDmg -= Mathf.RoundToInt(npcDmg * 0.25f);
                _logText.text += "Скил Героя \"Разряд\" уменьшает урон Монстра на 25%. Теперь урон Монстра " + npcDmg + ".\n\r";
            }

            //int npcPenetration = 1;
            //int heroArmor = PlayerStats.PhysArmor;
            if (AccurateHit) //Точный удар с шансом пробивает броню
            {
                int rndm = Random.Range(1, 6);
                rndm = 5;
                _logText.text +=
                    "У Монстра активна пассивка \"Точный удар\", шанс срабатывание 1d5. На кубике выпало " + rndm + ". ";
                if (rndm == 5)
                {
                    //heroArmor = 0;
                    _logText.text += "Броня пробита.\n\r";
                }
                else
                    _logText.text += "скил не прошёл.\n\r";
            }
            else //если нет Точного удара, то идёт вычисление обычного удара
            {
                if (HeavensPunishment || Cannibalism) //если один их этих скилов, то пробивает броню сразу
                {
                    //heroArmor = 0;
                    if (HeavensPunishment)
                        _logText.text += "скил \"Кара небесная\" пробивает полностью броню Героя.\n\r";
                    else
                        _logText.text += "скил \"Людоедство\" пробивает полностью броню Героя.\n\r";
                }
                else //если нету скилов, то, как и положено, вычисляем пробитие
                {
                    //heroArmor += PlayerStats.BonusDef;
                    //npcPenetration = Random.Range(1, 9); //рандом вычисляем
                    //_logText.text += "6. Считаем пробитие брони Монстра: кидаем кубик от 1 до 8 (выпадает " +
                    //                 npcPenetration + ")";
                    //npcPenetration = npcPenetration + Lvl + PenetrationPercent;
                    //_logText.text += " + лвл моба(" + Lvl + ") + бонус к пробитию(" + PenetrationPercent +
                    //                 ") = " + npcPenetration + ".\n\r";
                    //if (PlayerStats.PSpikes)
                    //{
                    //    _logText.text +=
                    //        "скил Шипы отнимает ХП основываясь от количества пробитой брони. Здоровье Монстра = " +
                    //        CurrentHp +
                    //        " - ";
                    //    CurrentHp -= npcPenetration;
                    //    if (CurrentHp < 0)
                    //        CurrentHp = 0;
                    //    _logText.text += npcPenetration + " = " + CurrentHp + ". \n\r";
                    //    if (CurrentHp == 0)
                    //    {
                    //        if (Reincarnation)
                    //        {
                    //            EnemyReincarnation();
                    //        }
                    //        else
                    //        {
                    //            BattleMechanics.WhoIsLoosing(this);
                    //        }
                    //        return;
                    //    }
                    //}
                    PlayerStats.BonusDef = 0;
                }
            }
            if (Devourer)
            {
                _logText.text += "У Монстра активна пассивка \"Пожиратель\". В этом ходу мы ";
            }
            int heroArmorResult = 0;
            if (PenetrationPercent >= PlayerStats.PhysArmorPercent)
            //если пробитие больше, чем защита, то идёт чистый урон от оружия
            {
                //if (Devourer)
                //{
                //    _logText.text += "пробиваем броню";
                //    if (npcPenetration > heroArmor)
                //    {
                //        DevourerBonus = npcPenetration - heroArmor;
                //        _logText.text += ". Бонус к урону составляет " + DevourerBonus + ".\n\r";
                //    }
                //    else
                //        _logText.text += ", но бонус равняется 0.\n\r";
                //}
                _logText.text += "6. Пробитие Монстра(" + PenetrationPercent + "%) больше защиты Героя(" + PlayerStats.PhysArmorPercent +
                    "%) По Герою проходит чистый урон.\n\r";
            }
            else
            {
                heroArmorResult = PlayerStats.PhysArmorPercent - PenetrationPercent;
                _logText.text += "6. Защита Героя(" + PlayerStats.PhysArmorPercent + "%) - пробитие Монстра(" + PenetrationPercent + ") = " + heroArmorResult +
                    ".\n\r";
            }


            //if (SkinProtection)
            //{
            //    _logText.text += "У Монстра есть пассивка \"Кожная защита\", +25% Монстру от текущей брони. Броня Монстра была " + NpcMonster.CurrentPhysDef +
            //        ", а после удара стала ";
            //    CurrentPhysDef = Mathf.RoundToInt(CurrentPhysDef * 1.25f);
            //    _logText.text += CurrentPhysDef + ".\n\r";
            //}
            //обрезка урона за счёт брони
            if (heroArmorResult != 0)
            {
                //int currentHundredPercentArmor = PlayerStats.CurrentNumberWhichIsHundredPercent + (int)(2.5f * PlayerStats.CurrentLevelCounter * PlayerStats.CurrentNumberWhichIsHundredPercent);
                int armorToPercent = heroArmorResult / PlayerStats.CurrentNumberWhichIsHundredPercent;
                _logText.text += "100% брони сейчас " + PlayerStats.CurrentNumberWhichIsHundredPercent + ", остаток брони " + heroArmorResult +
                    " = " + armorToPercent * 100 + "% от 100% брони. Урон Монстра(" + npcDmg + ") - " + armorToPercent * 100 + "% = ";
                npcDmg -= Mathf.RoundToInt(npcDmg * armorToPercent);
                _logText.text += npcDmg + ".\n\r";
            }

            _logText.text += "ХП Героя(" + PlayerStats.CurrentHp + ") - урон Монстра(" + npcDmg + ") = ";
            PlayerStats.CurrentHp -= npcDmg;
            if (PlayerStats.CurrentHp <= 0)
            {
                PlayerStats.CurrentHp = 0;
                _logText.text += PlayerStats.CurrentHp + ".\n\r";
                if (PlayerStats.PBerserk)
                {
                    _logText.text += "Активен скил \"Берсерк\". ХП Героя не может быть 0 и теперь равно 1.\n\r";
                    PlayerStats.CurrentHp = 1;
                }
                else
                {
                    BattleMechanics.GameOver = true;
                    return;
                }
            }
            _logText.text += PlayerStats.CurrentHp + ".\n\r";

            if (!PlayerStats.PoisonEffectOnHero && PoisonBonus > 0)
            {
                if (PlayerStats.DebuffCount >= 3)
                {
                    _logText.text += "Скил \"Яд\" не прошёл. Количетсво дебаффов не может быть больше 3.\n\r";
                }
                else
                {
                    int rndmPoison = Random.Range(1, 6);
                    _logText.text += "При успешном пробитии у Монстра есть шанс 1d5 наложить Яд. На кубике выпадает " +
                                     rndmPoison + ".\n\r";
                    if (rndmPoison == 5)
                    {
                        PlayerStats.PoisonEffectOnHero = true;
                        _logText.text +=
                            "\n\rЯд сработал! Ищи противоядие, глупец!\n\rКаждый ход Герою будет наносится " +
                            PoisonBonus + " урона.\n\r\n\r";
                        PlayerStats.DebuffCount++;
                        _logText.text += "Теперь у Героя " + PlayerStats.DebuffCount + " дебаффа.\n\r";
                    }
                    else
                        _logText.text += "Яд оказался плохого качества. Ничего не произошло.\n\r";
                }
            }

            if (Bloodiness)
            {
                if (PlayerStats.DebuffCount >= 3)
                {
                    _logText.text += "Скил \"Кровожадность\" не прошёл. Количетсво дебаффов не может быть больше 3.\n\r";
                }
                else
                {
                    int bleedRndm = Random.Range(1, 5);
                    _logText.text +=
                        "\n\rУ Монстра есть скил \"Кровожадность\", который может оставить кровотечение на Герое с шансом 1d4. На кубике выпало " +
                        bleedRndm + ". ";
                    if (bleedRndm == 4)
                    {
                        PlayerStats.BloodinessEffectOnHero.Add(3);
                        _logText.text += "Наложен эффект кровотечения. -2 хп 3 хода. ";
                        PlayerStats.DebuffCount++;
                        _logText.text += "Теперь у Героя " + PlayerStats.DebuffCount + " дебаффа.\n\r";
                    }
                    else
                        _logText.text += "Кровотечение не прошло.\n\r";
                }
            }

            if (Cannibalism)
            {
                _logText.text += "\"Людоедство\" восполняет ХП от нанесённого урона. ХП Монстра(" + CurrentHp +
                    ") + лечение от пассивки(" + npcDmg + ") = ";
                CurrentHp += npcDmg;
                if (CurrentHp > MaxHp)
                    CurrentHp = MaxHp;
                _logText.text += CurrentHp + " ХП.\n\r";
            }

            if (Vampirism)
            {
                int vampirismHeal = Mathf.RoundToInt(npcDmg * 0.5f);
                _logText.text += "\"Вампиризм\" восполняет ХП от 50% нанесённого урона. ХП Монстра(" + CurrentHp +
                    ") + лечение от пассивки(" + vampirismHeal + ") = ";
                CurrentHp += vampirismHeal;
                if (CurrentHp > MaxHp)
                    CurrentHp = MaxHp;
                _logText.text += CurrentHp + " ХП.\n\r";
            }

            //_logText.text += "\n\r";
        }
        else
        {
            _logText.text += " Фух, пронесло... Монстр промазал.\n\r";
        }


    }

    int CalculateMonsterPhysDamage()
    //отдельный метод для вычисления урона Монстра. Нужен отдельно для геройского скила "Зеркало", 
    //который при срабатывании отражает в 50% эквиваленте урон Монстра 
    {
        int npcDmg = 0;
        _logText.text += "3. Вычисляем урон монстра:\n\r";

        int rndmDmg = Random.Range(PhysMinDmg, PhysMaxDmg + 1);
        npcDmg += rndmDmg;
        _logText.text += "Разброс урона = " + PhysMinDmg + "-" + PhysMaxDmg + ". Получилось " + rndmDmg + ".\n\r";

        //if (BonusPhysAttack > 0)
        //{
        //    npcDmg += BonusPhysAttack;
        //    _logText.text += "К результату прибавляем бонус атаки(" + BonusPhysAttack + ") = " + npcDmg + ".\n\r";
        //}

        //if (DaggerInTeeth)
        //{
        //    _logText.text += "Пассивка \"Кинжал в зубы\" даёт бонус к криту. Крит был 1d" + critChance +
        //                    ", а теперь ";
        //    critChance -= Mathf.RoundToInt((critChance - 1) * 0.4f);
        //    _logText.text += "1d" + critChance + ".\n\r";
        //}
        //int rndmCritChance = Random.Range(1, critChance + 1); //вычисление крита, +1, потому что рандом невключительный
        //_logText.text += "4. Шанс крита 1d" + critChance + ". На кубике выпало " + rndmCritChance + ", что ";
        _logText.text += "4. Шанс критического удара: " + CritChancePercent + "%. ";
        bool criticalHit = BattleMechanics.ChanceCalculation(CritChancePercent);
        if (criticalHit) //если крит прошёл, то умножаем урон на 2
        {
            npcDmg *= 2;
            //_logText.text += "является максимальным значением. Урон увеличен вдвое\n\r";
            if (PlayerStats.CurrentHp - npcDmg > 0)
            {
                _logText.text += "Прошёл крит. урон!  Шанс ранить Героя удваивается: 20%";
                BattleMechanics.WoundChance(this, 2, 20);
            }
        }
        else
        {
            _logText.text += "не является максимальным значением.\n\r";
            if (PlayerStats.CurrentHp - npcDmg > 0)
            {
                _logText.text += "Шанс ранить Героя: 10%";
                BattleMechanics.WoundChance(this, 2, 10);
            }
        }
        return npcDmg;
    }


    public void MonsterMagAttack() //магическая атака монстра
    {
        int npcDamage = 0;
        _logText.text += "\n\r" + Name + " атакует магической атакой\n\r";
        _logText.text += "2. Высчитываем урон монстра:\n\r";

        for (int i = 0; i < MagMinDmg; i++)
        {
            int rndmDmg = Random.Range(1, MagMaxDmg + 1);
            npcDamage += rndmDmg;
            _logText.text += "   На кубике №" + (i + 1) + " выпало " + rndmDmg + ". Суммарный урон = " + npcDamage + ".\n\r";
        }

        npcDamage = npcDamage + Lvl + BonusMagAttack;


        _logText.text += "К полученому результату добавляем уровень монстра(" + Lvl + ") + бонус к маг атаке(" + BonusMagAttack +
            ") = " + npcDamage + "\n\r";
        int critChance = CritChancePercent + 1;
        int rndmcritChance = Random.Range(1, critChance); //вычисление крита
        _logText.text += "3. Шанс крита 1d" + critChance + ". На кубике выпало " + critChance + ", что ";
        if (critChance == rndmcritChance) //если крит прошёл, то умножаем урон на 2
        {
            npcDamage *= 2;
            _logText.text += "является максимальным значением. Урон увеличен вдвое\n\r";
        }
        else
        {
            _logText.text += "не является максимальным значением.\n\r";
        }
        if (PlayerStats.PAntimagic)
        {
            npcDamage /= 2;
            _logText.text += "На Герое действует скил Антимагия. -50% маг. урона = " + npcDamage + ".\n\r";
        }

        if (npcDamage > PlayerStats.MagArmor) //если урона хватает, чтобы пробить броню
        {
            _logText.text += "МАГИЧЕСКАЯ АТАКА ПРОХОДИТ!\n\r 3. Брони у Героя было " + PlayerStats.MagArmor + ", а через броню прошло ";
            npcDamage = npcDamage - PlayerStats.MagArmor;
            _logText.text += npcDamage + " урона.\n\r";

            PlayerStats.MagArmor = PlayerStats.MagArmor;

            _logText.text += "4. Полученный урон вычитаем из здоровья Героя: " + PlayerStats.CurrentHp + " - " + npcDamage + " = ";
            PlayerStats.CurrentHp -= npcDamage;
            if (PlayerStats.CurrentHp < 0)
                PlayerStats.CurrentHp = 0;
            _logText.text += PlayerStats.CurrentHp + ".\n\r";
            if (PlayerStats.CurrentHp != 0) //условия для того, чтобы не ранить монстра, когда он и так мёртв
            {
                if (critChance == rndmcritChance) //если был крит, то больше шанс ранить
                {
                    _logText.text += "Прошёл крит. урон!  Шанс ранить Героя удваивается: 20%";
                    BattleMechanics.WoundChance(this, 2, 20);
                }
                else
                {
                    _logText.text += "Шанс ранить Героя: 10%";
                    BattleMechanics.WoundChance(this, 2, 10);
                }
            }
        }
        else
        {
            if (PlayerStats.MagArmor - npcDamage == 0)
            {
                npcDamage = Random.Range(1, 3);
                PlayerStats.CurrentHp -= npcDamage;
                if (PlayerStats.CurrentHp < 0)
                {
                    if (PlayerStats.PBerserk)
                        PlayerStats.CurrentHp = 1;
                    else
                        PlayerStats.CurrentHp = 0;
                }

                _logText.text += "3. Броня разбита магией! Расплавленные осколки брони шрапнелью выстреливают в Героя. Держись, " +
                                 "Герой.\n\r4. Урон от малого пробития от 1 до 2(выпало " + npcDamage + ").\n\r";
                _logText.text += "У Героя осталось " + PlayerStats.CurrentHp + ".\n\r";
                if (PlayerStats.CurrentHp != 0) //условия для того, чтобы не ранить монстра, когда он и так мёртв
                {
                    if (critChance == rndmcritChance)
                    {
                        _logText.text += "Был крит. урон! При малом пробитии с крит. уроном шанс удваивается: 10% шанс ранить Героя";
                        BattleMechanics.WoundChance(this, 2, 10);
                    }
                    else
                    {
                        _logText.text += "При малом пробитии 5% шанс ранить Героя";
                        BattleMechanics.WoundChance(this, 2, 5);
                    }
                }
                return;
            }
            _logText.text += Name + " не пробил броню! Хвала Богам, что Герой взял сегодня свой амулет!\n\r";
        }
    }


    public void MonsterBattlePhase()
    {
        if (BattleMechanics.GameOver)
            return;
        if (Regeneration)
        {
            int regenAmount = 2;
            CurrentHp += regenAmount;
            if (CurrentHp > MaxHp)
                CurrentHp = MaxHp;
            _logText.text += "\n\rПассивка \"Регенерация\" восстановила 2 ХП. Теперь у Монстра " + CurrentHp + " ХП.\n\r";
        }
        //NegativeEffectsOnMonsters();
        //if (Id != SelectedNpc)
        //{
        if (EnemyIsSleeping || KnightHookEffectOnEnemy || AllLayDownEffectOnEnemy)
        //если есть стан или слип
        {
            if (EnemyIsSleeping)
            {
                SleepTurns--;
                if (SleepTurns == 0)
                {
                    EnemyIsSleeping = false;
                    _logText.text += "\n\r" + Name + " проснулся.\n\r";
                    //return;
                }
                else
                    _logText.text += "\n\r" + Name + " ещё будет спать " + SleepTurns + " ход.\n\r";
            }
            if (KnightHookEffectOnEnemy)
            {
                KnightHookDuration--;
                if (KnightHookDuration == 0)
                {
                    KnightHookEffectOnEnemy = false;
                    _logText.text += "\n\r" + Name + " оправился от скила \"Рыцарский хук\".\n\r";
                    //return;
                }
                else
                    _logText.text += "\n\r" + Name + " ещё будет огушении от скила  \"Рыцарский хук\" " + KnightHookDuration + " ход.\n\r";
            }
            if (AllLayDownEffectOnEnemy)
            {
                AllLayDownDuration--;
                if (AllLayDownDuration == 0)
                {
                    AllLayDownEffectOnEnemy = false;
                    _logText.text += "\n\r" + Name + " оправился от скила \"Всем лежать!\".\n\r";
                    //return;
                }
                else
                    _logText.text += "\n\r" + Name + "ещё будет огушении от скила  \"Всем лежать!\" " + AllLayDownDuration + " ход.\n\r";
            }
            NpcMadeHisTurn = true;
        }
        else
        //если не в стане/слипе, то атакует/использует скил
        {
            CheckForEnemySkills();
            if (EnemyHit)
                MonsterDmgPhase();
            else
                EnemyHit = true;
            NpcMadeHisTurn = true;
            //if (PlayerStats.CurrentHp == 0 || CurrentHp == 0)
            //{
            //    BattleMechanics.GameOver = true;
            //return;
            //}
        }
        //if (!KnightHookEffectOnEnemy || !AllLayDownEffectOnEnemy)//если нет стана, то Монстр бьёт
        //{

        //}
        //}

        //else
        //{
        //    if (!KnightHookEffectOnEnemy || !AllLayDownEffectOnEnemy)
        //    {
        //        CheckForEnemySkills();
        //        if (EnemyHit)
        //            MonsterDmgPhase(npc);
        //        else
        //            EnemyHit = true;
        //        NpcMadeHisTurn = true;
        //        if (PlayerStats.CurrentHp == 0 || CurrentHp == 0)
        //        {
        //            WhoIsLoosing(npc);
        //            //return;
        //        }
        //    }
        //}
    }
}