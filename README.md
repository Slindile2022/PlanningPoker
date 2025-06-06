PlanningPoker
A modern Agile estimation tool for development teams.

Key Features
Create Estimation Sessions: Moderators can create named sessions and invite team members
Real-time Collaboration: All actions (joining, voting, revealing) happen instantly for all participants
Fibonacci Card Deck: Standard estimation using Fibonacci sequence (1, 2, 3, 5, 8, 13, 21, etc.)
Voting & Results: Cast votes privately, reveal them simultaneously, and view statistical aggregations
Story Management: Add multiple stories to estimate in a single session
Secure & Private: Moderator controls via secure tokens
How It Works
Session Creation & Joining
Moderator Creates Session:

Moderator opens the app and creates a new Planning Poker session
System generates a unique session ID and moderator token
Session appears in the active sessions list
Participants Join:

Team members open the app and see active sessions
They select the relevant session and enter their name
All participants see updates as team members join
Estimation Process
Story Setup:

Moderator adds user stories with title and description
Stories start in "Not Started" status
Voting Phase:

Moderator selects a story and starts voting
Participants select cards (1, 2, 3, 5, 8, 13, etc.) representing their estimate
Everyone sees who has voted, but not the values (until reveal)
Results & Discussion:

Moderator reveals all votes simultaneously
App displays all votes, average, and median
Team discusses outliers and reasoning
Finalization:

Team reaches consensus or revotes if needed
Moderator marks story as completed and moves to next story
Real-time Experience
The app provides a truly collaborative experience:

Instant Updates: When anyone joins, votes, or changes status, all participants see it immediately
Synchronized Reveals: The excitement of simultaneous card reveals is preserved
Live Participation Tracking: See who's in the session and who has voted
Technical Architecture
The application consists of:

Mobile App (Android):

Intuitive UI for all Planning Poker activities
Real-time updates via SignalR client
REST API integration via Retrofit
Backend API (ASP.NET Core):

RESTful endpoints for all operations
SignalR hub for real-time communication
Clear separation of concerns:
Controllers handle API requests
Repository pattern for data access
DTOs for data transfer
Database:

SQL Server for persistent storage
Entity Framework Core for data access
Structured data model with sessions, participants, stories, and votes
Data Flow
Session Management:

Create, join, and manage Planning Poker sessions
Real-time participant tracking
Story Workflow:

Not Started → Voting → Revealed → Completed
Seamless progression through estimation phases
Voting Mechanics:

Private voting during the Voting phase
Synchronized reveal when moderator is ready
Statistical analysis of voting results
Core Components
Sessions: Container for an estimation meeting
Participants: Team members joining a session
Stories: Items being estimated
Votes: Estimations cast by participants
Real-time Hub: Enables instant communication
