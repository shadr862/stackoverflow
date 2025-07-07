import { J, R } from '@angular/cdk/keycodes';
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
  tagSearch: string = ''; // Plain variable, NOT part of form group
  filteredTags: TagDto[] = [];
  selectedTagIds: any[] = [];
  selectedTags: string[] = []
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

    // ✅ Step 1: Load tags
    this.service.getTags().subscribe((res: any) => {
      this.tags = res.items.map((tag: TagDto) => ({
        ...tag,
        selected: false,
      }));
      this.filteredTags = [...this.tags];

      // ✅ Step 2: After tags are loaded, load question
      this.service.getQuestionById(this.postId).subscribe((data: any) => {
        this.question = data;
        this.loaddata(); // ✅ Tags are guaranteed to be ready
      });
    });
  }

  loaddata() {
    this.selectedTags = Array.isArray(this.question.tags) ? this.question.tags.map((tag: any) => tag.tagName) : [];

    
    this.selectedTagIds = this.tags
      .filter(tag => this.selectedTags.includes(tag.tagName))
      .map(tag => tag.id);

    


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
      this.service.editQuestion(rawValue, this.postId).subscribe(() => {
        this.router.navigateByUrl('dashboard');
      });
    }
  }

}
