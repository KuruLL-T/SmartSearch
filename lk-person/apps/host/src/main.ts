import {RemoteServices} from './services/remote.service';

RemoteServices.getServices().then((_) => import('./bootstrap'));