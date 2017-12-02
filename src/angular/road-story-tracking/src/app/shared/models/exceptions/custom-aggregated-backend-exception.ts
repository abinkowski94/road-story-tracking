import { BackendException } from './backend-exception.model';

export class CustomAggregatedBackendException extends BackendException {
    public Exceptions: BackendException[];
}
