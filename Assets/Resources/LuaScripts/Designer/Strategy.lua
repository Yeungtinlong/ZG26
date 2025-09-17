---
--- Created by Yeung.
--- DateTime: 2025/9/17 15:14
---

local TimelineNode = CS.MBF.TimelineNode;
local GameLuaInterface = CS.TheGame.GM.GameLuaInterface;
local ChaResType = CS.MBF.ChaResType;

local strategy = {
    strategy_speed = {
        id = "strategy_speed",
        name = "神速",
        description = [[「兵贵神速，疾如雷电」全军速度提升50点]],
        effect = TimelineNode(0,
            function(timelineObj, args)
                local allCs = GameLuaInterface.GetAllCharacters();
                for idx, cs in pairs(allCs) do
                    if (cs.side == 0) then
                        cs:SetResource(ChaResType.Speed, cs.Prop.speed + 50);
                    end
                end
            end, {}),
    },
    strategy_shield = {
        id = "strategy_shield",
        name = "固守",
        description = [[「不动如山，坚若磐石」全军获得50%护盾]],
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
};

return strategy;
