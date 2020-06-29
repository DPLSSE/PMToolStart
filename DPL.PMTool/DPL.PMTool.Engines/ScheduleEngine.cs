using System;
using System.Linq;
using DPL.PMTool.Engines.Shared;
using DPL.PMTool.Managers;

namespace DPL.PMTool.Engines
{
    public class ScheduleEngine : EngineBase, IScheduleEngine
    {
        public Project CalculateSchedule(Project project)
        {
            FixSequences(project);
            
            // not correct schedule algorithm below
            var ordered = project.Activities.OrderBy(a => a.Sequence);

            var lastDate = project.Start;
            foreach (var activity in ordered)
            {
                activity.Start = lastDate;
                if (activity.Estimate <= 0.0M)
                {
                    activity.Finish = activity.Start.AddDays(1); // default to a day
                }
                else
                {
                    activity.Finish = 
                        activity.Start.AddDays(
                            Convert.ToDouble(activity.Estimate));    
                }

                lastDate = activity.Finish;
            }
            
            return project;
        }

        private void FixSequences(Project project)
        {
            var nextSequence = project.Activities.Max(a => a.Sequence) + 1;
            
            foreach (var activity in project.Activities)
            {
                if (activity.Sequence <= 0)
                {
                    activity.Sequence = nextSequence++;
                }
            }
        }
    }
}