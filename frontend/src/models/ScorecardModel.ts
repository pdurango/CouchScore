export default class ScorecardModel {
    Id: number = 0;
    Title: string = '';
    CreatedDate: Date = new Date();
    ScorecardMatches: ScorecardMatchModel[] = [];
}

export class ScorecardMatchModel {
    Id: number = 0;
    Title: string = '';
    CreatedDate: Date = new Date();
    ScorecardMatches: ScorecardMatchOptionModel[] = [];
}

export class ScorecardMatchOptionModel {
    Id: number = 0;
    Title: string = '';
    CreatedDate: Date = new Date();
}