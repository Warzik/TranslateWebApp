import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { NgbModalRef, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { Router, ActivatedRoute } from '@angular/router';
import { IProjectViewModel } from '../../_interfaces';
import { ProjectService } from '../../_services/project.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ProjectViewModel } from '../../_models';
import { trigger, transition, query, style, animate, stagger } from '@angular/animations';
import { TranslatorService } from '../../_services/translator.service';
import { TranslateViewModel } from '../../_models/translate-view-model';
import { ITranslateApi } from '../../_models/translate-api';

@Component({
  selector: 'app-translator-body',
  templateUrl: './translator-body.component.html',
  styleUrls: ['./translator-body.component.scss'],
  animations: [
    trigger('OnLoad', [
      transition('*=>*', [
        query(':self', style({ opacity: '0.0' })),
        query(':self', animate('600ms', style({ opacity: '1' })))
      ])
    ]),
  ]
})
export class TranslatorBodyComponent implements OnInit {

  projectId: string;
  translates: any;
  loading: boolean = true;
  disableButton: boolean = false;

  constructor(private route: ActivatedRoute, private translatorService: TranslatorService, private modalService: NgbModal, private router: Router, private formBuilder: FormBuilder) {
  }

  ngOnInit() {
    this.projectId = this.route.snapshot.paramMap.get('id');
    this.loadAllTranslats(this.projectId);
  }

  ngAfterViewInit() {

    if (document.getElementById('sidebar').classList[0] === 'active') {
      document.getElementById('content').classList.add('active');
    }

  }

  addTranslate() {
    let translateModel: TranslateViewModel = new TranslateViewModel();
    translateModel.projectId = +this.projectId;
    translateModel.translateApi = ITranslateApi.Google;

    this.translatorService.addTranslate(translateModel).subscribe(t => {
      this.loading = true;
      this.ngOnInit();
    });
  }

  useGoogleApi(id: string) {
    document.getElementById('loader' + id).classList.replace('translate-loader-hide', 'translate-loader-show');
    this.disableButton = true;
    let translateModel: TranslateViewModel = new TranslateViewModel();
    translateModel.projectId = +this.projectId;
    translateModel.id = +id;
    translateModel.translateApi = ITranslateApi.Google;
    translateModel.textToTranslate = (<HTMLInputElement>document.getElementById(id)).value;

    this.translatorService.updateTranslate(translateModel).subscribe(t => {
      (<HTMLInputElement>document.getElementById("translatedText" + id)).value = t.toString();
      this.disableButton = false;
      document.getElementById('loader' + id).classList.replace('translate-loader-show', 'translate-loader-hide');
    });
  }

  useGoogleApiAutom(id: string) {
    let translateModel: TranslateViewModel = new TranslateViewModel();
    translateModel.projectId = +this.projectId;
    translateModel.id = +id;
    translateModel.translateApi = ITranslateApi.Google;
    translateModel.textToTranslate = (<HTMLInputElement>document.getElementById(id)).value;

    this.translatorService.updateTranslate(translateModel).subscribe(t => {
      (<HTMLInputElement>document.getElementById("translatedText" + id)).value = t.toString();
    });
  }

  useYandexApi(id: string) {
    document.getElementById('loader' + id).classList.replace('translate-loader-hide', 'translate-loader-show');
    this.disableButton = true;
    let translateModel: TranslateViewModel = new TranslateViewModel();
    translateModel.projectId = +this.projectId;
    translateModel.id = +id;
    translateModel.translateApi = ITranslateApi.Yandex;
    translateModel.textToTranslate = (<HTMLInputElement>document.getElementById(id)).value;

    this.translatorService.updateTranslate(translateModel).subscribe(t => {
      (<HTMLInputElement>document.getElementById("translatedText" + id)).value = t.toString();
      this.disableButton = false;
      document.getElementById('loader' + id).classList.replace('translate-loader-show', 'translate-loader-hide');
    });
  }

  useYandexApiAutom(id: string) {
    let translateModel: TranslateViewModel = new TranslateViewModel();
    translateModel.projectId = +this.projectId;
    translateModel.id = +id;
    translateModel.translateApi = ITranslateApi.Yandex;
    translateModel.textToTranslate = (<HTMLInputElement>document.getElementById(id)).value;

    this.translatorService.updateTranslate(translateModel).subscribe(t => {
      (<HTMLInputElement>document.getElementById("translatedText" + id)).value = t.toString();
    });
  }

  onEnter(id: string)
  {
    document.getElementById('loader' + id).classList.replace('translate-loader-hide','translate-loader-show');

    this.disableButton = true;
    let translateModel: TranslateViewModel = new TranslateViewModel();
    translateModel.projectId = +this.projectId;
    translateModel.id = +id;
    if ((<HTMLInputElement>document.getElementById("radio" + id)).checked)
      translateModel.translateApi = ITranslateApi.Google;
    else
      translateModel.translateApi = ITranslateApi.Yandex;
    translateModel.textToTranslate = (<HTMLInputElement>document.getElementById(id)).value;

    this.translatorService.updateTranslate(translateModel).subscribe(t => {
      (<HTMLInputElement>document.getElementById("translatedText" + id)).value = t.toString();
      this.disableButton = false;
      document.getElementById('loader' + id).classList.replace('translate-loader-show', 'translate-loader-hide');
    });
  }

  onEnterAutom(id: string) {
    let translateModel: TranslateViewModel = new TranslateViewModel();
    translateModel.projectId = +this.projectId;
    translateModel.id = +id;
    if ((<HTMLInputElement>document.getElementById("radio" + id)).checked)
      translateModel.translateApi = ITranslateApi.Google;
    else
      translateModel.translateApi = ITranslateApi.Yandex;
    translateModel.textToTranslate = (<HTMLInputElement>document.getElementById(id)).value;

    this.translatorService.updateTranslate(translateModel).subscribe(t => {
      (<HTMLInputElement>document.getElementById("translatedText" + id)).value = t.toString();
    });
  }


  deleteTranslate(id: string) {
    this.translatorService.deleteTranslate(id).subscribe(t => {
      this.loading = true;
      this.ngOnInit();
    });
  }

  private loadAllTranslats(pojectId: string) {

    this.translatorService.getAllTranslats(pojectId).subscribe(t => {
      this.loading = false;
      this.translates = t;
    });
  }
}
