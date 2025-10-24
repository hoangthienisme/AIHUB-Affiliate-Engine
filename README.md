# AIHUB Affiliate Engine

AIHUB Affiliate Engine là dịch vụ Affiliate Tracking System cho phép:
- Quản lý đối tác (Affiliates)
- Theo dõi click
- Ghi nhận chuyển đổi (conversions)
- Tính và xuất commission
- Quản lý payout

Ứng dụng được xây dựng bằng:
- **.NET 8 Web API**
- **PostgreSQL (Render Hosting)**
- **Entity Framework Core**
- **Swagger cho API Docs**

---

##  Tech Stack
| Layer | Technology |
|------|------------|
| Backend | ASP.NET Core 8 Web API |
| Database | PostgreSQL |
| ORM | Entity Framework Core |
| Auth | (TBD nếu có OAuth/JWT) |
| Deploy | Render Cloud |

---

##  Features
 Affiliate tracking  
 Save click information (IP, UserAgent, Referer)  
 Create conversion from external partner  
 CRUD đối tác, chiến dịch  
 Swagger UI để test API

---

##  Project Structure



AIHUB_Affiliate_Engine/
│── Controllers/
│ ├── AffiliateController.cs
│ ├── ClickController.cs
│ ├── ConversionController.cs
│── Data/
│ ├── AffiliateDbContext.cs
│── Entities/
│
│── appsettings.json
│── Program.cs


---

##  Setup Local Dev

### 1️ Clone source
```sh
git clone https://github.com/yourrepo/AIHUB_Affiliate_Engine.git
cd AIHUB_Affiliate_Engine

2️ Update database connection

File appsettings.json:

"ConnectionStrings": {
  "AffiliateDb": "Host=localhost;Port=5432;Database=AffiliateDB;Username=postgres;Password=yourpassword"
}

3️ Run migration
dotnet ef database update

4️ Run API
dotnet run


Mở Swagger:

https://localhost:7157/swagger

 Deploy on Render
 Build Docker
docker build -t aihub-affiliate-engine .
docker run -p 8080:8080 aihub-affiliate-engine


App tự expose cổng theo Render env:

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://0.0.0.0:{port}");

 API Documentation
1️ Create Click

POST /api/click

Request
{
  "partner_id": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "affiliate_code": "AFF123",
  "ip_address": "192.168.1.10",
  "user_agent": "Mozilla/5.0",
  "referer_url": "https://google.com"
}

Response
{
  "click_id": "xxxx",
  "status": "success"
}

2️ Create Conversion

POST /api/conversion

{
  "click_id": "xxxx",
  "amount": 100.00
}


Response:

{
  "conversion_id": "xxxx",
  "commission": 10.00
}

3️ List Clicks

GET /api/click

 System Flow & Diagram
 Affiliate Tracking Flow
User Clicks Affiliate Link
        │
        ▼
Landing Page → Send Click API → Save to DB
        │
        ▼
User Purchases → Conversion API → Calculate Commission → Save Result
        │
        ▼
Partner Dashboard → View Report

 ERD (mô tả thực thể)
Affiliate (1) ──── (∞) Clicks ──── (∞) Conversions

 Future Improvements

JWT Authentication

Campaign rules & Postbacks

Fraud detection

Admin Dashboard UI
