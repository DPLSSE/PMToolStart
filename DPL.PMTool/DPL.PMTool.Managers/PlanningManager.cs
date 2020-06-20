using System;
using System.Collections.Generic;
using System.Diagnostics;
using DPL.PMTool.Accessors;
using DPL.PMTool.Managers.Shared;

namespace DPL.PMTool.Managers
{
    public class PlanningManager : ManagerBase, IPlanningManager
    {
        public TestMeResponse TestMe()
        {
            return new TestMeResponse()
            {
                Message = "TestMe"
            };
        }

        public Project NewProject()
        {
            return new Project()
            {
                Id = 0,
                Name = "NEW",
                Start = DateTime.Now,
                Activities = new[]
                {
                    new Activity()
                    {
                        Id = 0,
                        TaskName = "NEW"
                    }
                }
            };
        }

        public Project Project(int id)
        {
            var projectAccess = AccessorFactory.CreateAccessor<IProjectAccess>();

            var loadedDbProject = projectAccess.Project(id);
            if (loadedDbProject == null)
            {
                return null;
            }

            var resultProject = new Project()
            {
                Id = loadedDbProject.Id,
                Start = loadedDbProject.Start,
                Name = loadedDbProject.Name
            };

            var dbActivities = projectAccess.ActivitiesForProject(id);
            var activities = new List<Activity>();
            foreach (var dbActivity in dbActivities)
            {
                activities.Add(new Activity()
                {
                    Id = dbActivity.Id,
                    Estimate = dbActivity.Estimate,
                    TaskName = dbActivity.TaskName,
                    Start = dbActivity.Start,
                    Finish = dbActivity.Finish,
                    Predecessors = dbActivity.Predecessors,
                    Priority = dbActivity.Priority,
                    Resource = dbActivity.Resource,
                });
            }
            resultProject.Activities = activities.ToArray();

            return resultProject;
        }
        
        
        public Project SaveProject(Project project)
        {
            if (project == null)
                throw new InvalidOperationException("project cannot be null");
            
            var projectAccess = AccessorFactory.CreateAccessor<IProjectAccess>();

            var dbProject = new DPL.PMTool.Accessors.Shared.EntityFramework.Project()
            {
                Name = project.Name,
                Id =  project.Id,
                Start = project.Start,
            };

            var savedProject = projectAccess.SaveProject(dbProject);
            
            foreach (var activity in project.Activities)
            {
                var dbActivity = new DPL.PMTool.Accessors.Shared.EntityFramework.Activity()
                {
                    Id = activity.Id,
                    Estimate = activity.Estimate,
                    TaskName = activity.TaskName,
                    Predecessors = activity.Predecessors,
                    Priority = activity.Priority,
                    Finish = activity.Finish,
                    Start = activity.Start,
                    Resource = activity.Resource,
                    ProjectId = dbProject.Id
                };
                projectAccess.SaveActivity(dbActivity);
            }

            return Project(savedProject.Id);
        }

        public ProjectListItem[] Projects()
        {
            var projectAccess = AccessorFactory.CreateAccessor<IProjectAccess>();

            var projects = new List<ProjectListItem>();
            var dbProjects = projectAccess.Projects();
            foreach (var dbProject in dbProjects)
            {
                projects.Add(new ProjectListItem()
                {
                    Id = dbProject.Id,
                    Name = dbProject.Name
                });
            }

            return projects.ToArray();
        }
    }
}