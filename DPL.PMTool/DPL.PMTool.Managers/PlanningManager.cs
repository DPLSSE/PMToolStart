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

        public Project Project(int id)
        {
            // this should load from the ProjectAccess service.
            // var projectAccess = AccessorFactory.CreateAccessor<IProjectAccess>();
            // var dbProject = projectAccess.Project(id);
            // convert db project to client version
            
            return new Project()
            {
                Id =  id,
                Name = "TEST",
                Start = DateTime.Now,
                Activities = new [] {
                    new Activity()
                    {
                        TaskName = "TEST",
                    }
                }
            };
        }
        
        
        public Project SaveProject(Project project)
        {
            var projectAccess = AccessorFactory.CreateAccessor<IProjectAccess>();

            var dbProject = new DPL.PMTool.Accessors.Shared.EntityFramework.Project()
            {
                Name = project.Name,
                Id =  project.Id,
                Start = project.Start,
            };

            var savedProject = projectAccess.SaveProject(dbProject);
            var resultProject = new Project()
            {
                Id = savedProject.Id,
                Start = savedProject.Start,
                Name = savedProject.Name
            };
            
            var activities = new List<Activity>();
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
                var savedActivity = projectAccess.SaveActivity(dbActivity);
                activities.Add(new Activity()
                {
                    Id = savedActivity.Id,
                    Estimate = savedActivity.Estimate,
                    TaskName = savedActivity.TaskName,
                    Start = savedActivity.Start,
                    Finish = savedActivity.Finish,
                    Predecessors = savedActivity.Predecessors,
                    Priority = savedActivity.Priority,
                    Resource = savedActivity.Resource,
                });
            }

            resultProject.Activities = activities.ToArray();

            return resultProject;
        }
    }
}