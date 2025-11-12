# --- Stage 1: Build the optimized React application ---
FROM node:20-alpine AS build
WORKDIR /app

# Copy package.json and install dependencies
COPY package.json package-lock.json ./
RUN npm install

# Copy all source code
COPY . .

# Build the application. CRA output is saved to the 'build' folder.
RUN npm run build 

# --- Stage 2: Serve the application using Nginx ---
FROM nginx:alpine

#Copy the built files from the '/app/build' directory (CRA default)
COPY --from=build /app/build /usr/share/nginx/html

# Nginx listens on port 80 by default
EXPOSE 80

# The container will run Nginx when it starts
CMD ["nginx", "-g", "daemon off;"]