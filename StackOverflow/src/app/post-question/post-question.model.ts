export enum PostType {
  Question = 0,
  Answer = 1
}

export interface PostCreateDto {
  appUserId: string;
  postType: PostType;
  parentId?: string;
  acceptedAnswerId?: string;
  title?: string;
  body: string;
  tagIds: string[];
}

export interface TagDto {
  id: string;
  tagName: string;
  tagDescription: string;
  selected?: boolean; // For UI tracking
}
