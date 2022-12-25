start cmd.exe /C "color 3f && echo START BACKEND SERVICES && cd backend/WebApi && dotnet run"
start cmd.exe /C "color 3f && echo START FRONTEND SERVICES && cd frontend && npm run dev"
start http://localhost:3000/