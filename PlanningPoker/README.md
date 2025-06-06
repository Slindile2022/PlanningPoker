# 🃏 Planning Poker App

## 🚀 About The Project
A **modern Agile estimation tool** built to enhance collaboration and estimation accuracy for software development teams.

### 🎮 Key Highlights
- ⚡ **Real-time collaboration** with instant updates using SignalR
- 📱 Android mobile app powered by a robust ASP.NET Core backend
- 🔄 **Seamless synchronization** of estimations across all team members
- 📊 **Statistical analysis**: view average and median estimates
- 🔒 **Secure moderation** using token-based access controls

---

## ✨ Features

### 🛠️ Create Estimation Sessions
- Create uniquely named sessions as a moderator
- Automatically generate secure moderator tokens
- Invite team members to join sessions easily

### 🔁 Real-time Collaboration
- All actions sync instantly for every participant
- Track who joined and who voted in real-time
- No need for page refreshes or manual syncing

### 🧮 Fibonacci Card Deck
- Estimation values follow the standard Fibonacci sequence  
  (1, 2, 3, 5, 8, 13, 21, ...)

### 🕵️‍♂️ Private Voting & Synchronized Reveals
- Votes are private until the moderator reveals them
- Reveal all votes at once
- Automatically display average and median values for discussion

### 📚 Story Management
- Add multiple user stories to estimate in one session
- Track progress of stories from pending to completed

---

## 🔄 How It Works

### 1️⃣ Session Creation & Joining
- Moderator creates a new session
- System generates a unique session ID + moderator token
- Participants join by selecting the session and entering their name
- All users see participants joining in real-time

### 2️⃣ Story Setup & Estimation
- Moderator adds user stories (title + description)
- Moderator starts the voting phase
- Participants select cards to vote
- See who voted — values remain hidden until reveal

### 3️⃣ Results & Discussion
- Moderator reveals all votes
- App displays each vote + calculated average and median
- Team discusses differences, then proceeds to next story

---

## 🏗️ Technical Architecture

### 📱 Mobile App (Android)
- Built using **MVVM architecture**
- Real-time communication via **SignalR client**
- REST API integration with **Retrofit**
- Clean, intuitive UI for session management and voting

### 🖥️ Backend API (ASP.NET Core)
- RESTful API endpoints for session, story, and vote management
- **SignalR Hub** for live updates
- Follows the **Repository Pattern**
- DTOs used for clean and efficient data transfer

### 💾 Database
- Powered by **SQL Server**
- Access through **Entity Framework Core**
- Structured schema for:
  - Sessions
  - Participants
  - Stories
  - Votes

---

## 📫 Contact
Want to collaborate or learn more?

- 🌐 [LinkedIn](https://www.linkedin.com/in/slindile-maseko-402280277/)
- 💌 [Email](mailto:slindilerowen@gmail.com)

---

## 💡 Future Improvements
- Web frontend version for cross-platform use
- Authentication with user accounts
- Exportable reports and estimation history
- Timeboxed voting with countdowns

---

## 🙌 Contributions Welcome!
If you're interested in contributing, feel free to fork the repo and open a pull request. Let's build a better estimation experience together!

---

