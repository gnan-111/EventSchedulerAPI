# 📅 Event Scheduler Application

A full-stack application for managing events, meetings, and attendees. This project features a **robust .NET 8 Web API backend** with **MSSQL** and a **responsive React frontend** styled with custom Vanilla CSS.

---

## 🚀 Features

* **Create Events:** Schedule meetings with a title, description, location, and time.
* **Manage Attendees:** Add multiple attendees with name & email.
* **Dashboard View:** View all upcoming events in a responsive layout.
* **Delete Events:** Remove events and their associated data.
* **MSSQL Integration:** Persistent storage using **Entity Framework Core**.
* **Responsive UI:** Works seamlessly across devices.
* **Unit Testing:** Service layer tested using **XUnit** with **In-Memory DB**.

---

## 🛠️ Tech Stack

### 🔹 Backend
* **Framework:** .NET 8 Web API (C#)
* **Database:** MSSQL
* **ORM:** Entity Framework Core
* **Testing:** xUnit, FluentAssertions, Moq
* **Docs:** Swagger / OpenAPI

### 🔹 Frontend
* **Framework:** React (Vite)
* **Styling:** Vanilla CSS
* **HTTP:** Axios
* **Icons:** Lucide React
* **Date Utils:** Date-fns

---

## 📂 Project Structure

```plaintext
├── EventSchedulerAPI/            # .NET 8 Web API Backend
│   ├── Controllers/              # API Controllers
│   ├── Data/                     # DbContext & EF Migrations
│   ├── DTOs/                     # Data Transfer Objects
│   ├── Models/                   # Database Entities
│   ├── Services/                 # Business Logic Layer
│   │   ├── IEventService.cs
│   │   └── EventService.cs
│   ├── UnitTest/                 # xUnit Tests
│   │   └── EventServiceTests.cs
│   └── Program.cs                # App Startup
│
├── clientapp/                    # React Frontend
│   ├── src/
│   │   ├── Comp/                 # Components
│   │   ├── App.jsx
│   │   ├── App.css
│   │   └── axiosconfig.js
│   └── package.json
│
└── README.md                     # Documentation
````

---

## 📋 Prerequisites

Ensure you have installed:

✔ .NET 9 SDK
✔ Node.js 16+
✔ MSSQL
✔ VS Code / Visual Studio (Recommended)

---

## ⚙️ Getting Started

### 🧩 1️⃣ Backend Setup

```bash
cd EventSchedulerAPI
```

Update connection string inside `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=EventDb;User Id=sa;Password=your_password;Trusted_Connection=False;MultipleActiveResultSets=True;"
}

```

Apply migrations:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Run API:

```bash
dotnet run
```

🔗 Swagger UI: `/swagger/index.html`
🔗 Example URLs: `http://localhost:5000` or `https://localhost:7xxx`

---

### 🖥️ 2️⃣ Frontend Setup

```bash
cd clientapp
npm install
```

Update backend API URL in `src/axiosconfig.js`:

```javascript
const API_URL = 'http://localhost:5000/api/events';
```

Run app:

```bash
npm run dev
```

➡ Open the suggested link (e.g. `http://localhost:5173`)

---

## 📡 API Endpoints

| Method | Endpoint           | Description         |
| ------ | ------------------ | ------------------- |
| GET    | `/api/events`      | Retrieve all events |
| GET    | `/api/events/{id}` | Get event by ID     |
| POST   | `/api/events`      | Create event        |
| DELETE | `/api/events/{id}` | Delete event        |

**POST Example Body:**

```json
{
  "title": "Team Sync",
  "description": "Weekly standup discussion",
  "startTime": "2024-12-25T10:00:00",
  "endTime": "2024-12-25T11:00:00",
  "location": "Zoom",
  "attendees": [
    { "name": "John Doe", "email": "john@example.com" }
  ]
}
```

---

## 🧪 Unit Testing (xUnit)

Unit tests validate **CRUD operations** of the `EventService`.

### ✔ Uses In-Memory EF DB for isolation

```csharp
private AppDbContext GetInMemoryContext()
{
    var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseInMemoryDatabase(Guid.NewGuid().ToString())
        .Options;

    return new AppDbContext(options);
}
```

### 🏁 Run Tests

```bash
dotnet test
```

---

## 📌 Future Enhancements

* Update Events
* Authentication (JWT Auth)
* Search & Filters
* Calendar UI View
* Email Notifications

---

## ❤️ Contributions Welcome!

Feel free to fork, improve, and submit a PR 🚀

---

✨ Thanks for checking out this project!

