# Build
FROM node:lts-alpine as build
WORKDIR /app
COPY ./frontend/package*.json .
RUN npm install
COPY ./frontend/ .
RUN npm run build

# Production
FROM nginx:stable-alpine as production
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]