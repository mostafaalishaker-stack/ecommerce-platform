# E-Commerce Platform

A full-stack e-commerce platform built with Angular 18 and .NET 9 Web API.

## Tech Stack

| Layer | Technology |
|-------|-----------|
| Frontend | Angular 18, TypeScript, TailwindCSS |
| Backend | .NET 9 Web API, C# |
| Database | SQL Server (Entity Framework Core) |
| Auth | JWT (ASP.NET Core Identity) |
| Payments | Stripe integration |
| Deployment | Vercel (frontend) + Azure (backend) |

## Features

- Product catalog with search & filters
- Shopping cart with persistent state
- User authentication & roles (Customer/Admin)
- Order management & checkout
- Admin dashboard for product/order management
- Payment integration with Stripe
- Responsive design

## Setup

### Backend
```bash
cd backend
dotnet restore
dotnet run
```

### Frontend
```bash
cd frontend
npm install
ng serve
```
