export enum VoteType {
  Upvote = 0,
  Downvote = 1
}

export interface Vote {
  postId: string;
  appUserId: string;
  voteType: VoteType;
}


