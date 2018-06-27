
public class HeroData  {

    public string id; 
    // 第1位：
    //1神族，2巨人，3亚人，4人类，5魔兽
    //第2位：星级
    //第3位：赛季
    public string name; // cn版名字
    public int sex; 
    // 性别
    //1男
    //2女
    public bool isActive; // 是否已投放
    public string specialIcon; // 满星变形象后的icon 没有则不填
    public int hgroup; // 所属将领组 (可以相互进阶) 0表示没有（必须要填）
    public int tgroup; // 所属队伍组 (相同组一只部队只能上阵一个) 0表示没有（必须要填）
    public int baseAtk; // 基础攻击
    public int baseDef; // 基础防御
    public int baseStrategy; // 基础谋略
    public int baseSpeed; // 基础速度
    public int baseBuilddmg; // 基础攻城
    public int growAtk; // 攻击成长
    public int growDef; // 防御成长
    public int growStrategy; // 谋略成长
    public int growSpeed; // 速度成长
    public int growBuilddmg; // 攻城成长
    public int atkDistance; // 攻击距离
    public int cost; // cost值
    public int star; // 星级
    public string skillId; // 天赋技能
    public int sectId;
    // 大兵种
    //1步兵
    //2骑兵
    //4弓兵
    public int armyId; // 小兵种
    public int camp; 
    // 阵营
    //1神族
    //2巨人
    //4异族
    //8人类
    //16魔兽
    public int season; // 赛季
    public string disassemblSkill; // 可拆解技能id1
    public string disassemblSkill2; // 可拆解技能id2
    public string icon; // 图标
    public string stroy; // 英雄列传
}

