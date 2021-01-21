import { TestBed } from '@angular/core/testing';

import { ProcessHttpmsg.ServiceService } from './process-httpmsg.service.service';

describe('ProcessHttpmsg.ServiceService', () => {
  let service: ProcessHttpmsg.ServiceService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProcessHttpmsg.ServiceService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
