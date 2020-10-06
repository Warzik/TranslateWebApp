import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { TranslateViewModel } from '../_models/translate-view-model';
@Injectable({
  providedIn: 'root'
})
export class TranslatorService {

 constructor(private http: HttpClient) { }


  addTranslate(translate: TranslateViewModel) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');

    return this.http.post('/Translator/AddTranslate', JSON.stringify(translate), { headers })
  }

  getAllTranslats(projectId: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');

    return this.http.post('/Translator/GetAllTranslats', JSON.stringify(projectId), { headers });
  }

  deleteTranslate(id: string) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');

    return this.http.post('/Translator/DeleteTranslate', JSON.stringify(id), { headers });
  }

  updateTranslate(translate: TranslateViewModel) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');

    return this.http.post('/Translator/UpdateTranslate', JSON.stringify(translate), { headers });
  }

}
