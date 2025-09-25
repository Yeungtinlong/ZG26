---
--- Created by Yeung.
--- DateTime: 2025/9/17 15:14
---

local TimelineNode = CS.MBF.TimelineNode;
local GameLuaInterface = CS.TheGame.GM.GameLuaInterface;
local ChaResType = CS.MBF.ChaResType;

local strategy = {
    {
        id = "strategy_common",
        name = "均衡",
        description = [[「其徐如林，其疾如风」攻守兼备之策，全军速度提升30，获得20%护盾]],
        unlockCondition = function() return true end,
        unlockDescription = "",
        effect = TimelineNode(0,
            function(timelineObj, args)
                local allCs = GameLuaInterface.GetAllCharacters();
                for idx, cs in pairs(allCs) do
                    if (cs.side == 0) then
                        cs:SetResource(ChaResType.Speed, cs.Prop.speed + 30);
                        cs:SetResource(ChaResType.Shield, math.ceil(cs.Prop.hp * 0.2));
                    end
                end
            end, {}),
    },
    {
        id = "strategy_speed",
        name = "神速",
        description = [[「兵贵神速，疾如雷电」全军初始速度提升80点]],
        unlockCondition = function() return true end,
        unlockDescription = "",
        effect = TimelineNode(0,
            function(timelineObj, args)
                local allCs = GameLuaInterface.GetAllCharacters();
                for idx, cs in pairs(allCs) do
                    if (cs.side == 0) then
                        cs:SetResource(ChaResType.Speed, cs.Prop.speed + 90);
                    end
                end
            end, {}),
    },
    {
        id = "strategy_shield",
        name = "固守",
        description = [[「不动如山，坚若磐石」全军获得50%护盾]],
        unlockCondition = function() return true end,
        unlockDescription = "",
        effect = TimelineNode(0,
            function(timelineObj, args)
                local allCs = GameLuaInterface.GetAllCharacters();
                for idx, cs in pairs(allCs) do
                    if (cs.side == 0) then
                        cs:SetResource(ChaResType.Shield, math.ceil(cs.Prop.hp * 0.5));
                    end
                end
            end, {}),
    },
    {
        id = "strategy_storm",
        name = "妖风",
        description = [[「卧龙唤星，奇门遁甲」诸葛亮夜观天象，借北斗之力催动妖风。敌军获得100%护盾，但速度变为0]],
        unlockCondition = function()
            local chaInstances = GameLuaInterface.GetAllChaInstances();
            for _, chaInstance in pairs(chaInstances) do
                if (chaInstance.id == "cha_zhugeliang" and chaInstance.owned) then
                    return true;
                end
            end
            return false;
        end,
        unlockDescription = "解锁诸葛亮后可以使用",
        effect = TimelineNode(0,
            function(timelineObj, args)
                local allCs = GameLuaInterface.GetAllCharacters();
                for idx, cs in pairs(allCs) do
                    if (cs.side == 1) then
                        cs:SetResource(ChaResType.Speed, 0);
                        cs:SetResource(ChaResType.Shield, math.ceil(cs.Prop.hp));
                    end
                end
            end, {}),
    },
};

return strategy;
