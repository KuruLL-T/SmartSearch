import {Injectable} from '@angular/core';
import {environment} from '../environments/environment';
import {setManifest} from '@angular-architects/module-federation';

@Injectable({
  providedIn: 'root',
})
export class RemoteServices {
  static async getServices(): Promise<Record<string, string>> {
    const response = this.parseRemotes(environment.services);
    await setManifest({...response}, true).catch((err) => console.error(err));
    return response;
  }

  static parseRemotes(template: {name: string, path: string, remoteURL?: string}[]) {
    const items: Record<string, string> = {};
    template.forEach((remote) => {
      items[remote.name] = remote.remoteURL
        ? `http://${remote.remoteURL}/remoteEntry.js`
        : `https://${remote.name}-${environment.remoteEntryTemplateURL}/remoteEntry.js`;
    });
    return items;
  }
}
