import { ITranslateApi } from '../_models/translate-api';

export interface ITranslateViewModel {
  id: number;
  projectId: number;
  textToTranslate: string; 
  translatedText: string;
  translateApi: ITranslateApi;
}
