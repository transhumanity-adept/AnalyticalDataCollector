start cmd.exe /K "echo START BACKEND SERVICES && cd backend && docker build -t analytical-data-collector-backend . && docker run -it -p 5020:80 --rm --name analytical-data-collector-b analytical-data-collector-backend"
start cmd.exe /K "echo START FRONTEND SERVICES && cd frontend && docker build -t analytical-data-collector-frontend . && docker run -it -p 8080:8080 --rm --name analytical-data-collector-f analytical-data-collector-frontend"
start http://localhost:8080/