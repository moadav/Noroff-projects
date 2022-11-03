FROM node:18
RUN npm install -g serve
WORKDIR /usr/src/app
COPY package*.json ./
RUN npm install
COPY . .
ENV NODE_ENV production
RUN npm run build
EXPOSE 3000
CMD ["serve", "-s", "build", "-l", "3000"]
