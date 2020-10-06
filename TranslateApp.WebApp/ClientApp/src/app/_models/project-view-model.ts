import { IProjectViewModel } from '../_interfaces';

export class ProjectViewModel implements IProjectViewModel {
    automaticallyTranslate: boolean;
    importFromProject: string;
    id: number;    title: string;
    description: string;
    numberOfTranslations: string;
    fromLanguage: string;
    toLanguage: string;
    userId: string;

}
