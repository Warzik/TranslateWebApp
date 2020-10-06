import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { ProjectListViewModel } from '../_models';
import { ProjectViewModel } from '../_models';

@Injectable()
export class ProjectService {

  constructor(private http: HttpClient) { }


  addProject(project: ProjectViewModel) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');

    return this.http.post('/Home/AddProject', JSON.stringify(project), { headers });
  }

  getAllProjects() {
    return this.http.post<ProjectListViewModel>('/Home/GetAllProjects', '');
  }

  deleteProject(id: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');

    return this.http.post('/Home/DeleteProject', JSON.stringify(id), { headers });
  }

  updateProject(project: ProjectViewModel) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');

    return this.http.post('/Home/UpdateProject', JSON.stringify(project), { headers });
  }

}
