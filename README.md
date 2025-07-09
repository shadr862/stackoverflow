# 💬 Stack Overflow Clone – Q&A Platform

A full-featured Q&A system inspired by Stack Overflow, built with Angular (frontend) and ASP.NET Core using the ABP Framework (backend). This platform allows users to ask and answer questions, vote, comment, and organize content with tags — all within a modular and scalable architecture.

---

## 🚀 Features

- ✅ **Ask & Answer Questions** – Users can post questions and answers with full markdown support.
- ✅ **Commenting System** – Add and view comments on both questions and answers.
- ✅ **Voting Mechanism** – Upvote or unvote content to reflect user support.
- ✅ **Accepted Answer** – Question owners can mark an answer as accepted.
- ✅ **Tagging System** – Multi-select tag input with real-time search.
- ✅ **Search & Filter** – Filter questions by tags and search through content.
- ✅ **User Profiles** – Display user-related activity and contribution.

---

## 🧱 Technical Overview

### 🔹 Frontend
- **Framework**: Angular 17
- **Language**: TypeScript
- **Features**:
  - Tag-based filtering UI
  - Reusable components (questions, answers, tags, comments)
  - Reactive forms for submission and editing
  - Real-time tag search and dropdown selection

### 🔹 Backend
- **Framework**: ASP.NET Core with ABP Framework
- **Language**: C#
- **Architecture**: Modular, layered clean architecture
- **Key Features**:
  - Self-referencing posts for Q&A structure
  - Many-to-many relationships for tags
  - One-to-many relationships for comments and votes
  - Secure APIs with JWT Authentication

---

## 🧩 Database Design

- `Post` (Questions & Answers) – self-referencing
- `Tag` – many-to-many with posts
- `Comment` – one-to-many from posts
- `Vote` – one-to-many with posts
- `User` – related to all user-generated content

---

## 📷 UI Screenshots

> *(Replace image URLs with your actual screenshots)*
 **. Dashboard View**
![Question View](https://github.com/shadr862/stackoverflow/blob/main/images/dashboard.png)

**. Create Question View**
![Question View](https://github.com/shadr862/stackoverflow/blob/main/images/question.png)

**. Detail Question**
![Voting](https://github.com/shadr862/stackoverflow/blob/main/images/detail_question.png)

**. Comments**
![Comments](https://github.com/shadr862/stackoverflow/blob/main/images/comment.png)

**.Post Answers**
![Comments](https://github.com/shadr862/stackoverflow/blob/main/images/post_answer.png)

**.View Answers**
![Comments](https://github.com/shadr862/stackoverflow/blob/main/images/all_answer.png)

---

## 🛠️ Getting Started

### ⚙️ Prerequisites

- .NET SDK 6+
- Node.js and Angular CLI
- SQL Server

### 🔧 Backend Setup

```bash
cd api/
dotnet restore
dotnet ef database update
dotnet run
