# Online Career Guidance Platform

A user‐friendly web app to help you discover, plan and advance your career through personalized assessments, learning resources and mentor connections.

## 🚀 Demo

![Black and Purple Modern Technology Presentation (14)](https://github.com/user-attachments/assets/50b5a1ad-1018-4a8d-8174-87330ea10ca8)
<p align="center"><em>Hero Section.</em></p>


![Black and Purple Modern Technology Presentation (15)](https://github.com/user-attachments/assets/ce1a909f-d396-4bbf-8c8d-4da7f3586a3e)
<p align="center"><em>Interactive overview of your career goals, progress and recommended actions.</em></p>

![Black and Purple Modern Technology Presentation (16)](https://github.com/user-attachments/assets/af9cfc7b-139d-48f4-a2bc-5b099e6ee4b7)
<p align="center"><em>Quick quizzes generate your Career Compatibility Score.</em></p>

![Black and Purple Modern Technology Presentation (18)](https://github.com/user-attachments/assets/6f104213-f71c-41dc-8255-d1c38db1eaf0)
<p align="center"><em>CV Builder tool.</em></p>

![Black and Purple Modern Technology Presentation (17)](https://github.com/user-attachments/assets/7401ff46-b0b3-44d7-ba4f-fd244d733d81)
<p align="center"><em>Built-in messaging and video calls to connect with industry mentors, users, admins, and more....</em></p>

## ✨ Features

- **Personalized Quizzes & Scores**  
- **Curated Learning Library**  
- **Mentor Directory & Chat/Video**  
- **Interactive Career Maps**  
- **Job/Internship Listings & Tracker**  
- **Goal Setting & Networking Events**

## ⚡ Quick Start

### 1. Clone Repositories

```bash
# Clone backend API
git clone https://github.com/kaiquincy/eproject_Sem3_BE.git
# Clone frontend app
git clone https://github.com/kaiquincy/eproject_Sem3.git
```

### 2. Backend Setup (.NET + XAMPP MySQL)

#### 1. Start XAMPP
Launch Apache and MySQL via the XAMPP control panel.
Config your MySQL in ```program.cs```

#### 2. Create a Database
In phpMyAdmin, create a new database named ```career_guidance```

#### 3. Migrations & Run
```bash
cd eproject_Sem3_BE
dotnet restore             # Install package .NET
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run                 # Start API on http://localhost:5037
```

#### 4. Account for testing
*(Sample data will automacally be added when execute ```dotnet run```, you can check sql files in ```/data/seed/```)*
```bash
usename: admin
password: 123
```

### 3. Frontend Setup

```bash
cd ../eproject_Sem3
npm install                # Install dependency React
npm start                  # http://localhost:3000
```
