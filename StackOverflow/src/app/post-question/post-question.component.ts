import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { PostType, TagDto } from './post-question.model';
import { QuestionAndanswerService } from '../services/questionAndanswerService/question-andanswer.service';
import { CommonModule } from '@angular/common';
import { AuthService } from '../services/authService/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-post-question',
  imports: [CommonModule, ReactiveFormsModule, FormsModule],
  templateUrl: './post-question.component.html',
  styleUrls: ['./post-question.component.css']
})
export class PostQuestionComponent implements OnInit {
  postForm!: FormGroup;
  postTypes = PostType;
  tags: TagDto[] = [];
  tagSearch: string = ''; // Plain variable, NOT part of form group
  filteredTags: TagDto[] = [];
  selectedTagIds: any[] = [];
  showTagDropdown = false;

  constructor(
    private fb: FormBuilder,
    private service: QuestionAndanswerService,
    private router: Router
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

    const currentUserId = localStorage.getItem('userId');

    const now = new Date();
    const bangladeshOffsetMs = 6 * 60 * 60 * 1000; // 6 hours in ms
    const bangladeshTime = new Date(now.getTime() + bangladeshOffsetMs);

    this.postForm.patchValue({
      appUserId: currentUserId,
      postType: PostType.Question,
      name: localStorage.getItem('userName'),
      created: bangladeshTime.toISOString(),
    });

    this.service.getTags().subscribe((res: any) => {
      this.tags = res.items.map((tag: TagDto) => ({
        ...tag,
        selected: false,
      }));
      this.filteredTags = [...this.tags];
    });
  }

  filterTags() {
    const filter = this.tagSearch.toLowerCase();
    this.filteredTags = this.tags.filter(
      tag =>
        tag.tagName.toLowerCase().includes(filter) &&
        !this.selectedTagIds.includes((tag.id))
    );
  }

  addTag(id: any) {

    if (!this.selectedTagIds.includes(id)) {
      this.selectedTagIds = [...this.selectedTagIds, id];
      this.postForm.patchValue({ tagIds: this.selectedTagIds });
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
    this.filterTags();
  }

  getTagNameById(id: any): string {
    const tag = this.tags.find(t => (t.id) === id);
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
      alert(JSON.stringify(rawValue))
      this.service.createPost(rawValue).subscribe(() => {
        this.router.navigateByUrl('dashboard');
      });
    }
  }
}
