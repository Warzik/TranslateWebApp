
export interface IProjectViewModel {
  id: number;
  title: string;
  description: string;
  numberOfTranslations: string;
  fromLanguage: string;
  toLanguage: string;
  userId: string;
  importFromProject: string;
  automaticallyTranslate: boolean; 
}
