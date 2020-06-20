import { Injectable } from '@angular/core';
import { Project } from '../dtos/Project';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ProjectListItem } from '../dtos/ProjectListItem';

@Injectable({
  providedIn: 'root'
})
export class PlanningService {

  constructor(private httpClient: HttpClient) { }

  public async newProject(): Promise<Project> {
    return this.httpClient.get<Project>(
      'https://localhost:5001/Planning/NewProject').toPromise();
  }
  
  public async project(id: number): Promise<Project> {
    // note: the data might not match with server.
    return this.httpClient.get<Project>(
      'https://localhost:5001/Planning/Project?id=' + id).toPromise();
  }

  public async saveProject(project: Project): Promise<Project> {
    // note: the data might not match with server.
    return this.httpClient.post<Project>(
      'https://localhost:5001/Planning/SaveProject', project).toPromise();
  }

  public async projects(): Promise<ProjectListItem[]> {
    return this.httpClient.get<ProjectListItem[]>(
      'https://localhost:5001/Planning/Projects').toPromise();
  }
}
