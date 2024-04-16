FROM nginx:stable-alpine as base
RUN apk add --no-cache nodejs npm git
EXPOSE 80

FROM base as build
WORKDIR /build
COPY . /build/
RUN npm install
RUN npm run build

FROM build as release
COPY --from=build /build/dist /usr/share/nginx/html