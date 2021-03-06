using System.Linq;
using DPL.PMTool.Accessors.Shared.EntityFramework;

namespace DPL.PMTool.Accessors
{
    class ProjectAccess : IProjectAccess
    {
        public Project Project(int id)
        {
            using (var db = DatabaseContext.Create())
            {
                return db.Projects.FirstOrDefault(p => p.Id == id);
            }
        }
        
        public Project SaveProject(Project project)
        {
            using (var db = DatabaseContext.Create())
            {
                if (project.Id == 0)
                {
                    db.Projects.Add(project);
                }
                else
                {
                    db.Projects.Attach(project);
                }

                db.SaveChanges();
            }

            return project;
        }
       
        public Activity Activity(int id)
        {
            using (var db = DatabaseContext.Create())
            {
                return db.Activities.FirstOrDefault(p => p.Id == id);
            }
        }
        
        public Activity SaveActivity(Activity activity)
        {
            using (var db = DatabaseContext.Create())
            {
                if (activity.Id == 0)
                {
                    db.Activities.Add(activity);
                }
                else
                {
                    db.Activities.Attach(activity);
                }

                db.SaveChanges();
            }

            return activity;
        }
    }
}