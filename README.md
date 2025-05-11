
# AI-Powered Real-Time Chatbot 💬🤖

A real-time chatbot platform built with **ASP.NET Core 8**, **SignalR**, and **Tavily AI**. This system enables users to chat with an AI assistant, manage their chat history, and perform moderation actions like edit, delete, and approve messages.

---

## 🚀 Features

| Feature               | Description                                          |
|----------------------|------------------------------------------------------|
| 💬 Real-Time Chat     | SignalR-based instant communication                  |
| 🤖 AI Responses       | Integrated with Tavily AI for intelligent replies    |
| 🔐 User Authentication| ASP.NET Identity with role-based access             |
| 📜 Chat History       | Persistent storage of messages with infinite scroll |
| ✏️ Moderation         | Edit, approve, or delete messages                   |
| ♾️ Infinite Scroll     | Load past conversations on scroll                   |

---

## 🛠 Tech Stack

| Layer      | Technology                   |
|------------|------------------------------|
| Backend    | ASP.NET Core 8 (Web API)     |
| Real-Time  | SignalR (WebSockets)         |
| Frontend   | ASP.NET Core MVC and Javascript(JQuery) |
| Database   | SQL Server / SQLite (EF Core)|
| AI Engine  | Tavily AI         |
| Auth       | ASP.NET Identity             |
| Arch       | Repository     |

---

## 🧩 System Architecture

### 🔄 Flow

1. User sends message → Saved to DB
2. API calls Tavily AI → Gets response
3. Bot's reply sent via SignalR → Saved to DB
4. User views/manage history (CRUD) + infinite scroll

### 🗃 Database Schema

#### `ChatMessages`
- `Id`, `UserId`, `SessionId`, `Sender`, `Message`, `IsApproved`, `IsDeleted`, `Timestamp`

#### `MessageEdits` *(Optional Bonus)*
- Track edit history of messages

#### `AspNetUsers`
- Handled by ASP.NET Identity

---

## 📡 API Endpoints

| Endpoint                     | Method | Description                             |
|-----------------------------|--------|-----------------------------------------|
| `/api/chat/send`            | POST   | Send a user message and get AI response |
| `/api/chat/history`         | GET    | Get paginated chat history              |
| `/api/chat/{id}`            | PUT    | Edit a specific message                 |
| `/api/chat/{id}`            | DELETE | Soft-delete a message                   |
| `/api/chat/{id}/approve`    | PATCH  | Approve a message (admin only)          |

---

## 💻 UI Features

### Chat Panel

- ✅ Real-time message bubbles (User & Bot)
- 📝 Editable messages
- 🗑 Deletable messages
- ✅ Bot is typing indicator
- 📤 Input box with validation

### History Panel

- ♾️ Infinite scroll
- 👮 Moderation controls (for Admins)

---

## ✅ Input & Server Validation

- All user inputs validated client-side (empty, length, type, etc.)
- Server-side validation enforced for all API endpoints
- Unauthorized access is blocked by JWT role-based policies

---

## ⚙️ Setup Instructions

Just add db connection and restore the .bak file from repository. I think dont need to run migration. 

## 🔐 Credentials

Use the following demo accounts to test the system:

### 🛠 Admin Account
- **Username:** `Admin`
- **Password:** `123456`

### 👤 Client Account
- **Username:** `Rafsun`
- **Password:** `123456`

### ✍️ Registering New Users
You can create additional users by navigating to the **Register** page in the frontend application.

### 🔄 Role Assignment
To assign roles to new users:

1. Launch the backend project and open **Swagger UI** (`/swagger`).
2. Use the available **Role Assignment API** endpoint to assign the desired role (e.g., `Admin`, `Client`).

> 🔔 **Note:** The application is split into two separate projects:
> - One for the **Backend API**
> - One for the **Frontend (MVC or Razor)**  
>
> Make sure to run **both projects simultaneously** for full functionality. (I mean multiple startup project)


### 1. Clone the Repository

```bash
git clone https://github.com/your-username/ai-chatbot.git


