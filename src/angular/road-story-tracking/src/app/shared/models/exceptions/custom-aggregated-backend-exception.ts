import { BackendException } from './backend-exception.model';

export class CustomAggregatedBackendException extends BackendException {
    public exceptions: BackendException[];
}
