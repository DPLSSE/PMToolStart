using DPL.PMTool.Accessors.Shared.EntityFramework;

namespace DPL.PMTool.Accessors
{
    public interface IProjectAccess
    {
        public Project Project(int id);
        Project SaveProject(Project project);
    }
}