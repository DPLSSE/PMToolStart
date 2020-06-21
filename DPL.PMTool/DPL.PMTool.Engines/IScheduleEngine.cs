using DPL.PMTool.Managers;

namespace DPL.PMTool.Engines
{
    public interface IScheduleEngine
    {
        Project CalculateSchedule(Project project);
    }
}