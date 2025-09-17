using System.Collections.Generic;
using Common;
using UnityEngine;

namespace MBF
{
    public sealed class TimelineManager : MonoBehaviour
    {
        private readonly List<TimelineObj> _timelines = new List<TimelineObj>();

        public void CreateTimeline(TimelineObj timelineObj)
        {
            _timelines.Add(timelineObj);
        }

        public void LogicTick()
        {
            bool hasTimelineObjRemoved = false;
            
            for (int i = 0; i < _timelines.Count; i++)
            {
                TimelineObj timelineObj = _timelines[i];
                int willTick = timelineObj.tickElapsed + 1;
                
                for (int j = 0; j < timelineObj.model.nodes.Count; j++)
                {
                    float tickPoint = timelineObj.model.nodes[j].tickElapsed / timelineObj.timescale;
                    
                    if (willTick > tickPoint && timelineObj.tickElapsed <= tickPoint)
                    {
                        timelineObj.model.nodes[j].doEvent?.Invoke(timelineObj, timelineObj.model.nodes[j].eventParams);
                    }
                }

                // 处理TimelineObj生命周期
                timelineObj.tickElapsed++;
                if (timelineObj.tickElapsed > timelineObj.model.duration)
                {
                    _timelines[i] = null;
                    hasTimelineObjRemoved = true;
                }
            }

            if (hasTimelineObjRemoved)
                _timelines.RemoveNullElements();
        }
    }
}