import { TestBed } from '@angular/core/testing';

import { QuestionAndanswerService } from './question-andanswer.service';

describe('QuestionAndanswerService', () => {
  let service: QuestionAndanswerService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(QuestionAndanswerService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
