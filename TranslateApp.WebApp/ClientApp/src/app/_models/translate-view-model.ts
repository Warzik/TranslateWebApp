import { ITranslateViewModel } from '../_interfaces';

export class TranslateViewModel implements ITranslateViewModel
{
    id: number;    projectId: number;
    textToTranslate: string;
    translatedText: string;
    translateApi: import("./translate-api").ITranslateApi;

}
