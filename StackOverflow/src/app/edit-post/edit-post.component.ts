import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { PostType, TagDto } from '../post-question/post-question.model';
import { QuestionAndanswerService } from '../services/questionAndanswerService/question-andanswer.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-edit-post',
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './edit-post.component.html',
  styleUrl: './edit-post.component.css'
})
export class EditPostComponent {
  postForm!: FormGroup;
  postTypes = PostType;
  tags: TagDto[] = [];
  tagSearch: string = '';
  filteredTags: TagDto[] = [];
  selectedTagIds: any[] = [];
  showTagDropdown = false;
  postId!: string;
  question: any;

  constructor(
    private fb: FormBuilder,
    private service: QuestionAndanswerService,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit(): void {
    this.postForm = this.fb.group({
      appUserId: [{ value: '', disabled: true }, Validators.required],
      postType: ['', Validators.required],
      name: [''],
      parentId: [''],
      acceptedAnswerId: [''],
      title: [''],
      body: ['', Validators.required],
      created: [''],
      tagIds: [[]],
    });

    this.postId = this.activatedRoute.snapshot.paramMap.get('id')!;

    this.service.getTags().subscribe((res: any) => {
      this.tags = res.items.map((tag: TagDto) => ({
        ...tag,
        selected: false,
      }));
      this.filteredTags = [...this.tags];

      this.service.getQuestionById(this.postId).subscribe((data: any) => {
        this.question = data;
        this.loaddata();
      });
    });
  }

  loaddata() {
    this.selectedTagIds = [];

    if (Array.isArray(this.question.tags)) {
      this.selectedTagIds = this.tags
        .filter(tag => this.question.tags.some((qt: any) => qt.tagName === tag.tagName))
        .map(tag => tag.id);
    }

    this.postForm.patchValue({
      appUserId: localStorage.getItem('userId'),
      postType: this.question.postType,
      name: this.question.name,
      parentId: this.question.parentId,
      acceptedAnswerId: this.question.acceptedAnswerId,
      title: this.question.title,
      body: this.question.body,
      created: new Date().toISOString(),
      tagIds: this.selectedTagIds,
    });

    this.filterTags();
  }

  filterTags() {
    const filter = this.tagSearch.toLowerCase();
    this.filteredTags = this.tags.filter(
      tag =>
        tag.tagName.toLowerCase().includes(filter) &&
        !this.selectedTagIds.includes(tag.id)
    );
  }

  addTag(id: any) {
    if (!this.selectedTagIds.includes(id)) {
      this.selectedTagIds = [...this.selectedTagIds, id];
      this.postForm.patchValue({ tagIds: this.selectedTagIds });
      this.postForm.get('tagIds')?.updateValueAndValidity();
      this.tagSearch = '';
      this.filterTags();
    }
    this.showTagDropdown = false;
  }

  addTagOrCreate(event?: Event) {
    event?.preventDefault();

    const tagName = this.tagSearch.trim();
    if (!tagName) return;

    const existing = this.tags.find(t => t.tagName.toLowerCase() === tagName.toLowerCase());
    if (existing) {
      this.addTag(existing.id);
      return;
    }

    const newTag = {
      tagName: tagName,
      tagDescription: `${tagName} related questions.`
    };

    this.service.createTag(newTag).subscribe({
      next: (createdTag: TagDto) => {
        this.tags.push(createdTag);
        this.filteredTags.push(createdTag);
        this.addTag(createdTag.id);
      },
      error: (err) => {
        console.error('Tag creation failed:', err);
      }
    });
  }

  removeTag(id: any) {
    this.selectedTagIds = this.selectedTagIds.filter(tagId => tagId !== id);
    this.postForm.patchValue({ tagIds: this.selectedTagIds });
    this.postForm.get('tagIds')?.updateValueAndValidity();
    this.filterTags();
  }

  getTagNameById(id: any): string {
    const tag = this.tags.find(t => t.id === id);
    return tag ? tag.tagName : '';
  }

  onTagInputFocus() {
    this.showTagDropdown = true;
    this.filterTags();
  }

  onTagInputBlur() {
    setTimeout(() => {
      this.showTagDropdown = false;
    }, 150);
  }

  onSubmit() {
    if (this.postForm.valid) {
      const rawValue = this.postForm.getRawValue();
      console.log('Submitting edit:', rawValue);
      this.service.editQuestion(rawValue, this.postId).subscribe(() => {
        this.router.navigateByUrl('dashboard');
      });
    }
  }
}
