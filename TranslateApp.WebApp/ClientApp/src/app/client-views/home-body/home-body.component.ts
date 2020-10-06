import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { IProjectViewModel } from '../../_interfaces';
import { ProjectService } from '../../_services/project.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProjectViewModel } from '../../_models';
import { trigger, transition, query, style, animate, stagger } from '@angular/animations';
@Component({
  selector: 'app-home-body',
  templateUrl: './home-body.component.html',
  styleUrls: ['./home-body.component.scss'],
  animations: [
    trigger('OnLoad', [
      transition('*=>*', [
        query(':self', style({ opacity: '0.0' })),
        query(':self', animate('900ms', style({ opacity: '1' })))
      ])
    ]),
  ]
})
export class HomeBodyComponent implements OnInit {
  addProjectForm: FormGroup;
  editProjectForm: FormGroup;
  deletingModal: NgbModalRef;
  addingModal: NgbModalRef;
  editingModal: NgbModalRef;
  projects: any;
  currentProjectId: string;
  submittedAdding = false;
  submittedEditing = false;
  loading: boolean = true;
  translateReplace: string;
  errors: string[];

  constructor(private projectService: ProjectService, private modalService: NgbModal, private router: Router, private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.errors = [];
    this.loadAllProjects();
  }

  ngAfterViewInit() {

    if (document.getElementById('sidebar').classList[0] === 'active') {
      document.getElementById('content').classList.add('active');
    }

  }

  get e() { return this.editProjectForm.controls; }

  get f() { return this.addProjectForm.controls; }

  private loadAllProjects() {
    this.projectService.getAllProjects().subscribe(p => {
      this.projects = p;
      this.loading = false;
    });
  }

  confDeleteProject() {
    this.deletingModal.close();

    this.loading = true;

    this.projectService.deleteProject(this.currentProjectId).subscribe(
      data => {
        this.ngOnInit();
      }
    );
  }

  openDeletingModel(deletingContent, id) {
    this.currentProjectId = id;
    this.deletingModal= this.modalService.open(deletingContent, { centered: true, backdrop: 'static', windowClass: 'fadeInDown', size: 'sm' });
  }

  openAddingModel(addingContent) {

    this.addProjectForm = this.formBuilder.group({
      name: ['', [Validators.required]],
      description: [''],
      fromLanguage: ['Polish'],
      toLanguage: ['English'],
      importFromProject: ["Nie importuj"],
      automaticallyTranslate: [false]
    });

    this.addingModal = this.modalService.open(addingContent, { centered: true, backdrop: 'static', windowClass: 'fadeInDown' });
  }

  openEditingModel(editingContent, project) {

    this.editProjectForm = this.formBuilder.group({
      name: [project.title, [Validators.required]],
      description: [project.description],
      fromLanguage: [project.fromLanguage],
      toLanguage: [project.toLanguage],
      id: [project.id],
      numberOfTranslations: [project.numberOfTranslations],
      importFromProject: ["Nie importuj"],
      automaticallyTranslate: [project.automaticallyTranslate]
    });

    this.editingModal = this.modalService.open(editingContent, { centered: true, backdrop: 'static', windowClass: 'fadeInDown' });
  }



  onEditingSubmit() {


    this.submittedEditing = true;

    // stop here if form is invalid
    if (this.editProjectForm.invalid) {
      return;
    }

    const postData = new ProjectViewModel();
    postData.title = this.editProjectForm.controls.name.value;
    postData.description = this.editProjectForm.controls.description.value;
    postData.fromLanguage = this.editProjectForm.controls.fromLanguage.value;
    postData.toLanguage = this.editProjectForm.controls.toLanguage.value;
    postData.id = this.editProjectForm.controls.id.value;
    postData.numberOfTranslations = this.editProjectForm.controls.numberOfTranslations.value;
    postData.automaticallyTranslate = this.editProjectForm.controls.automaticallyTranslate.value;
    postData.importFromProject = this.editProjectForm.controls.importFromProject.value == "Nie importuj" ? null : this.editProjectForm.controls.importFromProject.value;

    //postData.webSiteUrl = this.addProjectForm.controls.webSiteUrl.value;
    //postData.webSiteTitle = this.addProjectForm.controls.webSiteTitle.value;
    //postData.webSiteDescription = this.addProjectForm.controls.webSiteDescription.value;


    


    this.projectService.updateProject(postData).subscribe(data => {
      this.editingModal.close();
      this.loading = true;
      this.submittedEditing = false;
      this.ngOnInit();
    },
      error => {
        this.loading = false;
        this.errors = error[''];
      });
  }

  onAddingSubmit() {


    this.submittedAdding = true;

    // stop here if form is invalid
    if (this.addProjectForm.invalid) {
      return;
    }

    const postData = new ProjectViewModel();
    postData.title = this.addProjectForm.controls.name.value;
    postData.description = this.addProjectForm.controls.description.value;
    postData.fromLanguage = this.addProjectForm.controls.fromLanguage.value;
    postData.toLanguage = this.addProjectForm.controls.toLanguage.value;
    postData.automaticallyTranslate = this.addProjectForm.controls.automaticallyTranslate.value;
    postData.importFromProject = this.addProjectForm.controls.importFromProject.value == "Nie importuj" ? null : this.addProjectForm.controls.importFromProject.value;
    //postData.webSiteUrl = this.addProjectForm.controls.webSiteUrl.value;
    //postData.webSiteTitle = this.addProjectForm.controls.webSiteTitle.value;
    //postData.webSiteDescription = this.addProjectForm.controls.webSiteDescription.value;


    this.projectService.addProject(postData).subscribe(data => {
      this.addingModal.close();
      this.loading = true;
      this.submittedAdding = false;
      this.ngOnInit();
    },
      error => {
        this.loading = false;
        this.errors = error[''];
      }
    );
  }
}
