import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable()
export class ConfigurationService {
  public static readonly appVersion: string = "0.0.1";
  public static readonly defaultLanguage: string = "en";
  public reloadInterval = environment.reloadInterval;

  private _dateFormat: string = 'dd-MM-yyyy';
  private _genders = [
    { value: 2, display: 'Male' },
    { value: 1, display: 'Female' },
    { value: 0, display: 'Other' }
  ];

  get dateFormat() {
    return this._dateFormat;
  }

  get genders() {
    return this._genders;
  }

}
