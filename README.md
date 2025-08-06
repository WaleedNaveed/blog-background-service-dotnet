# BackgroundService for Long-Running Tasks in ASP.NET Core

This project demonstrates how to handle **long-running tasks** in ASP.NET Core without causing API timeouts, using `BackgroundService`.

We simulate a real-world Excel import scenario where data is processed in the background, while the API returns immediately and exposes a separate status endpoint to check job progress.

> 🔧 Ideal for use cases like file imports, report generation, bulk email processing, etc.

---

## 🚀 Features

- ✅ Triggers background processing from an API
- ✅ Uses `BackgroundService` for long-running work
- ✅ Tracks progress with status updates (`InProgress`, `Completed`)
- ✅ Injects `BackgroundService` into controllers via interface workaround
- ✅ Example delay simulates real-world row processing
- ✅ Uses SQLite as a lightweight database
- ✅ Clean, production-like folder structure (Models, Services, Controllers, etc.)

---

## 🧪 How It Works

1. **Trigger Import**  
   Send a request to `POST /api/UserImport`  
   → This triggers a background job without blocking the API.

2. **Check Status**  
   Send a request to `GET /api/UserImport/status`  
   → You’ll see job status: `NotStarted`, `InProgress`, or `Completed`.

3. **Simulated Delay**  
   Each row processing includes a `Task.Delay(200)` to simulate heavy operations like validations or API calls.

4. **View Results**  
   Once completed, data is inserted into SQLite. You can inspect it using any SQLite viewer.

---

## 🛠️ Technologies Used

- ASP.NET Core 8
- BackgroundService
- SQLite (via `Microsoft.Data.Sqlite`)
- Dependency Injection
- API for trigger + status

---

## 📝 Blog
Want a full walkthrough with code and explanation?  
👉 [Read the blog post here](https://wntech.hashnode.dev/handling-long-running-tasks-in-aspnet-core-using-backgroundservice)

