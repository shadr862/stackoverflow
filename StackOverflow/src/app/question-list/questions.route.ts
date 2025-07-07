import { Component } from "@angular/core";
import { QuestionListComponent } from "./question-list.component";
import { Routes } from "@angular/router";
import { DetailQuestionComponent } from "../detail-question/detail-question.component";
import { EditPostComponent } from "../edit-post/edit-post.component";
import { canAcivateGuard } from "../guards/can-acivate.guard";


export const questionRoute: Routes = [
    {path:'', component: QuestionListComponent},
    {path:'details/:id',component:DetailQuestionComponent,canActivate:[canAcivateGuard]},
    {path:'edit/:id',component:EditPostComponent,canActivate:[canAcivateGuard]},
]