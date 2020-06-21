using System;
using System.Diagnostics;
using System.Linq;
using DPL.PMTool.Accessors.Shared.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Activity = DPL.PMTool.Accessors.Shared.EntityFramework.Activity;

namespace DPL.PMTool.Accessors
{
    class ProjectAccess : IProjectAccess
    {
        public Project[] Projects()
        {
            using (var db = DatabaseContext.Create())
            {
                return db.Projects.ToArray();
            } 
        }
        
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
                project.UpdatedAt = DateTime.Now;
                
                if (project.Id == 0)
                {
                    project.CreatedAt = DateTime.Now;
                    db.Projects.Add(project);
                }
                else
                {
                    var loaded = db.Projects.Find(project.Id);
                    loaded.Name = project.Name;
                    loaded.Start = project.Start;
                    loaded.UpdatedAt = DateTime.Now;
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
                activity.UpdatedAt = DateTime.Now;
                if (activity.Id == 0)
                {
                    activity.CreatedAt = DateTime.Now;
                    db.Activities.Add(activity);
                }
                else
                {
                    var dbActivity = db.Activities.Find(activity.Id);
                    dbActivity.Sequence = activity.Sequence;
                    dbActivity.Estimate = activity.Estimate;
                    dbActivity.Finish = activity.Finish;
                    dbActivity.Predecessors = activity.Predecessors;
                    dbActivity.Priority = activity.Priority;
                    dbActivity.Resource = activity.Resource;
                    dbActivity.Start = activity.Start;
                    dbActivity.TaskName = activity.TaskName;
                    dbActivity.UpdatedAt = activity.UpdatedAt;
                }

                db.SaveChanges();
            }

            return activity;
        }
        
        public Activity[] ActivitiesForProject(int projectId)
        {
            using (var db = DatabaseContext.Create())
            {
                return db.Activities.Where(a => a.ProjectId == projectId).ToArray();
            }
        }        
    }
}