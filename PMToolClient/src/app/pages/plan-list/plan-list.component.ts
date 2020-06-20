import { Component, OnInit } from '@angular/core';
import { Project } from 'src/app/dtos/Project';
import { ProjectListItem } from 'src/app/dtos/ProjectListItem';
import { Router } from '@angular/router';
import { PlanningService } from 'src/app/services/planning.service';

@Component({
  selector: 'app-plan-list',
  templateUrl: './plan-list.component.html',
  styleUrls: ['./plan-list.component.scss']
})
export class PlanListComponent implements OnInit {

  projects: ProjectListItem[];

  constructor(private router: Router, private planning: PlanningService) { }

  async ngOnInit() {

    this.projects = await this.planning.projects();
  }

  openProject(id: number) {
    this.router.navigate(['/plan/' + id]);
  }

}
